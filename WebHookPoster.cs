using System.Net.Http.Headers;

namespace PlannerPoc
{
    public interface IWebhookPoster
    {
        Task PostMessageAsync(string message);
    }
    public class WebhookPoster : IWebhookPoster
    {
        HttpClient client;
        private readonly string webhookUrl;

        private const string CardBody = "{" +
        "\"type\":\"message\",  " +
    "\"attachments\":[ " +
        "{" +
            "\"contentType\":\"application/vnd.microsoft.card.adaptive\"," +
            "\"contentUrl\":null," +
            "\"content\":{"+
                "\"$schema\":\"http://adaptivecards.io/schemas/adaptive-card.json\","+
                "\"type\":\"AdaptiveCard\","+
                "\"version\":\"1.2\","+
                "\"body\":["+
                    "{"+
                    "\"type\": \"TextBlock\","+
                    "\"text\": \"REPLACEME\""+
                    "}"+
                "]"+
            "}"+
        "}"+
    "]"+
    "}";

        public WebhookPoster(HttpClient client, string webhookUrl)
        {
            this.client = client;
            this.webhookUrl = webhookUrl;
        }

        public async Task PostMessageAsync(string text)
        {
            try
                {
                    string body = CardBody.Replace("REPLACEME", text);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var content = new StringContent(body, System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(webhookUrl, content);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }
    }
}