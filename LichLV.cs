namespace KoiVetService
{
    public class WorkSchedule
    {
        public int WorkScheduleId { get; set; }
        public int VeterinarianId { get; set; }
        public DateTime WorkDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Status { get; set; }

        public Veterinarian Veterinarian { get; set; }
    }

}
