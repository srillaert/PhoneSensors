using System.Windows.Media.Media3D;

namespace PhoneSensors.UI
{
    public partial class VectorControl
    {
        public VectorControl() {
            InitializeComponent();
        }

        public void UpdateVector(Vector3D vector) {
            const string format = "F";

            XValueLabel.Content = vector.X.ToString(format);
            XGauge.Update(vector.X);

            YValueLabel.Content = vector.Y.ToString(format);
            YGauge.Update(vector.Y);

            ZValueLabel.Content = vector.Z.ToString(format);
            ZGauge.Update(vector.Z);
        }

        public double Multiply { 
            set { 
                XGauge.Multiply = value;
                YGauge.Multiply = value;
                ZGauge.Multiply = value;
            }
        }
    }
}
