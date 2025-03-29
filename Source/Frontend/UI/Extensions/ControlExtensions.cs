using System.Reflection;

namespace RTCV.UI.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public static class ControlExtensions
    {
        private const int SRCCOPY = 0xCC0020;

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [DllImport("gdi32.dll")]
        private static extern int BitBlt(IntPtr hdc, int x, int y, int cx, int cy, IntPtr hdcSrc, int x1, int y1, int rop);

        internal static Bitmap getFormScreenShot(this Control con)
        {
            logger.Trace($"getFormScreenShot ClientRectangle | Width: {con.ClientRectangle.Width} | Height: {con.ClientRectangle.Height} | X: {con.ClientRectangle.X} | Y: {con.ClientRectangle.Y}");
            try
            {
                var bmp = new Bitmap(con.ClientRectangle.Width, con.ClientRectangle.Height);
                using (var bmpGraphics = Graphics.FromImage(bmp))
                {
                    var bmpDC = bmpGraphics.GetHdc();
                    using (var formGraphics = Graphics.FromHwnd(con.Handle))
                    {
                        var formDC = formGraphics.GetHdc();
                        BitBlt(bmpDC, 0, 0, con.ClientRectangle.Width, con.ClientRectangle.Height, formDC, 0, 0, SRCCOPY);
                        formGraphics.ReleaseHdc(formDC);
                    }

                    bmpGraphics.ReleaseHdc(bmpDC);
                }
                return bmp;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Failed to get form screenshot.");
                return new Bitmap(1, 1);
            }
        }

        internal static List<Control> getControlsWithTag(this Control.ControlCollection controls)
        {
            var allControls = new List<Control>();

            foreach (Control c in controls)
            {
                if (c.Tag != null)
                {
                    allControls.Add(c);
                }

                if (c.HasChildren)
                {
                    allControls.AddRange(c.Controls.getControlsWithTag()); //Recursively check all children controls as well; ie groupboxes or tabpages
                }
            }

            return allControls;
        }
        
        internal static bool SetAnyPadding(this Control control, Padding padding)
        {
            if (control.Padding == padding)
            {
                return true;
            }

            try
            {
                Padding pad = new Padding(-5, -5, -5, -5);
                Type iArrangedElement = Type.GetType("System.Windows.Forms.Layout.IArrangedElement, System.Windows.Forms");
                Type propertyStore = Type.GetType("System.Windows.Forms.PropertyStore, System.Windows.Forms");
                Type commonProperties = Type.GetType("System.Windows.Forms.Layout.CommonProperties, System.Windows.Forms");
                PropertyInfo properties = iArrangedElement.GetProperties()[3];
                MethodInfo setPadding = propertyStore.GetMethod("SetPadding");
                FieldInfo paddingProperty = commonProperties.GetField("_paddingProperty", BindingFlags.NonPublic | BindingFlags.Static);
                int paddingPropertyVal = (int)paddingProperty.GetValue(null);
                object o = properties.GetValue(control);
                setPadding.Invoke(o, new object[] { paddingPropertyVal, pad });
                return true;
            }
            catch
            {
                //cry
                control.Padding = new Padding(Math.Max(padding.Left, 0), Math.Max(padding.Top, 0), Math.Max(padding.Right, 0), Math.Max(padding.Bottom, 0));
                return false;
            }
        }
    }
}
