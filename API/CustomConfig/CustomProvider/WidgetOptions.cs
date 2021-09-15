using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomConfig.CustomProvider
{
    public class WidgetOptions
    {
        public Guid EndpointId { get; set; }

        public string DisplayLabel { get; set; }

        public string WidgetRoute { get; set; }
    }
}
