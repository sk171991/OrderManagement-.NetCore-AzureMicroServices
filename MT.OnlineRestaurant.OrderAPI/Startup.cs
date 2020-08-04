using AutoMapper;
using LoggingManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MT.OnlineRestaurant.BusinessLayer;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.DataLayer;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.DataLayer.interfaces;
using MT.OnlineRestaurant.Logging;
using MT.OnlineRestaurant.ValidateUserHandler;
using Swashbuckle.AspNetCore.Swagger;

namespace MT.OnlineRestaurant.OrderAPI
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup constructor
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            _applicationPath = env.WebRootPath;
            _contentRootPath = env.ContentRootPath;
            // Setup configuration sources.

            var builder = new ConfigurationBuilder()
                .SetBasePath(_contentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                // builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        private static string _applicationPath = string.Empty;
        private static string _contentRootPath = string.Empty;
        public IConfigurationRoot Configuration { get; set; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IPlaceOrderActions, PlaceOrderActions>();
            services.AddTransient<IPlaceOrderDbAccess, PlaceOrderDbAccess>();
            services.AddTransient<IPaymentActions, PaymentActions>();
            services.AddTransient<IPaymentDbAccess, PaymentDbAccess>();
            services.AddTransient<IBookYourTableBusiness, BookYourTableBusiness>();
            services.AddTransient<IBookYourTableRepository, BookYourTableRepository>();
            services.AddTransient<ILogService, LoggerService>();
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton<IItemStockServiceBusConsumer, ItemStockServiceBusConsumer>();
            services.AddSingleton<IPriceChangeServiceBusConsumer, PriceChangeServiceBusConsumer>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Info { Title = "OrderManager", Version = "1.0" });
                c.OperationFilter<HeaderFilter>();
            });

            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            services.AddDbContext<OrderManagementContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnectionString"),
               b => b.MigrationsAssembly("MT.OnlineRestaurant.DataLayer")));

            services.AddMvc()
                    .AddMvcOptions(options =>
                    {
                        options.Filters.Add(new Authorization());
                        options.Filters.Add(new LoggingFilter(Configuration.GetConnectionString("DatabaseConnectionString")));
                        options.Filters.Add(new ErrorHandlingFilter(Configuration.GetConnectionString("DatabaseConnectionString")));
                    });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.ExceptionMiddlewareExtensions();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "OrderManager (V 1.0)");
            });
            
            app.UseMvc();
            var bus = app.ApplicationServices.GetService<IItemStockServiceBusConsumer>();
            bus.RegisterOnMessageHandlerAndReceiveMessages();
            var servicebus = app.ApplicationServices.GetService<IPriceChangeServiceBusConsumer>();
            servicebus.RegisterOnMessageHandlerAndReceiveMessages();
        }
    }
}
