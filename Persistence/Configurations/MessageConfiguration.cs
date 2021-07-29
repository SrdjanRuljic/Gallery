using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.SenderId).IsRequired();
            builder.Property(x => x.RecipientId).IsRequired();

            builder.Property(x => x.SenderDeleted).HasDefaultValue(false);
            builder.Property(x => x.RecipientDeleted).HasDefaultValue(false);

            builder.HasOne(d => d.Recipient)
                   .WithMany(p => p.MessagesRecived)
                   .HasForeignKey(d => d.RecipientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Sender)
                   .WithMany(p => p.MessagesSent)
                   .HasForeignKey(d => d.SenderId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
