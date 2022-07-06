namespace CheeseSharp;

public class Operation
{
    public OperationType OperationType;

    public List<Operation> SubOperations;

    public string Text;

    public Operation(OperationType operationType)
    {
        OperationType = operationType;
        SubOperations = new List<Operation>();
    }
}