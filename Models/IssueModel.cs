namespace Database.Models
{
    public class IssueModel
    {
        public int id { get; set; }
        public int volume { get; set; }
        public int Issue { get; set; }
        public bool is_archived { get; set; }
        public int semester { get; set; }

    }
	public class IssueView
	{
		public int volume { get; set; }
		public int Issue { get; set; }
		public bool is_archived { get; set; }
		public int semester { get; set; }

	}
}
