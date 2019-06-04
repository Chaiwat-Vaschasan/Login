using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Login.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Login.Extensions
{
    public static class ServiveExtensions
    {
        public static void ConfigDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Connection Database SqlServar
            services.AddDbContext<Database.DatabaseContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection_sql_server")));

            // Check Database Ensure Created
            var serviceProvider = services.BuildServiceProvider();
            DatabaseInit.INIT(serviceProvider);
        }

        public static void ConfigCROS(this IServiceCollection services)
        {
            services.AddCors(cors =>
                cors.AddPolicy("AllowAll", builder =>
                     builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials()));
        }

        public static void ConfigJWT(this IServiceCollection services, IConfiguration configuration)
        {
            //key
            string securityKey = configuration.GetConnectionString("securityKey");
            //symmetric key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //what to validate
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        //setup validate data
                        ValidIssuer = configuration.GetConnectionString("Issuer"),
                        ValidAudience = configuration.GetConnectionString("Audience"),
                        IssuerSigningKey = symmetricSecurityKey
                    };
                });


        }

    }
}