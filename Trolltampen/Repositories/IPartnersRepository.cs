using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trolltampen.Models;

namespace Trolltampen.Repositories
{
    public interface IPartnersRepository:IDisposable
    {
        IEnumerable<Article> GetPartners();
    }
}
