using System.Windows.Forms;
using System.Resources;

namespace SystemInformation
{
	/// <summary>
    /// Designer for Computer.
	/// </summary>
    public partial class Computer : SystemInformation.TaskPanelBase
	{

        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());

        public Computer()
        {

            //This call is required by the Windows Form Designer.
            InitializeComponent();

        }

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerTimeUp = new System.Windows.Forms.Timer(this.components);
            this.labelUpTimeDesc = new System.Windows.Forms.Label();
            this.labelBootupStateDesc = new System.Windows.Forms.Label();
            this.labelSystemTypeDesc = new System.Windows.Forms.Label();
            this.labelDescriptionDesc = new System.Windows.Forms.Label();
            this.labelMainboardModelDesc = new System.Windows.Forms.Label();
            this.labelMainboardMfgDesc = new System.Windows.Forms.Label();
            this.labelComputerModelDesc = new System.Windows.Forms.Label();
            this.labelComputerMfgDesc = new System.Windows.Forms.Label();
            this.labelUpTime = new System.Windows.Forms.Label();
            this.labelSystemType = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelMBModel = new System.Windows.Forms.Label();
            this.labelMBManufacturer = new System.Windows.Forms.Label();
            this.labelAvailableVirtual = new System.Windows.Forms.Label();
            this.labelTotalVirtual = new System.Windows.Forms.Label();
            this.labelAvailablePhysical = new System.Windows.Forms.Label();
            this.labelTotalPhysical = new System.Windows.Forms.Label();
            this.labelAVDesc = new System.Windows.Forms.Label();
            this.labelTVDesc = new System.Windows.Forms.Label();
            this.labelAPDesc = new System.Windows.Forms.Label();
            this.labelTPDesc = new System.Windows.Forms.Label();
            this.labelBootupState = new System.Windows.Forms.Label();
            this.labelCompModel = new System.Windows.Forms.Label();
            this.labelCompManufacturer = new System.Windows.Forms.Label();
            this.labelMemory = new System.Windows.Forms.Label();
            this.labelSeparator2 = new System.Windows.Forms.Label();
            this.labelGeneral = new System.Windows.Forms.Label();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.picturePanel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).BeginInit();
            this.SuspendLayout();
            // 
            // timerTimeUp
            // 
            this.timerTimeUp.Interval = 1000;
            this.timerTimeUp.Tick += new System.EventHandler(this.timerTimeUp_Tick);
            // 
            // labelUpTimeDesc
            // 
            this.labelUpTimeDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelUpTimeDesc.ForeColor = System.Drawing.Color.Black;
            this.labelUpTimeDesc.Location = new System.Drawing.Point(13, 244);
            this.labelUpTimeDesc.Name = "labelUpTimeDesc";
            this.labelUpTimeDesc.Size = new System.Drawing.Size(204, 15);
            this.labelUpTimeDesc.TabIndex = 35;
            // 
            // labelBootupStateDesc
            // 
            this.labelBootupStateDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelBootupStateDesc.ForeColor = System.Drawing.Color.Black;
            this.labelBootupStateDesc.Location = new System.Drawing.Point(13, 224);
            this.labelBootupStateDesc.Name = "labelBootupStateDesc";
            this.labelBootupStateDesc.Size = new System.Drawing.Size(204, 15);
            this.labelBootupStateDesc.TabIndex = 34;
            // 
            // labelSystemTypeDesc
            // 
            this.labelSystemTypeDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelSystemTypeDesc.ForeColor = System.Drawing.Color.Black;
            this.labelSystemTypeDesc.Location = new System.Drawing.Point(13, 204);
            this.labelSystemTypeDesc.Name = "labelSystemTypeDesc";
            this.labelSystemTypeDesc.Size = new System.Drawing.Size(204, 15);
            this.labelSystemTypeDesc.TabIndex = 33;
            // 
            // labelDescriptionDesc
            // 
            this.labelDescriptionDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelDescriptionDesc.ForeColor = System.Drawing.Color.Black;
            this.labelDescriptionDesc.Location = new System.Drawing.Point(13, 184);
            this.labelDescriptionDesc.Name = "labelDescriptionDesc";
            this.labelDescriptionDesc.Size = new System.Drawing.Size(204, 15);
            this.labelDescriptionDesc.TabIndex = 32;
            // 
            // labelMainboardModelDesc
            // 
            this.labelMainboardModelDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelMainboardModelDesc.ForeColor = System.Drawing.Color.Black;
            this.labelMainboardModelDesc.Location = new System.Drawing.Point(13, 164);
            this.labelMainboardModelDesc.Name = "labelMainboardModelDesc";
            this.labelMainboardModelDesc.Size = new System.Drawing.Size(204, 15);
            this.labelMainboardModelDesc.TabIndex = 31;
            // 
            // labelMainboardMfgDesc
            // 
            this.labelMainboardMfgDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelMainboardMfgDesc.ForeColor = System.Drawing.Color.Black;
            this.labelMainboardMfgDesc.Location = new System.Drawing.Point(13, 144);
            this.labelMainboardMfgDesc.Name = "labelMainboardMfgDesc";
            this.labelMainboardMfgDesc.Size = new System.Drawing.Size(204, 15);
            this.labelMainboardMfgDesc.TabIndex = 30;
            // 
            // labelComputerModelDesc
            // 
            this.labelComputerModelDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelComputerModelDesc.ForeColor = System.Drawing.Color.Black;
            this.labelComputerModelDesc.Location = new System.Drawing.Point(13, 124);
            this.labelComputerModelDesc.Name = "labelComputerModelDesc";
            this.labelComputerModelDesc.Size = new System.Drawing.Size(204, 15);
            this.labelComputerModelDesc.TabIndex = 29;
            // 
            // labelComputerMfgDesc
            // 
            this.labelComputerMfgDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelComputerMfgDesc.ForeColor = System.Drawing.Color.Black;
            this.labelComputerMfgDesc.Location = new System.Drawing.Point(13, 104);
            this.labelComputerMfgDesc.Name = "labelComputerMfgDesc";
            this.labelComputerMfgDesc.Size = new System.Drawing.Size(204, 15);
            this.labelComputerMfgDesc.TabIndex = 28;
            // 
            // labelUpTime
            // 
            this.labelUpTime.BackColor = System.Drawing.Color.Transparent;
            this.labelUpTime.ForeColor = System.Drawing.Color.Black;
            this.labelUpTime.Location = new System.Drawing.Point(225, 244);
            this.labelUpTime.Name = "labelUpTime";
            this.labelUpTime.Size = new System.Drawing.Size(260, 15);
            this.labelUpTime.TabIndex = 27;
            // 
            // labelSystemType
            // 
            this.labelSystemType.BackColor = System.Drawing.Color.Transparent;
            this.labelSystemType.ForeColor = System.Drawing.Color.Black;
            this.labelSystemType.Location = new System.Drawing.Point(225, 204);
            this.labelSystemType.Name = "labelSystemType";
            this.labelSystemType.Size = new System.Drawing.Size(260, 15);
            this.labelSystemType.TabIndex = 26;
            // 
            // labelDescription
            // 
            this.labelDescription.BackColor = System.Drawing.Color.Transparent;
            this.labelDescription.ForeColor = System.Drawing.Color.Black;
            this.labelDescription.Location = new System.Drawing.Point(225, 184);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(260, 15);
            this.labelDescription.TabIndex = 25;
            // 
            // labelMBModel
            // 
            this.labelMBModel.BackColor = System.Drawing.Color.Transparent;
            this.labelMBModel.ForeColor = System.Drawing.Color.Black;
            this.labelMBModel.Location = new System.Drawing.Point(225, 164);
            this.labelMBModel.Name = "labelMBModel";
            this.labelMBModel.Size = new System.Drawing.Size(260, 15);
            this.labelMBModel.TabIndex = 24;
            // 
            // labelMBManufacturer
            // 
            this.labelMBManufacturer.BackColor = System.Drawing.Color.Transparent;
            this.labelMBManufacturer.ForeColor = System.Drawing.Color.Black;
            this.labelMBManufacturer.Location = new System.Drawing.Point(225, 144);
            this.labelMBManufacturer.Name = "labelMBManufacturer";
            this.labelMBManufacturer.Size = new System.Drawing.Size(260, 15);
            this.labelMBManufacturer.TabIndex = 23;
            // 
            // labelAvailableVirtual
            // 
            this.labelAvailableVirtual.BackColor = System.Drawing.Color.Transparent;
            this.labelAvailableVirtual.ForeColor = System.Drawing.Color.Black;
            this.labelAvailableVirtual.Location = new System.Drawing.Point(225, 359);
            this.labelAvailableVirtual.Name = "labelAvailableVirtual";
            this.labelAvailableVirtual.Size = new System.Drawing.Size(70, 15);
            this.labelAvailableVirtual.TabIndex = 22;
            // 
            // labelTotalVirtual
            // 
            this.labelTotalVirtual.BackColor = System.Drawing.Color.Transparent;
            this.labelTotalVirtual.ForeColor = System.Drawing.Color.Black;
            this.labelTotalVirtual.Location = new System.Drawing.Point(225, 339);
            this.labelTotalVirtual.Name = "labelTotalVirtual";
            this.labelTotalVirtual.Size = new System.Drawing.Size(70, 15);
            this.labelTotalVirtual.TabIndex = 21;
            // 
            // labelAvailablePhysical
            // 
            this.labelAvailablePhysical.BackColor = System.Drawing.Color.Transparent;
            this.labelAvailablePhysical.ForeColor = System.Drawing.Color.Black;
            this.labelAvailablePhysical.Location = new System.Drawing.Point(225, 319);
            this.labelAvailablePhysical.Name = "labelAvailablePhysical";
            this.labelAvailablePhysical.Size = new System.Drawing.Size(70, 15);
            this.labelAvailablePhysical.TabIndex = 20;
            // 
            // labelTotalPhysical
            // 
            this.labelTotalPhysical.BackColor = System.Drawing.Color.Transparent;
            this.labelTotalPhysical.ForeColor = System.Drawing.Color.Black;
            this.labelTotalPhysical.Location = new System.Drawing.Point(225, 299);
            this.labelTotalPhysical.Name = "labelTotalPhysical";
            this.labelTotalPhysical.Size = new System.Drawing.Size(70, 15);
            this.labelTotalPhysical.TabIndex = 19;
            // 
            // labelAVDesc
            // 
            this.labelAVDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelAVDesc.ForeColor = System.Drawing.Color.Black;
            this.labelAVDesc.Location = new System.Drawing.Point(13, 360);
            this.labelAVDesc.Name = "labelAVDesc";
            this.labelAVDesc.Size = new System.Drawing.Size(115, 15);
            this.labelAVDesc.TabIndex = 18;
            // 
            // labelTVDesc
            // 
            this.labelTVDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelTVDesc.ForeColor = System.Drawing.Color.Black;
            this.labelTVDesc.Location = new System.Drawing.Point(13, 340);
            this.labelTVDesc.Name = "labelTVDesc";
            this.labelTVDesc.Size = new System.Drawing.Size(115, 15);
            this.labelTVDesc.TabIndex = 17;
            // 
            // labelAPDesc
            // 
            this.labelAPDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelAPDesc.ForeColor = System.Drawing.Color.Black;
            this.labelAPDesc.Location = new System.Drawing.Point(13, 320);
            this.labelAPDesc.Name = "labelAPDesc";
            this.labelAPDesc.Size = new System.Drawing.Size(115, 15);
            this.labelAPDesc.TabIndex = 16;
            // 
            // labelTPDesc
            // 
            this.labelTPDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelTPDesc.ForeColor = System.Drawing.Color.Black;
            this.labelTPDesc.Location = new System.Drawing.Point(13, 300);
            this.labelTPDesc.Name = "labelTPDesc";
            this.labelTPDesc.Size = new System.Drawing.Size(115, 15);
            this.labelTPDesc.TabIndex = 15;
            // 
            // labelBootupState
            // 
            this.labelBootupState.BackColor = System.Drawing.Color.Transparent;
            this.labelBootupState.ForeColor = System.Drawing.Color.Black;
            this.labelBootupState.Location = new System.Drawing.Point(225, 224);
            this.labelBootupState.Name = "labelBootupState";
            this.labelBootupState.Size = new System.Drawing.Size(260, 15);
            this.labelBootupState.TabIndex = 14;
            // 
            // labelCompModel
            // 
            this.labelCompModel.BackColor = System.Drawing.Color.Transparent;
            this.labelCompModel.ForeColor = System.Drawing.Color.Black;
            this.labelCompModel.Location = new System.Drawing.Point(225, 124);
            this.labelCompModel.Name = "labelCompModel";
            this.labelCompModel.Size = new System.Drawing.Size(260, 15);
            this.labelCompModel.TabIndex = 12;
            // 
            // labelCompManufacturer
            // 
            this.labelCompManufacturer.BackColor = System.Drawing.Color.Transparent;
            this.labelCompManufacturer.ForeColor = System.Drawing.Color.Black;
            this.labelCompManufacturer.Location = new System.Drawing.Point(225, 104);
            this.labelCompManufacturer.Name = "labelCompManufacturer";
            this.labelCompManufacturer.Size = new System.Drawing.Size(260, 15);
            this.labelCompManufacturer.TabIndex = 11;
            // 
            // labelMemory
            // 
            this.labelMemory.AutoSize = true;
            this.labelMemory.BackColor = System.Drawing.Color.Transparent;
            this.labelMemory.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMemory.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelMemory.Location = new System.Drawing.Point(13, 268);
            this.labelMemory.Name = "labelMemory";
            this.labelMemory.Size = new System.Drawing.Size(0, 17);
            this.labelMemory.TabIndex = 10;
            // 
            // labelSeparator2
            // 
            this.labelSeparator2.BackColor = System.Drawing.Color.Black;
            this.labelSeparator2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator2.Location = new System.Drawing.Point(13, 290);
            this.labelSeparator2.Name = "labelSeparator2";
            this.labelSeparator2.Size = new System.Drawing.Size(483, 3);
            this.labelSeparator2.TabIndex = 9;
            // 
            // labelGeneral
            // 
            this.labelGeneral.AutoSize = true;
            this.labelGeneral.BackColor = System.Drawing.Color.Transparent;
            this.labelGeneral.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGeneral.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelGeneral.Location = new System.Drawing.Point(13, 72);
            this.labelGeneral.Name = "labelGeneral";
            this.labelGeneral.Size = new System.Drawing.Size(0, 17);
            this.labelGeneral.TabIndex = 8;
            // 
            // labelSeparator
            // 
            this.labelSeparator.BackColor = System.Drawing.Color.Black;
            this.labelSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator.Location = new System.Drawing.Point(13, 94);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(483, 3);
            this.labelSeparator.TabIndex = 7;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelTitle.Location = new System.Drawing.Point(80, 28);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(0, 25);
            this.labelTitle.TabIndex = 1;
            // 
            // picturePanel
            // 
            this.picturePanel.BackColor = System.Drawing.Color.Transparent;
            this.picturePanel.Image = global::SystemInformation.Properties.Resources.Computer_48x48;
            this.picturePanel.Location = new System.Drawing.Point(16, 16);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(48, 48);
            this.picturePanel.TabIndex = 0;
            this.picturePanel.TabStop = false;
            // 
            // Computer
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.labelUpTimeDesc);
            this.Controls.Add(this.labelBootupStateDesc);
            this.Controls.Add(this.labelSystemTypeDesc);
            this.Controls.Add(this.labelDescriptionDesc);
            this.Controls.Add(this.labelMainboardModelDesc);
            this.Controls.Add(this.labelMainboardMfgDesc);
            this.Controls.Add(this.labelComputerModelDesc);
            this.Controls.Add(this.labelComputerMfgDesc);
            this.Controls.Add(this.labelUpTime);
            this.Controls.Add(this.labelSystemType);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelMBModel);
            this.Controls.Add(this.labelMBManufacturer);
            this.Controls.Add(this.labelAvailableVirtual);
            this.Controls.Add(this.labelTotalVirtual);
            this.Controls.Add(this.labelAvailablePhysical);
            this.Controls.Add(this.labelTotalPhysical);
            this.Controls.Add(this.labelAVDesc);
            this.Controls.Add(this.labelTVDesc);
            this.Controls.Add(this.labelAPDesc);
            this.Controls.Add(this.labelTPDesc);
            this.Controls.Add(this.labelBootupState);
            this.Controls.Add(this.labelCompModel);
            this.Controls.Add(this.labelCompManufacturer);
            this.Controls.Add(this.labelMemory);
            this.Controls.Add(this.labelSeparator2);
            this.Controls.Add(this.labelGeneral);
            this.Controls.Add(this.labelSeparator);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.picturePanel);
            this.Name = "Computer";
            this.Load += new System.EventHandler(this.Computer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        System.Windows.Forms.PictureBox picturePanel;
        System.Windows.Forms.Label labelTitle;
        System.Windows.Forms.Label labelGeneral;
        System.Windows.Forms.Label labelSeparator;
        System.Windows.Forms.Label labelMemory;
        System.Windows.Forms.Label labelSeparator2;
        System.Windows.Forms.Label labelCompManufacturer;
        System.Windows.Forms.Label labelCompModel;
        System.Windows.Forms.Label labelBootupState;
        System.Windows.Forms.Label labelTPDesc;
        System.Windows.Forms.Label labelAPDesc;
        System.Windows.Forms.Label labelTVDesc;
        System.Windows.Forms.Label labelAVDesc;
        System.Windows.Forms.Label labelTotalPhysical;
        System.Windows.Forms.Label labelAvailablePhysical;
        System.Windows.Forms.Label labelTotalVirtual;
        System.Windows.Forms.Label labelAvailableVirtual;
        System.Windows.Forms.Label labelMBModel;
        System.Windows.Forms.Label labelMBManufacturer;
        System.Windows.Forms.Label labelSystemType;
        System.Windows.Forms.Label labelDescription;
        System.Windows.Forms.Label labelUpTime;
        System.Windows.Forms.Timer timerTimeUp;
        Label labelComputerMfgDesc;
        Label labelComputerModelDesc;
        Label labelMainboardMfgDesc;
        Label labelMainboardModelDesc;
        Label labelDescriptionDesc;
        Label labelSystemTypeDesc;
        Label labelBootupStateDesc;
        Label labelUpTimeDesc;

        #endregion
		
	}
	
}
