using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace MongoDBClient.Database {

  //
  public class DBHpr {
    private readonly MongoClient _client;
    private readonly MongoClient _clientAlt;
    private readonly IMongoDatabase _db;
    private readonly IMongoDatabase _dbAlt;

    //
    public DBHpr() {
      _client = new MongoClient(CSHelper.ConfigurationManager.AppSetting["ConnectionStrings:MongoLocal"]);
      _clientAlt = new MongoClient(CSHelper.ConfigurationManager.AppSetting["ConnectionStrings:MongoAlt"]);
      _db = _client.GetDatabase("test");
      _dbAlt = _clientAlt.GetDatabase("NotesDb");
    }

    //
    public List<Note> ObtemNotas(int userId = 0) {
      IMongoCollection<Note> cNote = _dbAlt.GetCollection<Note>("Note");
      List<Note> notas = cNote.Find(_ => userId == 0 || _.UserId == userId).ToList();
      return notas;
    }

    //
    public Aluno ObtemAluno(int userId = 0) {
      IMongoCollection<Aluno> cAluno = _db.GetCollection<Aluno>("aluno");
      Aluno aluno = cAluno.Find(_ => userId == 0 || _.matricula == userId).FirstOrDefault();
      return aluno;
    }

    //
    public List<Aluno> ObtemAlunos(string turma = null) {
      IMongoCollection<Aluno> cAluno = _db.GetCollection<Aluno>("aluno");
      List<Aluno> alunos = cAluno.Find(_ => turma == null || _.turma == turma).ToList();
      return alunos;
    }

    //
    public List<object> ObtemAlunosExt(string[] query) {
      IMongoCollection<Aluno> cAluno = _db.GetCollection<Aluno>("aluno");

      BsonDocument[] pipeline = new BsonDocument[query.Length];
      for (int i = 0; i < query.Length; i++) {
        pipeline[i] = BsonSerializer.Deserialize<BsonDocument>(query[i]);
      }

      IAsyncCursor<object> temp = cAluno.Aggregate<object>(pipeline);
      List<object> result = temp.ToList();
      return result;
    }
  }
}
