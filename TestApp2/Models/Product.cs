using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestApp2.Models
{
	public class Product
	{ 

	public int Id { get; set; }

	[Required]
	[DisplayName("Product Name")]
	public string Name { get; set; }

	[Required]
	[DisplayName("Product Price")]
	public double Price { get; set; }
	}
}