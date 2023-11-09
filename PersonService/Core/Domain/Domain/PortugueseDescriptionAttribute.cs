using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class PortugueseDescriptionAttribute : Attribute
    {
        public string Description { get; }

        public PortugueseDescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
