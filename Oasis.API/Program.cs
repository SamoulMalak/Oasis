using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Oasis.BL.IServices;
using Oasis.BL.Services;
using Oasis.Data;
using Oasis.Data.Entities;
using Oasis.Data.IPersistance;
using Oasis.Data.IRepositories;
using Oasis.Data.Persistance;
using Oasis.Data.Repositories;
using System.Text;

namespace Oasis.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //add DB context
            builder.Services.AddDbContext<DataContext>
              (
              option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
              );

            builder.Services.AddIdentity<UserTable, IdentityRole>()
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

            // Register Services 
            builder.Services.AddScoped<IHttpClientCall, HttpClientCall>();

            //register Account Services 
            builder.Services.AddScoped<IAccountServices, AccountServices>();

            builder.Services.AddScoped<IDbFactory, DbFactory>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IToDoRepository, ToDoRepository>();

            //register  ToDoServices 
            builder.Services.AddScoped<IToDoServices, ToDoServices>();
            
            // register fluent validation services in all assembly 
            builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            //register autoMapper in all assembly 
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // add authentication 
            builder.Services.AddAuthentication(options=>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            }
            );

            // Enabling CORS.
            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); ;
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
                        c.RoutePrefix = "swagger";
                    });


            }

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}