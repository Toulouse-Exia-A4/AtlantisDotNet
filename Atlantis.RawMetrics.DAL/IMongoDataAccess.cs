using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.RawMetrics.DAL
{
    public interface IMongoDataAccess<TEntity, TIdentifier>
    {
        TEntity Get(TIdentifier id);

        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TIdentifier id);
    }
}
