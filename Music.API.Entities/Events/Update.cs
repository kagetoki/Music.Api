using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Entities.Events
{
    public class Update<T>
    {
        public T OldValue { get; set; }
        public T NewValue { get; set; }
    }
}
