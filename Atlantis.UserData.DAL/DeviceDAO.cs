using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.UserData.DAL
{
    public class DeviceDAO : IEntityProvider<Device>
    {
        private UserDataContext _context;

        public DeviceDAO(UserDataContext context)
        {
            _context = context;
        }

        public virtual Device Add(Device entity)
        {
            try
            {
                if (entity.DeviceId == null || entity.DeviceId.Length == 0)
                    throw new Exception("DeviceId Field is required in order to add entity in DB.");

                if (_context.DeviceType.Find(entity.DeviceTypeId) == null)
                {
                    throw new Exception("Device.TypeId doesn't exist.");
                }

                var device = _context.Device.Add(entity);
                _context.SaveChanges();

                return device;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual List<Device> All()
        {
            try
            {
                return _context.Device.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Device>> AllAsync()
        {
            try
            {
                return await _context.Device.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Device Get(int id)
        {
            try
            {
                return _context.Device.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Device> GetAsync(int id)
        {
            try
            {
                return await _context.Device.FindAsync(id);
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
                var device = _context.Device.Find(id);
                _context.Device.Remove(device);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual Device Update(Device newEntity)
        {
            try
            {
                var device = _context.Device.Find(newEntity.Id);
                device.UserId = newEntity.UserId;
                _context.SaveChanges();
                return device;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Device> GetAllDevicesOfType(DeviceType type)
        {
            try
            {
                return _context.Device.Where(x => x.DeviceTypeId == type.Id).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual Device GetByName(string deviceId)
        {
            try
            {
                return _context.Device.FirstOrDefault(x => x.DeviceId == deviceId);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
