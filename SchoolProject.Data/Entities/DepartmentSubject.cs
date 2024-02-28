namespace SchoolProject.Data.Entities
{
    public class DepartmentSubject
    {
        public int DID { get; set; }
        public int SubID { get; set; }
        public virtual Department? Department { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
