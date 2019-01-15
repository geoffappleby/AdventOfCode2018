using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace Day13
{
    public class Track
    {
        public TrackOrientation Orientation { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Cart Cart { get; set; }
        public Track()
        {
        }

    }
}
