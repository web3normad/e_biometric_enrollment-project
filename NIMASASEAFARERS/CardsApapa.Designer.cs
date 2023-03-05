
namespace NIMASASEAFARERS
{
    partial class CardsApapa
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardsApapa));
            this.panel2 = new System.Windows.Forms.Panel();
            this.bunifuLabel1 = new Bunifu.UI.WinForms.BunifuLabel();
            this.comboID = new Bunifu.UI.WinForms.BunifuDropdown();
            this.dockworkersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.iDCard = new NIMASASEAFARERS.IDCard();
            this.dockworkers1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.iDCard1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.iDCard1 = new NIMASASEAFARERS.IDCard();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.DocksApapa1 = new NIMASASEAFARERS.DocksApapa();
            this.dockworkersTableAdapter = new NIMASASEAFARERS.IDCardTableAdapters.DockworkersTableAdapter();
            this.dockworkers1TableAdapter = new NIMASASEAFARERS.IDCardTableAdapters.Dockworkers1TableAdapter();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bunifuLabel1);
            this.panel2.Controls.Add(this.comboID);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1004, 50);
            this.panel2.TabIndex = 2;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // bunifuLabel1
            // 
            this.bunifuLabel1.AllowParentOverrides = false;
            this.bunifuLabel1.AutoEllipsis = false;
            this.bunifuLabel1.CursorType = null;
            this.bunifuLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuLabel1.Location = new System.Drawing.Point(258, 15);
            this.bunifuLabel1.Name = "bunifuLabel1";
            this.bunifuLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel1.Size = new System.Drawing.Size(101, 20);
            this.bunifuLabel1.TabIndex = 2;
            this.bunifuLabel1.Text = "Search ID Card";
            this.bunifuLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel1.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // comboID
            // 
            this.comboID.BackColor = System.Drawing.Color.Transparent;
            this.comboID.BackgroundColor = System.Drawing.Color.White;
            this.comboID.BorderColor = System.Drawing.Color.Silver;
            this.comboID.BorderRadius = 6;
            this.comboID.Color = System.Drawing.Color.Silver;
            this.comboID.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.dockworkersBindingSource, "DockworkerID", true));
            this.comboID.DataSource = this.dockworkers1BindingSource;
            this.comboID.Direction = Bunifu.UI.WinForms.BunifuDropdown.Directions.Down;
            this.comboID.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.comboID.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.comboID.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.comboID.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.comboID.DisabledIndicatorColor = System.Drawing.Color.DarkGray;
            this.comboID.DisplayMember = "DockworkerID";
            this.comboID.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboID.DropdownBorderThickness = Bunifu.UI.WinForms.BunifuDropdown.BorderThickness.Thin;
            this.comboID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboID.DropDownTextAlign = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.comboID.FillDropDown = true;
            this.comboID.FillIndicator = false;
            this.comboID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboID.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboID.ForeColor = System.Drawing.Color.Black;
            this.comboID.FormattingEnabled = true;
            this.comboID.Icon = null;
            this.comboID.IndicatorAlignment = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.comboID.IndicatorColor = System.Drawing.Color.Gray;
            this.comboID.IndicatorLocation = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.comboID.ItemBackColor = System.Drawing.Color.White;
            this.comboID.ItemBorderColor = System.Drawing.Color.White;
            this.comboID.ItemForeColor = System.Drawing.Color.Black;
            this.comboID.ItemHeight = 26;
            this.comboID.ItemHighLightColor = System.Drawing.Color.DodgerBlue;
            this.comboID.ItemHighLightForeColor = System.Drawing.Color.White;
            this.comboID.ItemTopMargin = 3;
            this.comboID.Location = new System.Drawing.Point(372, 10);
            this.comboID.Name = "comboID";
            this.comboID.Size = new System.Drawing.Size(260, 32);
            this.comboID.TabIndex = 1;
            this.comboID.Text = "System.Data.DataViewManagerListItemTypeDescriptor";
            this.comboID.TextAlignment = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.comboID.TextLeftMargin = 5;
            this.comboID.ValueMember = "DockworkerID";
            this.comboID.SelectedIndexChanged += new System.EventHandler(this.comboID_SelectedIndexChanged);
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = 0;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(3, 49);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ReportSource = this.DocksApapa1;
            this.crystalReportViewer1.Size = new System.Drawing.Size(998, 615);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // dockworkersTableAdapter
            // 
            this.dockworkersTableAdapter.ClearBeforeFill = true;
            // 
            // dockworkers1TableAdapter
            // 
            this.dockworkers1TableAdapter.ClearBeforeFill = true;
            // 
            // CardsApapa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "CardsApapa";
            this.Size = new System.Drawing.Size(1004, 669);
            this.Load += new System.EventHandler(this.CardsApapa_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Panel panel2;
        private Bunifu.UI.WinForms.BunifuDropdown comboID;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel1;
        private System.Windows.Forms.BindingSource dockworkersBindingSource;
        private IDCard iDCard;
        private DocksApapa DocksApapa1;
        private IDCardTableAdapters.DockworkersTableAdapter dockworkersTableAdapter;
        private System.Windows.Forms.BindingSource dockworkers1BindingSource;
        private System.Windows.Forms.BindingSource iDCard1BindingSource;
        private IDCard iDCard1;
        private IDCardTableAdapters.Dockworkers1TableAdapter dockworkers1TableAdapter;
    }
}
