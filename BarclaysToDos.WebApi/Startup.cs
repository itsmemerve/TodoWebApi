using AutoMapper;
using BarclaysToDos.Data;
using BarclaysToDos.Services.Core;
using BarclaysToDos.Services.ToDoItemServices;
using BarclaysToDos.Services.ToDoItemServices.Mapper;
using BarclaysToDos.Services.ToDoItemServices.Validation;
using FluentValidation.AspNetCore;

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
            services.AddMvc(options => { options.Filters.Add<ValidationFilter>(); })
                    .AddFluentValidation(opt => 
                    {
                        opt.RegisterValidatorsFromAssemblyContaining<Startup>();
                        opt.RegisterValidatorsFromAssemblyContaining<NameValidator>();
                    });

            var mapperconfig = new MapperConfiguration(x =>
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
