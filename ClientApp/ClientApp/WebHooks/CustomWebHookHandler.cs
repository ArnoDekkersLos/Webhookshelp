using Microsoft.AspNet.WebHooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.WebHooks
{
    class CustomWebHookHandler : WebHookHandler
    {
        public CustomWebHookHandler()
        {

        }

        public override Task ExecuteAsync(string generator, WebHookHandlerContext context)
        {
            System.Windows.Forms.MessageBox.Show("In handler");

            
            return Task.FromResult(true);
        }
    }
}
