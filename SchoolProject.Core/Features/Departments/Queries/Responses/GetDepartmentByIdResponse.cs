using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.Departments.Queries.Responses
{
    public class GetDepartmentByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public PaginatedResult<GetDepartmentByIdStudentsResponse>? StudentsL { get; set; }
        public List<GetDepartmentByIdSubjectsResponse>? Subjects { get; set; }
        public List<GetDepartmentByIdInstructorsResponse>? Instructors { get; set; }
    }

    public class GetDepartmentByIdStudentsResponse
    {
        public GetDepartmentByIdStudentsResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GetDepartmentByIdSubjectsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GetDepartmentByIdInstructorsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
