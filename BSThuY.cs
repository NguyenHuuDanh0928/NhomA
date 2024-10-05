namespace KoiVetService
{
    public class Veterinarian
    {
        public int VeterinarianId { get; set; }
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public int YearsOfExperience { get; set; }

        public ICollection<ServiceOrder> ServiceOrders { get; set; }
        public ICollection<WorkSchedule> WorkSchedules { get; set; }
    }

}
