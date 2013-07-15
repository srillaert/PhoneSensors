using System;
using System.Windows.Controls;

namespace PhoneSensors.UI
{
    public partial class LinearGaugeControl
    {
        public LinearGaugeControl() {
            InitializeComponent();
        }

        public void Update(double value) {
            value *= _multiply;
            double left = value < 0 ? 50 + value : 50;
            double width = Math.Abs(value);

            ValueRectangle.SetValue(Canvas.LeftProperty, left);
            ValueRectangle.Width = width;
        }

        private double _multiply = 20;
        public double Multiply {
            set {
                _multiply = value;
            }
        }
    }
}
