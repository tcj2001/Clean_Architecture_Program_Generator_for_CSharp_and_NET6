//////////////////////////
// generated Program.cs //
/////////////////////////
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Context;

namespace startup
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //required if you are using entity framework
                    //now using Sqllite as database
                    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("DataSource=app.db"));
                    //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=DESKTOP-GBANT4V; Database=BookStoresDB; Trusted_Connection=True;"));
                })
                .RunConsoleAsync();
        }
    }
}
