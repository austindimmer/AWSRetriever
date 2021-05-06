﻿namespace Retriever
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainerFront = new System.Windows.Forms.SplitContainer();
            this.modernShadowPanel1 = new NickAc.ModernUIDoneRight.Controls.ModernShadowPanel();
            this.listViewFound = new System.Windows.Forms.ListView();
            this.ColumnService = new System.Windows.Forms.ColumnHeader();
            this.ColumnType = new System.Windows.Forms.ColumnHeader();
            this.ColumnRegion = new System.Windows.Forms.ColumnHeader();
            this.columnName = new System.Windows.Forms.ColumnHeader();
            this.columnID = new System.Windows.Forms.ColumnHeader();
            this.columnArn = new System.Windows.Forms.ColumnHeader();
            this.contextMenuObjects = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tilePanelReborn2 = new NickAc.ModernUIDoneRight.Controls.TilePanelReborn();
            this.modernShadowPanel2 = new NickAc.ModernUIDoneRight.Controls.ModernShadowPanel();
            this.listViewMessages = new System.Windows.Forms.ListView();
            this.columnProgressTime = new System.Windows.Forms.ColumnHeader();
            this.columnProgressAPI = new System.Windows.Forms.ColumnHeader();
            this.columnProgressService = new System.Windows.Forms.ColumnHeader();
            this.columnProgressRegion = new System.Windows.Forms.ColumnHeader();
            this.columnProgressResult = new System.Windows.Forms.ColumnHeader();
            this.contextMenuMessages = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.viewInProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runAgainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.tilePanelReborn1 = new NickAc.ModernUIDoneRight.Controls.TilePanelReborn();
            this.splitContainerBack = new System.Windows.Forms.SplitContainer();
            this.splitContainerObject = new System.Windows.Forms.SplitContainer();
            this.modernShadowPanel4 = new NickAc.ModernUIDoneRight.Controls.ModernShadowPanel();
            this.richTextBoxCobo = new System.Windows.Forms.RichTextBox();
            this.tilePanelReborn3 = new NickAc.ModernUIDoneRight.Controls.TilePanelReborn();
            this.modernShadowPanel3 = new NickAc.ModernUIDoneRight.Controls.ModernShadowPanel();
            this.propertyGridObject = new System.Windows.Forms.PropertyGrid();
            this.tilePanelReborn4 = new NickAc.ModernUIDoneRight.Controls.TilePanelReborn();
            this.appBar = new NickAc.ModernUIDoneRight.Controls.AppBar();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.sidebarControl = new NickAc.ModernUIDoneRight.Controls.SidebarControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFront)).BeginInit();
            this.splitContainerFront.Panel1.SuspendLayout();
            this.splitContainerFront.Panel2.SuspendLayout();
            this.splitContainerFront.SuspendLayout();
            this.modernShadowPanel1.SuspendLayout();
            this.contextMenuObjects.SuspendLayout();
            this.modernShadowPanel2.SuspendLayout();
            this.contextMenuMessages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBack)).BeginInit();
            this.splitContainerBack.Panel1.SuspendLayout();
            this.splitContainerBack.Panel2.SuspendLayout();
            this.splitContainerBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerObject)).BeginInit();
            this.splitContainerObject.Panel1.SuspendLayout();
            this.splitContainerObject.Panel2.SuspendLayout();
            this.splitContainerObject.SuspendLayout();
            this.modernShadowPanel4.SuspendLayout();
            this.modernShadowPanel3.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerFront
            // 
            this.splitContainerFront.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerFront.Location = new System.Drawing.Point(0, 0);
            this.splitContainerFront.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.splitContainerFront.Name = "splitContainerFront";
            this.splitContainerFront.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerFront.Panel1
            // 
            this.splitContainerFront.Panel1.Controls.Add(this.modernShadowPanel1);
            // 
            // splitContainerFront.Panel2
            // 
            this.splitContainerFront.Panel2.Controls.Add(this.modernShadowPanel2);
            this.splitContainerFront.Size = new System.Drawing.Size(1008, 712);
            this.splitContainerFront.SplitterDistance = 454;
            this.splitContainerFront.SplitterWidth = 6;
            this.splitContainerFront.TabIndex = 3;
            // 
            // modernShadowPanel1
            // 
            this.modernShadowPanel1.Controls.Add(this.listViewFound);
            this.modernShadowPanel1.Controls.Add(this.tilePanelReborn2);
            this.modernShadowPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modernShadowPanel1.Location = new System.Drawing.Point(0, 0);
            this.modernShadowPanel1.Name = "modernShadowPanel1";
            this.modernShadowPanel1.Size = new System.Drawing.Size(1008, 454);
            this.modernShadowPanel1.TabIndex = 2;
            // 
            // listViewFound
            // 
            this.listViewFound.AllowColumnReorder = true;
            this.listViewFound.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnService,
            this.ColumnType,
            this.ColumnRegion,
            this.columnName,
            this.columnID,
            this.columnArn});
            this.listViewFound.ContextMenuStrip = this.contextMenuObjects;
            this.listViewFound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFound.FullRowSelect = true;
            this.listViewFound.HideSelection = false;
            this.listViewFound.Location = new System.Drawing.Point(0, 40);
            this.listViewFound.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.listViewFound.MultiSelect = false;
            this.listViewFound.Name = "listViewFound";
            this.listViewFound.Size = new System.Drawing.Size(1008, 414);
            this.listViewFound.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewFound.TabIndex = 0;
            this.listViewFound.UseCompatibleStateImageBehavior = false;
            this.listViewFound.View = System.Windows.Forms.View.Details;
            this.listViewFound.VirtualMode = true;
            this.listViewFound.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.ListViewFound_RetrieveVirtualItem);
            this.listViewFound.SelectedIndexChanged += new System.EventHandler(this.ListViewFound_SelectedIndexChanged);
            // 
            // ColumnService
            // 
            this.ColumnService.Text = "Service";
            this.ColumnService.Width = 120;
            // 
            // ColumnType
            // 
            this.ColumnType.Text = "Type";
            this.ColumnType.Width = 120;
            // 
            // ColumnRegion
            // 
            this.ColumnRegion.Text = "Region";
            this.ColumnRegion.Width = 120;
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 120;
            // 
            // columnID
            // 
            this.columnID.Text = "ID";
            this.columnID.Width = 120;
            // 
            // columnArn
            // 
            this.columnArn.Text = "ARN";
            this.columnArn.Width = 120;
            // 
            // contextMenuObjects
            // 
            this.contextMenuObjects.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuObjects.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveResultsToolStripMenuItem,
            this.toolStripMenuItem3,
            this.clearToolStripMenuItem});
            this.contextMenuObjects.Name = "contextMenuStripObjects";
            this.contextMenuObjects.Size = new System.Drawing.Size(160, 58);
            // 
            // saveResultsToolStripMenuItem
            // 
            this.saveResultsToolStripMenuItem.Name = "saveResultsToolStripMenuItem";
            this.saveResultsToolStripMenuItem.Size = new System.Drawing.Size(159, 24);
            this.saveResultsToolStripMenuItem.Text = "Save Results";
            this.saveResultsToolStripMenuItem.Click += new System.EventHandler(this.SaveResultsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(156, 6);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(159, 24);
            this.clearToolStripMenuItem.Text = "&Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
            // 
            // tilePanelReborn2
            // 
            this.tilePanelReborn2.BrandedTile = false;
            this.tilePanelReborn2.CanBeHovered = false;
            this.tilePanelReborn2.Checkable = false;
            this.tilePanelReborn2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tilePanelReborn2.Flat = true;
            this.tilePanelReborn2.Image = null;
            this.tilePanelReborn2.Location = new System.Drawing.Point(0, 0);
            this.tilePanelReborn2.Name = "tilePanelReborn2";
            this.tilePanelReborn2.Size = new System.Drawing.Size(1008, 40);
            this.tilePanelReborn2.TabIndex = 1;
            this.tilePanelReborn2.Text = "Objects Found";
            // 
            // modernShadowPanel2
            // 
            this.modernShadowPanel2.Controls.Add(this.listViewMessages);
            this.modernShadowPanel2.Controls.Add(this.tilePanelReborn1);
            this.modernShadowPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modernShadowPanel2.Location = new System.Drawing.Point(0, 0);
            this.modernShadowPanel2.Name = "modernShadowPanel2";
            this.modernShadowPanel2.Size = new System.Drawing.Size(1008, 252);
            this.modernShadowPanel2.TabIndex = 0;
            // 
            // listViewMessages
            // 
            this.listViewMessages.AllowColumnReorder = true;
            this.listViewMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnProgressTime,
            this.columnProgressAPI,
            this.columnProgressService,
            this.columnProgressRegion,
            this.columnProgressResult});
            this.listViewMessages.ContextMenuStrip = this.contextMenuMessages;
            this.listViewMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMessages.FullRowSelect = true;
            this.listViewMessages.GridLines = true;
            this.listViewMessages.HideSelection = false;
            this.listViewMessages.LargeImageList = this.imageList;
            this.listViewMessages.Location = new System.Drawing.Point(0, 40);
            this.listViewMessages.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.listViewMessages.MultiSelect = false;
            this.listViewMessages.Name = "listViewMessages";
            this.listViewMessages.Size = new System.Drawing.Size(1008, 212);
            this.listViewMessages.SmallImageList = this.imageList;
            this.listViewMessages.TabIndex = 0;
            this.listViewMessages.UseCompatibleStateImageBehavior = false;
            this.listViewMessages.View = System.Windows.Forms.View.Details;
            this.listViewMessages.VirtualMode = true;
            this.listViewMessages.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.ListViewMessages_RetrieveVirtualItem);
            this.listViewMessages.SelectedIndexChanged += new System.EventHandler(this.ListViewMessages_SelectedIndexChanged);
            // 
            // columnProgressTime
            // 
            this.columnProgressTime.Text = "Time";
            this.columnProgressTime.Width = 120;
            // 
            // columnProgressAPI
            // 
            this.columnProgressAPI.Text = "API";
            this.columnProgressAPI.Width = 120;
            // 
            // columnProgressService
            // 
            this.columnProgressService.Text = "Service";
            this.columnProgressService.Width = 120;
            // 
            // columnProgressRegion
            // 
            this.columnProgressRegion.Text = "Region";
            this.columnProgressRegion.Width = 120;
            // 
            // columnProgressResult
            // 
            this.columnProgressResult.Text = "Result";
            this.columnProgressResult.Width = 120;
            // 
            // contextMenuMessages
            // 
            this.contextMenuMessages.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuMessages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem1,
            this.toolStripMenuItem2,
            this.viewInProfileToolStripMenuItem,
            this.runAgainToolStripMenuItem});
            this.contextMenuMessages.Name = "contextMenuMessages";
            this.contextMenuMessages.Size = new System.Drawing.Size(174, 82);
            // 
            // clearToolStripMenuItem1
            // 
            this.clearToolStripMenuItem1.Name = "clearToolStripMenuItem1";
            this.clearToolStripMenuItem1.Size = new System.Drawing.Size(173, 24);
            this.clearToolStripMenuItem1.Text = "&Clear";
            this.clearToolStripMenuItem1.Click += new System.EventHandler(this.ClearToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(170, 6);
            // 
            // viewInProfileToolStripMenuItem
            // 
            this.viewInProfileToolStripMenuItem.Name = "viewInProfileToolStripMenuItem";
            this.viewInProfileToolStripMenuItem.Size = new System.Drawing.Size(173, 24);
            this.viewInProfileToolStripMenuItem.Text = "View in Profile";
            this.viewInProfileToolStripMenuItem.Click += new System.EventHandler(this.ViewInProfileToolStripMenuItem_Click);
            // 
            // runAgainToolStripMenuItem
            // 
            this.runAgainToolStripMenuItem.Name = "runAgainToolStripMenuItem";
            this.runAgainToolStripMenuItem.Size = new System.Drawing.Size(173, 24);
            this.runAgainToolStripMenuItem.Text = "Run Again";
            this.runAgainToolStripMenuItem.Click += new System.EventHandler(this.RunAgainToolStripMenuItem_Click);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tilePanelReborn1
            // 
            this.tilePanelReborn1.BrandedTile = false;
            this.tilePanelReborn1.CanBeHovered = false;
            this.tilePanelReborn1.Checkable = false;
            this.tilePanelReborn1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tilePanelReborn1.Flat = true;
            this.tilePanelReborn1.Image = null;
            this.tilePanelReborn1.Location = new System.Drawing.Point(0, 0);
            this.tilePanelReborn1.Name = "tilePanelReborn1";
            this.tilePanelReborn1.Size = new System.Drawing.Size(1008, 40);
            this.tilePanelReborn1.TabIndex = 0;
            this.tilePanelReborn1.Text = "Messages";
            // 
            // splitContainerBack
            // 
            this.splitContainerBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerBack.Location = new System.Drawing.Point(1, 83);
            this.splitContainerBack.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.splitContainerBack.Name = "splitContainerBack";
            // 
            // splitContainerBack.Panel1
            // 
            this.splitContainerBack.Panel1.Controls.Add(this.splitContainerFront);
            // 
            // splitContainerBack.Panel2
            // 
            this.splitContainerBack.Panel2.Controls.Add(this.splitContainerObject);
            this.splitContainerBack.Size = new System.Drawing.Size(1409, 712);
            this.splitContainerBack.SplitterDistance = 1008;
            this.splitContainerBack.SplitterWidth = 6;
            this.splitContainerBack.TabIndex = 4;
            // 
            // splitContainerObject
            // 
            this.splitContainerObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerObject.Location = new System.Drawing.Point(0, 0);
            this.splitContainerObject.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.splitContainerObject.Name = "splitContainerObject";
            this.splitContainerObject.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerObject.Panel1
            // 
            this.splitContainerObject.Panel1.Controls.Add(this.modernShadowPanel4);
            // 
            // splitContainerObject.Panel2
            // 
            this.splitContainerObject.Panel2.Controls.Add(this.modernShadowPanel3);
            this.splitContainerObject.Size = new System.Drawing.Size(395, 712);
            this.splitContainerObject.SplitterDistance = 350;
            this.splitContainerObject.SplitterWidth = 6;
            this.splitContainerObject.TabIndex = 3;
            // 
            // modernShadowPanel4
            // 
            this.modernShadowPanel4.Controls.Add(this.richTextBoxCobo);
            this.modernShadowPanel4.Controls.Add(this.tilePanelReborn3);
            this.modernShadowPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modernShadowPanel4.Location = new System.Drawing.Point(0, 0);
            this.modernShadowPanel4.Name = "modernShadowPanel4";
            this.modernShadowPanel4.Size = new System.Drawing.Size(395, 350);
            this.modernShadowPanel4.TabIndex = 0;
            // 
            // richTextBoxCobo
            // 
            this.richTextBoxCobo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxCobo.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.richTextBoxCobo.Location = new System.Drawing.Point(0, 34);
            this.richTextBoxCobo.Name = "richTextBoxCobo";
            this.richTextBoxCobo.Size = new System.Drawing.Size(395, 316);
            this.richTextBoxCobo.TabIndex = 1;
            this.richTextBoxCobo.Text = "";
            // 
            // tilePanelReborn3
            // 
            this.tilePanelReborn3.BrandedTile = false;
            this.tilePanelReborn3.CanBeHovered = false;
            this.tilePanelReborn3.Checkable = false;
            this.tilePanelReborn3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tilePanelReborn3.Flat = true;
            this.tilePanelReborn3.Image = null;
            this.tilePanelReborn3.Location = new System.Drawing.Point(0, 0);
            this.tilePanelReborn3.Name = "tilePanelReborn3";
            this.tilePanelReborn3.Size = new System.Drawing.Size(395, 34);
            this.tilePanelReborn3.TabIndex = 0;
            this.tilePanelReborn3.Text = "Object";
            // 
            // modernShadowPanel3
            // 
            this.modernShadowPanel3.Controls.Add(this.propertyGridObject);
            this.modernShadowPanel3.Controls.Add(this.tilePanelReborn4);
            this.modernShadowPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modernShadowPanel3.Location = new System.Drawing.Point(0, 0);
            this.modernShadowPanel3.Name = "modernShadowPanel3";
            this.modernShadowPanel3.Size = new System.Drawing.Size(395, 356);
            this.modernShadowPanel3.TabIndex = 0;
            // 
            // propertyGridObject
            // 
            this.propertyGridObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridObject.Location = new System.Drawing.Point(0, 35);
            this.propertyGridObject.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.propertyGridObject.Name = "propertyGridObject";
            this.propertyGridObject.Size = new System.Drawing.Size(395, 321);
            this.propertyGridObject.TabIndex = 3;
            // 
            // tilePanelReborn4
            // 
            this.tilePanelReborn4.BrandedTile = false;
            this.tilePanelReborn4.CanBeHovered = false;
            this.tilePanelReborn4.Checkable = false;
            this.tilePanelReborn4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tilePanelReborn4.Flat = true;
            this.tilePanelReborn4.Image = null;
            this.tilePanelReborn4.Location = new System.Drawing.Point(0, 0);
            this.tilePanelReborn4.Name = "tilePanelReborn4";
            this.tilePanelReborn4.Size = new System.Drawing.Size(395, 35);
            this.tilePanelReborn4.TabIndex = 0;
            this.tilePanelReborn4.Text = "Properties";
            // 
            // appBar
            // 
            this.appBar.CastShadow = true;
            this.appBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.appBar.HamburgerButtonSize = 32;
            this.appBar.IconVisible = false;
            this.appBar.Location = new System.Drawing.Point(1, 33);
            this.appBar.Name = "appBar";
            this.appBar.OverrideParentText = false;
            this.appBar.Size = new System.Drawing.Size(1409, 50);
            this.appBar.TabIndex = 5;
            this.appBar.Text = "AWS Retriever";
            this.appBar.TextFont = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.appBar.ToolTip = null;
            // 
            // panelStatus
            // 
            this.panelStatus.Controls.Add(this.statusLabel);
            this.panelStatus.Controls.Add(this.progressBar);
            this.panelStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatus.Location = new System.Drawing.Point(1, 795);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(1409, 37);
            this.panelStatus.TabIndex = 7;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(6, 7);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(65, 28);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "label1";
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.progressBar.Location = new System.Drawing.Point(816, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(593, 37);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 0;
            // 
            // sidebarControl
            // 
            this.sidebarControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sidebarControl.ContextMenuStrip = this.contextMenuMessages;
            this.sidebarControl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sidebarControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarControl.IsClosed = true;
            this.sidebarControl.Location = new System.Drawing.Point(1, 83);
            this.sidebarControl.Name = "sidebarControl";
            this.sidebarControl.Size = new System.Drawing.Size(258, 712);
            this.sidebarControl.TabIndex = 6;
            this.sidebarControl.Text = "sidebarControl";
            this.sidebarControl.TopBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.sidebarControl.TopBarSize = 10;
            this.sidebarControl.TopBarSpacing = 32;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1411, 833);
            this.ColorScheme.MouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(64)))), ((int)(((byte)(101)))));
            this.ColorScheme.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.ColorScheme.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.ColorScheme.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.Controls.Add(this.sidebarControl);
            this.Controls.Add(this.splitContainerBack);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.appBar);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MinimumSize = new System.Drawing.Size(108, 45);
            this.Name = "FormMain";
            this.Text = "AWS Retriever";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.splitContainerFront.Panel1.ResumeLayout(false);
            this.splitContainerFront.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFront)).EndInit();
            this.splitContainerFront.ResumeLayout(false);
            this.modernShadowPanel1.ResumeLayout(false);
            this.contextMenuObjects.ResumeLayout(false);
            this.modernShadowPanel2.ResumeLayout(false);
            this.contextMenuMessages.ResumeLayout(false);
            this.splitContainerBack.Panel1.ResumeLayout(false);
            this.splitContainerBack.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBack)).EndInit();
            this.splitContainerBack.ResumeLayout(false);
            this.splitContainerObject.Panel1.ResumeLayout(false);
            this.splitContainerObject.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerObject)).EndInit();
            this.splitContainerObject.ResumeLayout(false);
            this.modernShadowPanel4.ResumeLayout(false);
            this.modernShadowPanel3.ResumeLayout(false);
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainerFront;
        private System.Windows.Forms.ContextMenuStrip contextMenuObjects;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.SplitContainer splitContainerBack;
        private System.Windows.Forms.ListView listViewFound;
        private System.Windows.Forms.ListView listViewMessages;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ColumnHeader columnProgressAPI;
        private System.Windows.Forms.ColumnHeader columnProgressService;
        private System.Windows.Forms.ColumnHeader columnProgressRegion;
        private System.Windows.Forms.ColumnHeader columnProgressResult;
        private System.Windows.Forms.ColumnHeader ColumnType;
        private System.Windows.Forms.ColumnHeader ColumnService;
        private System.Windows.Forms.ColumnHeader ColumnRegion;
        private System.Windows.Forms.ContextMenuStrip contextMenuMessages;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnID;
        private System.Windows.Forms.ColumnHeader columnArn;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem1;
        private System.Windows.Forms.ColumnHeader columnProgressTime;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem viewInProfileToolStripMenuItem;
        private NickAc.ModernUIDoneRight.Controls.AppBar appBar;
        private NickAc.ModernUIDoneRight.Controls.SidebarControl sidebarControl;
        private System.Windows.Forms.Panel panelStatus;
        private NickAc.ModernUIDoneRight.Controls.TilePanelReborn tilePanelReborn2;
        private System.Windows.Forms.SplitContainer splitContainerObject;
        private NickAc.ModernUIDoneRight.Controls.ModernShadowPanel modernShadowPanel1;
        private NickAc.ModernUIDoneRight.Controls.ModernShadowPanel modernShadowPanel2;
        private NickAc.ModernUIDoneRight.Controls.TilePanelReborn tilePanelReborn1;
        private NickAc.ModernUIDoneRight.Controls.ModernShadowPanel modernShadowPanel4;
        private NickAc.ModernUIDoneRight.Controls.TilePanelReborn tilePanelReborn3;
        private NickAc.ModernUIDoneRight.Controls.ModernShadowPanel modernShadowPanel3;
        private NickAc.ModernUIDoneRight.Controls.TilePanelReborn tilePanelReborn4;
        private System.Windows.Forms.RichTextBox richTextBoxCobo;
        private System.Windows.Forms.PropertyGrid propertyGridObject;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ToolStripMenuItem runAgainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveResultsToolStripMenuItem;
    }
}

