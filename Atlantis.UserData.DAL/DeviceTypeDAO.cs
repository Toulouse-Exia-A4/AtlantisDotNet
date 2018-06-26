using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.UserData.DAL
{
    public class DeviceTypeDAO : IEntityProvider<DeviceType>
    {

        private UserDataContext _context;

        public DeviceTypeDAO(UserDataContext context)
        {
            _context = context;
        }

        public DeviceType Add(DeviceType entity)
        {
            try
            {
                if (_context.DeviceType.FirstOrDefault(x => x.Type == entity.Type) == null)
                {
                    var deviceType = _context.DeviceType.Add(new DeviceType() { Type = entity.Type, Unit = entity.Unit });
                    _context.SaveChanges();
                    return deviceType;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            throw new Exception("DeviceType.Type already exists.");
        }


        public List<DeviceType> All()
        {
            try
            {
                return _context.DeviceType.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<DeviceType>> AllAsync()
        {
            try
            {
                return await _context.DeviceType.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DeviceType Get(int id)
        {
            try
            {
                return _context.DeviceType.Find(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DeviceType> GetAsync(int id)
        {
            try
            {
                return await _context.DeviceType.FindAsync(id);
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
                var entity = _context.DeviceType.Find(id);
                _context.DeviceType.Remove(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DeviceType Update(DeviceType newEntity)
        {
            try
            {
                var entity = _context.DeviceType.Find(newEntity.Id);
                entity.Type = newEntity.Type;
                entity.Unit = newEntity.Unit;
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
