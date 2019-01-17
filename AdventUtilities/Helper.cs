using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventUtilities
{
    /// <summary>
    /// Handy helper functions
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Load the first line from the file into a model
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="inputPath">The file to load</param>
        /// <returns>a model</returns>
        public static T LoadFromFile<T>(string inputPath) where T : InputModel, new()
        {
            var file = System.IO.File.ReadAllText(inputPath);

            using (var reader = new StringReader(file))
            {
                string line = reader.ReadLine();
                var model = new T { Input = line };
                model.LoadInput();
                return model;
            }

            return null;

        }

        /// <summary>
        /// Load the first line from the file into a string
        /// </summary>
        /// <param name="inputPath">the file to load</param>
        /// <returns>a string</returns>
        public static string LoadFromFile(string inputPath)
        {
            var file = System.IO.File.ReadAllText(inputPath);
            using (var reader = new StringReader(file))
            {
                return reader.ReadLine();
            }
        }

        /// <summary>
        /// Load all lines from the file into a list of models
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="inputPath">The file to load</param>
        /// <returns>a list</returns>
        public static List<T> LoadAllFromFile<T>(string inputPath) where T : InputModel, new()
        {
            var file = System.IO.File.ReadAllText(inputPath);
            var list = new List<T>();
            using (var reader = new StringReader(file))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var model = new T { Input = line };
                    model.LoadInput();
                    list.Add(model);
                    line = reader.ReadLine();
                }
            }

            return list;
        }

        /// <summary>
        /// Loads all lines from the file into a list of string
        /// </summary>
        /// <param name="inputPath">The file to load</param>
        /// <returns>a list</returns>
        public static List<string> LoadAllFromFile(string inputPath)
        {
            var file = System.IO.File.ReadAllText(inputPath);
            var list = new List<string>();
            using (var reader = new StringReader(file))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    list.Add(line);
                    line = reader.ReadLine();
                }
            }

            return list;
        }

        /// <summary>
        /// Wait for enter
        /// </summary>
        public static void Pause()
        {
            Console.Write("Press enter to continue...");
            Console.ReadLine();
        }

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
