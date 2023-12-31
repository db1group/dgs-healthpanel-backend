using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Configs.Middlewares;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddMapster();
builder.Services.AddDomainServices();
builder.Services.AddCompressionToResponse();
builder.Services.AddAzureAdAuth(builder.Configuration);
builder.Services.AddIntegrations();

SentryConfig.AddSentry(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(builder =>
{
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowAnyOrigin();
});

app.UseMiddleware<SentryExceptionMiddleware>();

app.UseResponseCompression();
app.MapControllers();
app.UsePathBase("/api");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.Run();