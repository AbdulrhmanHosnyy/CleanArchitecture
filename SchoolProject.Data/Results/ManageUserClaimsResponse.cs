namespace SchoolProject.Data.Results
{
    public class ManageUserClaimsResponse
    {
        public int UserId { get; set; }
        public List<UserClaim> Claims { get; set; }
    }
    public class UserClaim
    {
        public string Type { get; set; }
        public bool Value { get; set; }
    }
}
