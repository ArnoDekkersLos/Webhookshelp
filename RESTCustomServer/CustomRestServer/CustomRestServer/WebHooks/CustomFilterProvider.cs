using Microsoft.AspNet.WebHooks;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CustomRestServer.WebHooks
{
    public class CustomFilterProvider : IWebHookFilterProvider
    {
        public const string EmployeeChanged = "EmployeeChanged";

        private readonly Collection<WebHookFilter> filters = new Collection<WebHookFilter>
        {
            new WebHookFilter { Name = EmployeeChanged, Description = "An Employee got edited." },
        };

        public Task<Collection<WebHookFilter>> GetFiltersAsync()
        {
            return Task.FromResult(this.filters);
        }
    }
}