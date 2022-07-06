using System;
using CheeseSharp;

class Program {
  
    // Main Method
    static public void Main(String[] args)
    {
        var code = File.ReadAllText("Codes/Test.txt");
        var parts = Lexical.GetCodeParts(code);
        var operations = Syntactic.ValidateBrackets(parts);
        Console.Write(String.Join("\n", parts));
        
    }
}