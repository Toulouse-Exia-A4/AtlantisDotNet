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

        public User Add(User entity)
        {
            try
            {
                if (_context.User.FirstOrDefault(x => x.UserId == entity.UserId) == null)
                {
                    var user = _context.User.Add(new User()
                    {
                        UserId = entity.UserId
                    });

                    _context.SaveChanges();
                    return user;
                }
                else
                {
                    throw new Exception("User already registered.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<User> All()
        {
            try
            {
                return _context.User.ToList();
            }
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Remove(int id)
        {
            try
            {
                var usr = _context.User.Find(id);
                _context.User.Remove(usr);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public User Update(User newEntity)
        {
            throw new Exception("User cannot be modified.");
        }
    }
}
