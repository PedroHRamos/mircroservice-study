using CQRS.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CQRS.Core.Infrastructure
{
    public interface ICommandDispatcher
    {
        void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand;
        Task SendAsync(BaseCommand command);
    }
}
