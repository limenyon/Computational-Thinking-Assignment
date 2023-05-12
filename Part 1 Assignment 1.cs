//Vytenis Sakalinskas 20816648 / CE
using System;
using System.IO; //using this to access files

namespace Part_1_of_Assignment_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] ingredientType1 = { "Strawberry", "Mint", "Nougat", "Truffle", "Hazelnut", "Orange", "Toffee" }; //just storing everything in 3 arrays, makes looping far easier
            string[] ingredientType2 = { "Rosemary", "Thyme", "Sage", "Chilli", "Pepper", "Lemongrass", "Seasalt" };
            string[] ingredientType3 = { "Surprise", "Whip", "Delight", "Explosion", "Cream", "Crunch", "Whirl" };
            string path = @"all_chocolates.txt"; //pathing starts in the debug folder, that's where I'll create chocolates.txt
            int combinationCounter = 1; //Using this to display on which loop the combination was created

            if (!File.Exists(path)) //checking if the file exists, if it doesn't start writing into it, if it does it ran the program already
            {
                for (int i = 0; i < ingredientType3.Length; i++) //I have to loop through each array for the length of the array, this comes out as 7 * 7 * 7 loops
                {
                    for (int j = 0; j < ingredientType2.Length; j++)
                    {
                        for (int k = 0; k < ingredientType1.Length; k++)
                        {
                            using (StreamWriter sw = File.CreateText(path)) //I'm using the built in System.IO function to write to my file in the filepath location
                            {
                                sw.WriteLine("{0}. {1} and {2} {3}", combinationCounter, ingredientType1[k], ingredientType2[j], ingredientType3[i]); //the last loop will occur for each combination that's why it the writeLine is in the last loop
                                combinationCounter++; //increasing the counter each loop for visual indicated asked in the spec
                            }
                        }
                    }
                }
            }
        }
    }
}
