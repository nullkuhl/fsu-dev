using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace SystemInformation
{
	/// <summary>
	/// Designer for Cpu.
	/// </summary>
    public partial class Cpu : SystemInformation.TaskPanelBase
    {

        public ResourceManager rm = new ResourceManager("SystemInformation.Resources",
            System.Reflection.Assembly.GetExecutingAssembly());

        public Cpu()
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
            this.labelPowerManagementCapabilities = new System.Windows.Forms.Label();
            this.labelPowerManagementCapabilitiesDesc = new System.Windows.Forms.Label();
            this.labelPowerManagementSupported = new System.Windows.Forms.Label();
            this.labelProcessorId = new System.Windows.Forms.Label();
            this.labelPowerManagementSupportedDesc = new System.Windows.Forms.Label();
            this.labelProcessorIdDesc = new System.Windows.Forms.Label();
            this.labelNumLogicalProc = new System.Windows.Forms.Label();
            this.labelNumLogicalProcDesc = new System.Windows.Forms.Label();
            this.labelNumberCores = new System.Windows.Forms.Label();
            this.labelProcessorSocket = new System.Windows.Forms.Label();
            this.labelL2CacheSpeed = new System.Windows.Forms.Label();
            this.labelL2CacheSize = new System.Windows.Forms.Label();
            this.labelFSBSpeed = new System.Windows.Forms.Label();
            this.labelProcessorSpeed = new System.Windows.Forms.Label();
            this.labelAddressWidth = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelManufacturer = new System.Windows.Forms.Label();
            this.labelNumCoresDesc = new System.Windows.Forms.Label();
            this.labelProcSockDesc = new System.Windows.Forms.Label();
            this.labelL2CacheSpdDesc = new System.Windows.Forms.Label();
            this.labelL2CacheSzDesc = new System.Windows.Forms.Label();
            this.labelFSBSpdDesc = new System.Windows.Forms.Label();
            this.labelProcSpdDesc = new System.Windows.Forms.Label();
            this.labelAddrWdthDesc = new System.Windows.Forms.Label();
            this.labelDescDesc = new System.Windows.Forms.Label();
            this.labelNameDesc = new System.Windows.Forms.Label();
            this.labelManDesc = new System.Windows.Forms.Label();
            this.labelProcessor = new System.Windows.Forms.Label();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.picturePanel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).BeginInit();
            this.SuspendLayout();
            // 
            // labelPowerManagementCapabilities
            // 
            this.labelPowerManagementCapabilities.BackColor = System.Drawing.Color.Transparent;
            this.labelPowerManagementCapabilities.Location = new System.Drawing.Point(228, 362);
            this.labelPowerManagementCapabilities.Name = "labelPowerManagementCapabilities";
            this.labelPowerManagementCapabilities.Size = new System.Drawing.Size(266, 15);
            this.labelPowerManagementCapabilities.TabIndex = 49;
            this.labelPowerManagementCapabilities.Visible = false;
            // 
            // labelPowerManagementCapabilitiesDesc
            // 
            this.labelPowerManagementCapabilitiesDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelPowerManagementCapabilitiesDesc.Location = new System.Drawing.Point(20, 362);
            this.labelPowerManagementCapabilitiesDesc.Name = "labelPowerManagementCapabilitiesDesc";
            this.labelPowerManagementCapabilitiesDesc.Size = new System.Drawing.Size(203, 15);
            this.labelPowerManagementCapabilitiesDesc.TabIndex = 48;
            this.labelPowerManagementCapabilitiesDesc.Visible = false;
            // 
            // labelPowerManagementSupported
            // 
            this.labelPowerManagementSupported.BackColor = System.Drawing.Color.Transparent;
            this.labelPowerManagementSupported.Location = new System.Drawing.Point(228, 342);
            this.labelPowerManagementSupported.Name = "labelPowerManagementSupported";
            this.labelPowerManagementSupported.Size = new System.Drawing.Size(266, 15);
            this.labelPowerManagementSupported.TabIndex = 47;
            // 
            // labelProcessorId
            // 
            this.labelProcessorId.BackColor = System.Drawing.Color.Transparent;
            this.labelProcessorId.Location = new System.Drawing.Point(228, 322);
            this.labelProcessorId.Name = "labelProcessorId";
            this.labelProcessorId.Size = new System.Drawing.Size(266, 15);
            this.labelProcessorId.TabIndex = 46;
            // 
            // labelPowerManagementSupportedDesc
            // 
            this.labelPowerManagementSupportedDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelPowerManagementSupportedDesc.Location = new System.Drawing.Point(20, 342);
            this.labelPowerManagementSupportedDesc.Name = "labelPowerManagementSupportedDesc";
            this.labelPowerManagementSupportedDesc.Size = new System.Drawing.Size(203, 15);
            this.labelPowerManagementSupportedDesc.TabIndex = 45;
            // 
            // labelProcessorIdDesc
            // 
            this.labelProcessorIdDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelProcessorIdDesc.Location = new System.Drawing.Point(20, 322);
            this.labelProcessorIdDesc.Name = "labelProcessorIdDesc";
            this.labelProcessorIdDesc.Size = new System.Drawing.Size(203, 15);
            this.labelProcessorIdDesc.TabIndex = 44;
            // 
            // labelNumLogicalProc
            // 
            this.labelNumLogicalProc.BackColor = System.Drawing.Color.Transparent;
            this.labelNumLogicalProc.Location = new System.Drawing.Point(228, 303);
            this.labelNumLogicalProc.Name = "labelNumLogicalProc";
            this.labelNumLogicalProc.Size = new System.Drawing.Size(266, 15);
            this.labelNumLogicalProc.TabIndex = 37;
            // 
            // labelNumLogicalProcDesc
            // 
            this.labelNumLogicalProcDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelNumLogicalProcDesc.Location = new System.Drawing.Point(19, 304);
            this.labelNumLogicalProcDesc.Name = "labelNumLogicalProcDesc";
            this.labelNumLogicalProcDesc.Size = new System.Drawing.Size(203, 15);
            this.labelNumLogicalProcDesc.TabIndex = 36;
            this.labelNumLogicalProcDesc.Visible = false;
            // 
            // labelNumberCores
            // 
            this.labelNumberCores.BackColor = System.Drawing.Color.Transparent;
            this.labelNumberCores.Location = new System.Drawing.Point(228, 283);
            this.labelNumberCores.Name = "labelNumberCores";
            this.labelNumberCores.Size = new System.Drawing.Size(266, 15);
            this.labelNumberCores.TabIndex = 35;
            // 
            // labelProcessorSocket
            // 
            this.labelProcessorSocket.BackColor = System.Drawing.Color.Transparent;
            this.labelProcessorSocket.Location = new System.Drawing.Point(228, 264);
            this.labelProcessorSocket.Name = "labelProcessorSocket";
            this.labelProcessorSocket.Size = new System.Drawing.Size(266, 15);
            this.labelProcessorSocket.TabIndex = 34;
            // 
            // labelL2CacheSpeed
            // 
            this.labelL2CacheSpeed.BackColor = System.Drawing.Color.Transparent;
            this.labelL2CacheSpeed.Location = new System.Drawing.Point(228, 244);
            this.labelL2CacheSpeed.Name = "labelL2CacheSpeed";
            this.labelL2CacheSpeed.Size = new System.Drawing.Size(266, 15);
            this.labelL2CacheSpeed.TabIndex = 33;
            // 
            // labelL2CacheSize
            // 
            this.labelL2CacheSize.BackColor = System.Drawing.Color.Transparent;
            this.labelL2CacheSize.Location = new System.Drawing.Point(228, 224);
            this.labelL2CacheSize.Name = "labelL2CacheSize";
            this.labelL2CacheSize.Size = new System.Drawing.Size(266, 15);
            this.labelL2CacheSize.TabIndex = 32;
            // 
            // labelFSBSpeed
            // 
            this.labelFSBSpeed.BackColor = System.Drawing.Color.Transparent;
            this.labelFSBSpeed.Location = new System.Drawing.Point(228, 204);
            this.labelFSBSpeed.Name = "labelFSBSpeed";
            this.labelFSBSpeed.Size = new System.Drawing.Size(266, 15);
            this.labelFSBSpeed.TabIndex = 31;
            // 
            // labelProcessorSpeed
            // 
            this.labelProcessorSpeed.BackColor = System.Drawing.Color.Transparent;
            this.labelProcessorSpeed.Location = new System.Drawing.Point(228, 184);
            this.labelProcessorSpeed.Name = "labelProcessorSpeed";
            this.labelProcessorSpeed.Size = new System.Drawing.Size(266, 15);
            this.labelProcessorSpeed.TabIndex = 30;
            // 
            // labelAddressWidth
            // 
            this.labelAddressWidth.BackColor = System.Drawing.Color.Transparent;
            this.labelAddressWidth.Location = new System.Drawing.Point(228, 164);
            this.labelAddressWidth.Name = "labelAddressWidth";
            this.labelAddressWidth.Size = new System.Drawing.Size(266, 15);
            this.labelAddressWidth.TabIndex = 29;
            // 
            // labelDescription
            // 
            this.labelDescription.BackColor = System.Drawing.Color.Transparent;
            this.labelDescription.Location = new System.Drawing.Point(228, 144);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(266, 15);
            this.labelDescription.TabIndex = 27;
            // 
            // labelName
            // 
            this.labelName.BackColor = System.Drawing.Color.Transparent;
            this.labelName.Location = new System.Drawing.Point(228, 124);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(266, 15);
            this.labelName.TabIndex = 26;
            // 
            // labelManufacturer
            // 
            this.labelManufacturer.BackColor = System.Drawing.Color.Transparent;
            this.labelManufacturer.Location = new System.Drawing.Point(228, 104);
            this.labelManufacturer.Name = "labelManufacturer";
            this.labelManufacturer.Size = new System.Drawing.Size(266, 15);
            this.labelManufacturer.TabIndex = 25;
            // 
            // labelNumCoresDesc
            // 
            this.labelNumCoresDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelNumCoresDesc.Location = new System.Drawing.Point(19, 284);
            this.labelNumCoresDesc.Name = "labelNumCoresDesc";
            this.labelNumCoresDesc.Size = new System.Drawing.Size(203, 15);
            this.labelNumCoresDesc.TabIndex = 24;
            this.labelNumCoresDesc.Visible = false;
            // 
            // labelProcSockDesc
            // 
            this.labelProcSockDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelProcSockDesc.Location = new System.Drawing.Point(19, 264);
            this.labelProcSockDesc.Name = "labelProcSockDesc";
            this.labelProcSockDesc.Size = new System.Drawing.Size(203, 15);
            this.labelProcSockDesc.TabIndex = 23;
            // 
            // labelL2CacheSpdDesc
            // 
            this.labelL2CacheSpdDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelL2CacheSpdDesc.Location = new System.Drawing.Point(19, 244);
            this.labelL2CacheSpdDesc.Name = "labelL2CacheSpdDesc";
            this.labelL2CacheSpdDesc.Size = new System.Drawing.Size(203, 15);
            this.labelL2CacheSpdDesc.TabIndex = 22;
            // 
            // labelL2CacheSzDesc
            // 
            this.labelL2CacheSzDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelL2CacheSzDesc.Location = new System.Drawing.Point(19, 224);
            this.labelL2CacheSzDesc.Name = "labelL2CacheSzDesc";
            this.labelL2CacheSzDesc.Size = new System.Drawing.Size(203, 15);
            this.labelL2CacheSzDesc.TabIndex = 21;
            // 
            // labelFSBSpdDesc
            // 
            this.labelFSBSpdDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelFSBSpdDesc.Location = new System.Drawing.Point(19, 204);
            this.labelFSBSpdDesc.Name = "labelFSBSpdDesc";
            this.labelFSBSpdDesc.Size = new System.Drawing.Size(203, 15);
            this.labelFSBSpdDesc.TabIndex = 20;
            // 
            // labelProcSpdDesc
            // 
            this.labelProcSpdDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelProcSpdDesc.Location = new System.Drawing.Point(19, 184);
            this.labelProcSpdDesc.Name = "labelProcSpdDesc";
            this.labelProcSpdDesc.Size = new System.Drawing.Size(203, 15);
            this.labelProcSpdDesc.TabIndex = 19;
            // 
            // labelAddrWdthDesc
            // 
            this.labelAddrWdthDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelAddrWdthDesc.Location = new System.Drawing.Point(19, 164);
            this.labelAddrWdthDesc.Name = "labelAddrWdthDesc";
            this.labelAddrWdthDesc.Size = new System.Drawing.Size(203, 15);
            this.labelAddrWdthDesc.TabIndex = 18;
            // 
            // labelDescDesc
            // 
            this.labelDescDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelDescDesc.Location = new System.Drawing.Point(19, 144);
            this.labelDescDesc.Name = "labelDescDesc";
            this.labelDescDesc.Size = new System.Drawing.Size(203, 15);
            this.labelDescDesc.TabIndex = 16;
            // 
            // labelNameDesc
            // 
            this.labelNameDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelNameDesc.Location = new System.Drawing.Point(19, 124);
            this.labelNameDesc.Name = "labelNameDesc";
            this.labelNameDesc.Size = new System.Drawing.Size(203, 15);
            this.labelNameDesc.TabIndex = 15;
            // 
            // labelManDesc
            // 
            this.labelManDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelManDesc.Location = new System.Drawing.Point(19, 104);
            this.labelManDesc.Name = "labelManDesc";
            this.labelManDesc.Size = new System.Drawing.Size(203, 15);
            this.labelManDesc.TabIndex = 14;
            // 
            // labelProcessor
            // 
            this.labelProcessor.AutoSize = true;
            this.labelProcessor.BackColor = System.Drawing.Color.Transparent;
            this.labelProcessor.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProcessor.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelProcessor.Location = new System.Drawing.Point(19, 72);
            this.labelProcessor.Name = "labelProcessor";
            this.labelProcessor.Size = new System.Drawing.Size(0, 17);
            this.labelProcessor.TabIndex = 13;
            // 
            // labelSeparator
            // 
            this.labelSeparator.BackColor = System.Drawing.Color.Black;
            this.labelSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSeparator.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSeparator.Location = new System.Drawing.Point(19, 94);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(475, 3);
            this.labelSeparator.TabIndex = 12;
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
            this.labelTitle.TabIndex = 7;
            // 
            // picturePanel
            // 
            this.picturePanel.BackColor = System.Drawing.Color.Transparent;
            this.picturePanel.Image = global::SystemInformation.Properties.Resources.processor48;
            this.picturePanel.Location = new System.Drawing.Point(16, 16);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(48, 48);
            this.picturePanel.TabIndex = 6;
            this.picturePanel.TabStop = false;
            // 
            // Cpu
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.labelPowerManagementCapabilities);
            this.Controls.Add(this.labelPowerManagementCapabilitiesDesc);
            this.Controls.Add(this.labelPowerManagementSupported);
            this.Controls.Add(this.labelProcessorId);
            this.Controls.Add(this.labelPowerManagementSupportedDesc);
            this.Controls.Add(this.labelProcessorIdDesc);
            this.Controls.Add(this.labelNumLogicalProc);
            this.Controls.Add(this.labelNumLogicalProcDesc);
            this.Controls.Add(this.labelNumberCores);
            this.Controls.Add(this.labelProcessorSocket);
            this.Controls.Add(this.labelL2CacheSpeed);
            this.Controls.Add(this.labelL2CacheSize);
            this.Controls.Add(this.labelFSBSpeed);
            this.Controls.Add(this.labelProcessorSpeed);
            this.Controls.Add(this.labelAddressWidth);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelManufacturer);
            this.Controls.Add(this.labelNumCoresDesc);
            this.Controls.Add(this.labelProcSockDesc);
            this.Controls.Add(this.labelL2CacheSpdDesc);
            this.Controls.Add(this.labelL2CacheSzDesc);
            this.Controls.Add(this.labelFSBSpdDesc);
            this.Controls.Add(this.labelProcSpdDesc);
            this.Controls.Add(this.labelAddrWdthDesc);
            this.Controls.Add(this.labelDescDesc);
            this.Controls.Add(this.labelNameDesc);
            this.Controls.Add(this.labelManDesc);
            this.Controls.Add(this.labelProcessor);
            this.Controls.Add(this.labelSeparator);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.picturePanel);
            this.Name = "Cpu";
            this.Load += new System.EventHandler(this.Cpu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        System.Windows.Forms.Label labelTitle;
        System.Windows.Forms.PictureBox picturePanel;
        System.Windows.Forms.Label labelManDesc;
        System.Windows.Forms.Label labelProcessor;
        System.Windows.Forms.Label labelSeparator;
        System.Windows.Forms.Label labelNameDesc;
        System.Windows.Forms.Label labelDescDesc;
        System.Windows.Forms.Label labelAddrWdthDesc;
        System.Windows.Forms.Label labelProcSpdDesc;
        System.Windows.Forms.Label labelFSBSpdDesc;
        System.Windows.Forms.Label labelL2CacheSzDesc;
        System.Windows.Forms.Label labelL2CacheSpdDesc;
        System.Windows.Forms.Label labelProcSockDesc;
        System.Windows.Forms.Label labelNumCoresDesc;
        System.Windows.Forms.Label labelManufacturer;
        System.Windows.Forms.Label labelName;
        System.Windows.Forms.Label labelDescription;
        System.Windows.Forms.Label labelAddressWidth;
        System.Windows.Forms.Label labelProcessorSpeed;
        System.Windows.Forms.Label labelFSBSpeed;
        System.Windows.Forms.Label labelL2CacheSize;
        System.Windows.Forms.Label labelL2CacheSpeed;
        System.Windows.Forms.Label labelProcessorSocket;
        System.Windows.Forms.Label labelNumberCores;
        Label labelNumLogicalProc;
        Label labelNumLogicalProcDesc;

        #endregion
        Label labelPowerManagementCapabilities;
        Label labelPowerManagementCapabilitiesDesc;
        Label labelPowerManagementSupported;
        Label labelProcessorId;
        Label labelPowerManagementSupportedDesc;
        Label labelProcessorIdDesc;
		
	}
	
}
