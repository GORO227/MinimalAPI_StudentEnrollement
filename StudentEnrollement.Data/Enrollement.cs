namespace StudentEnrollement.Data
{
    public class Enrollement : BaseEntity
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public virtual Course Course { get; set;}
        public virtual Stutent Stutent { get; set;}
    }
}
