using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
	public interface IRoleService
	{
		public bool CreateRole(Role role);
		public bool DeleteRole(Guid idrole);
		public bool UpdateRole(Role role);
		public List<Role> GetAllRoles();
		public Role GetById(Guid id);
	}
}
