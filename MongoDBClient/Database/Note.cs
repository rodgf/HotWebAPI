using System;

namespace MongoDBClient.Database {

  //
  public class Note {
    public object _id { get; set; }
    public string Body { get; set; }
    public DateTime UpdatedOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public int UserId { get; set; }
  }
}
