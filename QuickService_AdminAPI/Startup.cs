using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using QuickServiceAdmin.Core.Converters;
using QuickServiceAdmin.Core.Entities;
using QuickServiceAdmin.Core.Filter;
using QuickServiceAdmin.Core.Helpers;
using QuickServiceAdmin.Core.Interface;
using QuickServiceAdmin.Core.Model;
using QuickServiceAdmin.Core.Services;
using QuickServiceAdmin.Infrastructure.Repository;

namespace QuickService_AdminAPI
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AppSettings.ConnectToLocalRedis = Convert.ToBoolean(Configuration["AppSettings:ConnectToLocalRedis"]);
            AppSettings.SetRedisApi = Configuration["AppSettings:SetRedisApi"];
            AppSettings.GetRedisApi = Configuration["AppSettings:GetRedisApi"];
            AppSettings.ValidateToken = Configuration["AppSettings:ValidateToken"];

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new DateTimeConverter());
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddControllers();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuickServiceAPI", Version = "v1" });
            });
            services.AddDbContext<QuickServiceContext>(item =>
                item.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("QuickServiceDBConnection"),
                    b => b.MigrationsAssembly("QuickService_AdminAPI")));
            services.AddScoped<ICustomerRequestRepository, CustomerRequestRepository>();

            services.AddScoped<IJsonRequestHelper, JsonRequestHelper>();
            // services.AddScoped<IJsonRequestHelper, JsonRequestHelper>();

            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IRedboxService, RedboxService>();
            services.AddScoped<AuthorizationFilter>();
            //services.AddSingleton<PartialViewResultExecutor>();

            var ignoreCertHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            services.AddHttpClient<ICardRequestService, CardRequestService>("CardRequestServiceClient", c =>
            {
                c.BaseAddress = new Uri(Configuration["AppSettings:CardRequestServiceConfig:Endpoint"]);
            });

            services.AddHttpClient<IAddressRequestService, AddressRequestService>(c =>
            {
                c.BaseAddress = new Uri(Configuration["AppSettings:AddressRequestServiceConfig:Host"]);
            }).ConfigurePrimaryHttpMessageHandler(() => ignoreCertHandler);

            // services.AddHttpClient<IFacialIdentityRequestService, FacialIdentityRequestService>(c =>
            // {
            //     c.BaseAddress = new Uri(Configuration["AppSettings:FacialIdentityRequestServiceConfig:Host"]);
            //     c.DefaultRequestHeaders.Add("token", Configuration["AppSettings:FacialIdentityRequestServiceConfig:Authorization"]);
            // });


            services.AddHttpClient("AuthClient", client => { })
                .ConfigurePrimaryHttpMessageHandler(() => ignoreCertHandler);

            services.AddMemoryCache();

            services.AddStackExchangeRedisCache(options =>
                options.Configuration = Configuration.GetConnectionString("RedisConnection"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            // else
            // {
            app.UseExceptionHandler("/error");
            // }

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuickServiceAPI V1"); });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}