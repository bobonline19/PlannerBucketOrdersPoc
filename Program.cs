using Azure.Identity;
using Microsoft.Graph;

namespace PlannerPoc
{

  public static class Program
  {

    public static async Task Main(StringSplitOptions[] args)
    {
      Console.WriteLine("Hello, World!");
      var scopes = new[] { "User.Read", "Group.Read.All", "Tasks.Read" };
      var interactiveBrowserCredentialOptions = new InteractiveBrowserCredentialOptions
      {
        ClientId = Secrets.ClientID
      };

      string groupName = "Orders";
      string planName = "ShipOrders";

      var tokenCredential = new InteractiveBrowserCredential(interactiveBrowserCredentialOptions);

      var graphClient = new GraphServiceClient(tokenCredential, scopes);

      var me = await graphClient.Me.GetAsync();

      var groups = await graphClient.Groups.GetAsync(r => r.QueryParameters.Filter=$"displayName eq '{groupName}'");
      var group = groups.Value.AsEnumerable().Single();
      //var group = groups.Where(g => g.Name == groupName).Select(g => g.Id).FirstOrDefault();

      var plans = await graphClient.Groups[group.Id].Planner.Plans.GetAsync();// .GetAsync (r => r.QueryParameters.Filter=$"title eq '{planName}'");
      var plan = plans.Value.Where(p => p.Title == planName).SingleOrDefault();

      var tasks = await graphClient.Planner.Plans[plan.Id].Tasks.GetAsync ();

      List<TaskDto> taskData = tasks.Value.Select(t => new TaskDto(t.Id, t.BucketId, t.Title)).ToList();
      ////var result = await graphClient.Planner.Plans["{plannerPlan-id}"].Tasks.GetAsync();

      var contents = System.Text.Json.JsonSerializer.Serialize(taskData);
      var groupDrive = await graphClient.Groups[group.Id].Drive.GetAsync(r => r.QueryParameters.Expand=new string[]{"children($select=id,name)"});
      var root = groupDrive.Root;

      Console.WriteLine($"Hello {me?.DisplayName}!");
    }
  }
}