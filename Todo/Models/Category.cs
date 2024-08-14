using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
	public class Category
	{
		public int Id { get; set; }
		[MinLength(3, ErrorMessage = "Min Length Should Be 3 Letters")]
		[MaxLength(20, ErrorMessage = "Max Length Should Be 20 Letters")]
		public string Name { get; set; }
		public ICollection<Todo>? Todos { get; set; }
	}
}
