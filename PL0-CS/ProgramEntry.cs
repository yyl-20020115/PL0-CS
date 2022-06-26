namespace PL0
{
    public class ProgramEntry
    {
        public static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("usage: PL0 <Input> [Output]");
                return 1;
            }


            using var reader = new StreamReader(args[0]);
            using var writer = args.Length >= 2
                ? new StreamWriter(args[1])
                : Console.Out
                ;

            var parser = new Parser(reader, writer);
            var program = parser.Parse();
            if(program != null){
                var evaluator = new Evaluator(reader, writer);
                program.Accept(evaluator);
                return 0;
            }

            return -1;

        }
    }
}
