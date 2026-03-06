using System.Drawing.Drawing2D;
using RTCV.Common;
using RTCV.NetCore;

namespace RTCV.UI
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using CorruptCore;
    using Extensions;
    using RTCV.UI.Components.Controls;

    public static class Colors
    {
        public static Color GeneralColor = Color.LightSteelBlue;

        public static Color Light1Color { get; private set; }
        public static Color Light2Color { get; private set; }
        public static Color NormalColor { get; private set; }
        public static Color Dark1Color { get; private set; }
        public static Color Dark2Color { get; private set; }
        public static Color Dark3Color { get; private set; }
        public static Color Dark4Color { get; private set; }

        public static int CornerRoundness = 5;

        public static void SetRTCColor(Color color, Control ctr)
        {
            HashSet<Control> allControls = new HashSet<Control>();

            if (ctr is Form || ctr is UserControl)
            {
                foreach (var c in ctr.Controls.getControlsWithTag())
                {
                    allControls.Add(c);
                }

                allControls.Add(ctr);
            }
            else if (ctr is Form)
            {
                allControls.Add(ctr);
            }

            foreach (var c in allControls)
            {
                c.Paint -= RoundedPaint;
                if (CornerRoundness <= 0)
                {
                    c.Region = null;
                    continue;
                }
                
                if (    (c is Form f && (!f.TopLevel || !(f.Parent is null)))
                    ||   c is Button
                    ||  (c is Panel p && p.BorderStyle == BorderStyle.None)
                    &&  (!(c is TableLayoutPanel)))
                {
                    c.Paint += RoundedPaint;
                }
            }

            float generalDarken = -0.50f;
            float light1 = 0.10f;
            float light2 = 0.45f;
            float dark1 = -0.20f;
            float dark2 = -0.35f;
            float dark3 = -0.50f;
            float dark4 = -0.85f;

            color = color.ChangeColorBrightness(generalDarken);

            Light1Color = color.ChangeColorBrightness(light1);
            Light2Color = color.ChangeColorBrightness(light2);
            NormalColor = color;
            Dark1Color = color.ChangeColorBrightness(dark1);
            Dark2Color = color.ChangeColorBrightness(dark2);
            Dark3Color = color.ChangeColorBrightness(dark3);
            Dark4Color = color.ChangeColorBrightness(dark4);

            var tag2ColorDico = new Dictionary<string, Color>
            {
                { "color:light2", Light2Color },
                { "color:light1", Light1Color },
                { "color:normal", NormalColor },
                { "color:dark1", Dark1Color },
                { "color:dark2", Dark2Color },
                { "color:dark3", Dark3Color },
                { "color:dark4", Dark4Color }
            };

            foreach (var c in allControls)
            {
                var tag = c.Tag?.ToString().Split(' ');

                if (tag == null || tag.Length == 0)
                {
                    continue;
                }

                //Snag the tag and look for the color.
                var ctag = tag.FirstOrDefault(x => x.Contains("color:"));

                //We didn't find a valid color
                if (ctag == null || !tag2ColorDico.TryGetValue(ctag, out Color _color))
                {
                    continue;
                }

                if (c is Label l && l.BackColor != Color.FromArgb(30, 31, 32))
                {
                    c.ForeColor = _color;
                }
                else
                {
                    c.BackColor = _color;
                }

                if (c is Button btn)
                {
                    btn.FlatAppearance.BorderColor = _color;
                }

                if (c is DataGridView dgv)
                {
                    dgv.BackgroundColor = _color;
                }

                c.Invalidate();
            }

            return;

            void RoundedPaint(object sender, PaintEventArgs pevent)
            {
                Control c = (Control)sender;
                if (c is Form f && f.Parent == null)
                {
                    c.Region = null;
                    return;
                }

                Control parent = c.Parent;
                Control topParent = parent;
                while (topParent.Parent != null)
                    topParent = topParent.Parent;

                Color[] cornerColors = Enumerable.Repeat(parent.BackColor, 4).ToArray();

                int leftOffset = c.Left, topOffset = c.Top, rightOffset = 0, bottomOffset = 0;
                while (parent != null && (cornerColors[0] == Color.Transparent || (leftOffset == 0 && topOffset == 0)))
                {
                    leftOffset += parent.Left;
                    topOffset += parent.Top;
                    cornerColors[0] = parent.BackColor;
                    parent = parent.Parent;
                }
                cornerColors[0] = parent?.BackColor ?? cornerColors[0];

                leftOffset = c.Left;
                topOffset = c.Top;
                rightOffset = bottomOffset = 0;
                parent = c.Parent;
                while (parent != null &&
                       (cornerColors[1] == Color.Transparent || (topOffset == 0 && c.Right + rightOffset == parent.Width)))
                {
                    topOffset += parent.Top;
                    rightOffset += parent.Right;
                    cornerColors[1] = parent.BackColor;
                    parent = parent.Parent;
                }
                cornerColors[1] = parent?.BackColor ?? cornerColors[1];

                leftOffset = c.Left;
                topOffset = c.Top;
                rightOffset = bottomOffset = 0;
                parent = c.Parent;
                while (parent != null && (cornerColors[2] == Color.Transparent ||
                                          (c.Right + rightOffset == parent.Width && c.Bottom + bottomOffset == parent.Height)))
                {
                    rightOffset += parent.Left;
                    bottomOffset += parent.Top;
                    cornerColors[2] = parent.BackColor;
                    parent = parent.Parent;
                }
                cornerColors[2] = parent?.BackColor ?? cornerColors[2];

                leftOffset = c.Left;
                topOffset = c.Top;
                rightOffset = bottomOffset = 0;
                parent = c.Parent;
                while (parent != null &&
                       (cornerColors[3] == Color.Transparent || (leftOffset == 0 && c.Bottom + bottomOffset == parent.Height)))
                {
                    leftOffset += parent.Left;
                    bottomOffset += parent.Top;
                    cornerColors[3] = parent.BackColor;
                    parent = parent.Parent;
                }
                cornerColors[3] = parent?.BackColor ?? cornerColors[3];

                var controlRect = new RectangleF(-1f, -1f, c.ClientSize.Width + 1f, c.ClientSize.Height + 1f);
                int radius = (int)Math.Min(CornerRoundness, Math.Min(controlRect.Width / 2, controlRect.Height / 2));

                using (GraphicsPath pathNW = GetFigurePath(controlRect, radius, 0))
                using (GraphicsPath pathNE = GetFigurePath(controlRect, radius, 1))
                using (GraphicsPath pathSE = GetFigurePath(controlRect, radius, 2))
                using (GraphicsPath pathSW = GetFigurePath(controlRect, radius, 3))
                using (GraphicsPath pathFull = GetFigurePath(controlRect, radius, -1))
                using (Pen pen = new Pen(Color.Magenta, 0))
                {
                    pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    c.Region = new Region(pathFull);
                    pen.Color = cornerColors[0];
                    pevent.Graphics.DrawPath(pen, pathNW);
                    pen.Color = cornerColors[1];
                    pevent.Graphics.DrawPath(pen, pathNE);
                    pen.Color = cornerColors[2];
                    pevent.Graphics.DrawPath(pen, pathSE);
                    pen.Color = cornerColors[3];
                    pevent.Graphics.DrawPath(pen, pathSW);
                }
            }

            GraphicsPath GetFigurePath(RectangleF rect, int radius, int corner)
            {
                GraphicsPath path = new GraphicsPath();
                float curveSize = radius * 2F;
                path.StartFigure();
                float left = rect.Left;
                float top = rect.Top;
                if (corner == 0 || corner == -1)
                    path.AddArc(left, top, curveSize, curveSize, 180, 90);
                if (corner == 1 || corner == -1)
                    path.AddArc(rect.Right - curveSize, top, curveSize, curveSize, 270, 90);
                if (corner == 2 || corner == -1)
                    path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
                if (corner == 3 || corner == -1)
                    path.AddArc(left, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
                if (corner == -1)
                    path.CloseFigure();
                return path;
            }
        }

        public static void SelectRTCColor()
        {
            // Show the color dialog.
            Color color;
            ColorDialog cd = new ColorDialog
            {
                Color = GeneralColor,
                CustomColors = new[]
                    { ColorTranslator.ToOle(GeneralColor), ColorTranslator.ToOle(Color.LightSteelBlue) }
            };
            DialogResult result = cd.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                color = cd.Color;
            }
            else
            {
                return;
            }

            GeneralColor = color;
            S.RecolorRegisteredColorizables();

            SaveRTCColor(color);
        }

        public static void LoadRTCColor()
        {
            if (Params.IsParamSet("COLOR"))
            {
                string[] bytes = Params.ReadParam("COLOR").Split(',');
                GeneralColor = Color.FromArgb(Convert.ToByte(bytes[0]), Convert.ToByte(bytes[1]),
                    Convert.ToByte(bytes[2]));
            }
            else
            {
                GeneralColor = Color.FromArgb(110, 150, 193);
            }

            S.RecolorRegisteredColorizables();
        }

        public static void SaveRTCColor(Color color)
        {
            Params.SetParam("COLOR", $"{color.R},{color.G},{color.B}");
        }
    }
}