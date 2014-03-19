using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trolltampen.DAL;

namespace Trolltampen.Repositories
{
    public class FrontRepositoryBase
    {
        protected TTDBContext db;
        protected bool disposed = false;
        public FrontRepositoryBase(TTDBContext db)
        {
            this.db = db;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}