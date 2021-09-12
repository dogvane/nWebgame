using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nWebgame.platform.DB
{
    public class DBSetManager
    {
        static DefaultObjectPool<DBSet> objectPool = new DefaultObjectPool<DBSet>(new DefaultPooledObjectPolicy<DBSet>(), 10);

        public static DBSet GetByPool()
        {
            DBSet ret = objectPool.Get();
            if(ret != null)
            {
                ret = new DBSet();
            }

            return ret;
        }

        public static void ReleaseToPool(DBSet dBSet)
        {
            objectPool.Return(dBSet);
        }
    }
}
