using System.ComponentModel.Design;
using WebApiSetvi.Model;

namespace WebApiSetvi.Repository.Interfaces
{
	public interface ICompanyUserRepository
	{
		Task<Result<List<User>>> GetUsersAsync(int companyId);
		Task<Result<User>> AddUser(User user);
	}
}
