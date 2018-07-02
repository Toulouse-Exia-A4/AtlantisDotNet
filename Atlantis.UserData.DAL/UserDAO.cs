using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.UserData.DAL
{
    public class UserDAO : IEntityProvider<User>
    {
        private UserDataContext _context;

        public UserDAO(UserDataContext context)
        {
            _context = context;
        }

        public virtual User Add(User entity)
        {
            try
            {
                if (_context.User.FirstOrDefault(x => x.UserId == entity.UserId) == null)
                {
                    var user = _context.User.Add(entity);

                    _context.SaveChanges();
                    return user;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual List<User> All()
        {
            try
            {
                return _context.User.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<User>> AllAsync()
        {
            try
            {
                return await _context.User.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User Get(int id)
        {
            try
            {
                return _context.User.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual User GetByUserId(string userId)
        {
            try
            {
                return _context.User.Where(x => x.UserId == userId).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetAsync(int id)
        {
            try
            {
                return await _context.User.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Remove(int id)
        {
            try
            {
                var usr = _context.User.Find(id);
                if (usr != null)
                {
                    _context.User.Remove(usr);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual bool RemoveByUserId(string userId)
        {
            try
            {
                var usr = _context.User.Where(x => x.UserId == userId).FirstOrDefault();
                if (usr != null)
                {
                    _context.User.Remove(usr);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        public User Update(User newEntity)
        {
            throw new Exception("User cannot be modified.");
        }

        public virtual ICollection<Device> GetUserDevices(User user)
        {
            try
            {
                return _context.User.Find(user.Id).Device;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
