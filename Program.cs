using Azure.Identity;
using Microsoft.Graph;

namespace PlannerPoc
{

  public static class Config
  {
      public const string GroupName = "Orders";
      public const string PlanName = "ShipOrders";
  }

  public static class Program
  {
    public static async Task Main(string[] args)
    {

      ISecrets secrets = new Secrets();
      IWebhookPoster poster = new WebhookPoster(new HttpClient(), secrets.IncomingWebHookUrl);

      var scopes = new[] { "User.Read", "Group.Read.All", "Tasks.Read" };
      var interactiveBrowserCredentialOptions = new InteractiveBrowserCredentialOptions
      {
        ClientId = secrets.ClientID
      };
  
      var tokenCredential = new InteractiveBrowserCredential(interactiveBrowserCredentialOptions);

      var graphClient = new GraphServiceClient(tokenCredential, scopes);

      var me = await graphClient.Me.GetAsync();

      var groups = await graphClient.Groups.GetAsync(r => r.QueryParameters.Filter=$"displayName eq '{Config.GroupName}'");
      var group = groups.Value.AsEnumerable().Single();
      //var group = groups.Where(g => g.Name == groupName).Select(g => g.Id).FirstOrDefault();

      var plans = await graphClient.Groups[group.Id].Planner.Plans.GetAsync();// .GetAsync (r => r.QueryParameters.Filter=$"title eq '{planName}'");
      var plan = plans.Value.Where(p => p.Title == Config.PlanName).SingleOrDefault();
      var buckets = await graphClient.Groups[group.Id].Planner.Plans[plan.Id].Buckets.GetAsync ();
      var bucketNames  = buckets.Value.ToDictionary(k => k.Id, v => v.Name);
      bucketNames.Add(string.Empty, string.Empty);

      var tasks = await graphClient.Planner.Plans[plan.Id].Tasks.GetAsync ();

      List<TaskDto> taskData = tasks.Value.Select(t => new TaskDto(t.Id, t.BucketId, t.Title)).ToList();
      ////var result = await graphClient.Planner.Plans["{plannerPlan-id}"].Tasks.GetAsync();

      var contents = System.Text.Json.JsonSerializer.Serialize(taskData);
      //var groupDrive = await graphClient.Groups[group.Id].Drive.GetAsync(r => r.QueryParameters.Expand=new string[]{"children($select=id,name)"});
      //var groupDrive = await graphClient.Groups[group.Id].Drive.GetAsync();
      //groupDrive.Root.[]
      //graphClient.Drives[groupDrive.Id].Items[3].it
      //var root = groupDrive.Root;
      FileInfo newest = new DirectoryInfo(".").GetFiles("contents*.json").OrderByDescending(f => f.CreationTime).FirstOrDefault();
      if (newest.Exists)
      {
        string oldContents = File.ReadAllText(newest.FullName);
        var oldData = System.Text.Json.JsonSerializer.Deserialize<List<TaskDto>>(oldContents) ?? new List<TaskDto>();

        foreach(var task in taskData)
        {
          var oldBucket = oldData.Where(x =>x.id == task.id).Select(b => b.bucket).FirstOrDefault () ?? string.Empty;
          if (task.bucket != oldBucket)
          {
            string change = $"CHANGE For '{task.title}' moved from '{bucketNames[oldBucket]}' to '{bucketNames[task.bucket]}'";
            Console.WriteLine(change);
            await poster.PostMessageAsync(change);
          }
        }
      }

      File.WriteAllText($"contents{(Guid.NewGuid())}.json", contents);

      Console.WriteLine($"Hello {me?.DisplayName}!");
    }
  }
}