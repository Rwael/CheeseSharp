using System.Text.RegularExpressions;

namespace CheeseSharp;

public static class Syntactic
{
    public static List<Operation> ValidateBrackets(List<string> parts)
    {
        //Validate cheese
        if (!parts.Contains("Cheese") || !parts.Contains("NoCheese"))
        {
            throw new Exception("Must begin with Cheese and end with NoCheese");
        }

        var beginning = parts.IndexOf("Cheese");
        var ending = parts.IndexOf("NoCheese");

        var realParts = parts.GetRange(beginning, ending);

        var operations = new List<Operation>();
        foreach (var part in realParts)
        {
            
            operations.Add(GetOperation(part));
        }

        return operations;




    }

    public static Operation GetOperation(string code)
    {
        var first = true;
        var operation = new Operation(GetOperationType(code));
        operation.Text = code;
        
        //here recursion till it doesnt find anything anymore

        var allowedKeyWords = new List<string>()
        {
            "Wensleydale\\(", "Swiss", "GlynPrint\\(", "Cheddar\n", "Coleraine\\(", "Stilton",
            "Blue", "White", "Belgian", "Brie", "Parmesan\\(\\)", "GlynMake\\("
        };
        var allowedRegex = string.Join("|", allowedKeyWords);
        while (Regex.IsMatch(code, allowedRegex, RegexOptions.Multiline))
        {
            var match = Regex.Match(code, allowedRegex);
            
            code = code.Remove(0, match.Index + match.Length);
            var matchOpType = GetOperationType(match.Value);
         
            
            if (match.Value.EndsWith("("))
            {
                var count = 1;
                var codeParts = code.ToCharArray();
                for (int i = 0; i<  codeParts.Length; i++ )
                {
                    var codePart = codeParts[i];
                    switch (codePart){
                        case '(':
                            count++;
                            break;
                        case ')'  :
                            count--;
                            break;
                        default:
                            break;
                    }
                    if(count < 0){
                        Console.WriteLine("Syntax Exception: closed more brackets than opened");
                        //throw new InvalidParameterException("Syntax Exception: closed more brackets than opened");
                    }

                    if (count == 0)
                    {
                        var subOperationCode = code.Substring(0, i);
                        
                        //var newOp = GetOperation(matchOpType + "(" +subOperationCode);
                        var newOp = GetOperation(subOperationCode);
                        switch (matchOpType)
                        {
                            case OperationType.GlynMake:
                                newOp.OperationType = OperationType.Assignment;
                                break;
                            case OperationType.GlynPrint:
                                newOp.OperationType = OperationType.Calculation;
                                break;
                            case OperationType.Coleraine:
                                newOp.OperationType = OperationType.Comparison;
                                break;
                            case OperationType.Wensleydale:
                                newOp.OperationType = OperationType.Text;
                                break;
                        }
                        operation.SubOperations.Add(newOp);
                        code = code.Remove(0, i);
                        //
                        break;
                    }
                }
            }
            else if (matchOpType == OperationType.Swiss)
            {
                //find next swiss
                var nextSwissIndex = Regex.Match(code, "Swiss").Index;
                var sub = code.Substring(0,nextSwissIndex);
                code = code.Remove(0, nextSwissIndex + "Swiss".Length);
            
                var newOp = new Operation(OperationType.Swiss);
                newOp.Text = sub;
                operation.SubOperations.Add(newOp);
            }
            else
            {
                var newOp = new Operation(matchOpType);
                newOp.Text = match.Value;
                operation.SubOperations.Add(newOp);
            }
         
        }

        return operation;

        //RICHTIGE REGEx NUR MIT OFFENER KLAMMER DANN MIT COUNTER DIE PASSENDE KLAMMER FINDEN UND REKURsION DARIN.
        //if getOperatons equal nothing then it must be a text or assignment or comparison
    }


    public static OperationType GetOperationType(string operation)
    {
       var possibleEnumValues = Enum.GetValues<OperationType>().Where(x =>
            x != OperationType.Assignment && x != OperationType.Text && x != OperationType.Comparison);
        foreach (var operationType in possibleEnumValues)
        {
            if (operation.StartsWith(operationType.ToString()))
            {
                return operationType;
            }
        }
        //never gonna happen
        return OperationType.Swiss;
    }
}