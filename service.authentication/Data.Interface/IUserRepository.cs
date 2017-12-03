using System;
using System.Threading.Tasks;
using Ketan.Square2.Service.Authentication.Model;

namespace Ketan.Square2.Service.Authentication.Data.Interface
{
    public interface IUserRepository
    {
        /// <summary>
        /// Crée l'utilisateur fourni en paramètre dans le repository
        /// </summary>
        /// <param name="user">L'utilisateur à créer dans le repository, ne doit pas être null</param>
        /// <exception cref="ArgumentNullException">Exception lancée si le paramètre <paramref name="user"/> est null</exception>
        /// <exception cref="ObjectExistsException">Exception lancée si le paramètre <paramref name="user"/> contient un login qui existe déjà dans le repository</exception>
        Task CreateUserAsync(User user);

        /// <summary>
        /// Récupère un utilisateur par login.
        /// </summary>
        /// <param name="login">Login de l'utilisateur à récupérer</param>
        /// <returns>L'utilisateur s'il a été trouvé, null sinon</returns>
        /// <exception cref="ArgumentNullException">Exception lancée si le paramètre <paramref name="login"/> est null</exception>
        /// <exception cref="ArgumentException">Exception lancée si le paramètre <paramref name="login"/> est vide</exception>
        Task<User> GetUserByLoginAsync(string login);
    }
}
