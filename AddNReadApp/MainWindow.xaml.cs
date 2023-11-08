using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AddNReadApp
{

	public partial class MainWindow : Window
	{
		private List<Border> borders = new List<Border>();

		private MainApp app = new MainApp();

		public MainWindow()
		{
			InitializeComponent();

			Database.modelDb = new Entities();

			listView1.ItemsSource = Database.modelDb.Product.ToList();

			sortProp.ItemsSource = new string[] { "Name", "Price"};
			sortDir.ItemsSource = Enum.GetNames(typeof(ListSortDirection));
			sortProp.SelectionChanged += SelectionChanged;
			sortDir.SelectionChanged += SelectionChanged;

			listView1.Items.Filter = NameFilter;

			listView1.Items.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

			borders = GetBorders();
		}

		private bool NameFilter(object obj)
		{
			var FilterObj = obj as Product;

			return FilterObj.Name.Contains(filter.Text);
		}
		//private bool PriceFilter(object obj)
		//{
		//	var FilterObj = obj as Product;

		//	return FilterObj.Price.
		//}

		//public Predicate<object> GetFilters()
		//{
		//	return
		//}

		private void SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var SortProperty = sortProp.SelectedItem.ToString();
			var SortDiretion = sortDir.SelectedItem.ToString() == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending;

			listView1.Items.SortDescriptions[0] = new SortDescription(SortProperty, SortDiretion);
			listView1.Items.Filter = NameFilter;
		}
		private void filter_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (filter.Text == null)
				listView1.Items.Filter = null;
			else
				listView1.Items.Filter = NameFilter;
		}

		#region ContentBorders

		public List<Border> GetBorders()
		{
			List<Border> borders = new List<Border>();

			borders.Add(LogInBorder);
			borders.Add(SignUpBorder);
			borders.Add(UserBorder);
			borders.Add(ProductBorder);

			borders.Add(HomeBorder);

			return borders;
		}


		public void Show(Border border)
		{
			foreach (Border b in borders)
			{
				b.Visibility = b.Equals(border) ? Visibility.Visible : Visibility.Hidden;
			}
		}

		public void ShowUserPanels()
		{
			NotAuthPanel.Visibility = Visibility.Hidden;
			AuthPanel.Visibility = Visibility.Visible;
		}
		public void HideUserPanels()
		{
			NotAuthPanel.Visibility = Visibility.Visible;
			AuthPanel.Visibility = Visibility.Hidden;
		}

		#endregion

		private void LogInButton_Click(object sender, RoutedEventArgs e)
		{
			Show(LogInBorder);
		}

		private void SignUpButton_Click(object sender, RoutedEventArgs e)
		{
			Show(SignUpBorder);
		}

		private void AccountButton_Click(object sender, RoutedEventArgs e)
		{
			Show(UserBorder);
		}
		private void ProductButton_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Maximized;
			Show(ProductBorder);
		}

		private void LogOutButton_Click(object sender, RoutedEventArgs e) //Log Out
		{
			MessageBoxResult result = MessageBox.Show("хОтесь выйти?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				app.LogOut();

				HideUserPanels();
				Show(HomeBorder);
			}
		}

		private void RegistrationButton_Click(object sender, RoutedEventArgs e)
		{

			int id = 0;

			string name = RegNameBox.Text.Trim();

			string login = RegLoginBox.Text.Trim();

			string password = RegPasswordBox.Password.Trim();
			string passwordConfirm = RegPasswordConfirmBox.Password.Trim();
;

			if (!string.IsNullOrEmpty(login) && login.Length >= 3 && login.Length <= 50)
			{
				if (!string.IsNullOrEmpty(name))
				{
					if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(passwordConfirm) && 3 <= password.Length && password.Length <= 50)
					{
						if (password == passwordConfirm)
						{
							User userObj = new User()
							{
								ID = Database.modelDb.User.Max(x => x.ID) + 1,
								Login = login,
								Password = password,
								Name = name,
								IDrole = 2
							};
							Database.modelDb.User.Add(userObj);
							Database.modelDb.SaveChanges();

							MessageBox.Show("Поздравляем! Теперь вы можете войти!»", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
							Show(LogInBorder);
						}
						else
						{
							MessageBox.Show("Пароли разные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
						}
					}
					else
					{
						MessageBox.Show("Одно или оба поля пароля пусты! (Длина от 3 до 50)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
					}
				}
				else
				{
					MessageBox.Show("Поле имени пусто!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
			}
			else
			{
				MessageBox.Show("Поле для входа пусто!(Длина от 3 до 50)", "ErОшибкаror", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}

		private void EnterButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				string login = LogLoginBox.Text.Trim();
				string password = LogPasswordBox.Password.Trim();

				if (!string.IsNullOrEmpty(login) && login.Length >= 3 && login.Length <= 50)
				{
					if (!string.IsNullOrEmpty(password) && 3 <= password.Length && password.Length <= 50)
					{
						var userDb = Database.modelDb.User.FirstOrDefault(x => x.Login == login && x.Password == password);
						if (userDb.ID != 0)
						{
							app.User = userDb;
							app.CurrUser = userDb;

							ShowUserPanels();
							Show(HomeBorder);
						}
						else
						{
							MessageBox.Show("Не можете войти! Возможно, вы ввели неправильный логин или пароль.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
						}
					}
					else
					{
						MessageBox.Show("Поле пароля пусто!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
					}
				}
				else
				{
					MessageBox.Show("Поле для входа пусто!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
			}
			catch 
			{
				MessageBox.Show("Пользователь не найден");
			}
		}
	}
}
