using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FileSplitterAndJoiner.Properties;

namespace FileSplitterAndJoiner
{
	/// <summary>
	/// Bottom styling control
	/// </summary>
	public partial class BottomControl : UserControl
	{
		/// <summary>
		/// Contains bottom styling image
		/// </summary>
		public BottomControl()
		{
			InitializeComponent();
			SetImage();
		}

		/// <summary>
		/// Sets BotomBar.Image property to filename correspondent image object from resources
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

			lblBotomBar.Image = image;
		}
	}
}