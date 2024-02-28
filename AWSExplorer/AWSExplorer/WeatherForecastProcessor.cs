
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace AWSExplorer
{
    public class WeatherForecastProcessor : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Backgroun process started");

            var cred = new BasicAWSCredentials("", "");
            var client = new AmazonSQSClient(cred, Amazon.RegionEndpoint.APSoutheast1);

            while(!stoppingToken.IsCancellationRequested)
            {
                var request = new ReceiveMessageRequest()
                {
                    QueueUrl = ""
                };
                var response = await client.ReceiveMessageAsync(request);
                foreach (var message in response.Messages)
                {
                    Console.WriteLine(message.Body);
                }
            }
        }
    }
}
