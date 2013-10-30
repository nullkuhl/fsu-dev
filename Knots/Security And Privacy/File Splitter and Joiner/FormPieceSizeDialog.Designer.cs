using System.ComponentModel;
using System.Resources;
using System.Windows.Forms;

namespace FileSplitterAndJoiner
{
	partial class FormPieceSizeDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		IContainer components = null;

		/// <summary>
		/// ResourceManager instance
		/// </summary>
		public ResourceManager rm = new ResourceManager("FileSplitterAndJoiner.Resources",
			System.Reflection.Assembly.GetExecutingAssembly());

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent()
		{
			this.btnOK = new Button();
			this.rdbKB = new RadioButton();
			this.rdbMB = new RadioButton();
			this.rdbGB = new RadioButton();
			this.txtPieceSize = new RichTextBox();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(110, 33);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(34, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// rdbKB
			// 
			this.rdbKB.AutoSize = true;
			this.rdbKB.Location = new System.Drawing.Point(12, 12);
			this.rdbKB.Name = "rdbKB";
			this.rdbKB.Size = new System.Drawing.Size(39, 19);
			this.rdbKB.TabIndex = 1;
			this.rdbKB.TabStop = true;
			this.rdbKB.Text = "KB";
			this.rdbKB.UseVisualStyleBackColor = true;
			// 
			// rdbMB
			// 
			this.rdbMB.AutoSize = true;
			this.rdbMB.Checked = true;
			this.rdbMB.Location = new System.Drawing.Point(57, 12);
			this.rdbMB.Name = "rdbMB";
			this.rdbMB.Size = new System.Drawing.Size(43, 19);
			this.rdbMB.TabIndex = 2;
			this.rdbMB.TabStop = true;
			this.rdbMB.Text = "MB";
			this.rdbMB.UseVisualStyleBackColor = true;
			// 
			// rdbGB
			// 
			this.rdbGB.AutoSize = true;
			this.rdbGB.Location = new System.Drawing.Point(104, 12);
			this.rdbGB.Name = "rdbGB";
			this.rdbGB.Size = new System.Drawing.Size(40, 19);
			this.rdbGB.TabIndex = 3;
			this.rdbGB.Text = "GB";
			this.rdbGB.UseVisualStyleBackColor = true;
			// 
			// txtPieceSize
			// 
			this.txtPieceSize.Location = new System.Drawing.Point(12, 35);
			this.txtPieceSize.MaxLength = 14;
			this.txtPieceSize.Multiline = false;
			this.txtPieceSize.Name = "txtPieceSize";
			this.txtPieceSize.Size = new System.Drawing.Size(92, 20);
			this.txtPieceSize.TabIndex = 16;
			this.txtPieceSize.Text = "";
			this.txtPieceSize.KeyDown += new KeyEventHandler(this.txtPieceSize_KeyDown);
			// 
			// frmPieceSizeDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(154, 70);
			this.Controls.Add(this.txtPieceSize);
			this.Controls.Add(this.rdbGB);
			this.Controls.Add(this.rdbMB);
			this.Controls.Add(this.rdbKB);
			this.Controls.Add(this.btnOK);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPieceSizeDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Specify piece size";
			this.Load += new System.EventHandler(this.frmPieceSizeDialog_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		Button btnOK;
		RadioButton rdbKB;
		RadioButton rdbMB;
		RadioButton rdbGB;
		RichTextBox txtPieceSize;
	}
}