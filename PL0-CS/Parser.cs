using PL0.AST;
namespace PL0
{
    public class Parser
    {
        public TextReader Reader => this.reader;
        public TextWriter Writer => this.writer;
        public Lexer Lexers => this.lexer;
        public bool Failed => this.failed;
        protected TextWriter writer;
        protected TextReader reader;
        protected Lexer lexer;
        protected Token.ID id = Token.ID.EndOfFile;
        protected bool failed = false;
        public Parser(TextReader reader, TextWriter writer)
        {
            this.lexer = new(this.reader = reader, this.writer = writer);
            this.id = this.lexer.NextToken();
        }        
        public Program? Parse()
        {
            var program = ParseProgram();

            if (!Match(Token.ID.EndOfFile))
            {
                Error("junk after end of program");
            }

            if (failed) return null;

            return program;

        }
        public Program? ParseProgram()
        {
            var Program = new Program
            {
                Block = ParseBlock()
            };

            if (!Consume(Token.ID.Period))
            {
                Error("missing '.' at end of program");
            }

            return Program;
        }
        public Block? ParseBlock()
        {
            var Block = new Block();

            if (Consume(Token.ID.Const))
            {
                do
                {
                    var constant = new Constant();

                    if (Match(Token.ID.Identifier))
                    {
                        constant.Identifier = ExtractIdentifier();
                    }
                    else
                    {
                        Error("Identifier expected for const name");
                        Skip();
                        continue;
                    }

                    if (!Consume(Token.ID.Equal))
                    {
                        Error("missing '=' after const Identifier");
                        Skip();
                        continue;
                    }

                    if (Match(Token.ID.Number))
                    {
                        constant.Number = ExtractNumber();
                    }
                    else
                    {
                        Error("Number expected for const value");
                        Skip();
                        continue;
                    }

                    Block.Constants.Add((constant));
                } while (Consume(Token.ID.Comma));

                if (!Consume(Token.ID.Semicolon))
                {
                    Error("missing ';' after const definitions");
                    Skip();
                }
            }

            if (Consume(Token.ID.Var))
            {
                do
                {
                    if (Match(Token.ID.Identifier) && ExtractIdentifier() is Identifier id)
                    {
                        Block.Variables.Add(id);
                    }
                    else
                    {
                        Error("Identifier expected for var name");
                        Skip();
                        continue;
                    }
                } while (Consume(Token.ID.Comma));

                if (!Consume(Token.ID.Semicolon))
                {
                    Error("missing ';' after var declarations");
                    Skip();
                }
            }

            while (Consume(Token.ID.Procedure))
            {
                var procedure = new Procedure();

                if (Match(Token.ID.Identifier))
                {
                    procedure.Identifier = ExtractIdentifier();
                }
                else
                {
                    Error("Identifier expected for procedure name");
                    Skip();
                    continue;
                }

                if (!Consume(Token.ID.Semicolon))
                {
                    Error("missing ';' after procedure name");
                    Skip();
                    continue;
                }

                procedure.Block = ParseBlock();

                if (!Consume(Token.ID.Semicolon))
                {
                    Error("missing ';' after procedure Block");
                    Skip();
                    continue;
                }

                Block.Procedures.Add((procedure));
            }

            Block.Statement = ParseStatement();

            return Block;
        }

        public Statement? ParseStatement()
        {
            switch (id)
            {
                case Token.ID.Identifier:
                    {
                        var Statement = new AssignmentStatement();

                        Statement.Left = ExtractIdentifier();

                        if (!Consume(Token.ID.Assign))
                        {
                            Error("missing ':=' after Identifier");
                            Skip();
                        }

                        Statement.Right = ParseExpression();

                        return Statement;
                    }
                case Token.ID.Call:
                    {
                        Next();

                        var Statement = new CallStatement();

                        if (Match(Token.ID.Identifier))
                        {
                            Statement.Callee = ExtractIdentifier();
                        }
                        else
                        {
                            Error("Identifier expected after keyword \"call\"");
                            Skip();
                        }

                        return Statement;
                    }
                case Token.ID.Read:
                    {
                        Next();

                        var Statement = new ReadStatement();

                        if (Match(Token.ID.Identifier))
                        {
                            Statement.Identifier = ExtractIdentifier();
                        }
                        else
                        {
                            Error("Identifier expected after '?'");
                            Skip();
                        }

                        return Statement;
                    }
                case Token.ID.Write:
                    {
                        Next();
                        var Statement = new WriteStatement();
                        Statement.Expression = ParseExpression();
                        return Statement;
                    }
                case Token.ID.Begin:
                    {
                        Next();

                        var Statement = new BeginStatement();

                        do
                        {
                            if(ParseStatement() is Statement s)
                            {
                                Statement.Children.Add(s);
                            }
                        } while (Consume(Token.ID.Semicolon));

                        if (!Consume(Token.ID.End))
                        {
                            Error("expected keyword \"end\" after begin Statement");
                            Skip();
                        }

                        return Statement;
                    }
                case Token.ID.If:
                    {
                        Next();

                        var Statement = new IfStatement();

                        Statement.Condition = ParseCondition();

                        if (!Consume(Token.ID.Then))
                        {
                            Error("missing keyword \"then\" after if-Condition");
                            Skip();
                        }

                        Statement.Statement = ParseStatement();

                        return Statement;
                    }
                case Token.ID.While:
                    {
                        Next();

                        var Statement = new WhileStatement();

                        Statement.Condition = ParseCondition();

                        if (!Consume(Token.ID.Do))
                        {
                            Error("missing keyword \"do\" after while-Condition");
                            Skip();
                        }

                        Statement.Statement = ParseStatement();

                        return Statement;
                    }
                default:
                    {
                        return new EmptyStatement();
                    }
            }
        }

