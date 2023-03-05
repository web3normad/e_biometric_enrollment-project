using System;
using System.Drawing;
using Guna.Charts.WinForms;
using System.Data.SqlClient;

namespace BasicExamples
{
    class RoundedBar
    {
        public static void Example(Guna.Charts.WinForms.GunaChart chart)
        {
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November","December" };

            //Chart configuration
            chart.Misc.BarCornerRadius = 5;
            chart.YAxes.GridLines.Display = false;


            var color = new Guna.Charts.WinForms.GunaBarDataset();
            Color gridColor = Color.FromArgb(214, 219, 224);
            Color foreColor = Color.FromArgb(105, 121, 139);
            Color[] colors = new Color[] { Color.FromArgb(255, 48, 162) };

         



            //Create a new dataset 
            var dataset = new Guna.Charts.WinForms.GunaBarDataset();
            var r = new Random();
            for (int i = 0; i < months.Length; i++)
            {
                //random number
                int num = r.Next(0, 1000);

                dataset.DataPoints.Add(months[i], num);
            }

            //Add a new dataset to a chart.Datasets
            chart.Datasets.Add(dataset);

            //An update was made to re-render the chart
            chart.Update();
        }

        
            

        }
}
