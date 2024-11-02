using CQRS.Core.Messages;
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

        public PostAggregate(Guid id, string author, string message) {
            RaiseEvent(new PostCreatedEvent
            {
                Id = id,
                Author = author,
                Message = message,
                DatePosted = DateTime.UtcNow
            });
        }

        public void Apply(PostCreatedEvent @event)
        {
            _id = @event.Id;
            _author = @event.Author;
            _isActive = true;
        }

        public void EditMessage(string message)
        {
            if (_isActive is false)
                throw new InvalidOperationException("You cannot edit a message of an inactive post.");

            if (string.IsNullOrWhiteSpace(message))
                throw new InvalidOperationException($"The value of {nameof(message)} cannot be null or empty. Please provide a valid {nameof(message)}.");

            RaiseEvent(new MessageUpdatedEvent
            {
                Id = _id,
                Message = message,
            });
        }

        public void Apply(MessageUpdatedEvent @event)
        {
            _id = @event.Id;
        }

        public void LikePost()
        {
            if (_isActive is false)
                throw new InvalidOperationException("You cannot like an inactive post.");

            RaiseEvent(new PostLikedEvent
            {
                Id = _id
            });
        }

        public void Apply(PostLikedEvent @event)
        {
            _id = @event.Id;
        }

        public void AddComment(string comment, string username)
        {
            if (_isActive is false)
                throw new InvalidOperationException("You cannot add a comment to an inactive post.");

            if (string.IsNullOrWhiteSpace(comment))
                throw new InvalidOperationException($"The value of {nameof(comment)} cannot be null or empty. Please provide a valid {nameof(comment)}.");

            RaiseEvent(new CommentAddedEvent
            {
                Id = _id,
                CommentId = Guid.NewGuid(),
                Comment = comment,
                UserName = username,
                CommentDate = DateTime.Now
            });

        }

        public void Apply(CommentAddedEvent @event)
        {
            _id = @event.Id;
            _comments.Add(@event.CommentId, new Tuple<string, string>(@event.Comment, @event.UserName));
        }

    }
}
