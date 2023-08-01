namespace LibraryManagementSystem
{
    public class Member
    {
        public int MemberId { get; }
        public string Name { get; }
        public List<Book> LendingHistory { get; }

        public Member(int memberId, string name)
        {
            MemberId = memberId;
            Name = name;
            LendingHistory = new List<Book>();
        }
    }
}
