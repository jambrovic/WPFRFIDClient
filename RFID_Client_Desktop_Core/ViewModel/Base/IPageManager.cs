using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RFIDClient.Desktop.Core
{
    public interface IPageManager
    {
        ICommand ClosePage { get; }
    }
}
