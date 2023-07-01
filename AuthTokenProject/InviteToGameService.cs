using Intepretator;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthTokenProject
{

    public class InviteToGameService
    {
        public Game CreateBattle(int userId)
        {
            var userRepository = IOC.Resolve<BaseRepository<User>>("UR");

            var user = userRepository.Get(userId);

            if (user == null)
                throw new UserNotFound("User not found {userId}.");

            var game = IOC.Resolve<BaseRepository<Game>>("GR").New();

            if (user == null)
                throw new ArgumentNullException("Game wasnt to create");

            game.Orgnaizator = user;

            foreach (var userItem in userRepository.GetList(s => s.ID != user.ID))
                game.Players.Add(userItem);

            return game;
        }

        public string GenerateToken(int userId)
        {
            var userRepository = IOC.Resolve<BaseRepository<User>>("UR");

            var user = userRepository.Get(userId);

            if (user == null)
                throw new UserNotFound("User not found {userId}.");

            var game = IOC.Resolve<BaseRepository<Game>>("GR").Get(s => s.Orgnaizator?.ID == user.ID || s.Players.Any(x => x.ID == user.ID));

            if (game == null)
                throw new ArgumentNullException("Game cannot find");

            game.Token = IOC.Resolve<ITokenGenerator>("TG").GenerateToken(string.Format($"{game.ID}:{userId}"));
            return game.Token;
        }

        public void VerifyToken(int userId, string token)
        {
            var userRepository = IOC.Resolve<BaseRepository<User>>("UR");

            var user = userRepository.Get(userId);

            if (user == null)
                throw new UserNotFound("User not found {userId}.");

            var game = IOC.Resolve<BaseRepository<Game>>("GR").Get(s => s.Orgnaizator?.ID == user.ID || s.Players.Any(x => x.ID == user.ID));

            if (game == null)
                throw new GameNotFound("Game cannot find");

            if (game.Token != token)
                throw new VerifyTokenException($"Token {token} isnt valid for {userId}.");
        }
    }
}
