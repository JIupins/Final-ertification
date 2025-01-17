using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography;
using System.Text;
using MessagingService.Repository;
using MessagingService.Models.DTO;
using MessagingService.Models.Context;

namespace MessagingService
{
    public class Program
    {
        private static RSA GetPublicKey()
        {
            var f = File.ReadAllText("rsa/public_key.pem");
            var rsa = RSA.Create();
            rsa.ImportFromPem(f);
            return rsa;
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "Token",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
            });

            // �������: �������� ������ AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // �������: ��������� ������� �������� Autofac
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            // �������: ��������� ������ ����������������� �����.
            var config = new ConfigurationBuilder();
            config.AddJsonFile("appsettings.json");
            var configRoot = config.Build();

            // �������: ��������������� ���������, ��������������� �������� ���� ������.
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder
                    .Register(c => new MessageDbContext(configRoot.GetConnectionString("db")))
                    .InstancePerDependency();
            });

            // �������: ��������������� ������, ��������������� �������� �����������.
            builder.Services.AddSingleton<IMessageRepository, MessageRepository>();

            // �������: ��������������� ������ ��������������.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        //    .GetBytes(builder.Configuration["Jwt:Key"]))
                        IssuerSigningKey = new RsaSecurityKey(GetPublicKey())
                    };
                });

            //builder.Services.AddScoped<IUserAuthenticationService,UserAuthenticationServiceMock>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            app.Run();
        }
    }
}
