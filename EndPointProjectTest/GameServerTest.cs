using EndPointProject;
using ExpandableFactory.IoC;
using ExpandableFactory.IoC.Exceptions;
using Moq;
using SpaceShipProject.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EndPointProjectTest
{
    public class GameServerTest
    {
        [Fact]
        public void IntepretCommandEqualTarget()
        {
            var request = new GameObjectRequest()
            {
                GameID = 1,
                UObjectID = 1,
                ActionID = "temp_cmd",
                Params = null
            };

            GameServer gameServer = new GameServer();

            gameServer.AddGame(1, new Game());
            gameServer.AddUObject(1, new UObject());

            Mock<ICommand> mock = new Mock<ICommand>();
            gameServer.AddCommand("temp_cmd", args => mock.Object);


            Assert.True(gameServer.IntepretCommand(request) == mock.Object);
        }

        [Fact]
        public void IntepretCommandAndAddToQueue()
        {
            var request = new GameObjectRequest()
            {
                GameID = 1,
                UObjectID = 1,
                ActionID = "temp_cmd",
                Params = null
            };

            GameServer gameServer = new GameServer();

            gameServer.AddGame(1, new Game());
            gameServer.AddUObject(1, new UObject());

            Mock<ICommand> mock = new Mock<ICommand>();
            gameServer.AddCommand("temp_cmd", args => mock.Object);

            gameServer.IntepretCommand(request);

            Assert.True(IOC.Resolve<ICommand>("queue.commands.dequeue") == mock.Object);
        }

        [Fact]
        public void IntepretCommand_VerifyGameAndUObjectEqualTarget()
        {
            var request = new GameObjectRequest()
            {
                GameID = 1,
                UObjectID = 1,
                ActionID = "temp_cmd",
                Params = null
            };

            var game = new Game();
            var uobject = new UObject();

            GameServer gameServer = new GameServer();

            gameServer.AddGame(1, game);
            gameServer.AddUObject(1, uobject);

            Mock<ICommand> mock = new Mock<ICommand>();
            gameServer.AddCommand("temp_cmd",
                args =>
                {
                    Assert.Equal(args[1], game);
                    Assert.Equal(args[2], uobject);

                    return mock.Object;
                });

            gameServer.IntepretCommand(request);
        }

        [Theory]
        [InlineData(0, 1, "temp_cmd")]
        [InlineData(1, 0, "temp_cmd")]
        [InlineData(1, 1, "another_cmd")]
        public void IntepretCommand_NotFoundGameAndUObjectAndCommand(int gameId, int uobjectId, string actionId)
        {
            var request = new GameObjectRequest()
            {
                GameID = gameId,
                UObjectID = uobjectId,
                ActionID = actionId,
                Params = null
            };

            GameServer gameServer = new GameServer();

            gameServer.AddGame(1, new Game());
            gameServer.AddUObject(1, new UObject());

            Mock<ICommand> mock = new Mock<ICommand>();
            gameServer.AddCommand("temp_cmd",
                args =>
                {
                    return mock.Object;
                });

            Assert.Throws<ResolveContainerException>(() => gameServer.IntepretCommand(request));
        }
    }
}
