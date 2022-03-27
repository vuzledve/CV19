using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CV19.Models;
using CV19.ViewModels.Base;

using OxyPlot;
using OxyPlot.Series;

namespace CV19.ViewModels
{
    internal class PlotViewModel : ViewModel
    {
        public PlotModel MyModel { get; private set; }

        public PlotViewModel()
        {
           
            this.MyModel = new PlotModel { Title = "Example 1" };
            this.MyModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));

        }
    }
}
