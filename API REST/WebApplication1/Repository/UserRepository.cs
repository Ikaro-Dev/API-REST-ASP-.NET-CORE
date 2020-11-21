using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
	public static class UserRepository
	{
		#region Métodos
		public static User Get(string username, string password)
		{
			var users = new List<User>();
			users.Add(new User { Id = 1, UserName = "Admin", Password = "PasswordAdmin" });
			return users.Where(x => x.UserName.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
		}

		#endregion

	}
}
