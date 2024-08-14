using System.ComponentModel.DataAnnotations;

namespace Todo.ViewModels
{
	public class LoginCred
	{
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
