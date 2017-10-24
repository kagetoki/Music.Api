using Music.API.Services.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Services.Events
{
    public class ReleaseUpdated
    {
        //public Update<string> Artist { get; set; }
        //public Update<string> Genre { get; set; }
        //public Update<string> Title { get; set; }
        //public Update<byte[]> Cover { get; set; }
        public ReleaseState OldValue { get; private set; }
        public ReleaseState NewValue { get; private set; }
    }
}
