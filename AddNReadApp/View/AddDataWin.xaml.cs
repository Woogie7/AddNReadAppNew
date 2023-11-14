using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace AddNReadApp.View
{
	/// <summary>
	/// Interaction logic for AddDataWin.xaml
	/// </summary>
	public partial class AddDataWin : Window
	{
		Entities _db = new Entities();
		public AddDataWin()
		{
			InitializeComponent();
		}
		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			string fileName;

			if (pictureAdd.Source != null)
			{
				Uri imageUri = ((BitmapImage)pictureAdd.Source)?.UriSource;

				// Получаем информацию о файле из Uri
				fileName = System.IO.Path.GetFileName(imageUri.LocalPath);
			}
			else
				fileName = "picture.png";

			Product newProduct = new Product()
			{
				Image = fileName,
				Name = TBName.Text,
				Price = int.Parse(TBPrice.Text),
				Max_discount = int.Parse(TBMax_discount.Text),
				Current_discount = int.Parse(TBCurrent_discount.Text),
				Provider = TBProvider.Text,
				Manufacturer = TBManufacturer.Text,
				Category = TBCategory.Text,
				Quantity = int.Parse(TBQuantity.Text),
				Description = TBDescription.Text,
				Articul = TBArticul.Text
			};
			_db.Product.Add(newProduct);
			_db.SaveChanges();
			MainWindow.listViewPub.ItemsSource = _db.Product.ToList();
			MessageBox.Show("Успешно создано", "Сообщение", MessageBoxButton.OK);
			this.Close();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string startUp = SerchMainFolder() + @"\AddNReadApp\Images";

			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image files|*.png; *.jpg";
			openFileDialog.InitialDirectory = startUp;
			openFileDialog.FilterIndex = 1;
			openFileDialog.Multiselect = false;

			if (openFileDialog.ShowDialog() == true)
			{
				FileInfo selectedFile = new FileInfo(openFileDialog.FileName);
				FileInfo destinationPath = new FileInfo(System.IO.Path.Combine(startUp, selectedFile.Name));

				if (!destinationPath.Exists)
				{
					selectedFile.CopyTo(destinationPath.ToString(), true);
				}

				pictureAdd.Source = new BitmapImage(new Uri(destinationPath.ToString()));
			}

		}

		private string SerchMainFolder()
		{
			string workingDirectory = Environment.CurrentDirectory;
			string startupPath = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
			return startupPath;
		}
	}
}
