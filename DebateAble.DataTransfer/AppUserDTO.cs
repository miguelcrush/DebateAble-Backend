namespace DebateAble.DataTransfer
{

	public class GetAppUserDTO
    {
		public Guid Id { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public bool Invited { get; set; }
	}

	public class PostAppUserDTO
	{
		public Guid Id { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public bool Invited { get; set; }
	}
}