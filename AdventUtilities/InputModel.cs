using System;
using System.Collections.Generic;
using System.Text;

namespace AdventUtilities
{
    /// <summary>
    /// Base class for generic reading of input model data
    /// </summary>
    public abstract class InputModel 
    {
        /// <summary>
        /// Where the line of data gets stored
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        /// Force implementors to parse the data
        /// </summary>
        public abstract void LoadInput();
    }
}
