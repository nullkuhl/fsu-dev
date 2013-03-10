using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FreemiumUtilities.TracksEraser.Properties;

namespace FreemiumUtilities.TracksEraser
{
	/// <summary>
	/// Bottom control
	/// </summary>
	public partial class BottomControl : UserControl
	{
		/// <summary>
		/// constructor for BottomControl
		/// </summary>
		public BottomControl()
		{
			InitializeComponent();
			SetImage();
		}

		/// <summary>
		/// change BottomControl image
		/// </summary>
		void SetImage()
		{
			Image image;
			try
			{
				string path = Path.GetDirectoryName(GetType().Assembly.Location) + "\\Skins\\blue\\bottomblue.png";
				image = Resources.bottomBar;
				if (File.Exists(path))
				{
					image = Image.FromFile(path);
				}
			}
			catch (Exception)
			{
				image = Resources.bottomBar;
			}

			if (image == null)
			{
				image = Resources.bottomBar;
			}

			lblMain.Image = image;
		}
	}
}