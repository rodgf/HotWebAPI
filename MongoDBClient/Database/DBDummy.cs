using System.Collections.Generic;

namespace MongoDBClient.Database {

  // Dados "hardcoded" para combinação com consulta de BD
  public class DBDummy {

    //
    public static List<Dummy> ObtemDummies() {
      List<Dummy> dummies = new List<Dummy> {
        new Dummy() {
          ID = 1,
          Name = "Robério Souza"
        },
        new Dummy() {
          ID = 2,
          Name = "Marta Rocha"
        }};

      return dummies;
    }
  }

  //
  public class Dummy {
    public int ID { get; set; }
    public string Name { get; set; }
  }
}
