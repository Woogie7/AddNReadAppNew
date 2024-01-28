using AddNReadApp.Core;
using AddNReadApp.Service.CartProviders;
using AddNReadApp.ViewModel;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace AddNReadApp.Command
{
	internal class CreatePdfCommand : BaseCommand
	{
		private readonly CartViewModel _cartViewModel;
		private readonly ICartProvider _cartProvider;

		public CreatePdfCommand(ICartProvider cartProvider)
		{
			_cartProvider = cartProvider;
		}

		public override bool CanExecute(object parameter)
		{
			return true;
		}
			public override void Execute(object parameter)
			{
				IEnumerable<Cart> carts = _cartProvider.GetCart();
			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

			// Создаем полный путь к файлу на рабочем столе
			string filePath = Path.Combine(desktopPath, $"PurchaseReport.pdf");

			// Создаем новый документ PDF
			PdfDocument document = new PdfDocument();
			document.Info.Title = "Отчет";

			// Добавить страницу с альбомной ориентацией
			PdfPage page = document.AddPage();
			page.Orientation = PageOrientation.Landscape;

			// Получить ширину и высоту страницы
			double pageWidth = page.Width;
			double pageHeight = page.Height;

			// Отрисовать содержимое на странице
			XGraphics gfx = XGraphics.FromPdfPage(page);

			// Создаем шрифт для текста
			XFont fontTitle = new XFont("Verdana", 16, XFontStyle.Bold);
			XFont fontHeader = new XFont("Verdana", 12, XFontStyle.Bold);
			XFont fontNormal = new XFont("Verdana", 8, XFontStyle.Regular);

			// Отображаем информацию о покупке
			gfx.DrawString("Отчет", fontTitle, XBrushes.Black, new XRect(0, 0, page.Width, 30), XStringFormats.Center);
			gfx.DrawString($"Дата покупки: {DateTime.Now}", fontHeader, XBrushes.Black, new XRect(50, 50, page.Width, fontNormal.Height), XStringFormats.TopLeft);
			gfx.DrawString($"Количество позиций: {carts.Count()}", fontHeader, XBrushes.Black, new XRect(50, 70, page.Width, 20), XStringFormats.TopLeft);

			// Добавляем таблицу с информацией о товарах
			int tableStartY = 100;
			int columnWidth = (int)(page.Width / 6);
			int tableRowHeight = 20;
			int columnX = 50;
			int tableHeaderY = tableStartY + 20;

			
			gfx.DrawString("Название", fontHeader, XBrushes.Black, new XRect(columnX + columnWidth, tableHeaderY, columnWidth, tableRowHeight), XStringFormats.TopLeft);
			gfx.DrawString("Цена", fontHeader, XBrushes.Black, new XRect(columnX + 2 * columnWidth, tableHeaderY, columnWidth, tableRowHeight), XStringFormats.TopLeft);
			gfx.DrawString("Дата", fontHeader, XBrushes.Black, new XRect(columnX + 3 * columnWidth, tableHeaderY, columnWidth, tableRowHeight), XStringFormats.TopLeft);
			gfx.DrawString("Количество", fontHeader, XBrushes.Black, new XRect(columnX + 4 * columnWidth, tableHeaderY, columnWidth, tableRowHeight), XStringFormats.TopLeft);
			gfx.DrawString("Категория", fontHeader, XBrushes.Black, new XRect(columnX + 5 * columnWidth, tableHeaderY, columnWidth, tableRowHeight), XStringFormats.TopLeft);

			int yPos = tableStartY + 40;
			foreach (var item in carts)
			{
				gfx.DrawString(item.Product.Name, fontNormal, XBrushes.Black, new XRect(columnX + columnWidth, yPos, columnWidth, tableRowHeight), XStringFormats.TopLeft);
				gfx.DrawString(item.Product.Price.ToString(), fontNormal, XBrushes.Black, new XRect(columnX + 2 * columnWidth, yPos, columnWidth, tableRowHeight), XStringFormats.TopLeft);
				gfx.DrawString(item.DateAdded.ToString(), fontNormal, XBrushes.Black, new XRect(columnX + 3 * columnWidth, yPos, columnWidth, tableRowHeight), XStringFormats.TopLeft);
				gfx.DrawString(item.Quantity.ToString(), fontNormal, XBrushes.Black, new XRect(columnX + 4 * columnWidth, yPos, columnWidth, tableRowHeight), XStringFormats.TopLeft);
				gfx.DrawString(item.Product.Category, fontNormal, XBrushes.Black, new XRect(columnX + 5 * columnWidth, yPos, columnWidth, tableRowHeight), XStringFormats.TopLeft);

				yPos += tableRowHeight;
			}

			// Строка для подписи
			int signatureY = yPos + 40;
			gfx.DrawString("Подпись:", fontHeader, XBrushes.Black, new XRect(columnX + 4 * columnWidth, signatureY, columnWidth, tableRowHeight), XStringFormats.TopLeft);

			XRect tableRect = new XRect(columnX, tableStartY, 6 * columnWidth, yPos - tableStartY + 40);
			gfx.DrawRectangle(XPens.Black, tableRect);

			// Сохранить документ PDF
			document.Save(filePath);

			// Показать сообщение об успешном создании и сохранении отчета
			MessageBox.Show("Отчет успешно создан и сохранен на рабочем столе.");


		}
	}
}
