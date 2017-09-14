using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.WebHooks
{
    class Registration
    {
        public string WebHookUri { get; set; }
        public string Secret { get; set; }
        public string Description { get; set; }
        public List<string> Filters { get; set; }
    }
}
