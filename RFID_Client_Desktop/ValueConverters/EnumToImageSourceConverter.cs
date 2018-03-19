using RFIDClient.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RFIDClient.Desktop
{
    /// <summary>
    /// A converter that takes an <see cref="Enum"/> and converts it to <see cref="ImageSource"/>
    /// </summary>
    public class EnumToImageSourceConverter : BaseValueConverter<EnumToImageSourceConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((PaymentImage)value)
            {
                case PaymentImage.AmericanExpress:
                    {
                        BitmapImage img = new BitmapImage();
                        img.BeginInit();
                        img.UriSource = new Uri(@"pack://application:,,,/RFIDClient.Desktop;component/Images/Icons_64x64/american-express.png");
                        img.EndInit();
                        return img;
                    }
                    
                case PaymentImage.Diners:
                    {
                        BitmapImage img = new BitmapImage();
                        img.BeginInit();
                        img.UriSource = new Uri(@"pack://application:,,,/RFIDClient.Desktop;component/Images/Icons_64x64/diners-club.png");
                        img.EndInit();
                        return img;
                    }
                case PaymentImage.MasterCard:
                    {
                        BitmapImage img = new BitmapImage();
                        img.BeginInit();
                        img.UriSource = new Uri(@"pack://application:,,,/RFIDClient.Desktop;component/Images/Icons_64x64/mastercard.png");
                        img.EndInit();
                        return img;
                    }
                case PaymentImage.Maestro:
                    {
                        BitmapImage img = new BitmapImage();
                        img.BeginInit();
                        img.UriSource = new Uri(@"pack://application:,,,/RFIDClient.Desktop;component/Images/Icons_64x64/maestro.png");
                        img.EndInit();
                        return img;
                    }
                case PaymentImage.Cash:
                    {
                        BitmapImage img = new BitmapImage();
                        img.BeginInit();
                        img.UriSource = new Uri(@"pack://application:,,,/RFIDClient.Desktop;component/Images/Icons_64x64/cash.png");
                        img.EndInit();
                        return img;
                    }
                case PaymentImage.PayPal:
                    {
                        BitmapImage img = new BitmapImage();
                        img.BeginInit();
                        img.UriSource = new Uri(@"pack://application:,,,/RFIDClient.Desktop;component/Images/Icons_64x64/paypal_big.png");
                        img.EndInit();
                        return img;
                    }
                case PaymentImage.Visa:
                    {
                        BitmapImage img = new BitmapImage();
                        img.BeginInit();
                        img.UriSource = new Uri(@"pack://application:,,,/RFIDClient.Desktop;component/Images/Icons_64x64/visa.png");
                        img.EndInit();
                        return img;
                    }
                default:
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
