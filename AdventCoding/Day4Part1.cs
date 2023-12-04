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
            //Real
            winningNums = new List<String>(Regex.Replace(fileLine.Substring(10, 29), @"\s+", " ").Split(' '));
            elfNums = new List<String>(Regex.Replace(fileLine.Substring(42, 74), @"\s+", " ").Split(' '));

            //winningNums = new List<String>(fileLine.Substring(9, fileLine.Length - 9).Split('|')[0].Split(' '));
            //elfNums = new List<String>(fileLine.Substring(9, fileLine.Length - 9).Split('|')[1].Split(' '));


            
            //Console.WriteLine(fileLine);
            //Console.WriteLine(fileLine.Substring(9, fileLine.Length - 9).Split('|')[0]);
            //Console.WriteLine(fileLine.Substring(9, fileLine.Length - 9).Split('|')[1]);
                


            points = 0;

            foreach (String value in elfNums)
            {              
                if (winningNums.Contains(value) && value != " " && value != "")
                {
                    points = points == 0 ? 1 : points * 2;    
                }                
            }
            //Console.WriteLine(points);

            sumValue += points;
        }       
      
        Console.WriteLine(sumValue.ToString());
        Console.ReadKey();
    }
}