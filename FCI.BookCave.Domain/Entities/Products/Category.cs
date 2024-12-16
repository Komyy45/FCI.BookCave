using FCI.BookCave.Domain.Entities.Common;

namespace FCI.BookCave.Domain.Entities.Products
{
	public class Category : BaseEntity<int>
	{
		public string Name { get; set; } = null!;

		public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
	}
}
