namespace FluentValidationApp.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Mail { get; set; }
        public int Age { get; set; }
        //AutoMapper best practice
        public string CreditCardNumber { get; set; }
        public DateTime CreditCardValidDate { get; set; }
        public string FullInfo { get; set; }
    }
}