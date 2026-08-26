using SalesWebApi.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesWebApi.Data.Entities
{
    public class Sale
    {
        public int SaleId { get; set; }
        public string CustomerName { get; set; }
        public PaymentType PaymentType { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }
        public DateOnly SaleDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public IEnumerable<SaleDetail> Details { get; set; } = new List<SaleDetail>();
    }
}
