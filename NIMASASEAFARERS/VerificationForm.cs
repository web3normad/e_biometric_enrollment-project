using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Business_Entities;

namespace NIMASASEAFARERS
{
    /* NOTE: This form is inherited from the CaptureForm,
		so the VisualStudio Form Designer may not load it properly
		(at least until you build the project).
		If you want to make changes in the form layout - do it in the base CaptureForm.
		All changes in the CaptureForm will be reflected in all derived forms 
		(i.e. in the EnrollmentForm and in the VerificationForm)
	*/

    public partial class VerificationForm : Scaning
    {
        public static List<Bus_FingerPrints> lst = new List<Bus_FingerPrints>();

        public static string cnic;

        public delegate void OnTemplateEventHandler(DPFP.Template template);

        public event OnTemplateEventHandler OnTemplate;

        //SqlConnection con = new SqlConnection(DAL.DalConnection.connectionstring);

        public static void variable()
        {
            string UserId = cnic.ToString();
        }

        public void Verify(DPFP.Template template)
        {
            Template = template;
            ShowDialog();
        }

        protected override void loadevents()
        {
            base.loadevents();
            base.Text = "Fingerprint Verification";                 // editing for comment before
            Verificator = new DPFP.Verification.Verification();		// Create a fingerprint template verificator
            UpdateStatus(0);
        }

        DPFP.Processing.Enrollment enrol = new DPFP.Processing.Enrollment();

        protected override void Process(DPFP.Sample Sample)
        {
            base.Process(Sample);

            // Process the sample and create a feature set for the enrollment purpose.
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

            // Check quality of the sample and start verification if it's good
            // TODO: move to a separate task
            if (features != null)
            {
                try
                {
                    DbConnection.checkConnection();
                    lst = getbyet();
                    DPFP.FeatureSet featur;
                    for (int i = 0; i < lst.Count; i++)
                    {

                        Stream s = new MemoryStream(lst[i].Prints);
                        DPFP.Sample sample = new DPFP.Sample(s);
                        featur = ExtractFeatures(sample, DPFP.Processing.DataPurpose.Enrollment);
                        enrol.AddFeatures(featur);
                        if (enrol.TemplateStatus == DPFP.Processing.Enrollment.Status.Ready)
                        {
                            // Compare the feature set with our template
                            DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();
                            Verificator.Verify(features, enrol.Template, ref result);
                            UpdateStatus(result.FARAchieved);
                            if (result.Verified)
                            {
                                cnic = lst[i].CNIC;
                                //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                                Picture.Image = null;

                                this.Close();
                                ProcessID processID = new ProcessID();


                                //ProcessID clocin = new ProcessID();

                                processID.txtStaff_ID.Text = cnic.ToString();

                                processID.BringToFront();
                                processID.ShowDialog();
                                //

                                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                                base.Stop();
                                //this.Stop();
                                
                                break;
                                this.Dispose();
                            }
                            else
                            {
                               
                               cnic = "";
                               MakeReport("No Matches Found.");
                               enrol.Clear();
                                
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private void UpdateStatus(int FAR)
        {
            // Show "False accept rate" value
            SetStatus(String.Format("False Accept Rate (FAR) = {0}\n", FAR));
        }

        private DPFP.Template templateadd()
        {

            lst = getbyet();
            DPFP.FeatureSet features;
            for (int i = 0; i < lst.Count; i++)
            {
                foreach (Byte b in lst[i].Prints)
                {
                    Stream s = new MemoryStream(b);
                    DPFP.Sample sample = new DPFP.Sample(s);
                    features = ExtractFeatures(sample, DPFP.Processing.DataPurpose.Enrollment);
                    enrol.AddFeatures(features);
                }
                cnic = lst[i].CNIC;
            }
            //switch (enrol.TemplateStatus)
            //{
            //    case DPFP.Processing.Enrollment.Status.Ready:	// report success and stop capturing
            //        OnTemplate(enrol.Template);                    
            //        break;

            //    case DPFP.Processing.Enrollment.Status.Failed:	// report failure and restart capturing
            //        enrol.Clear();
            //        OnTemplate(null);
            //        Start();
            //        break;
            //}
            return enrol.Template;

        }

        public List<Bus_FingerPrints> getbyet()
        {
            List<Bus_FingerPrints> lstbyte = null;
            try
            {

                SqlCommand cmd = new SqlCommand("select * from Finger", DbConnection.con);
                if (DbConnection.con.State == ConnectionState.Closed)
                {
                    DbConnection.con.Open();
                }
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lstbyte = new List<Bus_FingerPrints>();
                    do
                    {
                        Bus_FingerPrints bus = new Bus_FingerPrints();
                        bus.Prints = (Byte[])dr["Finger"];
                        bus.CNIC = dr["EmpCNIC"].ToString();

                        //Clock_In clon = new Clock_In();
                        //clon.txtStaff_ID.Text = dr["EmpCNIC"].ToString();

                        lstbyte.Add(bus);
                    } while (dr.Read());
                }
                dr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return lstbyte;
        }

        private DPFP.Template Template;

        private DPFP.Verification.Verification Verificator;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerificationForm));
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // VerificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(490, 426);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VerificationForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.VerificationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void VerificationForm_Load(object sender, EventArgs e)
        {
            base.startcapturing();
            cnic = "";
            MakeReport("No Matches Found.");
            enrol.Clear();
            Picture.Image = null;
        }
        //public VerificationForm()
        //{
        //    InitializeComponent();
        //}
    }
}
