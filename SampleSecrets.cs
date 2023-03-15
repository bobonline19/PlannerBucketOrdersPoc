namespace PlannerPoc
{
    public class SampleSecrets :ISecrets
    {
        public string ClientID => "<your client ID>";
        public string IncomingWebHookUrl => "<your webhook URL>";
    }
}