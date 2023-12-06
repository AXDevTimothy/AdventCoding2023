
using System.Text.RegularExpressions;

public class Day5Part2
{   
    public static void Main()
    {        
        StreamReader streamReader = new StreamReader("Day5Real.txt");
        List<Int64> seedList = new List<Int64>();
        List<Int64[]> map = new List<Int64[]>(), validRanges = new List<long[]>(), previousRanges = new List<long[]>();
        List<List<Int64[]>> mapList= new List<List<Int64[]>>();
        Int64 currentValue = 0, range = 0;
        Int64[] values = new Int64[3];
        SortedDictionary<Int64, Int64> locationSeed = new SortedDictionary<long, long>();
        int count = 0, lastMap = 0;
        Int64 lowestLocation = 9999999999999;

        //List of values in the seed line
        seedList = new List<Int64>(streamReader.ReadLine().Substring(6).Split(' ').Where(x => Int64.TryParse(x, out _)).Select(Int64.Parse).ToList());

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


        //Create valid ranges for ALL seed numbers
        foreach (var seed in seedList)
        {
            count++;

            if (count % 2 == 0) //Every other value is the seed or length
            {
                range = seed;
                lastMap = 0;

                Int64[] seedRange = { 0, currentValue, range };
                previousRanges.Add(seedRange);
            }
            else
            {
                currentValue = seed;
            }            
        }

        //Iterate through all maps, passing in the valid ranges (results from pervious iteration). The first iteration ranges are supplied from the original seed ranges. 
        //The results of the processing, will be a new set of valid ranges that will then get processed by the next Map
        for (int i = 0; i < mapList.Count; i++)
        {           
            previousRanges = processRanges(mapList[i], previousRanges); //See ProcessRanges method below
            lastMap = i;
        }

        //If we had seeds that made it to the final map (Humidity to location), then find the lowest location in that list
        if (lastMap == 6)
        {
            foreach (var location in previousRanges)
            {
                lowestLocation = lowestLocation < location[1] ? lowestLocation : location[1];
            }
        }

        //Return the answer
        Console.WriteLine(string.Format("Location: {0}", lowestLocation));
        Console.ReadKey();



        //This is where the magic happens...
        List<Int64[]> processRanges(List<Int64[]> puzzleMap, List<Int64[]> validRanges)
        {
            List<Int64[]> ret = new List<long[]>();
            SortedDictionary<long, long[]> l;
            List<Int64[]> f;
            long previousEnd;
            long validStart, validEnd,  offset;

            //Loop through all the valid Ranges we need to push through the map
            foreach (Int64[] r in validRanges)
            {
                l = new SortedDictionary<long,long[]>();
                f = new List<long[]>();
                
                //Loop through all the lines in the map
                foreach (Int64[] p in puzzleMap)
                {
                    //Determine if this map has a range that intersects with the range we are validating, and if so, get the start and end points of intersecting values
                    validStart = (p[1] <= r[1] && r[1] <= p[1] + p[2] - 1) ? r[1] : (r[1] <= p[1] && p[1] <= r[1] + r[2] - 1) ? p[1] : 0;
                    validEnd = (p[1] <= r[1] + r[2] - 1 && r[1] + r[2] - 1 <= p[1] + p[2] - 1) ? r[1] + r[2] - 1 : (r[1] <= p[1] + p[2] - 1 && p[1] + p[2] - 1 <= r[1] + r[2] - 1) ? p[1] + p[2] - 1 : 0;

                    offset = p[0] - p[1];

                    //If a valid range intersection was found, add it to a list
                    if (validStart - validEnd != 0)
                    {
                        Int64[] values = {validEnd, offset};
                        l.Add(validStart, values);  
                    }                    
                }

                //No Matches, just use the same start and count
                if(l.Count == 0)
                {
                    Int64[] values = {0, r[1], r[2]};
                    f.Add(values);
                }
                else
                { 
                    previousEnd = r[1] - 1;

                    //add ranges for all matches
                    foreach (var row in l)
                    {
                        //If the beginning of the range does not have a match, add itself without offset
                        if (row.Key - previousEnd > 1)
                        {
                            validStart = previousEnd + 1;
                            validEnd = row.Key - 1;
                            Int64[] values = { 0, validStart, validEnd - validStart };
                            f.Add(values);

                            previousEnd = validEnd;
                        }
                        else //If there is a match, use this new valid range
                        {
                            validStart = row.Key;
                            validEnd = row.Value[0];
                            Int64[] values = { 0, validStart + row.Value[1], validEnd - validStart }; //Add offset to start
                            f.Add(values);

                            previousEnd = validEnd;
                        }
                    }
                }

                ret.AddRange(f);
            } 

            //ret returns all valid ranges for the next level
            return ret;
        }
    }
}