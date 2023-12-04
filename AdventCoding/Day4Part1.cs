using System.Text.RegularExpressions;
using System.Linq;
public class Day4Part1
{   
    public static void Main()
    {        
        StreamReader streamReader = new StreamReader("Day4Real.txt");
        int sumValue = 0, points = 0;
        List<String> winningNums, elfNums;  

        //Populate PartList
        while (streamReader.ReadLine() is { } fileLine)
        {
            winningNums = new List<String>(Regex.Replace(fileLine.Substring(10, 29), @"\s+", " ").Split(' '));
            elfNums = new List<String>(Regex.Replace(fileLine.Substring(42, 74), @"\s+", " ").Split(' '));

            points = 0;

            foreach (String value in elfNums)
            {              
                if (winningNums.Contains(value) && value != " " && value != "")
                {
                    points = points == 0 ? 1 : points * 2;    
                }                
            }

            sumValue += points;
        }       
      
        Console.WriteLine(sumValue.ToString());
        Console.ReadKey();
    }
}