using Serilog;
using Serilog.Sinks.Seq;
using Serilog.Debugging;
using Serilog.Settings.Configuration;
using Serilog.Formatting.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using BibrusServer.Models;

string relative = @"..\..\bibrus\BibrusServer\";
string absolute = Path.GetFullPath(relative);

AppDomain.CurrentDomain.SetData("DataDirectory", absolute);


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("policy", builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

builder.Services.AddControllers();

//scoped schema

//GraphQL

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BibrusDbContext>(options =>
options.UseSqlServer(
        "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|BibrusDb.mdf;Integrated Security=True"
    ));

builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
loggerConfiguration
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    );

SelfLog.Enable(Console.Error);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(exceptionHandlerApp
        => exceptionHandlerApp.Run(async context
            => await Results.Problem()
                .ExecuteAsync(context)));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("policy");

app.UseEndpoints(endpoints=>
endpoints.MapControllers());

//app.UseGraphQL<ApplicationsSchema>();

app.Run();
