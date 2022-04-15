using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL;
using MongoDBClient.Database;

namespace MongoDBClient.GraphQL {

  //
  public class Query {

    //
    [GraphQLMetadata("notes")]
    public IEnumerable<NoteProject> GetNotes() {
      ProjectResolver pr = new ProjectResolver();
      List<Note> notas = new List<Note>();
      List<NoteProject> notes = new List<NoteProject>();
      using (DBHpr db = new DBHpr()) {
        notas = db.ObtemNotas();
        foreach (Note note in notas) {
          Project project = pr.GetProject(note, db);
          notes.Add(new NoteProject {
            Body = note.Body,
            UpdatedOn = note.UpdatedOn,
            CreatedOn = note.CreatedOn,
            project = project
          });
        }
      }

      return notes;
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
  }

  public class NoteProject {
    public string Body { get; set; }
    public DateTime UpdatedOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public Project project { get; set; }
  }
}
