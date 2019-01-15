using System;
using System.Collections.Generic;
using System.Text;
using AdventUtilities;

namespace Day10
{
    public class Vector : InputModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Vx { get; set; }
        public int Vy { get; set; }

        public override void LoadInput()
        {
            //position=<-3,  6> velocity=< 2, -1>
            var startIndex = 10;
            if (Input[startIndex] == ' ')
            {
                startIndex++;
            }
            var endIndex = Input.IndexOf(',', startIndex);

            X = int.Parse(Input.Substring(startIndex, endIndex - startIndex));

            startIndex = endIndex + 2;
            if (Input[startIndex] == ' ')
            {
                startIndex++;
            }
            endIndex = Input.IndexOf('>', startIndex);

            Y = int.Parse(Input.Substring(startIndex, endIndex - startIndex));

            startIndex = endIndex + 12;
            if (Input[startIndex] == ' ')
            {
                startIndex++;
            }
            endIndex = Input.IndexOf(',', startIndex);

            Vx = int.Parse(Input.Substring(startIndex, endIndex - startIndex));

            startIndex = endIndex + 2;
            if (Input[startIndex] == ' ')
            {
                startIndex++;
            }
            endIndex = Input.IndexOf('>', startIndex);

            Vy = int.Parse(Input.Substring(startIndex, endIndex - startIndex));
        }

        public void Move()
        {
            X += Vx;
            Y += Vy;
        }
    }
}
