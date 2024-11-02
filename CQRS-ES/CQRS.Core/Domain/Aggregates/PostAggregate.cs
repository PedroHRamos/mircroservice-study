using Post.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Domain.Aggregates
{
    public class PostAggregate : AggregateRoot
    {
        private bool _isActive;
        private string _author;
        private readonly Dictionary<Guid, Tuple<string, string>> _comments = new Dictionary<Guid, Tuple<string, string>>();

        public bool IsActive
        {
            get => _isActive; set => _isActive = value;
        }

        public PostAggregate() { }

        /*public PostAggregate(Guid id, string author, string message) {
            RaiseEvent(new PostCreatedEvent
            {

            });
        }*/

    }
}
