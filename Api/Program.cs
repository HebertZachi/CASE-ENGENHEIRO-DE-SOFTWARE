using Adapters;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region Application & Infrastructure
builder.Services.AddApplication();
builder.Services.AddAdapters();
builder.Services.AddInfrastructure(builder.Configuration);
#endregion

#region Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

var app = builder.Build();

#region HTTP Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
#endregion

app.Run();
