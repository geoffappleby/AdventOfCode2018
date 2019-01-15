using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Text;
using AdventUtilities;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            bool part1 = false;
            if (part1)
            {

                var recipes = new List<int>(327920);
                var loadTo = 9;
                recipes.Add(3);
                recipes.Add(7);
                var elf1 = 0;
                var elf2 = 1;
                while (recipes.Count < loadTo + 10 + 1)
                {
                    (elf1, elf2) = GenerateNext(recipes, elf1, elf2);
                }

                var answer = GetAnswer(recipes);
                Console.WriteLine($"Answer: {answer} - Expected Answer: 5158916779");

                recipes = new List<int>(327920);
                recipes.Add(3);
                recipes.Add(7);
                loadTo = 5;
                elf1 = 0;
                elf2 = 1;
                while (recipes.Count < loadTo + 10 + 1)
                {
                    (elf1, elf2) = GenerateNext(recipes, elf1, elf2);
                }

                answer = GetAnswer(recipes);
                Console.WriteLine($"Answer: {answer} - Expected Answer: 0124515891");

                recipes = new List<int>(327920);
                recipes.Add(3);
                recipes.Add(7);
                loadTo = 18;
                elf1 = 0;
                elf2 = 1;
                while (recipes.Count < loadTo + 10)
                {
                    (elf1, elf2) = GenerateNext(recipes, elf1, elf2);
                }

                answer = GetAnswer(recipes);
                Console.WriteLine($"Answer: {answer} - Expected Answer: 9251071085");

                recipes = new List<int>(327920);
                recipes.Add(3);
                recipes.Add(7);
                loadTo = 2018;
                elf1 = 0;
                elf2 = 1;
                while (recipes.Count < loadTo + 10)
                {
                    (elf1, elf2) = GenerateNext(recipes, elf1, elf2);
                }

                answer = GetAnswer(recipes);
                Console.WriteLine($"Answer: {answer} - Expected Answer: 5941429882");

                recipes = new List<int>(327920);
                recipes.Add(3);
                recipes.Add(7);
                loadTo = 327901;
                elf1 = 0;
                elf2 = 1;
                while (recipes.Count < loadTo + 10)
                {
                    (elf1, elf2) = GenerateNext(recipes, elf1, elf2);
                }

                answer = GetAnswer(recipes);
                Console.WriteLine($"Answer: {answer} - Expected Answer: ???");

            }
            else
            {
                //part 2
                var recipes = new List<int>(327920) {3, 7};
                var endsWith = 327901;
                var endsArray = new List<int>(6) { 3,2,7,9,0,1 };
                var endsWithString = endsWith.ToString();
                var endsWithLength = endsWithString.Length;
                var elf1 = 0;
                var elf2 = 1;
                var idx = -1;
                while (true)
                {
                    (elf1, elf2) = GenerateNext(recipes, elf1, elf2);
                    idx = EndsWith(recipes, endsWith, endsArray, endsWithLength);
                    if (idx > 0)
                    {
                        Console.WriteLine($"The answer is {idx}");
                        break;
                    }
                }


            }

            Helper.Pause();
        }



        private static int EndsWith(List<int> recipes, int endsWith, List<int> endsArray, int endsLength)
        {
            if (recipes.Count < 30)
            {
                return -1;
            }

            for (int z = recipes.Count - 20; z < recipes.Count - 5; z++)
            {
                if (recipes[z] == endsArray[0] &&
                    recipes[z+1] == endsArray[1] &&
                    recipes[z+2] == endsArray[2] &&
                    recipes[z+3] == endsArray[3] &&
                    recipes[z+4] == endsArray[4] &&
                    recipes[z+5] == endsArray[5]
                )
                {
                    return z;
                }
            }

            return -1;





            int j = 0;
            bool ret = true;
            for (int i = recipes.Count - endsLength; i < recipes.Count; i++)
            {
                if (recipes[i] != endsArray[j])
                {
                    ret = false;
                    break;
                }

                j++;
            }

            if (ret)
            {
                return recipes.Count - endsLength;
            }

            ret = true;
            j = 0;
            for (int i = recipes.Count - endsLength-1; i < recipes.Count-1; i++)
            {
                if (recipes[i] != endsArray[j])
                {
                    ret = false;
                    break;
                }

                j++;
            }

            return ret == false ? -1 : recipes.Count - endsLength - 1;
        }

        private static string GetAnswer(List<int> recipes)
        {
            StringBuilder b = new StringBuilder();
            for (var i = recipes.Count - 10; i < recipes.Count; i++)
            {
                if (i < 0)
                {
                    continue;
                }

                b.Append(recipes[i].ToString());
            }

            return b.ToString();
        }

        private static void PrintAnswer(List<int> recipes)
        {
            for (var i = recipes.Count - 11; i < recipes.Count; i++)
            {
                if (i < 0)
                {
                    continue;
                }
                Console.Write(recipes[i]);
            }
            Console.WriteLine(String.Empty);
        }

        private static void PrintRecipes(List<int> recipes, int elf1, int elf2)
        {
            for (var i = 0; i < recipes.Count; i++)
            {
                if (i < 0)
                {
                    continue;
                }
                if (i == elf1)
                {
                    Console.Write("(");
                } else if (i == elf2)
                {
                    Console.Write("[");
                }
                else
                {
                    Console.Write(" ");
                }
                Console.Write(recipes[i]);
                if (i == elf1)
                {
                    Console.Write(")");
                }
                else if (i == elf2)
                {
                    Console.Write("]");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine(String.Empty);
        }

        private static (int, int) GenerateNext(List<int> recipes, int elf1, int elf2)
        {
            var recipe1 = recipes[elf1];
            var recipe2 = recipes[elf2];

            var nextRecipe = recipe1 + recipe2;

            var recipeString = nextRecipe.ToString();

            foreach (var c in recipeString.ToCharArray())
            {
                recipes.Add((int)char.GetNumericValue(c));
            }

            elf1 = (elf1 + recipe1 + 1) % recipes.Count;
            elf2 = (elf2 + recipe2 + 1) % recipes.Count;

            return (elf1, elf2);
        }
    }
}
