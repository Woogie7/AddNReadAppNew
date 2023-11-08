using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp
{
	public class MainApp : INotifyPropertyChanged
	{

		private User user;
		private User currUser;
		private List<User> users;

		public event PropertyChangedEventHandler PropertyChanged;
		public void NotifyPropertyChanged(string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


		public MainApp()
		{
			this.users = new List<User>();
			this.user = new User();
			this.currUser = new User();
		}

		public void LogOut()
		{
			this.User = new User();
		}


		#region GettersSetters
		public User User
		{
			get
			{
				return this.user;
			}

			set
			{
				this.user = value;
				NotifyPropertyChanged();
			}
		}

		public User CurrUser
		{
			get
			{
				return this.currUser;
			}

			set
			{
				this.currUser = user;
				NotifyPropertyChanged();
			}
		}

		public List<User> Users
		{
			get
			{
				return this.users;
			}

			set
			{
				this.users = value;
				NotifyPropertyChanged();
			}
		}

		

		#endregion

	}
}
