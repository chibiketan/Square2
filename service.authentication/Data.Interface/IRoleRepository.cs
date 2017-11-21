using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ketan.Square2.Service.Authentication.Model;

namespace Ketan.Square2.Service.Authentication.Data.Interface
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Crée le role fourni en paramètre dans le repository
        /// </summary>
        /// <param name="role">Le role à créer, ne doit pas être null</param>
        /// <exception cref="ArgumentNullException">Exception lancée si le paramètre <paramref name="role"/> est null</exception>
        Task CreateAsync(Role role);

        /// <summary>
        /// Récupère l'ensemble des roles présents dans le repository
        /// </summary>
        /// <returns>La liste des roles existants</returns>
        Task<List<Role>> GetAllRoleAsync();

        /// <summary>
        /// Récupère un rôle par nom.
        /// </summary>
        /// <param name="name">Nom du rôle à récupérer</param>
        /// <returns>Le rôle s'il a été trouvé, null sinon</returns>
        Task<Role> GetRoleByNameAsync(string name);

    }
}