        public Condition? ParseCondition()
        {
            if (Consume(Token.ID.Odd))
            {
                var Condition = new OddCondition();
                Condition.Right = ParseExpression();
                return Condition;
            }

            return ParseBinaryCondition();
        }

        public BinaryCondition? ParseBinaryCondition()
        {
            BinaryCondition? Condition;

            var Left = ParseExpression();

            switch (id)
            {
                case Token.ID.Equal:
                    Condition = new EqualCondition();
                    break;
                case Token.ID.NotEqual:
                    Condition = new NotEqualCondition();
                    break;
                case Token.ID.LessThan:
                    Condition = new LessThanCondition();
                    break;
                case Token.ID.LessEqual:
                    Condition = new LessEqualCondition();
                    break;
                case Token.ID.GreaterThan:
                    Condition = new GreaterThanCondition();
                    break;
                case Token.ID.GreaterEqual:
                    Condition = new GreaterEqualCondition();
                    break;
                default:
                    Error("expected a Conditional operator");
                    Skip();
                    return null;
            }

            Next();

            Condition.Left = (Left);
            Condition.Right = ParseExpression();

            return Condition;
        }

        public Expression? ParseExpression()
        {
            Expression? Expression;

            if (Consume(Token.ID.Plus))
            {
                Expression = ParseTerm();
            }
            else if (Consume(Token.ID.Minus))
            {
                var subExpression = new NegationExpression();
                subExpression.Right = ParseTerm();
                Expression = (subExpression);
            }
            else
            {
                Expression = ParseTerm();
            }

            while (Match(Token.ID.Plus) || Match(Token.ID.Minus))
            {
                BinaryExpression subExpression;

                if (Match(Token.ID.Plus))
                {
                    subExpression = new AdditionExpression();
                }
                else
                {
                    subExpression = new SubtractionExpression();
                }

                Next();

                subExpression.Left = (Expression);
                subExpression.Right = ParseTerm();
                Expression = (subExpression);
            }

            return Expression;
        }

        public Expression? ParseTerm()
        {
            var term = ParseFactor();

            while (Match(Token.ID.Multiply) || Match(Token.ID.Divide))
            {
                BinaryExpression subTerm = Match(Token.ID.Multiply)
                    ? new MultiplicationExpression() : new DivisionExpression();
                Next();

                subTerm.Left = (term);
                subTerm.Right = ParseFactor();
                term = (subTerm);
            }

            return term;
        }

        public Expression? ParseFactor()
        {
            switch (id)
            {
                case Token.ID.Identifier:
                    {
                        return ExtractIdentifier();
                    }
                case Token.ID.Number:
                    {
                        return ExtractNumber();
                    }
                case Token.ID.LParen:
                    {
                        Next();

                        var Expression = ParseExpression();

                        if (!Consume(Token.ID.RParen))
                        {
                            Error("missing ')' after Expression");
                            Skip();
                        }

                        return Expression;
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        public Number? ExtractNumber()
        {
            var value = (int)(lexer.Value??0);
            Next();
            return new Number(value);
        }

        public Identifier? ExtractIdentifier()
        {
            var name = lexer.Value as string;
            Next();
            return new Identifier(name??"");
        }

        protected void Error(string message)
        {
            failed = true;
            writer.WriteLine($"{lexer.Line}: error: {message}");
        }
        public void Skip()
        {
            while (this.id < Token.ID.Const)
                this.Next();
        }

        public bool Match(Token.ID expected) => id == expected;

        public bool Consume(Token.ID expected)
        {
            if (Match(expected))
            {
                Next();
                return true;
            }

            return false;
        }

        public Token.ID Next() => this.id = lexer.NextToken();
    }
}