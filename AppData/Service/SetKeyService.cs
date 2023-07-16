using AppData.IService;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
    public class SetKeyService : ISetKeyService
    {
        private DB_Context _context;
        public SetKeyService()
        {
            _context = new DB_Context();
        }
        public bool CreateSetKey(Setkey setkey)
        {
            try
            {
                _context.setkey.Add(setkey);
                _context.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }

        public bool DeleteSetKey(Guid id)
        {
            try
            {
                var setkey = _context.setkey.FirstOrDefault(c => c.IDSetKey == id);
                if (setkey != null)
                {
                    _context.setkey.Remove(setkey);
                    _context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Setkey> GetSetKeys()
        {
            return _context.setkey.ToList();
        }
    }
}
