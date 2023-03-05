
namespace NIMASASEAFARERS
{
    partial class IdCardControl
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
            this.content = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnWarri = new MaterialSkin.Controls.MaterialFlatButton();
            this.btnCal = new MaterialSkin.Controls.MaterialFlatButton();
            this.btnPh = new MaterialSkin.Controls.MaterialFlatButton();
            this.btnApapa = new MaterialSkin.Controls.MaterialFlatButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // content
            // 
            this.content.ForeColor = System.Drawing.Color.Coral;
            this.content.Location = new System.Drawing.Point(3, 41);
            this.content.Name = "content";
            this.content.Size = new System.Drawing.Size(1101, 625);
            this.content.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnWarri);
            this.panel1.Controls.Add(this.btnCal);
            this.panel1.Controls.Add(this.btnPh);
            this.panel1.Controls.Add(this.btnApapa);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1104, 42);
            this.panel1.TabIndex = 1;
            // 
            // btnWarri
            // 
            this.btnWarri.AutoSize = true;
            this.btnWarri.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnWarri.Depth = 0;
            this.btnWarri.Icon = null;
            this.btnWarri.Location = new System.Drawing.Point(679, 2);
            this.btnWarri.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnWarri.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnWarri.Name = "btnWarri";
            this.btnWarri.Primary = false;
            this.btnWarri.Size = new System.Drawing.Size(157, 36);
            this.btnWarri.TabIndex = 3;
            this.btnWarri.Text = "Warri Port ID Card";
            this.btnWarri.UseVisualStyleBackColor = true;
            this.btnWarri.Click += new System.EventHandler(this.btnWarri_Click);
            // 
            // btnCal
            // 
            this.btnCal.AutoSize = true;
            this.btnCal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCal.Depth = 0;
            this.btnCal.Icon = null;
            this.btnCal.Location = new System.Drawing.Point(490, 2);
            this.btnCal.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCal.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCal.Name = "btnCal";
            this.btnCal.Primary = false;
            this.btnCal.Size = new System.Drawing.Size(176, 36);
            this.btnCal.TabIndex = 2;
            this.btnCal.Text = "Calabar Port ID Card";
            this.btnCal.UseVisualStyleBackColor = true;
            this.btnCal.Click += new System.EventHandler(this.btnCal_Click);
            // 
            // btnPh
            // 
            this.btnPh.AutoSize = true;
            this.btnPh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPh.Depth = 0;
            this.btnPh.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPh.Icon = null;
            this.btnPh.Location = new System.Drawing.Point(260, 3);
            this.btnPh.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnPh.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPh.Name = "btnPh";
            this.btnPh.Primary = false;
            this.btnPh.Size = new System.Drawing.Size(222, 36);
            this.btnPh.TabIndex = 1;
            this.btnPh.Text = "PortHarcourt Port ID Card";
            this.btnPh.UseVisualStyleBackColor = true;
            this.btnPh.Click += new System.EventHandler(this.btnPh_Click);
            // 
            // btnApapa
            // 
            this.btnApapa.AutoSize = true;
            this.btnApapa.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnApapa.Depth = 0;
            this.btnApapa.Icon = null;
            this.btnApapa.Location = new System.Drawing.Point(71, 3);
            this.btnApapa.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnApapa.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnApapa.Name = "btnApapa";
            this.btnApapa.Primary = false;
            this.btnApapa.Size = new System.Drawing.Size(160, 36);
            this.btnApapa.TabIndex = 0;
            this.btnApapa.Text = "Apapa Port ID Card";
            this.btnApapa.UseVisualStyleBackColor = true;
            this.btnApapa.Click += new System.EventHandler(this.btnApapa_Click);
            // 
            // IdCardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.content);
            this.Name = "IdCardControl";
            this.Size = new System.Drawing.Size(1104, 669);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel content;
        private System.Windows.Forms.Panel panel1;
        private MaterialSkin.Controls.MaterialFlatButton btnWarri;
        private MaterialSkin.Controls.MaterialFlatButton btnCal;
        private MaterialSkin.Controls.MaterialFlatButton btnPh;
        private MaterialSkin.Controls.MaterialFlatButton btnApapa;
    }
}
