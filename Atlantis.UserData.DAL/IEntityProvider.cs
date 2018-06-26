using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.UserData.DAL
{
    public interface IEntityProvider<TEnt>
    {
        TEnt Get(int id);

        Task<TEnt> GetAsync(int id);

        List<TEnt> All();

        Task<List<TEnt>> AllAsync();

        TEnt Add(TEnt entity);

        TEnt Update(TEnt newEntity);

        void Remove(int id);
    }
}
