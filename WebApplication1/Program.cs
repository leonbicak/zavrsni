using izlazniracuni.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;



        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(sgo =>
        { 
            var o = new Microsoft.OpenApi.Models.OpenApiInfo()
            {
                Title = "izlazni racuni",
                Version = "v1",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                {
                    Email = "lbicak96@gmail.com",
                    Name = "Leon Bičak"
                },
                Description = "Ovo je dokumentacija za izlazniracuni",
                License = new Microsoft.OpenApi.Models.OpenApiLicense()
                {
                    Name = "Edukacijska licenca"
                }
            };
            sgo.SwaggerDoc("v1", o);
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            sgo.IncludeXmlComments(xmlPath);


        });


builder.Services.AddCors(opcije =>
{
    opcije.AddPolicy("CorsPolicy",
        builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});



builder.Services.AddDbContext<izlazniracuniContext>(o =>
            o.UseSqlServer(builder.Configuration.GetConnectionString(name: "izlazniracuniContext")));




        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(opcije =>
            {
                opcije.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(opcije =>
            {
                opcije.ConfigObject.
                AdditionalItems.Add("requestSnippetsEnabled", true);
            });
        }

        app.UseHttpsRedirection();



        app.MapControllers();
        app.UseStaticFiles();

        app.UseCors("CorsPolicy");

        app.Run();
   
