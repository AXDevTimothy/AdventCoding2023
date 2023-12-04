public class Day3Part1
{
    public static void Main()
    {
        StreamReader streamReader = new StreamReader("PuzzleInput.txt");
        List<String> partList = new List<string>();
        int maxColumn = 0; int maxRow = 0;
        string number, row;
        int sumValue = 0, numStart = 0, numEnd = 0;
        char value;

        //Populate PartList
        while (streamReader.ReadLine() is { } fileLine)
        {
            maxColumn = fileLine.Length;
            partList.Add(fileLine);
        }
        maxRow = partList.Count;

        for (int x = 0; x < maxRow; x++)
        {
            row = partList[x];
            number = ""; numStart = 0; numEnd = 0;

            for (int i = 0; i < row.Length; i++)
            {
                value = row[i];

                if (char.IsDigit(value))
                {
                    numStart = numStart == 0 ? i : numStart;
                    numEnd = i;
                    number = number + value;
                }

                if (!char.IsDigit(value) || i == maxColumn - 1)
                {
                    if (number != "")
                    {
                        if (isEnginePart(x, numStart, numEnd))
                        {
                            sumValue += Int32.Parse(number);
                        }
                    }

                    //Reset number trackers
                    number = ""; numStart = 0; numEnd = 0;
                }
            }
        }
        Console.WriteLine(sumValue.ToString());
        Console.ReadKey();

        Boolean isEnginePart(int row, int start, int end)
        {
            Char[] partListRow;

            //Loop through row above and below
            for (int i = row == 0 ? 0 : row - 1; i <= (row == maxRow - 1 ? row : row + 1); i++)
            {
                partListRow = partList[i].ToArray();

                //Loop through applicable columns
                for (int j = start == 0 ? 0 : start - 1; j <= (end == maxColumn - 1 ? end : end + 1); j++)
                {
                    //If char not a letter or digit AND also NOT a period, return TRUE
                    if (!char.IsLetterOrDigit(partListRow[j]) && partListRow[j] != '.')
                    {
                        return true;
                    }
                }
            }

            return false;
        }


    }

}