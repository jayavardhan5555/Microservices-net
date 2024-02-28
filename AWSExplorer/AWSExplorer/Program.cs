using Amazon.S3;
using AWSExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddHostedService<WeatherForecastProcessor>();
//builder.Services.AddOptions<DatabaseSettings>().BindConfiguration(nameof(DatabaseSettings));
builder.Configuration.AddSecretsManager(configurator: config =>
{
    config.SecretFilter = record => record.Name.StartsWith($"{builder.Environment.EnvironmentName}/Demo/");
    config.KeyGenerator = (secret, name) => name.Replace($"{builder.Environment.EnvironmentName}/Demo/", string.Empty)
    .Replace("__", ":");
    config.PollingInterval = TimeSpan.FromSeconds(1);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
