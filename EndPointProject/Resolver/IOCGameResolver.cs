using ExpandableFactory.IoC;
using SpaceShipProject;
using SpaceShipProject.Contracts.Commands;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EndPointProject
{
    public static class IOCGameResolver
    {
        public static void RegisterQueueCommands()
        {
            Queue<ICommand> commands = new Queue<ICommand>();

            IOC.Register("queue.commands", s => commands);

            IOC.Register("queue.commands.enqueue",
                (args) =>
                {
                    var cmd = (ICommand)args[0];
                    commands.Enqueue(cmd);

                    return cmd;
                });

            IOC.Register("queue.commands.dequeue",
                (args) =>
                {
                    if (commands.Count == 0)
                        return null;

                    var cmd = commands.Dequeue();
                    cmd.Execute();

                    return cmd;
                });

            IOC.Register("find_object_in_storage", (s) => new FindObjectInStorageDependencyStrategy());

            IOC.Register("read_property_value", new ReadPropertyValueDependencyStrategy());

            IOC.Register("intepret", new IntepretDependencyStrategy());

            IOC.Register("intepret.queue.enqueue", (args) =>
            {
                var cmd = IOC.Resolve<ICommand>("intepret", args);

                if (cmd != null)
                    return IOC.Resolve<ICommand>("queue.commands.enqueue", cmd);

                return null;
            });
        }
    }
}
