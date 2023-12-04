using System.Text.RegularExpressions;
using System.Linq;
public class Day4Part2
{   
    public static void Main()
    {        
        StreamReader streamReader = new StreamReader("Day4real.txt");
        int sumValue = 0, points = 0;
        List<String> winningNums, elfNums;
        List<int> copies;
        int copyLevel, row = 0, maxRow;
        List<String> cardList = new List<string>();

        //Populate PartList
        while (streamReader.ReadLine() is { } fileLine)
        {
            cardList.Add(fileLine);
        }
        maxRow = cardList.Count;
        copies = new List<int>(new int[maxRow+1]);

        //Populate PartList
        for (int x = 0; x < maxRow; x++)
        {
            row++;

            //Real
            //winningNums = new List<String>(Regex.Replace(cardList[x].Substring(10, 29), @"\s+", " ").Split(' '));
            //elfNums = new List<String>(Regex.Replace(cardList[x].Substring(42, 74), @"\s+", " ").Split(' '));

            winningNums = new List<String>(cardList[x].Substring(9, cardList[x].Length - 9).Split('|')[0].Split(' '));
            elfNums = new List<String>(cardList[x].Substring(9, cardList[x].Length - 9).Split('|')[1].Split(' '));



            //Console.WriteLine(fileLine);
            //Console.WriteLine(fileLine.Substring(9, fileLine.Length - 9).Split('|')[0]);
            //Console.WriteLine(fileLine.Substring(9, fileLine.Length - 9).Split('|')[1]);


            copyLevel = row;
            sumValue += 1;

            //Process Original
            foreach (String value in elfNums)
            {              
                if (winningNums.Contains(value) && value != " " && value != "")
                {          
                    copyLevel += 1;

                    //if (copyLevel < maxRow)
                    //{
                        copies[copyLevel] = copies[copyLevel] + 1;
                        sumValue += 1;
                    //}
                }                
            }

            //Process Copies
            for (int i = 0; i < copies[row]; i++)
            {
                copyLevel = row;

                foreach (String value in elfNums)
                {
                    if (winningNums.Contains(value) && value != " " && value != "")
                    {                        
                        copyLevel += 1;

                        //if (copyLevel < maxRow)
                        //{
                            copies[copyLevel] = copies[copyLevel] + 1;
                            sumValue += 1;
                        //}
                    }
                }
            }   
        }       
      
        Console.WriteLine(sumValue.ToString());
        Console.ReadKey();
    }
}