using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities.Procedures
{
    public class DepartmentStudentCountProcedure : GeneralLocalizableEntity
    {
        public int DID { get; set; }
        public string? DNameAr { get; set; }
        public string? DNameEn { get; set; }
        public int StudentCount { get; set; }
    }
    public class DepartmentStudentCountProcedureParameters
    {
        public int DID { get; set; } = 0;
    }
}
