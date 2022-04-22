using System;
using System.Collections.Generic;

namespace MongoDBClient.Database {
  public class NoteProject {
    public string Body { get; set; }
    public DateTime UpdatedOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public Project project { get; set; }
    public List<Dummy> dummies { get; set; }
  }
}
