using System.Collections.Generic;

namespace MongoDBClient.Database {

  //
  public class DBDummy {

    //
    public static List<Dummy> ObtemDummies() {
      List<Dummy> dummies = new List<Dummy>();
      dummies.Add(new Dummy() {
        ID = 1,
        Name = "Robério Souza"
      });
      dummies.Add(new Dummy() {
        ID = 2,
        Name = "Marta Rocha"
      });

      return dummies;
    }
  }

  //
  public class Dummy {
    public int ID { get; set; }
    public string Name { get; set; }
  }
}
