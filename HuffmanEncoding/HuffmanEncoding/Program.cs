using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanEncoding
{
    class Program
    {
        static void Main(string[] args)
        {
            //get user input form console
            Console.WriteLine("Enter a string to convert into Huffman Encoding");
            string input = Console.ReadLine();

            //reference and instantiate a new tree from the class
            Tree tree = new Tree();
            
            //create a dictionary to store the symbols and the binary code
            Dictionary<char, string> tableOfCodes = new Dictionary<char, string>();
            

            // Build the Huffman tree
            tree.createTree(input);

            //traversing through
            string returnS = "";
            tableOfCodes = tree.recursiveSearch(tree.head, ref returnS, tableOfCodes);

            //order the table of codes by symbol
            var orderedDict = tableOfCodes.OrderBy(x => x.Key);

            //using the dictionary of symbols and codes to take the input again and generate the long binary string
            string inputAsBinary = tree.ProduceBinaryString(input, tableOfCodes);

            //Basic User interface
            Console.WriteLine("////////////////////////");
            Console.WriteLine("Encoding table for each character: \n");
            foreach (KeyValuePair<char,string> symbol in orderedDict)
            {
                Console.WriteLine(symbol.Key.ToString() + " : " + symbol.Value.ToString());
            }
            Console.WriteLine("//////////////////////// \n");
            Console.WriteLine("Input encoded into Huffman Code: ");
            Console.WriteLine(inputAsBinary + "\n");

            float ratio = calculateCompressionRatio(tree.weightDict, tableOfCodes);

            Console.WriteLine("Compression ratio for this message = " + ratio.ToString());

            Console.ReadLine();


            //FUNCTION
            float calculateCompressionRatio(Dictionary<char,int> weightDict, Dictionary<char,string> codeDict)
            {
                float abr = 0;
                foreach(KeyValuePair<char,int> symbol in weightDict)
                {
                    abr += symbol.Value * codeDict[symbol.Key].Length;
                }
                float compressionRatio = 8 / abr;
                return compressionRatio;
            }
        }
                
    }
}
