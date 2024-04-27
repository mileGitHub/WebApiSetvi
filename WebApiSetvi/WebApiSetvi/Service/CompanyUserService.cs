
using System.Net;
using WebApiSetvi.Model;
using WebApiSetvi.Repository.Interfaces;
using WebApiSetvi.Service.Interfaces;

namespace WebApiSetvi.Service
{
	public class CompanyUserService : ICompanyUserService
	{
		private readonly ICompanyUserRepository _userRepository;

		public CompanyUserService(ICompanyUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		public async Task<Result<List<User>>> GetUsersAsync(int companyId) 
		{
			try
			{
				var result = await _userRepository.GetUsersAsync(companyId);
				if (result.Data != null)
				{
					result.Data = result.Data.OrderBy(u => u.FirstName).ToList();
					return result;
				}
				return result;
			}
			catch (Exception ex) 
			{
				return new Result<List<User>>(HttpStatusCode.InternalServerError, "Exception: " + ex.Message);
			}
		}
		public async Task<Result<User>> AddUser(User user)
		{
			return await _userRepository.AddUser(user);
		}
	}
}
