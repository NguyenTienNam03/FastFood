using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface ISetKeyService
    {
        public bool CreateSetKey(Setkey setkey);
        public List<Setkey> GetSetKeys();
        public bool DeleteSetKey(Guid id);
    }
}
