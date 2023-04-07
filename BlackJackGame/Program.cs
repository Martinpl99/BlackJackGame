using BlackJackGame;
using Autofac;
using BlackJackGame.Interfaces;

class Program
{
    static void Main(string[] args)
    {

        var container = Container.Configure();
        using (var scope = container.BeginLifetimeScope())
        {
            var app = scope.Resolve<IBlackJackGameApplication>();
            app.Run();
        }
    }
}