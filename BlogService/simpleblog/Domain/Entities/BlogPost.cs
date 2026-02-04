using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BlogPost
    {
        public Guid Id{ get; set; }
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Title cannot be null or empty.");
                }
                _title = value;
            }
        }
        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Content cannot be null or empty.");
                }
                _content = value;
            }
        }
        public DateTime CreatedAt { get; }
        public BlogPost(Guid id,string title, string content, DateTime createat)
        {
            Id = id;
            Title = title;
            Content = content;
            CreatedAt = createat;
        }
        public BlogPost(string title, string content) : this(Guid.NewGuid(), title, content, DateTime.Now)
        {
        }
    }
}
