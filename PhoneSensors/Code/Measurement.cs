using System.Windows.Media.Media3D;

namespace PhoneSensors.Code
{
    internal class Measurement
    {
        internal Measurement(SensorType sensorType, Vector3D value)
        {
            SensorType = sensorType;
            Value = value;
        }

        public SensorType SensorType { get; private set; }

        public Vector3D Value { get; private set; }
    }
}
