using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndWebAPI.Repositories
{
  public  interface IDuserRepository
    {
        Task<IEnumerable<Duser>> GetDusers();
        Task<Duser> GetDuser(int Id);
        Task<Duser> AddDuser(Duser duser);
        Task<Duser> UpdateDuser(Duser duser);
        Task<Duser> DeleteDuser(int Id);
    }
}
