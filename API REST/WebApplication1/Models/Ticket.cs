using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
	public class Ticket
	{
		#region Propriedades

		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Campo Requerido!")]
		public string Description { get; set; }

		public string AuthorName { get; set; }

		public string Date { get; set; }

		#endregion

		#region Construtores
		public Ticket() {}
		
		#endregion

	}

}