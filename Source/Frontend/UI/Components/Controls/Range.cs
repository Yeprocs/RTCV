using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTCV.UI.Components.Controls
{
    public partial class Range : UserControl
    {
        private int _precision;
        public int Precision
        {
            get => _precision;
            set
            {
                _precision = value;
                ulong precisionMax = unchecked(((ulong)1 << (8 * value - 1))|((ulong)1 << (8 * value - 1)) - 1);
                ulong minValue = Math.Min(Min, precisionMax);
                ulong maxValue = Math.Min(Max, precisionMax);
                tbMin.MaxLength = value * 2;
                tbMax.MaxLength = value * 2;
                Min = minValue;
                Max = maxValue;
            }
        }

        public ulong Min
        {
            get => ulong.Parse(tbMin.Text, NumberStyles.HexNumber);
            set => tbMin.Text = value.ToString("X");
        }
        public ulong Max
        {
            get => ulong.Parse(tbMax.Text, NumberStyles.HexNumber);
            set => tbMax.Text = value.ToString("X");
        }

        public Range(EventHandler valueChangedHandler, Action<Range> removeHandler)
        {
            InitializeComponent();
            tbMax.TextChanged += valueChangedHandler;
            tbMin.TextChanged += valueChangedHandler;
            btnDelete.Click += (s, e) => removeHandler(this);
        }
    }
}
