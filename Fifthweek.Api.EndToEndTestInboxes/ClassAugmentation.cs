using System;
using System.Linq;

//// Generated on 13/02/2015 21:40:52 (UTC)
//// Mapped solution in 6.39s


namespace Fifthweek.Api.EndToEndTestMailboxes.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;

    public partial class TryGetLatestMessageQuery 
    {
        public TryGetLatestMessageQuery(
            Fifthweek.Api.EndToEndTestMailboxes.Shared.MailboxName mailboxName)
        {
            if (mailboxName == null)
            {
                throw new ArgumentNullException("mailboxName");
            }

            this.MailboxName = mailboxName;
        }
    }
}
namespace Fifthweek.Api.EndToEndTestMailboxes.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;

    public partial class Message 
    {
        public Message(
            System.String subject,
            System.String body)
        {
            if (subject == null)
            {
                throw new ArgumentNullException("subject");
            }

            if (body == null)
            {
                throw new ArgumentNullException("body");
            }

            this.Subject = subject;
            this.Body = body;
        }
    }
}
namespace Fifthweek.Api.EndToEndTestMailboxes.Controllers
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.EndToEndTestMailboxes.Commands;
    using Fifthweek.Api.EndToEndTestMailboxes.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;

    public partial class EndToEndTestInboxController 
    {
        public EndToEndTestInboxController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.EndToEndTestMailboxes.Commands.DeleteAllMessagesCommand> deleteAllMessages,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.EndToEndTestMailboxes.Queries.TryGetLatestMessageQuery,Fifthweek.Api.EndToEndTestMailboxes.Queries.Message> tryGetLatestMessage)
        {
            if (deleteAllMessages == null)
            {
                throw new ArgumentNullException("deleteAllMessages");
            }

            if (tryGetLatestMessage == null)
            {
                throw new ArgumentNullException("tryGetLatestMessage");
            }

            this.deleteAllMessages = deleteAllMessages;
            this.tryGetLatestMessage = tryGetLatestMessage;
        }
    }
}
namespace Fifthweek.Api.EndToEndTestMailboxes.Controllers
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.EndToEndTestMailboxes.Commands;
    using Fifthweek.Api.EndToEndTestMailboxes.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;

    public partial class MailboxNameData 
    {
        public MailboxNameData(
            System.String mailboxName)
        {
            if (mailboxName == null)
            {
                throw new ArgumentNullException("mailboxName");
            }

            this.MailboxName = mailboxName;
        }
    }
}
namespace Fifthweek.Api.EndToEndTestMailboxes.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.CodeGeneration;

    public partial class DeleteAllMessagesCommand 
    {
        public DeleteAllMessagesCommand(
            Fifthweek.Api.EndToEndTestMailboxes.Shared.MailboxName mailboxName)
        {
            if (mailboxName == null)
            {
                throw new ArgumentNullException("mailboxName");
            }

            this.MailboxName = mailboxName;
        }
    }
}
namespace Fifthweek.Api.EndToEndTestMailboxes
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence;

    public partial class EndToEndTestSendEmailServiceDecorator 
    {
        public EndToEndTestSendEmailServiceDecorator(
            Fifthweek.Api.Core.ISendEmailService baseService,
            Fifthweek.Api.EndToEndTestMailboxes.ISetLatestMessageDbStatement setLatestMessage)
        {
            if (baseService == null)
            {
                throw new ArgumentNullException("baseService");
            }

            if (setLatestMessage == null)
            {
                throw new ArgumentNullException("setLatestMessage");
            }

            this.baseService = baseService;
            this.setLatestMessage = setLatestMessage;
        }
    }
}
namespace Fifthweek.Api.EndToEndTestMailboxes
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence;

    public partial class SetLatestMessageDbStatement 
    {
        public SetLatestMessageDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Api.EndToEndTestMailboxes.Commands
{
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    public partial class DeleteAllMessagesCommandHandler 
    {
        public DeleteAllMessagesCommandHandler(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Api.EndToEndTestMailboxes.Queries
{
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    public partial class TryGetLatestMessageQueryHandler 
    {
        public TryGetLatestMessageQueryHandler(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}

namespace Fifthweek.Api.EndToEndTestMailboxes.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;

    public partial class TryGetLatestMessageQuery 
    {
        public override string ToString()
        {
            return string.Format("TryGetLatestMessageQuery({0})", this.MailboxName == null ? "null" : this.MailboxName.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((TryGetLatestMessageQuery)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.MailboxName != null ? this.MailboxName.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(TryGetLatestMessageQuery other)
        {
            if (!object.Equals(this.MailboxName, other.MailboxName))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.EndToEndTestMailboxes.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;

    public partial class Message 
    {
        public override string ToString()
        {
            return string.Format("Message(\"{0}\", \"{1}\")", this.Subject == null ? "null" : this.Subject.ToString(), this.Body == null ? "null" : this.Body.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((Message)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Subject != null ? this.Subject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Body != null ? this.Body.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(Message other)
        {
            if (!object.Equals(this.Subject, other.Subject))
            {
                return false;
            }
        
            if (!object.Equals(this.Body, other.Body))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.EndToEndTestMailboxes.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;
    using Fifthweek.CodeGeneration;

    public partial class DeleteAllMessagesCommand 
    {
        public override string ToString()
        {
            return string.Format("DeleteAllMessagesCommand({0})", this.MailboxName == null ? "null" : this.MailboxName.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((DeleteAllMessagesCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.MailboxName != null ? this.MailboxName.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(DeleteAllMessagesCommand other)
        {
            if (!object.Equals(this.MailboxName, other.MailboxName))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.EndToEndTestMailboxes.Controllers
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.EndToEndTestMailboxes.Commands;
    using Fifthweek.Api.EndToEndTestMailboxes.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.EndToEndTestMailboxes.Shared;

    public partial class MailboxNameData 
    {
        public class Parsed
        {
            public Parsed(
                MailboxName mailboxName)
            {
                if (mailboxName == null)
                {
                    throw new ArgumentNullException("mailboxName");
                }

                this.MailboxName = mailboxName;
            }
        
            public MailboxName MailboxName { get; private set; }
        }
    }

    public static partial class MailboxNameDataExtensions
    {
        public static MailboxNameData.Parsed Parse(this MailboxNameData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            MailboxName parsed0 = null;
            if (!MailboxName.IsEmpty(target.MailboxName))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!MailboxName.TryParse(target.MailboxName, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("MailboxName", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("MailboxName", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new MailboxNameData.Parsed(
                parsed0);
        }    
    }
}


