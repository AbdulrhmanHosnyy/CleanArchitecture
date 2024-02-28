namespace SchoolProject.Data.Entities
{
    public class InstructorSubject
    {
        public int InstID { get; set; }
        public int SubID { get; set; }
        public Instructor? Instructor { get; set; }
        public Subject? Subject { get; set; }
    }
}
