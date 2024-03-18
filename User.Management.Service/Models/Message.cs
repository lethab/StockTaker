using MimeKit;
using System;
using System.Collections.Generic;

namespace User.Management.Service.Models
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            foreach (var email in to)
            {
                var addressParts = email.Split('@');
                var name = addressParts.Length > 1 ? null : addressParts[0]; 

                var mailboxAddress = new MailboxAddress(name, email);
                To.Add(mailboxAddress);
            }
            Subject = subject;
            Content = content;
        }
    }
}
