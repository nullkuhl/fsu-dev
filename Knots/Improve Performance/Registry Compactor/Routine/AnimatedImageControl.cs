using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using Brush = System.Drawing.Brush;
using Image = System.Drawing.Image;
using Point = System.Windows.Point;

namespace RegistryCompactor
{
	internal class AnimatedImageControl : UserControl
	{
		readonly Image mAnimatedImage;
		readonly Brush mBrush;
		readonly HwndSource mHwnd;
		Point mPoint;
		Rectangle mRectangle;

		/// <summary>
		/// AnimatedImageControl constructor
		/// </summary>
		/// <param name="pWindow">Window</param>
		/// <param name="pBitmap">Bitmap</param>
		/// <param name="pBrush">Brush</param>
		public AnimatedImageControl(Window pWindow, Bitmap pBitmap, Brush pBrush)
		{
			mAnimatedImage = pBitmap;
			mHwnd = PresentationSource.FromVisual(pWindow) as HwndSource;
			mBrush = pBrush;
			Width = pBitmap.Width;
			Height = pBitmap.Height;
			mPoint = pWindow.PointToScreen(new Point(0, 0));

			ImageAnimator.Animate(mAnimatedImage, OnFrameChanged);

			Loaded += AnimatedImageControl_Loaded;
		}

		void AnimatedImageControl_Loaded(object sender, RoutedEventArgs e)
		{
			Point point = PointToScreen(new Point(0, 0));
			mRectangle = new Rectangle((int) (point.X - mPoint.X), (int) (point.Y - mPoint.Y), (int) Width, (int) Height);
		}

		void OnFrameChangedTS()
		{
			InvalidateVisual();
		}

		void OnFrameChanged(object o, EventArgs e)
		{
			Dispatcher.BeginInvoke(DispatcherPriority.Normal, new OnFrameChangedDelegate(OnFrameChangedTS));
		}

		protected override void OnRender(DrawingContext pDrawingContext)
		{
			//Get the next frame ready for rendering.
			ImageAnimator.UpdateFrames(mAnimatedImage);

			//Draw the next frame in the animation.
			Graphics gr = Graphics.FromHwnd(mHwnd.Handle);
			gr.FillRectangle(mBrush, mRectangle);
			gr.DrawImage(mAnimatedImage, mRectangle);
		}

		#region Nested type: OnFrameChangedDelegate

		delegate void OnFrameChangedDelegate();

		#endregion
	}
}