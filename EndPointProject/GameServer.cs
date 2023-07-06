using ExpandableFactory.IoC;
using SpaceShipProject.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPointProject
{
    public class GameServer
    {
        public GameServer()
        {
            IOCGameResolver.RegisterQueueCommands();
        }

        public void AddGame(int id, Game game)
        {
            StorageInstanceDependencyStrategy.Instace().Resolve<Game>(id, game);
        }

        public void AddUObject(int id, UObject uObject)
        {
            StorageInstanceDependencyStrategy.Instace().Resolve<UObject>(id, uObject);
        }

        public void AddCommand(string commandName, Func<object[], ICommand> commandCreator) 
        {
            StorageInstanceDependencyStrategy.Func().Resolve<ICommand>(commandName, commandCreator);
        }

        public ICommand IntepretCommand(GameObjectRequest request)
        {
            return IOC.Resolve<ICommand>("intepret.queue.enqueue", request);
        }
    }
}
