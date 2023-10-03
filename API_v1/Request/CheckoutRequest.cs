using System.ComponentModel.DataAnnotations;

namespace API.Request {
    public class CheckoutRequest {
        [Required]
        public List<int> ProductIds { get; set; }
        [Required]
        public int AddressId { get; set; }
    }
}
