using AppData.IService;
using AppData.Models;
using AppData.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private IRoleService roleService;
		public RoleController()
		{
			roleService = new RoleService();
		}
		//GET: api/<RoleController>
		[HttpGet("[action]")]
		public IEnumerable<Role> GetAllRole()
		{
			return roleService.GetAllRoles();
		}

		[HttpGet("[action]")]
		public Role GetbyIdRole(Guid id)
		{
			return roleService.GetById(id);
		}

		// POST api/<RoleController>
		[HttpPost("[action]")]
		public bool CreateRole(string name , string mota , int trangthai)
		{
			if(roleService.GetAllRoles().Any(c => c.RoleName.Contains(name)) == false)
			{
				Role role = new Role();
				role.IDRole = Guid.NewGuid();
				role.RoleName = name;
				role.RoleDescription = mota;
				role.Status = trangthai;
				return roleService.CreateRole(role);
			} else
			{
				return false;
			}
		
		}

		// PUT api/<RoleController>/5
		[HttpPut("[action]")]
		public bool UpdateRole(Guid id , string name, string mota, int trangthai)
		{
			if (roleService.GetAllRoles().Any(c => c.RoleName.Contains(name)) == false)
			{
				Role role = roleService.GetAllRoles().FirstOrDefault(c => c.IDRole == id);
				role.IDRole = role.IDRole;
				role.RoleName = name;
				role.RoleDescription = mota;
				role.Status = trangthai;
				return roleService.UpdateRole(role);
			}
			else
			{
				return false;
			}
			
		}

		// DELETE api/<RoleController>/5
		[HttpPut("[action]")]
		public bool DeleteRole(Guid id)
		{
			return roleService.DeleteRole(id);
		}
	}
}
