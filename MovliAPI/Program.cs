using System.Reflection;
using MovliAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(System.Net.IPAddress.Parse("172.16.10.21"), 7115);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<FuncionesService>();
builder.Services.AddSingleton<BotonesService>();
// Configurar Swagger para documentación de API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Stream Deck API",
        Version = "v1",
        Description = "API para gestionar botones y asistente de voz."
    });

    // Agrupar por categorías
    options.DocumentFilter<SwaggerCategoryFilter>();

    // Incluir comentarios XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Stream Deck API v1");
        options.RoutePrefix = "";
    });
}

//app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    // Log de la petición
    Console.WriteLine($"[{DateTime.Now}] Request: {context.Request.Method} {context.Request.Path}");

    // Para loguear el body de la petición (opcional, solo para métodos con body)
    if (context.Request.ContentLength > 0 && context.Request.Body.CanSeek)
    {
        context.Request.Body.Position = 0;
        using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;
        Console.WriteLine($"Request Body: {body}");
    }

    // Captura la respuesta
    var originalBodyStream = context.Response.Body;
    using var responseBody = new MemoryStream();
    context.Response.Body = responseBody;

    await next();

    context.Response.Body.Seek(0, SeekOrigin.Begin);
    var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
    context.Response.Body.Seek(0, SeekOrigin.Begin);

    Console.WriteLine($"[{DateTime.Now}] Response: {context.Response.StatusCode}");
    Console.WriteLine($"Response Body: {responseText}");

    await responseBody.CopyToAsync(originalBodyStream);
});

app.UseAuthorization();

app.MapControllers();

app.Run();

// Filtro para categorizar los endpoints
public class SwaggerCategoryFilter : Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter
{
    public void Apply(Microsoft.OpenApi.Models.OpenApiDocument swaggerDoc, Swashbuckle.AspNetCore.SwaggerGen.DocumentFilterContext context)
    {
        foreach (var path in swaggerDoc.Paths)
        {
            foreach (var operation in path.Value.Operations)
            {
                if (operation.Value.Tags.Count == 0)
                {
                    var tag = path.Key.Contains("/botones") ? "Botones" : "Asistente";
                    operation.Value.Tags.Add(new Microsoft.OpenApi.Models.OpenApiTag { Name = tag });
                }
            }
        }
    }
}
