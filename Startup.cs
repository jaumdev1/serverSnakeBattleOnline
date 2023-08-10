using SnakeSpaceBattle.Service;
using SnakeSpaceBattle.Service.Interfaces;

namespace SnakeSpaceBattle
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddSingleton<IGameService, GameService>();

        }
    }
    
}
