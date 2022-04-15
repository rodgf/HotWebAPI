using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace MongoDBClient.Database {

  //
  public class DBHpr : IDisposable {
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
    public void NovaNota(Note nota) {
      IMongoCollection<Note> cNote = _dbAlt.GetCollection<Note>("Note");
      cNote.InsertOne(nota);
    }

    //
    public Note ObtemNota(int userId) {
      IMongoCollection<Note> cNote = _dbAlt.GetCollection<Note>("Note");
      Note nota = cNote.Find(_ => _.UserId == userId).FirstOrDefault();
      return nota;
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
    public List<Project> ObtemAlunosExt(string[] query) {
      IMongoCollection<Aluno> cAluno = _db.GetCollection<Aluno>("aluno");

      BsonDocument[] pipeline = new BsonDocument[query.Length];
      for (int i = 0; i < query.Length; i++) {
        pipeline[i] = BsonSerializer.Deserialize<BsonDocument>(query[i]);
      }

      IAsyncCursor<Project> temp = cAluno.Aggregate<Project>(pipeline);
      List<Project> result = temp.ToList();
      return result;
    }

    //
    public List<Project> ObtemProjetos() {
      string[] query = {
                @"{
	""$project"": {
		""tipo"": {
			""$switch"": {
				""branches"": [{
						""case"": { ""$ne"": [ ""$nome"", ""Fonseca"" ] }, 
						""then"": ""Normal"" 
					}],
				""default"": ""$nome""
			}
		},
		""enturmada"": {
			""$switch"": {
				""branches"": [{
						""case"": { ""$ne"": [ ""$turma"", ""Enturmada"" ] }, 
						""then"": ""Desenturmado"" 
					}],
				""default"": ""$turma""
			}
		},
		""matricula"": ""$matricula"",
		""nome"": ""$nome"",
		""turma"": ""$turma""
	}
}", @"{
	$lookup: {
		from: ""turma"",
		localField: ""turma"",
		foreignField: ""nome"",
		as: ""refturma""
	}
}", @"{
		$lookup: {
			from: ""derivado"",
			localField: ""matricula"",
			foreignField: ""matricula"",
			as: ""derivacao""
		}
}" };

      return ObtemAlunosExt(query);
    }

    void IDisposable.Dispose() {
      
    }
  }
}
