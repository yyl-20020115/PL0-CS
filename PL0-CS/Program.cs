
using PL0;

if (args.Length < 2)
{
    Console.WriteLine("usage: PL0 Input Outputs");
    return 1;
}


using var reader = new StreamReader(args[0]);
using var writer = new StreamWriter(args[1]);

var parser = new Parser(reader,writer);
var program = parser.GetResult();

var evaluator = new Evaluator(reader, writer);
program.accept(evaluator);

return 0;

