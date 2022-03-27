using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CV19.Models;
using CV19.ViewModels.Base;

using OxyPlot;
using OxyPlot.Series;

namespace CV19.ViewModels.Lesson_1
{
    internal class PlotViewModel : ViewModel
    {
        public PlotViewModel() //setting all the parameters of the model
        {
            this.MyModel = new PlotModel { Title = "Example 1" };
            //MyModel.LegendPosition = LegendPosition.RightBottom;
            //MyModel.LegendPlacement = LegendPlacement.Outside;
            //MyModel.LegendOrientation = LegendOrientation.Horizontal;

            MyModel.Series.Add(GetFunction()); //добавляем функцию 

            var Yaxis = new OxyPlot.Axes.LinearAxis();
            OxyPlot.Axes.LinearAxis XAxis = new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom, Minimum = 0, Maximum = 100 };
            XAxis.Title = "X";
            //Yaxis.Title = "10 * x * x + 11 * x*y*y  + 12*x*y";
            MyModel.Axes.Add(Yaxis);
            MyModel.Axes.Add(XAxis);
           

 
            this.MyModel.Series.Add(new FunctionSeries(x => x*5, 0, 10, 0.1, "cos(x)"));
        }
        public PlotModel MyModel { get; set; }


        //setting the values to the function
        public FunctionSeries GetFunction()
        {
            int n = 100;
            FunctionSeries serie = new FunctionSeries();
            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    //adding the points based x,y
                    OxyPlot.DataPoint data = new OxyPlot.DataPoint(x, getValue(x, y));

                    //adding the point to the serie
                    serie.Points.Add(data);
                }
            }
            //returning the serie
            return serie;
        }

        //your function based on x,y
        public double getValue(int x, int y)
        {
            return (10 * x * x + 11 * x * y * y + 12 * x * y);
        }







        //public PlotViewModel()
        //{
        //    this.Title = "Example 2";
        //    this.Points = new List<OxyPlot.DataPoint>
        //                {
        //                new OxyPlot.DataPoint(0, 4),
        //                new OxyPlot.DataPoint(10, 13),
        //                new OxyPlot.DataPoint(20, 15),
        //                new OxyPlot.DataPoint(30, 16),
        //                new OxyPlot.DataPoint(40, 12),
        //                new OxyPlot.DataPoint(50, 12)
        //                };


        //}


        //public string Title { get; private set; }
        //public IList<OxyPlot.DataPoint> Points { get; private set; }
    }
}
