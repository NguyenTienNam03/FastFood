using AppData.IService;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
	public class RoleService : IRoleService
	{
		private DB_Context _context;
		public RoleService()
		{
			_context = new DB_Context();
		}

		public bool CreateRole(Role role)
		{
			try
			{
				_context.roles.Add(role);
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool DeleteRole(Guid idrole)
		{
			try
			{
				var role = _context.roles.FirstOrDefault(c => c.IDRole == idrole);
				if (role != null)
				{
					role.Status = 0;
					_context.roles.Update(role);
					_context.SaveChanges();
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public List<Role> GetAllRoles()
		{
			return _context.roles.ToList();
		}

		public Role GetById(Guid id)
		{
			return _context.roles.FirstOrDefault(c => c.IDRole == id);
		}

		public bool UpdateRole(Role role)
		{
			try
			{
				var role1 = _context.roles.FirstOrDefault(c => c.IDRole == role.IDRole);
				if (role1 != null)
				{
					role1.RoleName = role.RoleName;
					role1.RoleDescription = role.RoleDescription;
					role1.Status = role.Status;
					_context.roles.Update(role1);
					_context.SaveChanges();
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
