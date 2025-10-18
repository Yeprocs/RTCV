using RTCV.Common;

namespace RTCV.UI.Components.EngineConfig.EngineControls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using CorruptCore;
    using Controls;
    using EngineConfig;
    using System.Linq;
    using System.Linq.Expressions;

    public partial class NightmareEngineControl : EngineConfigControl
    {
        private bool updatingMinMax = false;
        private bool updatingBlastType = false;
        private List<Range> RangeControls => flpRanges.Controls.OfType<Range>().ToList();
        private (ulong Min, ulong Max)[] Ranges => RangeControls.Select(r => (r.Min, r.Max)).ToArray();

        internal NightmareEngineControl(Point location) : base(location)
        {
            InitializeComponent();

            cbBlastType.SelectedIndex = 0;
            nmMinValueNightmare.ValueChanged += UpdateMinValue;
            nmMaxValueNightmare.ValueChanged += UpdateMaxValue;
        }

        private void UpdateMinValue(object sender, EventArgs e)
        {
            //We don't want to trigger this if it caps when stepping downwards
            if (updatingMinMax)
            {
                return;
            }

            ulong minValue = Convert.ToUInt64(nmMinValueNightmare.Value);
            ulong maxValue = Convert.ToUInt64(nmMaxValueNightmare.Value);

            if (minValue > maxValue)
            {
                nmMinValueNightmare.Value = maxValue;
                minValue = maxValue;
                //return;
            }

            NightmareEngine.MinValue = minValue;
        }

        private void UpdateMaxValue(object sender, EventArgs e)
        {
            //We don't want to trigger this if it caps when stepping downwards
            if (updatingMinMax)
            {
                return;
            }

            ulong minValue = Convert.ToUInt64(nmMinValueNightmare.Value);
            ulong maxValue = Convert.ToUInt64(nmMaxValueNightmare.Value);

            if (maxValue < minValue)
            {
                nmMaxValueNightmare.Value = minValue;
                maxValue = minValue;
                //return;
            }

            NightmareEngine.MaxValue = maxValue;
        }

        private void UpdateBlastType(object sender, EventArgs e)
        {
            switch (cbBlastType.SelectedItem.ToString())
            {
                case "RANDOM":
                    if (!updatingBlastType) NightmareEngine.Algo = NightmareAlgo.RANDOM;
                    nmMinValueNightmare.Enabled = true;
                    nmMaxValueNightmare.Enabled = true;
                    break;

                case "RANDOMTILT":
                    if (!updatingBlastType) NightmareEngine.Algo = NightmareAlgo.RANDOMTILT;
                    nmMinValueNightmare.Enabled = true;
                    nmMaxValueNightmare.Enabled = true;
                    break;

                case "TILT":
                    if (!updatingBlastType) NightmareEngine.Algo = NightmareAlgo.TILT;
                    nmMinValueNightmare.Enabled = false;
                    nmMaxValueNightmare.Enabled = false;
                    break;
            }
        }

        private void RangeChanged(object sender, EventArgs e)
        {
            if (updatingMinMax) return;
            NightmareEngine.Ranges = Ranges;
        }
        
        private void RangeRemoved(Range range)
        {
            flpRanges.Controls.Remove(range);
            NightmareEngine.Ranges = Ranges;
        }

        private void ClickAddRange(object sender, EventArgs e)
        {
            AddRange();
            NightmareEngine.Ranges = Ranges;
        }

        private void cbAdvanced_CheckedChanged(object sender, EventArgs e)
        {
            NightmareEngine.ShouldUseRanges = cbAdvanced.Checked;
            pnAdvanced.Visible = cbAdvanced.Checked;
        }
        
        private void AddRange()
        {
            var range = new Range(RangeChanged, RangeRemoved)
            {
                Precision = RtcCore.CurrentPrecision
            };
            flpRanges.Controls.Add(range);
            
            if (Parent is IColorize colorize)
            {
                colorize.Recolor();
            }
        }

        internal void UpdateMinMaxBoxes()
        {
            updatingMinMax = true;

            nmMinValueNightmare.Maximum = RtcCore.CurrentPrecision switch
            {
                1 => nmMaxValueNightmare.Maximum = byte.MaxValue,
                2 => nmMaxValueNightmare.Maximum = ushort.MaxValue,
                4 => nmMaxValueNightmare.Maximum = uint.MaxValue,
                8 => nmMaxValueNightmare.Maximum = ulong.MaxValue,
                _ => nmMinValueNightmare.Maximum
            };
            nmMinValueNightmare.Value = NightmareEngine.MinValue;
            nmMaxValueNightmare.Value = NightmareEngine.MaxValue;

            flpRanges.Controls.Clear();
            foreach (var range in NightmareEngine.Ranges)
            {
                AddRange(); // Creates a range that already has the right precision
                RangeControls.Last().Min = range.Min;
                RangeControls.Last().Max = range.Max;
            }

            updatingMinMax = false;
        }

        public void ResyncEngineUI()
        {
            UpdateMinMaxBoxes();
            updatingBlastType = true;
            cbBlastType.SelectedIndex = cbBlastType.Items.IndexOf(NightmareEngine.Algo.ToString());
            updatingBlastType = false;
        }
    }
}
