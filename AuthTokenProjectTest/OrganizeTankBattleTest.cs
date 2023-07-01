using AuthTokenProject;
using Intepretator;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AuthTokenProjectTest
{
    public class OrganizeTankBattleTest
    {
        private readonly GameRepostory _gameRepostory = new GameRepostory();
        private readonly UserRepository _userRepository = new UserRepository();

        private readonly User user1, user2, user3;
        //        Один из пользователей организует танковый бой и определяет список участников(их может быть больше 2-х).
        //На сервер авторизации уходит запрос о том, что организуется танковый бой и присылается список его участников.Сервер в ответ возвращает id танкового боя.
        public OrganizeTankBattleTest()
        {
            IOC.Register("UR", new InstanceResolveDependencyStrategy(_userRepository));

            user1 = _userRepository.New();
            user2 = _userRepository.New();
            user3 = _userRepository.New();
        }

        [Fact]
        public void CreateBattle_ReturnGameID()
        {
            IOC.Register("GR", new FunctionResolveDependencyStrategy(s => _gameRepostory));

            var battle = new InviteToGameService().CreateBattle(user1.ID);

            Assert.NotNull(battle);
            Assert.True(_gameRepostory.Get(battle.ID) == battle);
        }

        [Fact]
        public void AddPlayersToNewGame()
        {
            IOC.Register("GR", new FunctionResolveDependencyStrategy(s => _gameRepostory));

            var battle = new InviteToGameService().CreateBattle(user1.ID);

            Assert.True(battle.Orgnaizator == user1);

            Assert.DoesNotContain<User>(user1, battle.Players);

            Assert.Contains<User>(user2, battle.Players);
            Assert.Contains<User>(user3, battle.Players);
        }

        //Аутентифицированный пользователь посылает запрос на выдачу jwt токена, который авторизует право этого пользователя на участие в танковом бое.
        //Для этого он должен указать в запросе id танкового боя.
        //Если пользователь был указан в списке участников танкового боя, то он выдает пользователю jwt токен, в котором указан id игры.
        [Fact]
        public void GenerateTokenByUserId_ReturnToken()
        {
            IOC.Register("GR", new FunctionResolveDependencyStrategy(s => _gameRepostory));

            new InviteToGameService().CreateBattle(user1.ID);

            Mock<ITokenGenerator> mock = new Mock<ITokenGenerator>();

            string storeToken = null;
            mock.Setup(t => t.GenerateToken(It.IsAny<string>()))
                .Returns<string>(s =>
                {
                    storeToken = s;
                    return storeToken;
                });

            IOC.Register("TG", new InstanceResolveDependencyStrategy(mock.Object));

            var token = new InviteToGameService().GenerateToken(user1.ID);

            Assert.NotNull(token);
            Assert.True(token == storeToken);
        }

        [Fact]
        public void VerifyToken_SuccessOrThrowException()
        {
            IOC.Register("GR", new FunctionResolveDependencyStrategy(s => _gameRepostory));

            new InviteToGameService().CreateBattle(user1.ID);

            Mock<ITokenGenerator> mock = new Mock<ITokenGenerator>();

            string storeToken = null;
            mock.Setup(t => t.GenerateToken(It.IsAny<string>()))
                .Returns<string>(s =>
                {
                    storeToken = s;
                    return storeToken;
                });

            IOC.Register("TG", new InstanceResolveDependencyStrategy(mock.Object));

            var token = new InviteToGameService().GenerateToken(user1.ID);

            try
            {
                new InviteToGameService().VerifyToken(user1.ID, token);
            }
            catch (Exception)
            {
                Assert.True(false);
            }

            Assert.Throws<VerifyTokenException>(() => new InviteToGameService().VerifyToken(user1.ID, "test_token"));
        }
    }
}
