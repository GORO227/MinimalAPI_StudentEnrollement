namespace StudentEnrollement.Data
{
    public class Stutent: BaseEntity 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdNumber { get; set; }
        public string Picture { get; set; }
        public List<Enrollement> Enrollements { get; set; } = new List<Enrollement>();
    }
}
