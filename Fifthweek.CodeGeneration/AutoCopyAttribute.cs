namespace Fifthweek.CodeGeneration
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class AutoCopyAttribute : Attribute
    {
        public AutoCopyAttribute()
        {
            this.RequiresBuilder = true;
        }

        public bool RequiresBuilder { get; set; }
    }
}