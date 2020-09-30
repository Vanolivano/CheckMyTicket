using System.ComponentModel.DataAnnotations;
namespace CheckMyTicket.Models
{
    public class Ticket
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Укажите тираж билета")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Недопустимая длина тиража билета")]
        public string Edition { get; set; }
        [Required(ErrorMessage = "Укажите номер билета")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Недопустимая длина номера билета")]
        public string Number { get; set; }
    }
}