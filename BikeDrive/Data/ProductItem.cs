using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BikeDrive.Data;

// Строка списка товаров: данные из БД и вычисляемые поля оформления
public class ProductItem
{
    private const int HighDiscountLimit = 15;
    private static readonly Brush HighDiscountBrush = new SolidColorBrush(Color.FromRgb(0x48, 0x3D, 0x8B));
    private static readonly Brush OutOfStockBrush = new SolidColorBrush(Color.FromRgb(0xD3, 0xD3, 0xD3));

    public ProductItem(Product product)
    {
        Article = product.Article;
        ProductName = product.ProductName;
        CategoryName = product.Category.CategoryName;
        Description = product.Description ?? "";
        ManufacturerName = product.Manufacturer.ManufacturerName;
        SupplierName = product.Supplier.SupplierName;
        UnitName = product.Unit.UnitName;
        Price = product.Price;
        Discount = product.Discount;
        Stock = product.Stock;
        PhotoFile = product.Photo;
    }

    public string Article { get; }
    public string ProductName { get; }
    public string CategoryName { get; }
    public string Description { get; }
    public string ManufacturerName { get; }
    public string SupplierName { get; }
    public string UnitName { get; }
    public decimal Price { get; }
    public int Discount { get; }
    public int Stock { get; }
    public string? PhotoFile { get; }

    public decimal FinalPrice => Math.Round(Price * (100 - Discount) / 100m, 2);
    public bool HasDiscount => Discount > 0;
    public string PriceText => Price.ToString("N2");
    public string FinalPriceText => FinalPrice.ToString("N2");
    public string DiscountText => Discount + "%";

    // Серый фон приоритетнее: товара нет на складе
    public Brush RowBackground
    {
        get
        {
            if (Stock <= 0)
            {
                return OutOfStockBrush;
            }

            return Discount > HighDiscountLimit ? HighDiscountBrush : Brushes.White;
        }
    }

    public Brush RowForeground
    {
        get
        {
            bool isDarkRow = Stock > 0 && Discount > HighDiscountLimit;
            return isDarkRow ? Brushes.White : Brushes.Black;
        }
    }

    // Фото из папки Images рядом с приложением, иначе заглушка из ресурсов
    public ImageSource Photo
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(PhotoFile))
            {
                string path = Path.Combine(AppContext.BaseDirectory, "Images", PhotoFile);
                if (File.Exists(path))
                {
                    return LoadFromFile(path);
                }
            }

            return new BitmapImage(new Uri("pack://application:,,,/Resources/picture.png"));
        }
    }

    private static BitmapImage LoadFromFile(string path)
    {
        BitmapImage image = new BitmapImage();
        image.BeginInit();
        image.CacheOption = BitmapCacheOption.OnLoad;
        image.UriSource = new Uri(path);
        image.EndInit();
        image.Freeze();
        return image;
    }
}
