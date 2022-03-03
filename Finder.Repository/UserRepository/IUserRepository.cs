using System;
using System.Threading.Tasks;
using Finder.Core.Model;

namespace Finder.Repository.UserRepository
{
    public interface IUserRepository

    {
        Task<Usuario> GetInformationUser(string name);
        Usuario GetInformationUser(Guid id);

    }
}