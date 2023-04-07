using Autofac;
using BlackJackGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackGame.Interfaces;

namespace BlackJackGame
{
    public static class Container
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<BlackJackGame>().As<IBlackJackGame>();
            builder.RegisterType<BlackJackGameApplication>().As<IBlackJackGameApplication>();
            builder.RegisterType<GenerateCards>().As<IGenerateCards>();

            return builder.Build();
        }
    }
}
