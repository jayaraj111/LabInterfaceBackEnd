var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient(); //Add on HttpClient

builder.Services.AddCors((corsoptions)=>  // Add cors policy
{
    corsoptions.AddPolicy("MyPolicy",(policyoptions)=>
    {
       // policyoptions.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
       policyoptions.AllowAnyHeader().WithOrigins("http://localhost:4200").AllowAnyMethod();
        
    });

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

app.UseCors("MyPolicy");  // Add cors policy


app.MapControllers();

app.Run();
