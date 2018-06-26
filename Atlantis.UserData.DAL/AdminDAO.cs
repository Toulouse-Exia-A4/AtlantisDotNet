using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.UserData.DAL
{
    public class AdminDAO : IEntityProvider<Admin>
    {
        private UserDataContext _context;

        string GetMD5Hash(string original)
        {
            using (MD5 md5hash = MD5.Create())
            {
                byte[] computedHash = md5hash.ComputeHash(Encoding.UTF8.GetBytes(original));

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < computedHash.Length; i++)
                {
                    sb.Append(computedHash[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public AdminDAO(UserDataContext context)
        {
            _context = context;
        }

        public Admin Add(Admin entity)
        {
            if(entity.AdminId == null || entity.Password == null || entity.AdminId.Length == 0 || entity.Password.Length == 0)
                throw new Exception("Identifier and Password cannot be empty");


            try
            {
                if (_context.Admin.FirstOrDefault(x => x.AdminId == entity.AdminId) == null)
                {
                    var newAdmin = _context.Admin.Add(new Admin()
                    {
                        AdminId = entity.AdminId,
                        Password = GetMD5Hash(entity.Password)
                    });

                    _context.SaveChanges();
                    return newAdmin;
                }
                else
                {
                    throw new Exception("Admin Identifier already exists.");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Admin> All()
        {
            try
            {
                return _context.Admin.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Admin>> AllAsync()
        {
            try
            {
                return await _context.Admin.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Admin Get(int id)
        {
            try
            {
                return _context.Admin.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Admin> GetAsync(int id)
        {
            try
            {
                return await _context.Admin.FindAsync(id);
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
                var admin = _context.Admin.Find(id);
                _context.Admin.Remove(admin);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Admin Update(Admin newEntity)
        {
            try
            {
                var oldAdmin = _context.Admin.Find(newEntity.Id);

                if (oldAdmin.AdminId != newEntity.AdminId)
                {
                    throw new Exception("You cannot update the Admin Identifier.");
                }

                oldAdmin.Password = newEntity.Password;
                _context.SaveChanges();
                return oldAdmin;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
