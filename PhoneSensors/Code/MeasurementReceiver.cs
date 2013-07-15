using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Media.Media3D;

namespace PhoneSensors.Code
{
    internal class MeasurementReceiver
    {
        #region Constants
        private static readonly Dictionary<Int32, SensorType> SensorTypeIntToEnum = new Dictionary<Int32, SensorType> {
            { 1, SensorType.Gps },
            { 2, SensorType.Compass },
            { 3, SensorType.Accelerometer },
            { 4, SensorType.Gyroscope }
        };
        private const int ListenPort = 5555;
        #endregion

        #region Fields
        private readonly UdpClient _client = new UdpClient(ListenPort);
        private Action<Measurement> _callback;
        private bool _open = true;
        #endregion

        #region Public
        public void Close() {
            _open = false;
            _client.Close();
            _callback = null;
        }

        public void RegisterCallback(Action<Measurement> callback) {
            _callback = callback;
            _client.BeginReceive(Receive, null);
        }
        #endregion

        #region Private 
        private void Receive(IAsyncResult res) {
            if (_open) {
                var remoteIpEndPoint = new IPEndPoint(IPAddress.Any, ListenPort);
                byte[] receivedByteArray = _client.EndReceive(res, ref remoteIpEndPoint);
                var receivedString = Encoding.UTF8.GetString(receivedByteArray);
                string[] dataParsed = receivedString.Split(',');

                if (dataParsed.Length >= 5) {
                    //var timestamp = long.Parse(dataParsed[0], CultureInfo.InvariantCulture);
                    var sensorTypeInt = int.Parse(dataParsed[1], CultureInfo.InvariantCulture);

                    float x = float.Parse(dataParsed[2], CultureInfo.InvariantCulture);
                    float y = float.Parse(dataParsed[3], CultureInfo.InvariantCulture);
                    float z = float.Parse(dataParsed[4], CultureInfo.InvariantCulture);
                    var vector = new Vector3D(x, y, z);

                    SensorType sensorType;
                    bool found = SensorTypeIntToEnum.TryGetValue(sensorTypeInt, out sensorType);
                    if (found) {
                        var measurement = new Measurement(sensorType, vector);
                        _callback(measurement);
                    }
                }

                _client.BeginReceive(Receive, null);
            }
        }
        #endregion
    }
}
