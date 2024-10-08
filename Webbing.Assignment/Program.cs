const string ORIGIN_POLICY = "MW2.0_CLIENT_ORIGIN_POLICY";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Install the service
builder.Services.AddAppService();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: ORIGIN_POLICY,
        builder =>
        {
            builder
                .WithOrigins(new string[] { "http://localhost:4200", "http:/localhost:49983" })
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(ORIGIN_POLICY);
app.MapControllers();

app.Run();
