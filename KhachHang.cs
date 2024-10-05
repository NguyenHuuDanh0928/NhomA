namespace KoiVetService
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime RegistrationDate { get; set; }

        public ICollection<ServiceOrder> ServiceOrders { get; set; }
    }

}
