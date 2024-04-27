
using System.Data;
using System.Data.SqlClient;
using System.Net;
using WebApiSetvi.Model;
using WebApiSetvi.Repository.Interfaces;

namespace WebApiSetvi.Repository
{
	public class CompanyUserRepository : ICompanyUserRepository
	{
		//private readonly IUserRepository _userRepository;
		private readonly string _connectionString;
		public CompanyUserRepository(string connectionString)
		{
			_connectionString = connectionString;
		}
		public async Task<Result<List<User>>> GetUsersAsync(int companyId)
		{
			try
			{
				using var connection = new SqlConnection(_connectionString);
				using var command = new SqlCommand("GetCompanyUsers", connection);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@companyId", SqlDbType.Int).Value = companyId;
				connection.Open();
				using var reader = await command.ExecuteReaderAsync();

				var users = new List<User>();
				while (await reader.ReadAsync())
				{
					//All user data are mandatory, not necessary to check is Null
					var user = new User(
						reader.GetInt32(reader.GetOrdinal("Id")),
						reader.GetString(reader.GetOrdinal("FirstName")),
						reader.GetString(reader.GetOrdinal("LastName")),
						reader.GetString(reader.GetOrdinal("Email")),
						reader.GetInt32(reader.GetOrdinal("CompanyId")) 
					);
					users.Add(user);
				}
				return new Result<List<User>>(users);
		    }
			catch (SqlException ex)
			{
				return new Result<List<User>>(HttpStatusCode.InternalServerError, " SQL DB Exception: " + ex.Message);
			}
			catch (Exception ex)
			{
				return new Result<List<User>>(HttpStatusCode.InternalServerError, "Exception: " + ex.Message);
			}
		}
		public async Task<Result<User>> AddUser(User user)
		{
			try
			{
				using var connection = new SqlConnection(_connectionString);
				await connection.OpenAsync();

				using var command = new SqlCommand("AddUser", connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@firstName", SqlDbType.NVarChar).Value = user.FirstName;
				command.Parameters.AddWithValue("@lastName", SqlDbType.NVarChar).Value = user.LastName;
				command.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = user.Email;
				command.Parameters.AddWithValue("@companyId", SqlDbType.Int).Value = user.CompanyId;

				using var reader = await command.ExecuteReaderAsync();
				if (await reader.ReadAsync())
				{
					//All user data are mandatory, not necessary to check is Null
					var newUser = new User(
						reader.GetInt32(reader.GetOrdinal("Id")) ,
					    reader.GetString(reader.GetOrdinal("FirstName")),
						reader.GetString(reader.GetOrdinal("LastName")),
						reader.GetString(reader.GetOrdinal("Email")),
						reader.GetInt32(reader.GetOrdinal("CompanyId"))
					);
					return new Result<User>(newUser);
				}
				else
				{
					return new Result<User>(HttpStatusCode.BadRequest, "Failed to return user!");
				}
			}
			catch (SqlException ex)
			{
				return new Result<User>(HttpStatusCode.InternalServerError, " SQL DB Exception: " + ex.Message);
			}
			catch (Exception ex)
			{
				return new Result<User>(HttpStatusCode.InternalServerError, "Exception: " + ex.Message);
			}

		}
	}
}
