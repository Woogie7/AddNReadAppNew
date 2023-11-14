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

namespace AddNReadApp.View
{
	/// <summary>
	/// Interaction logic for EditDataWin.xaml
	/// </summary>
	public partial class EditDataWin : Window
	{
		Entities _db = new Entities();

		private int id;
		public EditDataWin(int productID)
		{
			InitializeComponent();

			string startUp = SerchMainFolder() + @"\AddNReadApp\Images";

			id = productID;
			FileInfo destinationPath;

			var st = (from m in _db.Product where m.ID == id select m).First();
			if(String.IsNullOrEmpty(st.Image) || String.IsNullOrWhiteSpace(st.Image))
			{
				destinationPath = new FileInfo(System.IO.Path.Combine(startUp, "picture.png"));
			}
			else
			{
				destinationPath = new FileInfo(System.IO.Path.Combine(startUp, st.Image));
			}

			pictureAdd.Source = new BitmapImage(new Uri(destinationPath.ToString()));
			TBName.Text = st.Name;
			TBPrice.Text = st.Price.ToString();
			TBMax_discount.Text = st.Max_discount.ToString();
			TBCurrent_discount.Text = st.Current_discount.ToString();
			TBProvider.Text = st.Provider;
			TBManufacturer.Text = st.Manufacturer;
			TBCategory.Text = st.Category;
			TBQuantity.Text = st.Quantity.ToString();
			TBDescription.Text = st.Description;
			TBArticul.Text = st.Articul;
		}

		private void btnEdit_Click(object sender, RoutedEventArgs e)
		{
			Uri imageUri = ((BitmapImage)pictureAdd.Source)?.UriSource;

			// Получаем информацию о файле из Uri
			string fileName = System.IO.Path.GetFileName(imageUri.LocalPath);

			Product updateProduct = (from m in _db.Product where m.ID == id select m).Single();
			updateProduct.Image = fileName;
			updateProduct.Name = TBName.Text;
			updateProduct.Price = int.Parse(TBPrice.Text);
			updateProduct.Max_discount = int.Parse(TBMax_discount.Text);
			updateProduct.Current_discount = int.Parse(TBCurrent_discount.Text);
			updateProduct.Provider = TBProvider.Text;
			updateProduct.Manufacturer = TBManufacturer.Text;
			updateProduct.Category = TBCategory.Text;
			updateProduct.Quantity = int.Parse(TBQuantity.Text);
			updateProduct.Description = TBDescription.Text;
			updateProduct.Articul = TBArticul.Text;
			_db.SaveChanges();
			MainWindow.listViewPub.ItemsSource = _db.Product.ToList();
			MessageBox.Show("Изменине прошло успешно", "Сообщение", MessageBoxButton.OK);
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
