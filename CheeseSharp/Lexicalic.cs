using System.Text.RegularExpressions;

namespace CheeseSharp;
//https://esolangs.org/wiki/Cheese%2B%2B
public static class Lexical
{
    public static List<string> GetCodeParts(String text)
    {
        var allowedKeyWords = new List<string>()
        {
            "Cheese", "NoCheese", "Wensleydale\\(.*\\)", "Swiss", "GlynMake\\(.*\\)", "Cheddar\n", "Coleraine\\(.*\\)", "Stilton",
            "Blue", "White", "Belgian", "Brie", "Parmesan\\(.*\\)", "GlynPrint\\(.*\\)"
        };

        //brie = semikolon
        //remove all text in line after "//"

        var linesCleaned = RemoveComments(text);
        //linesCleaned = linesCleaned.Replace("Cheese", "").Replace("NoCheese", "");
        var parts = linesCleaned.Split("Brie").ToList();

        var realParts = new List<string>();
        for (int i = 0; i < parts.Count; i++)
        {
            var part = parts[i];

            //
            var matches = new List<string>();
            var allowedRegex = string.Join("|", allowedKeyWords);
            while (Regex.IsMatch(part, allowedRegex, RegexOptions.Multiline))
            {
                var match = Regex.Match(part, allowedRegex);
                part = part.Remove(0, match.Index + match.Length);
                matches.Add(match.Value);
            }
            //part is cleaned
            part = String.Join("", matches);
            realParts.AddRange(matches);
            
            //rekursiv mit den parts  die () haben
            //
            // //not found
            // if (!allowedKeyWords.Any(x => Regex.IsMatch(part, x)))
            // {
            //     parts.RemoveAt(i);
            //     i--;
            // }
        }

        return realParts;
    }

    public static string RemoveComments(string text)
    {
        var lines = text.Split("\n").ToList();
        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            if (line.Contains("//"))
            {
                var index = line.IndexOf("//");
                line = line.Substring(0, index);
            }
            lines[i] = line.Trim();
        }
        return String.Join("\n", lines);

    }
}