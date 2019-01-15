using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day8
{
    //represents a node in the license file
    public class Node
    {
        //assigned identifier
        public int Id { get; set; }
        
        //how many children supposed to be read
        public int ExpectedChildren { get; set; }

        //how many metadata values supposed to be read
        public int ExpectedMetadata { get; set; }

        //the children for this node
        public List<Node> Children { get; set; } = new List<Node>();

        //the metadata for this node
        public List<int> Metadata { get; set; } = new List<int>();

        public Node(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Load this node, and all its children
        /// </summary>
        /// <param name="licenseData">The data to load from</param>
        /// <param name="currentIndex">The current index into the licenseData to read from</param>
        /// <param name="nextIndex">The next id to set if creating new children</param>
        public void Load(string[] licenseData, ref int currentIndex, ref int nextIndex)
        {
            //read the next two numbers
            ExpectedChildren = int.Parse(licenseData[currentIndex]);
            currentIndex++;
            ExpectedMetadata = int.Parse(licenseData[currentIndex]);
            currentIndex++;

            for (int i = 0; i < ExpectedChildren; i++)
            {
                var childNode = new Node(nextIndex);
                nextIndex++;
                childNode.Load(licenseData, ref currentIndex, ref nextIndex);
                Children.Add(childNode);
            }

            for (int i = 0; i < ExpectedMetadata; i++)
            {
                Metadata.Add(int.Parse(licenseData[currentIndex]));
                currentIndex++;
            }
        }

        /// <summary>
        /// Print myself, and my children
        /// </summary>
        /// <param name="level"></param>
        public void Print(int level)
        {
            for (int i = 0; i < level; i++)
            {
                Console.Write(" ");
            }
            Console.Write($"{Id}");
            if (ExpectedMetadata > 0)
            {
                Console.Write("[ ");
                Metadata.ForEach(x => Console.Write($"{x} "));
                Console.Write("]");
            }
            Console.WriteLine(string.Empty);
            if (ExpectedChildren > 0)
            {
                Children.ForEach(x => x.Print(level+1));
            }
        }

        /// <summary>
        /// Calculate the checksum of this node
        /// </summary>
        /// <returns>the checksum</returns>
        public int GetChecksum()
        {
            //initial checksum is the sum of the metadata
            int checksum = Metadata.Sum(x => x);
            //add on the checksum of every childnode
            Children.ForEach(x => checksum += x.GetChecksum());
            return checksum;
        }

        /// <summary>
        /// Calculate the value of this node
        /// </summary>
        /// <returns>the value</returns>
        public int GetValue()
        {
            //if there's no children, just sum the metadata
            if (!Children.Any())
            {
                return Metadata.Sum(x => x);
            }


            int value = 0;
            //go through each metadata item
            foreach (var index in Metadata)
            {
                //if it can be referenced in the children list, add on its value
                if ((index-1) >= 0 && (index-1) < Children.Count)
                {
                    value += Children[index-1].GetValue();
                }
            }

            return value;
        }
    }
}
