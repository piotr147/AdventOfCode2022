namespace AdventOfCode2022.Utilities;

public static class Helper
{
    public async static Task<IEnumerable<int>> GetIntListFromFile(string fileName)
    {
        string[] lines = await File.ReadAllLinesAsync(fileName);

        return lines.Select(line => int.Parse(line));
    }

    public async static Task<IEnumerable<string>> GetStringListFromFile(string fileName) =>
        await File.ReadAllLinesAsync(fileName);
}
