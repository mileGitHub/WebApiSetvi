using System.ComponentModel.Design;
using WebApiSetvi.Model;

namespace WebApiSetvi.Service.Interfaces
{
	public interface ICompanyUserService
	{
		Task<Result<List<User>>> GetUsersAsync(int companyId);
		Task<Result<User>> AddUser(User user);

	}
}
