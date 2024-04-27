using System.ComponentModel.DataAnnotations;

namespace WebApiSetvi.Model
{
	public class User
	{
		public User()
		{
		}
		public User(int id, string firstName, string lastName, string email, int companyId)
		{
			Id = id;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			CompanyId = companyId;
		}

		public int Id { get; set; }

		[Required(ErrorMessage = "FirstName is required.")]
		public string? FirstName { get; set; }

		[Required(ErrorMessage = "LastName is required.")]
		public string? LastName { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		public string? Email { get; set; }

		public int CompanyId { get; set; }
	}
}
