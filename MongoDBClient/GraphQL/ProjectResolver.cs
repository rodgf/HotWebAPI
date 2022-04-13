using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using MongoDBClient.Database;

namespace MongoDBClient.GraphQL {

  [ExtendObjectType(Name = "project")]
  public class ProjectResolver {
    public Project GetProject([Parent] Note nota, [Service] DBHpr hpr) =>
        hpr.ObtemProjetos()
            .Where(_ => _.matricula == nota.UserId)
            .FirstOrDefault();
  }
}
