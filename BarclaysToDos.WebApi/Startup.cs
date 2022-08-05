using AutoMapper;
using BarclaysToDos.Data;
using BarclaysToDos.Services.ToDoItemServices;
using BarclaysToDos.Services.ToDoItemServices.Mapper;

namespace BarclaysToDos.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddCors(c => { c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()); });
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(Startup));

            var mapperconfig = new AutoMapper.MapperConfiguration(x =>
            {
                x.AddProfile(new ToDoItemProfile());
            });

            IMapper mapper = mapperconfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton<ToDoContext>();
            services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())
               .UseHttpsRedirection()
               .UseAuthorization()
               .UseStaticFiles()
               .UseRouting()
              .UseEndpoints(endpoints =>
              {
                  endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=ToDo}/{action=Index}/{id?}");
              });

            AppDbInitializer.Initialize(app);
        }
    }
}
