using BackEndWebAPI.DataContext;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndWebAPI.Repositories
{
    public class DuserRepository : IDuserRepository
    {
        private readonly ApplicationContext _context;

        public DuserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Duser> AddDuser(Duser duser)
        {
            var result = await _context.Dusers.AddAsync(duser);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Duser> DeleteDuser(int Id)
        {
             var result = await _context.Dusers.FindAsync(Id);
        //  var result=  await _context.Dusers.Where(a => a.DuserID == Id).FirstOrDefaultAsync();
            if (result != null)
            {
                 _context.Dusers.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
            
        }

        public async Task<Duser> GetDuser(int Id)
        {
          //  return await _context.Dusers.FindAsync(Id);
            return await _context.Dusers.FirstOrDefaultAsync(a => a.DuserID == Id);

        }                         
        public async Task<IEnumerable<Duser>> GetDusers()
        {
            return await _context.Dusers.ToListAsync();
        }
        
        public async Task<Duser> UpdateDuser(Duser duser)
        {
            var result = await _context.Dusers.FirstOrDefaultAsync(a=>a.DuserID==duser.DuserID);
            if (result != null)
            {
                result.Email = duser.Email;
                result.FirstName = duser.FirstName;
                result.SecondName = duser.SecondName;
                result.City = duser.City;
                await _context.SaveChangesAsync();
                return result;
            }
            return null;

        }
    }
}
