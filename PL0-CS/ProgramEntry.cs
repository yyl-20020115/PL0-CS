namespace PL0
{
    public class ProgramEntry
    {
        public const int PROGRAM_EXITS_SUCCESSFULLY = 0;
        public const int PROGRAM_EXITS_FAILUREFULLY = -1;    

        public static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("usage: PL0 <Input> [Output]");
                return PROGRAM_EXITS_FAILUREFULLY;
            }


            using var reader = new StreamReader(args[0]);
            using var writer = args.Length >= 2
                ? new StreamWriter(args[1])
                : Console.Out
                ;

            var parser = new Parser(reader, writer);
            var program = parser.Parse();
            if(program != null)
            {
                var evaluator = new Evaluator(reader, writer);
                program.Accept(evaluator);
                return PROGRAM_EXITS_SUCCESSFULLY;
            }

            return PROGRAM_EXITS_FAILUREFULLY;
        }
    }
}
