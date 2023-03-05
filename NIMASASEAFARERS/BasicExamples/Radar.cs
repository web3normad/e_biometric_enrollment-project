﻿using System;
using System.Drawing;
using Guna.Charts.WinForms;

namespace BasicExamples
{
    class Radar
    {
        public static void Example(Guna.Charts.WinForms.GunaChart chart)
        {
            string[] months = { "January", "February", "March", "April" };

            //Chart configuration  
            chart.Legend.Position = Guna.Charts.WinForms.LegendPosition.Right;
            chart.XAxes.Display = false;
            chart.YAxes.Display = false;

            //Create a new dataset 
            var dataset = new Guna.Charts.WinForms.GunaRadarDataset();
            dataset.BorderWidth = 2;
            dataset.PointStyle = PointStyle.Circle;
            var r = new Random();
            for (int i = 0; i < months.Length; i++)
            {
                //random number
                int num = r.Next(10, 100);

                dataset.DataPoints.Add(months[i], num);
            }

            //Add a new dataset to a chart.Datasets
            chart.Datasets.Add(dataset);

            //An update was made to re-render the chart
            chart.Update();
        }

    }
}