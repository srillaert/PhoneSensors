using System;
using System.Windows;
using PhoneSensors.Code;

namespace PhoneSensors.UI
{
    public partial class MainWindow
    {
        private MeasurementReceiver _receiver;

        public MainWindow() {
            InitializeComponent();

            Closed += MainWindow_Closed;
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Closed(object sender, EventArgs e) {
            _receiver.Close();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            try {
                _receiver = new MeasurementReceiver();
                _receiver.RegisterCallback(ShowMeasurement);
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ShowMeasurement(Measurement measurement) {
            switch (measurement.SensorType) {
                case SensorType.Accelerometer:
                    Dispatcher.BeginInvoke((Action)(() => AccelerationVectorControl.UpdateVector(measurement.Value)));
                    break;
                case SensorType.Gyroscope:
                    Dispatcher.BeginInvoke((Action)(() => GyroscopeVectorControl.UpdateVector(measurement.Value)));
                    break;
            }
        }
    }
}
