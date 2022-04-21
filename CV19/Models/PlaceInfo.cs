using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.Models
{
    internal class PlaceInfo
    {
        public string Name { get; set; }
        public virtual Point Location { get; set; }

        public IEnumerable<ConfirmedCount> Counts { get; set; }
    }
}
