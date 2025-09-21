using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FluentValidationApp.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Province { get; set; }
        public string PostCode { get; set; }

        [ValidateNever]
        public virtual Customer Customer{ get; set; }
    }
}