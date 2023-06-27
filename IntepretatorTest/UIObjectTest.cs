using Intepretator;
using Intepretator.Commands;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntepretatorTest
{
    public class UIObjectTest
    {
        public UIObjectTest()
        {

            IOC.Register("uiorder.get.property", new FunctionResolveDependencyStrategy(
                args => new UIOjectGetPropertyValueDependencyStrategy((string)args[0]))
                );
            IOC.Register("uiorder.read.property.value", new FunctionResolveDependencyStrategy(
                args => new UIOjectGetPropertyValueDependencyStrategy((string)args[0]).Resolve<object>(args[1]))
                );

            var commandDeendency = new StorageDependencyStrategy(IOC.Resolve<IResolveDependencyStrategy>("uiorder.get.property", "Action"))
                .Add("StartMove", new FunctionResolveDependencyStrategy(args =>
                {
                    var user_dep = IOC.Resolve<IResolveDependencyStrategy>("uiorder.collection.user.get");
                    var vel_dep = IOC.Resolve<IResolveDependencyStrategy>("uiorder.get.property", "Velocity");

                    return new StartMoveCommand(user_dep.Resolve<UIUserObject>(args[0]), vel_dep.Resolve<int>(args[0]));
                }))
                .Add("StopMove", new FunctionResolveDependencyStrategy(args =>
                {
                    var user_dep = IOC.Resolve<IResolveDependencyStrategy>("uiorder.collection.user.get");

                    return new StopMoveCommand(user_dep.Resolve<UIUserObject>(args[0]));
                }))
                .Add("Fire", new FunctionResolveDependencyStrategy(args =>
                {
                    var user_dep = IOC.Resolve<IResolveDependencyStrategy>("uiorder.collection.user.get");
                    var vel_dep = IOC.Resolve<IResolveDependencyStrategy>("uiorder.get.property", "Shoot");

                    return new ShootCommand(user_dep.Resolve<UIUserObject>(args[0]), vel_dep.Resolve<bool>(args[0]));
                }));

            IOC.Register("command.collection.user.get", commandDeendency);
        }



        [Fact]
        public void UIOrderStartMoveActionCall_Test()
        {
            UIOrder order = new UIOrder()
            {
                ID = 10,
                Action = "StartMove",
                Velocity = 5,
                Shoot = true
            };

            var user = new UIUserObject() { ID = 10, Velocity = 0, Shoot = false };

            var userDependency = new StorageDependencyStrategy(IOC.Resolve<IResolveDependencyStrategy>("uiorder.get.property", "ID"))
                .Add(user.ID, new InstanceResolveDependencyStrategy(user));

            IOC.Register("uiorder.collection.user.get", new FunctionResolveDependencyStrategy(args => userDependency));

            Assert.False(user.Velocity == order.Velocity);

            var cmd = IOC.Resolve<ICommand>("command.collection.user.get", order);
            cmd.Execute();

            Assert.True(user.Velocity == order.Velocity);
        }

        [Fact]
        public void UIOrderStopMoveActionCall_Test()
        {
            UIOrder order = new UIOrder()
            {
                ID = 10,
                Action = "StopMove",
                Velocity = 10
            };

            var user = new UIUserObject() { ID = 10, Velocity = 10 };

            var userDependency = new StorageDependencyStrategy(IOC.Resolve<IResolveDependencyStrategy>("uiorder.get.property", "ID"))
                .Add(user.ID, new InstanceResolveDependencyStrategy(user));

            IOC.Register("uiorder.collection.user.get", new FunctionResolveDependencyStrategy(args => userDependency));

            var cmd = IOC.Resolve<ICommand>("command.collection.user.get", order);
            cmd.Execute();

            Assert.True(user.Velocity == 0);
        }

        [Fact]
        public void UIOrderFireActionCall_Test()
        {
            UIOrder order = new UIOrder()
            {
                ID = 10,
                Action = "Fire",
                Velocity = 10,
                Shoot = true
            };

            var user = new UIUserObject() { ID = 10, Velocity = 10, Shoot = false };

            var userDependency = new StorageDependencyStrategy(IOC.Resolve<IResolveDependencyStrategy>("uiorder.get.property", "ID"))
                .Add(user.ID, new InstanceResolveDependencyStrategy(user));

            IOC.Register("uiorder.collection.user.get", new FunctionResolveDependencyStrategy(args => userDependency));

            var cmd = IOC.Resolve<ICommand>("command.collection.user.get", order);
            cmd.Execute();

            Assert.True(user.Velocity == order.Velocity);
            Assert.True(user.Shoot == order.Shoot);
        }
    }
}
