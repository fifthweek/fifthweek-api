using System;
using System.Linq;

//// Generated on 13/02/2015 16:58:01 (UTC)
//// Mapped solution in 3.99s



namespace Fifthweek.Api.EndToEndTestMailboxes.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Fifthweek.CodeGeneration;

    public partial class MailboxName 
    {
        public override string ToString()
        {
            return string.Format("MailboxName(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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
        
            return this.Equals((MailboxName)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(MailboxName other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }
        
            return true;
        }
    }
}


