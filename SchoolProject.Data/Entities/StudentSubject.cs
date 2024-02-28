namespace SchoolProject.Data.Entities
{
    public class StudentSubject
    {
        public int StudID { get; set; }
        public int SubID { get; set; }
        public decimal? Grade { get; set; }
        public virtual Student? Student { get; set; }
        public virtual Subject? Subject { get; set; }

    }
}
