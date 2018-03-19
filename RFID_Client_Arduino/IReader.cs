using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Arduino
{
    public delegate void ReaderEventHandler(string data);
    public interface IReader
    {
        event ReaderEventHandler onReaderDataReceived;
        IReader GetReader(string portName, int baudRate, bool dtrEnable);
        void OpenCommunication();
        void CloseCommunication();
        bool IsPortOpen();
    }
}
