using RFIDClient.Desktop.Core;
using System;
using System.Diagnostics;
using System.Globalization;

namespace RFIDClient.Desktop
{
    /// <summary>
    /// Converts the <see cref="ApplicationPage"/> to an actual view/page
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Find the appropriate page
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.Login:
                    return new LoginPage();

                case ApplicationPage.Receipt:
                    return new ReceiptPage();

                case ApplicationPage.NewUser:
                    return new NewUserPage();

                case ApplicationPage.UpdateUser:
                    return new UpdateUserPage();

                case ApplicationPage.Payment:
                    return new PaymentPage();

                case ApplicationPage.AllItems:
                    return new ItemsPage();

                case ApplicationPage.NewItem:
                    return new NewItemPage();

                default:
                    //Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
