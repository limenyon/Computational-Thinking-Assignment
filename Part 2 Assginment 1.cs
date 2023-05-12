//Vytenis Sakalinskas 20816648 / CE
using System;
using System.IO;
using System.Collections.Generic; //using it for list
using System.Collections; //using it for bitarray
using System.Linq; //using it to check amount of lines in file

namespace Assignment_1_Part_2
{
    class Chocolate
    {
        public string ChocolateName { get; set; } //using get; as a way to access the string and doubles to read them and set; to write into them
        public double ProductionCost { get; set; }
        public double RetailPrice { get; set; } 

        public Chocolate(string chocolateName, double productionCost, double retailPrice) //creating the attributes the class objects will have
        {
            ChocolateName = chocolateName;
            ProductionCost = productionCost;
            RetailPrice = retailPrice;
        }
        public override string ToString() //overiding the ToString function that's already present in one of C# libraries to output the desired classes' parameters
        {
            return ChocolateName + " Cost: " + ProductionCost +  "  Value: " + RetailPrice;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Chocolate> Chocolates = new List<Chocolate>();
            string filePath = @"chocolates.txt"; //the filepath that starts in the debugger
            int minCheckingPosition = 14; //the minimum amount of boxes that can be in a set
            int maxCheckingPosition = File.ReadLines(filePath).Count(); //using system.linq allows me to check how many liens are in the file, used this to limit amount of combinations
            bool[] bestOption = BuildArray(0);
            bool[] bitArray = BuildArray(0);
            int numberOfCombinations = (int)Math.Pow(2, maxCheckingPosition) - 1;
            double bestPrice = 0;
            double bestCost = 0;
            double bestProfit = 0;
            double maxCost = 1.96;
            string solutionFilePath = @"Solution.txt"; //filepath for creating a new file for the solution of this part of the assignment


            using (StreamReader sr = File.OpenText(filePath)) //opening the file of chocolates
            {
                for(int i = 0; i < maxCheckingPosition; i++) //loop for as many items as there are in the file opened
                {
                    var textfileLine = sr.ReadLine(); //store whatever is in the line read as a variable
                    var lineSplitArray = textfileLine.Split(","); //split the readline that was stored at the commas, known as CSV, and store it in a container, I believe this turns all parts into strings

                    Chocolate newChocolate = new Chocolate(lineSplitArray[0], Convert.ToDouble(lineSplitArray[1]), Convert.ToDouble(lineSplitArray[2])); //creating a new chocolate class, since the previous items are not doubles they need to be converted
                    Chocolates.Add(newChocolate); //adding the chocolates to the list of chocolates

                    Console.WriteLine(Chocolates[i].ToString()); //outputting to the console what just got added to the list
                }
            }
            for(int i = 1; i <= numberOfCombinations; i++) //a loop for creating bit arrays for each combination
            {
                double cost = 0f;
                double price = 0f;
                bitArray = BuildArray(i);

                int numberOfActiveBits = 0;
                for(int j = 0; j < maxCheckingPosition; j++)
                {
                    if(bitArray[j] == true)
                    {
                        cost += Chocolates[j].ProductionCost; //used to compare with the current best
                        price += Chocolates[j].RetailPrice; //used to compare with the current best
                        numberOfActiveBits++; //used to check if there is enough bits to meet the minimum criteria
                    }
                }
                if(numberOfActiveBits >= minCheckingPosition && cost <= maxCost) //if the number of active bits is less than 14 avoid going down since that data doesn't matter to us according to the spec
                {
                    if(price - cost >= bestProfit) //if the new profit price - cost is less than our current best profit don't store it as the new best
                    {
                        bestPrice = Math.Round(price, 2); //rounding down the currently best price we have
                        bestCost = Math.Round(cost, 2); //roudning to 2 places after the .
                        bestProfit = Math.Round(bestPrice - bestCost, 2); //rounding down to 2 places after the .
                        bestOption = BuildArray(i); //building whatever bitarray is currently the best for future use
                    }
                }
            }

            if(!File.Exists(solutionFilePath)) //create the solution text file if it doesn't exist
            {
                File.CreateText(solutionFilePath);
            }
            if (new FileInfo(solutionFilePath).Length == 0) //if the file already exists and isn't empty ignore anything that comes out after
            {
                using (StreamWriter solutionWriter = File.AppendText(solutionFilePath)) //using the built in C# writing to the file
                {
                    int bitArrayBestOutput = 0; //used for selecting items from the chocolates List 
                    foreach (var bit in bestOption) //20 bits in this case in the bestOption bitarray
                    {
                        if (bit == true) //if the bit is true and the ingredient is used in the best combination
                        {
                            solutionWriter.WriteLine(Chocolates[bitArrayBestOutput].ToString()); //outputting the item in the Chocolates List if the bit representing it is true
                        }
                        bitArrayBestOutput++; //increasing after each loop to check the next item from the Chocolates List 
                    }
                    solutionWriter.WriteLine("Cost £: {0}", bestCost); //using the solution writer to output 
                    solutionWriter.WriteLine("Retail Price £: {0}", bestPrice);
                    solutionWriter.WriteLine("Profit £: {0}", bestProfit);
                }
            }
        }
        static bool[] BuildArray(int iteration) //the function is used to create a bit array of the length of however many bits are passed
        {
            BitArray bitArray = new BitArray(new int[] { iteration }); //using System.collections lets me create a bitarray that can store as many bit combinations as I need for my loops without me needing to define that
            bool[] bits = new bool[bitArray.Count]; //since a bit is either true or false, on or off, I am using a boolean to check which one it is
            bitArray.CopyTo(bits, 0); //Copying to the bitarray all the bits starting at position 0

            return bits; //returning the amount of bits requested when the function is called using the passed parameter of iteration
        }
    }

}
