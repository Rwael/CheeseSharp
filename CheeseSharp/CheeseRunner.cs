namespace CheeseSharp;

public class CheeseRunner
{

    public CheeseRunner(List<Operation> source)
    {
        Source = source;
        SourcePointer = 0;
        Memory = new Dictionary<string, byte>();
        LookupTable = new Dictionary<int, int>();
        IfTable = new List<int[]>();
    }
    public void run(List<string> codeParts)
    {
        //in stilton or coleraine do a true false in the glyn and in other glyn do a zuewisig
        //Class und Ncalc in runner
        //true false expression nur variabel = wert oder variabel oder expression
    }
    
    public Dictionary<string, Byte> Memory;
    public List<Operation> Source;
    public int SourcePointer;
    public Dictionary<int, int> LookupTable;
    public List<int[]> IfTable;



    public void run()
    {
        //for the loops
        var stack = new List<int>();
        for (int i = 0; i< Source.Count; i++) {
            var part = Source[i];
            if(part.OperationType == OperationType.Cheddar){
                stack.Add(i);
            }
            if(part.OperationType == OperationType.Coleraine){
                var start = stack[stack.Count()-1];
                LookupTable.Add(start, i);
                stack.RemoveAt(stack.Count()-1);
            }
        }
        
        //for the If's
        OperationType lastOpType = OperationType.Blue;
        var lastOpIndex = -1;
        var currentIfStack = new int[3];
        while (Source.Exists(x=>  x.OperationType == OperationType.Stilton || x.OperationType == OperationType.Blue ||
                                  x.OperationType == OperationType.White))
        {
            
            var nextIfOp = Source.First(x =>
                x.OperationType == OperationType.Stilton || x.OperationType == OperationType.Blue ||
                x.OperationType == OperationType.White);
            var nextIfIndex = Source.IndexOf(nextIfOp);

            if (lastOpType == OperationType.Blue && nextIfOp.OperationType == OperationType.Stilton)
            {
                //new if
                //make a new currentIfStack with this position as start [0]
            }
            else if (lastOpType == OperationType.White && nextIfOp.OperationType == OperationType.Blue)
            {
                //if else
                //push to ifStack and make a new currentIfStack
                // [start]0 [lastOpIndex]1 [this index]2
            }
            else if(nextIfOp.OperationType == OperationType.Blue)
            {
                //push currentIfStack to ifStack
                // [start]0 [null]1 [this pos]2
                //so i can check if there is an else
            }

            lastOpIndex = nextIfIndex;
            lastOpType = nextIfOp.OperationType;
        }
    
        for (;SourcePointer < Source.Count; SourcePointer++) {
            var operation = Source[SourcePointer];
            switch (operation.OperationType){
                case OperationType.GlynMake:
                    //assign new value to Variable
                    break;
                case OperationType.GlynPrint:
                    //inserts varibale value in string
                    break;
                case OperationType.Stilton:
                    //if
                    break;
                case OperationType.Blue:
                    //then
                    break;
                case OperationType.White:
                    //else
                    //so stilton....white....blue
                    //if-else-then
                    //in storage like lookup table but iwth array so number 1 pos = if, if length = 3 then its if else then.
                    //just beginning [0] to next index = 
                    //search where the position [1] so position 2 == this one 
                    //maybe regex macth then remove with the different ones with | and then you have the positions of the if and elses 
                    break;
                case OperationType.Cheddar:
                    //skipper
                    //dont really have to do anything here
                    // //if not already exists then add it to lookuptable
                    // if(this.memory.get(this.memoryPointer) >0){
                    //     this.loopStack.add(this.memoryPointer);
                    // }
                    // else {
                    //     //jump to the closed
                    //     this.sourcePointer = this.lookupTable[this.sourcePointer] + 1;
                    // }
                    break;
                case OperationType.Coleraine:
                    //expression
                    if(true){
                        //jump back to pos designated in lookup table + 1 because we already checked if its ok
                        this.SourcePointer = this.LookupTable.First(x => x.Value == SourcePointer).Key;
                    }
                    else {
                        //loop finished
                        this.SourcePointer++;
                    }
                    break;
                case OperationType.Parmesan:
                    char read = System.Console.ReadLine().ToCharArray()[0];
                    break;
                case OperationType.Wensleydale:
                    
                    break;
            }
    
        }
    }
}