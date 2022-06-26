using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }        
        public AST.Program Parse()
        {
            var program = ParseProgram();

            if (!Match(Token.ID.EndOfFile))
            {
                Error("junk after end of program");
            }

            if (failed)
            {
                return new();
            }

            return program;

        }
        public AST.Program ParseProgram()
        {
            var Program = new AST.Program();

            Program.Block = ParseBlock();

            if (!Consume(Token.ID.Period))
            {
                Error("missing '.' at end of program");
            }

            return Program;
        }
        public AST.Block ParseBlock()
        {
            var Block = new AST.Block();

            if (Consume(Token.ID.Const))
            {
                do
                {
                    var constant = new AST.Constant();

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
                    if (Match(Token.ID.Identifier))
                    {
                        Block.Variables.Add(ExtractIdentifier());
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
                var procedure = new AST.Procedure();

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

        public AST.Statement ParseStatement()
        {
            switch (id)
            {
                case Token.ID.Identifier:
                    {
                        var Statement = new AST.AssignmentStatement();

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

                        var Statement = new AST.CallStatement();

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

                        var Statement = new AST.ReadStatement();

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
                        var Statement = new AST.WriteStatement();
                        Statement.Expression = ParseExpression();
                        return Statement;
                    }
                case Token.ID.Begin:
                    {
                        Next();

                        var Statement = new AST.BeginStatement();

                        do
                        {
                            Statement.Children.Add(ParseStatement());
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

                        var Statement = new AST.IfStatement();

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

                        var Statement = new AST.WhileStatement();

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
                        return new AST.EmptyStatement();
                    }
            }
        }

        public AST.Condition ParseCondition()
        {
            if (Consume(Token.ID.Odd))
            {
                var Condition = new AST.OddCondition();
                Condition.Right = ParseExpression();
                return Condition;
            }

            return ParseBinaryCondition();
        }

        public AST.BinaryCondition ParseBinaryCondition()
        {
            AST.BinaryCondition Condition;

            var Left = ParseExpression();

            switch (id)
            {
                case Token.ID.Equal:
                    Condition = new AST.EqualCondition();
                    break;
                case Token.ID.NotEqual:
                    Condition = new AST.NotEqualCondition();
                    break;
                case Token.ID.LessThan:
                    Condition = new AST.LessThanCondition();
                    break;
                case Token.ID.LessEqual:
                    Condition = new AST.LessEqualCondition();
                    break;
                case Token.ID.GreaterThan:
                    Condition = new AST.GreaterThanCondition();
                    break;
                case Token.ID.GreaterEqual:
                    Condition = new AST.GreaterEqualCondition();
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

        public AST.Expression ParseExpression()
        {
            AST.Expression Expression;

            if (Consume(Token.ID.Plus))
            {
                Expression = ParseTerm();
            }
            else if (Consume(Token.ID.Minus))
            {
                var subExpression = new AST.NegationExpression();
                subExpression.Right = ParseTerm();
                Expression = (subExpression);
            }
            else
            {
                Expression = ParseTerm();
            }

            while (Match(Token.ID.Plus) || Match(Token.ID.Minus))
            {
                AST.BinaryExpression subExpression;

                if (Match(Token.ID.Plus))
                {
                    subExpression = new AST.AdditionExpression();
                }
                else
                {
                    subExpression = new AST.SubtractionExpression();
                }

                Next();

                subExpression.Left = (Expression);
                subExpression.Right = ParseTerm();
                Expression = (subExpression);
            }

            return Expression;
        }

        public AST.Expression ParseTerm()
        {
            var term = ParseFactor();

            while (Match(Token.ID.Multiply) || Match(Token.ID.Divide))
            {
                AST.BinaryExpression subTerm;

                if (Match(Token.ID.Multiply))
                {
                    subTerm = new AST.MultiplicationExpression();
                }
                else
                {
                    subTerm = new AST.DivisionExpression();
                }

                Next();

                subTerm.Left = (term);
                subTerm.Right = ParseFactor();
                term = (subTerm);
            }

            return term;
        }

        public AST.Expression ParseFactor()
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

        public AST.Number ExtractNumber()
        {
            var value = (int)(lexer.Value??0);
            Next();
            return new AST.Number(value);
        }

        public AST.Identifier ExtractIdentifier()
        {
            var name = lexer.Value as string;
            Next();
            return new AST.Identifier(name);
        }

        protected void Error(string message)
        {
            failed = true;
            writer.WriteLine($"{lexer.Line}: error: {message}");
        }
        public void Skip()
        {
            while (id < Token.ID.Const)
                Next();
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