using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RFIDClient.Desktop.Utils
{
    public class WaitCursor : IDisposable
    {
        private Cursor _previous;

        public WaitCursor()
        {
            _previous = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
        }
        public void Dispose()
        {
            Mouse.OverrideCursor = null;
        }
    }
}
