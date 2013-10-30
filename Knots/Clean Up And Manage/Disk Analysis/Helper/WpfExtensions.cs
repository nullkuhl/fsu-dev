using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using IWin32Window = System.Windows.Forms.IWin32Window;

namespace DiskAnalysis.Helper
{
    /// <summary>
    /// WPF Extensions - Utility methods
    /// </summary>
    public static class WpfExtensions
    {
        /// <summary>
        /// Interop method to handle Winforms controls
        /// </summary>
        /// <param name="visual"></param>
        /// <returns></returns>
        public static IWin32Window GetIWin32Window(this Visual visual)
        {
            HwndSource source = PresentationSource.FromVisual(visual) as HwndSource;
            if (source != null)
            {
                IWin32Window win = new OldWindow(source.Handle);
                return win;
            }
            return null;
        }

        /// <summary>
        /// Utility method to retrieve a visual element from a visual tree
        /// </summary>
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                var v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null) child = GetVisualChild<T>(v);
                if (child != null) break;
            }
            return child;
        }
    }

    /// <summary>
    /// Helper class to access Winforms controls
    /// </summary>
    public class OldWindow : IWin32Window
    {
        readonly IntPtr handle;

        /// <summary>
        /// Old window handle
        /// </summary>
        /// <param name="handle">Window handle</param>
        public OldWindow(IntPtr handle)
        {
            this.handle = handle;
        }

        #region IWin32Window Members

        IntPtr IWin32Window.Handle
        {
            get { return handle; }
        }

        #endregion
    }
}