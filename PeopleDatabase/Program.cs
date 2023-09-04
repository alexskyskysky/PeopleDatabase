using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PeopleDatabase.Data;

namespace PeopleDatabase {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();
            if (app.Environment.IsDevelopment()) {
                //app.UseSwagger();
                //app.UseSwaggerUI();
                app.UseStaticFiles();
            }
            app.UseDefaultFiles(); 
            app.UseStaticFiles(); 
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}