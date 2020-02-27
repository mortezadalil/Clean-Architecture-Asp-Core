using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cms.Api.Presenter;
using Cms.Core.Interfaces.Repository;
using Cms.Core.Queries;
using Cms.Core.UseCase;
using Cms.Infrastructure.Data;
using Cms.Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Cms.Api
{
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string Realm { get; set; }
    }
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CmsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CmsDatabase")));

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(GetLatestPostsQuery).GetTypeInfo().Assembly);

            services.AddScoped<IEditPostUseCase, EditPostUseCase>();
            services.AddScoped(typeof(PostApiPresenter<>));
            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration.GetSection("Appsettings:SecretKey").Value)),
                        ValidateIssuer = false,
                        ValidIssuer = Configuration.GetSection("Appsettings:ValidIssuer").Value,
                        ValidateAudience = false,
                        ValidAudience = Configuration.GetSection("Appsettings:ValidAudience").Value,
                        ValidateLifetime = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
              {
                  endpoints.MapControllers();
              });
        }
    }
}
