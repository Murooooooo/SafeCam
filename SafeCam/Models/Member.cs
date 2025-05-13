namespace SafeCam.Models
{
    public class Member
    {
        public  int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string? PhotoUrl { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
    }
}
