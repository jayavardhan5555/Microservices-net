using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace AWSExplorer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DatabaseSettings options;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IOptionsSnapshot<DatabaseSettings> options)
        {
            _logger = logger;
            this.options = options.Value;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var test = options.ConnectionString;
            var client = new AmazonSecretsManagerClient();
            var response = await client.GetSecretValueAsync(new Amazon.SecretsManager.Model.GetSecretValueRequest()
            {
                 SecretId = "test"
            });
            var count = int.Parse(response.SecretString.Split(',')[0]);
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //SQS
        //[HttpPost]
        //public async Task Post(WeatherForecast data)
        //{
        //    var cred = new BasicAWSCredentials("", "");
        //    var client = new AmazonSQSClient(cred, Amazon.RegionEndpoint.APSoutheast1);

        //    var request = new SendMessageRequest()
        //    {
        //        QueueUrl = "",
        //        MessageBody = JsonSerializer.Serialize(data),
        //         DelaySeconds = 5,
                  
        //    };
        //    await client.SendMessageAsync(request);
        //}

        //SNS
        //[HttpPost]
        //public async Task Post(WeatherForecast data)
        //{
        //    var cred = new BasicAWSCredentials("", "");
        //    var client = new AmazonSimpleNotificationServiceClient(cred, Amazon.RegionEndpoint.APSoutheast1);

        //    var request = new PublishRequest()
        //    {
        //        TopicArn = "",
        //        Message = JsonSerializer.Serialize(data),
        //        Subject = "NewWeatherDara"

        //    };
        //    await client.PublishAsync(request);
        //}
    }
}
