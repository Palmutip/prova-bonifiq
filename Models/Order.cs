using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProvaPub.Models
{
	public class Order
	{
        [JsonIgnore]
        public int Id { get; set; }
		public decimal Value { get; set; }
		public int CustomerId { get; set; }
		public DateTime OrderDate { get; set; }

        [ForeignKey("CustomerId")]
        [JsonIgnore]
        public Customer Customer { get; set; }
    }
}
