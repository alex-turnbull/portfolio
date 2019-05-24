using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanEncoding
{
    class Tree
    {
        public Node head;
        public Dictionary<char, int> weightDict = new Dictionary<char, int>();
        public List<Node> listOfNodes = new List<Node>();

        public Node GetFirstNode() { return head; }

        //create the tree using the input from the user
        public void createTree(string textToEncode)
        {
            //call a function to generate the weights for symbol 
            weightDict = calculateWeights(textToEncode);

            //create a node for each symbol and their weight
            foreach (KeyValuePair<char, int> entry in weightDict)
            {
                var node = new Node
                {
                    Content = entry.Key,
                    Weight = entry.Value
                };

                listOfNodes.Add(node);
            }

            while (listOfNodes.Count > 1)
            {
                //get the lowest to weights
                List<Node> lowestNodes = ReturnLowestWeights(ref listOfNodes);

                //create a parent node for the two lowest weights
                Node newNode = new Node();

                Node left = lowestNodes[0];
                Node right = lowestNodes[1];
                newNode.Weight = left.Weight + right.Weight;
                newNode.Content = '$';
                newNode.leftChild = left;
                newNode.rightChild = right;

                //remove the base nodes from the list and add the new parent node
                listOfNodes.Remove(left);
                listOfNodes.Remove(right);
                listOfNodes.Add(newNode);

                //insert into tree
                head = listOfNodes.FirstOrDefault();
            }
        }

        //function tn calculate weight for each symbol
        public Dictionary<char, int> calculateWeights(string str)
        {
            //create a key for each symbol into a dictionary and increase the value for each time the symbol is found in the input
            Dictionary<char, int> letters = new Dictionary<char, int>();
            foreach (char c in str)
            {
                if (letters.ContainsKey(c))
                {
                    letters[c]++;
                }
                else
                {
                    letters.Add(c, 1);
                }
            }

            return letters;
        }

        //function to get the two lowest weights ready for making a parent node
        public List<Node> ReturnLowestWeights(ref List<Node> nodeList)
        {
            //create and sort a list of nodes by their weights
            List<Node> sortedList = nodeList.OrderBy(x => x.Weight).ThenBy(x => x.Content).ToList<Node>();

            List<Node> returnList = new List<Node>();

            //add the two into a list to return back for use
            if (sortedList.Count >= 2)
            {
                returnList.Add(sortedList[0]);
                returnList.Add(sortedList[1]);
            }
            

            return returnList;
        }


        public string ProduceBinaryString(string origInput, Dictionary<char,string> codeTable)
        {
            //using the code table, read each character in the original input string and write the binary code for it
            string binaryString = "";
            foreach(char c in origInput)
            {
               
                binaryString += codeTable[c];
            }

            return binaryString;
        }

        //using recursion, search through the binary tree using an inorder search
        public Dictionary<char,string> recursiveSearch(Node root,ref string binaryEncode, Dictionary<char,string> codeTable)
        {
            //check if left child can be tranversed
            if (root.leftChild != null)
            {
                //add a 0 for the left traverse
                binaryEncode += "0";
                recursiveSearch(root.leftChild,ref binaryEncode, codeTable); //call itself again to continue reading

            }
            //check if right child can be traversed
            if (root.rightChild != null)
            {
                //add a 1 for the right traverse
                binaryEncode += "1";
                recursiveSearch(root.rightChild,ref binaryEncode, codeTable); //call itself again to continue reading
            }
            //if a leaf is found
            if (root.Content != '$')
            {
                //add it's symbol and binary encode into a dictionary so it can be returned
                codeTable.Add(root.Content, binaryEncode);
                
            }
            //moving back up tree so remove the last binary character added
            if(binaryEncode.Length != 0)
            {
                binaryEncode = binaryEncode.Remove(binaryEncode.Length - 1);
            }
            return codeTable;
        }
    }
}
