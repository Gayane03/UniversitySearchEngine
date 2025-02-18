namespace SharedLibrary.DbModels.Request
{
	public class User
	{
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public bool IsActive { get; set; } = false;
		public int RoleId { get; set; } = 1;

	}
}
