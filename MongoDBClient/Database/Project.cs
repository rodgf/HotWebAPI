using System.Collections.Generic;

namespace MongoDBClient.Database {
  public class Project {
    public object _id { get; set; }
    public string tipo { get; set; }
    public string enturmada { get; set; }
    public int matricula { get; set; }
    public string nome { get; set; }
    public string turma { get; set; }
    public List<Turma> refturma { get; set; }
    public List<Derivado> derivacao { get; set; }
  }
}
