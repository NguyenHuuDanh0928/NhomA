namespace KoiVetService
{
    public class ServiceOrder
    {
        public int ServiceOrderId { get; set; }
        public int CustomerId { get; set; }
        public int VeterinarianId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ServiceType { get; set; }
        public string ServiceStatus { get; set; }
        public string ServiceResult { get; set; }

        public Customer Customer { get; set; }
        public Veterinarian Veterinarian { get; set; }
    }

}
