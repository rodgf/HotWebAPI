using System.Collections.Generic;
using System.Linq;
using GraphQL;
using MongoDBClient.Database;

namespace MongoDBClient.GraphQL {

  // Clientes para ProjectSchema
  public class Query {

    // Lista combinação entre Note e Project
    [GraphQLMetadata("notes")]
    public IEnumerable<NoteProject> GetNotes() {
      List<Note> notas = new List<Note>();
      List<NoteProject> projetos = new List<NoteProject>();
      using (DBHpr db = new DBHpr()) {
        notas = db.ObtemNotas();
        foreach (Note note in notas) {
          Project project = db.ObtemProjeto(note);
          projetos.Add(new NoteProject {
            Body = note.Body,
            UpdatedOn = note.UpdatedOn,
            CreatedOn = note.CreatedOn,
            project = project,
            dummies = DBDummy.ObtemDummies()
          }); ;
        }
      }

      return projetos;
    }

    //
    [GraphQLMetadata("project")]
    public Project GetProject(int userId) {
      using (DBHpr db = new DBHpr()) {
        return db.ObtemProjetos()
            .Where(_ => _.matricula == userId)
            .FirstOrDefault();
      }
    }

    //
    [GraphQLMetadata("dummies")]
    public List<Dummy> GetDummies() {
      return DBDummy.ObtemDummies();
    }
  }
}
