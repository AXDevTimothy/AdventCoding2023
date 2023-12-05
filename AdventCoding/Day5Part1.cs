
using System.Text.RegularExpressions;

public class Day5Part1
{   
    public static void Main()
    {        
        StreamReader streamReader = new StreamReader("Day5Real.txt");
        List<int> seedList = new List<int>();
        List<Int64[]> map = new List<Int64[]>();
        List<List<Int64[]>> mapList= new List<List<Int64[]>>();
        Int64 currentValue;
        Int64[] values = new Int64[3];
        SortedDictionary<Int64, Int64> locationSeed = new SortedDictionary<long, long>();

        seedList = new List<int>(streamReader.ReadLine().Substring(6).Split(' ').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList());

        //Populate Maps
        while (streamReader.ReadLine() is { } fileLine)
        {
            if(fileLine.Contains(':') && map.Count != 0)
            {
                mapList.Add(map);
                map = new List<Int64[]>();
                continue;
            }

            if (!string.IsNullOrEmpty(fileLine) && char.IsDigit(fileLine[0]))
            {
                map.Add(fileLine.Split(' ').Where(x => Int64.TryParse(x, out _)).Select(Int64.Parse).ToArray());
            }            
        }
        mapList.Add(map); //Add the last map to the list

        //Evaluate Seeds
        foreach (var seed in seedList)
        {
            currentValue = seed;

            foreach (var lookupMap in mapList)
            {
                values = lookupMap.FirstOrDefault(x => currentValue - x[1] >= 0 && currentValue - x[1] < x[2]);

                if(values != null)
                    currentValue = currentValue + (values[0] - values[1]);
            }

            locationSeed.Add(currentValue, seed);
        }

        Console.WriteLine(string.Format("Seed: {0}, Location: {1}",  locationSeed.First().Value, locationSeed.First().Key));
        Console.ReadKey();
    }
}