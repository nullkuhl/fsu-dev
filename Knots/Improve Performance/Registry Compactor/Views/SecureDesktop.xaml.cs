using System.ComponentModel;

namespace RegistryCompactor
{
	/// <summary>
	/// Interaction logic for SecureDesktop.xaml
	/// </summary>
	public partial class SecureDesktop
	{
		/// <summary>
		/// <see cref="SecureDesktop"/> constructor
		/// </summary>
		public SecureDesktop()
		{
			InitializeComponent();

			System.Windows.Forms.Cursor.Hide();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			System.Windows.Forms.Cursor.Show();

			base.OnClosing(e);
		}
	}
}