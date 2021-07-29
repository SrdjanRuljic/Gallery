using System;

namespace Domain.Entities
{
    public class Message
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime DateSent { get; set; } = DateTime.Now;
        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }
        public long SenderId { get; set; }
        public long RecipientId { get; set; }

        public User Sender { get; set; }
        public User Recipient { get; set; }
    }
}
