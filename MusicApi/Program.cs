var builder = WebApplication.CreateBuilder(args);

// Enable CORS for Angular

/* builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Angular app URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});*/

builder.Services.AddCors(options =>
{   
    options.AddPolicy("AllowAll", policy =>
    policy.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod());
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ... existing code ...

app.UseSwagger();   // This line requires the Swashbuckle.AspNetCore NuGet package
app.UseSwaggerUI(); // This line requires the Swashbuckle.AspNetCore NuGet package

app.UseCors("AllowAngularApp");

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
