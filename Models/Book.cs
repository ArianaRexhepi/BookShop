using System;
using System.ComponentModel.DataAnnotations;
namespace Books.Models
{
	public class Book
	{
		[Required]
		public int Id { get; set; }
		[MaxLength(length:100)]
		public string Title { get; set; }
		[Required]
		public string Author { get; set; }
		[MaxLength(length:10000)]
		public string Description { get; set;}
		[Required
		,MaxLength(length:15)]
		public string ISBN { get; set;}
		[Required,
		DataType(DataType.Date),
		Display(Name = "Date Time")]
		public DateTime DatePublished { get; set;}
		[Required,
		DataType(DataType.Currency),]
		public int Price { get; set;}
		[Display(Name = "Image")]
		public string Image { get; set;}

	}
}

