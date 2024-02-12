namespace StudentEnrollement.Data
{
    public class Course : BaseEntity 
    {
        public string Title { get; set; }
        public int Credits { get; set; }
        public List<Enrollement> Enrollements { get; set; } = new List<Enrollement>();
    }
}
