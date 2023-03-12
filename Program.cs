using Azure.Identity;
using Microsoft.Graph;

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

var groups = await graphClient.Groups.GetAsync();
//var group = groups.Where(g => g.Name == groupName).Select(g => g.Id).FirstOrDefault();
var plans = await graphClient.Planner.Plans.GetAsync();
//var result = await graphClient.Planner.Plans["{plannerPlan-id}"].Tasks.GetAsync();


Console.WriteLine($"Hello {me?.DisplayName}!");