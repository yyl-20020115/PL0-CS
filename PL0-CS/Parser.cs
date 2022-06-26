using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0
{
    public class Parser
    {
        protected TextWriter diagnostic;
        protected TextReader reader;
        protected Lexer lex;
        protected Token.ID id = Token.ID.EndOfFile;
        protected bool failed = false;

        public Parser(TextReader reader, TextWriter diagnostic)
        {
            this.lex = new(this.reader = reader, this.diagnostic = diagnostic);
        }
        
        public AST.Program GetResult()
        {
            var program = parseProgram();

            if (!match(Token.ID.EndOfFile))
            {
                error("junk after end of program");
            }

            if (failed)
            {
                return new();
            }

            return program;

        }
        public AST.Program parseProgram()
        {
            var program = parseProgram();

            if (!match(Token.ID.EndOfFile))
            {
                error("junk after end of program");
            }

            if (failed)
            {
                return new();
            }

            return program;
        }
        public AST.Block parseBlock()
        {
            var Block = new AST.Block();

            if (consume(Token.ID.Const))
            {
                do
                {
                    var constant = new AST.Constant();

                    if (match(Token.ID.Identifier))
                    {
                        constant.Identifier = extractIdentifier();
                    }
                    else
                    {
                        error("Identifier expected for const name");
                        skip();
                        continue;
                    }

                    if (!consume(Token.ID.Equal))
                    {
                        error("missing '=' after const Identifier");
                        skip();
                        continue;
                    }

                    if (match(Token.ID.Number))
                    {
                        constant.Number = extractNumber();
                    }
                    else
                    {
                        error("Number expected for const value");
                        skip();
                        continue;
                    }

                    Block.Constants.Add((constant));
                } while (consume(Token.ID.Comma));

                if (!consume(Token.ID.Semicolon))
                {
                    error("missing ';' after const definitions");
                    skip();
                }
            }

            if (consume(Token.ID.Var))
            {
                do
                {
                    if (match(Token.ID.Identifier))
                    {
                        Block.Variables.Add(extractIdentifier());
                    }
                    else
                    {
                        error("Identifier expected for var name");
                        skip();
                        continue;
                    }
                } while (consume(Token.ID.Comma));

                if (!consume(Token.ID.Semicolon))
                {
                    error("missing ';' after var declarations");
                    skip();
                }
            }

            while (consume(Token.ID.Procedure))
            {
                var procedure = new AST.Procedure();

                if (match(Token.ID.Identifier))
                {
                    procedure.Identifier = extractIdentifier();
                }
                else
                {
                    error("Identifier expected for procedure name");
                    skip();
                    continue;
                }

                if (!consume(Token.ID.Semicolon))
                {
                    error("missing ';' after procedure name");
                    skip();
                    continue;
                }

                procedure.Block = parseBlock();

                if (!consume(Token.ID.Semicolon))
                {
                    error("missing ';' after procedure Block");
                    skip();
                    continue;
                }

                Block.Procedures.Add((procedure));
            }

            Block.Statement = parseStatement();

            return Block;
        }

        public AST.Statement parseStatement()
        {
            switch (id)
            {
                case Token.ID.Identifier:
                    {
                        var Statement = new AST.AssignmentStatement();

                        Statement.Left = extractIdentifier();

                        if (!consume(Token.ID.Assign))
                        {
                            error("missing ':=' after Identifier");
                            skip();
                        }

                        Statement.Right = parseExpression();

                        return Statement;
                    }
                case Token.ID.Call:
                    {
                        next();

                        var Statement = new AST.CallStatement();

                        if (match(Token.ID.Identifier))
                        {
                            Statement.Callee = extractIdentifier();
                        }
                        else
                        {
                            error("Identifier expected after keyword \"call\"");
                            skip();
                        }

                        return Statement;
                    }
                case Token.ID.Read:
                    {
                        next();

                        var Statement = new AST.ReadStatement();

                        if (match(Token.ID.Identifier))
                        {
                            Statement.Identifier = extractIdentifier();
                        }
                        else
                        {
                            error("Identifier expected after '?'");
                            skip();
                        }

                        return Statement;
                    }
                case Token.ID.Write:
                    {
                        next();
                        var Statement = new AST.WriteStatement();
                        Statement.Expression = parseExpression();
                        return Statement;
                    }
                case Token.ID.Begin:
                    {
                        next();

                        var Statement = new AST.BeginStatement();

                        do
                        {
                            Statement.Children.Add(parseStatement());
                        } while (consume(Token.ID.Semicolon));

                        if (!consume(Token.ID.End))
                        {
                            error("expected keyword \"end\" after begin Statement");
                            skip();
                        }

                        return Statement;
                    }
                case Token.ID.If:
                    {
                        next();

                        var Statement = new AST.IfStatement();

                        Statement.Condition = parseCondition();

                        if (!consume(Token.ID.Then))
                        {
                            error("missing keyword \"then\" after if-Condition");
                            skip();
                        }

                        Statement.Statement = parseStatement();

                        return Statement;
                    }
                case Token.ID.While:
                    {
                        next();

                        var Statement = new AST.WhileStatement();

                        Statement.Condition = parseCondition();

                        if (!consume(Token.ID.Do))
                        {
                            error("missing keyword \"do\" after while-Condition");
                            skip();
                        }

                        Statement.Statement = parseStatement();

                        return Statement;
                    }
                default:
                    {
                        return new AST.EmptyStatement();
                    }
            }
        }

        public AST.Condition parseCondition()
        {
            if (consume(Token.ID.Odd))
            {
                var Condition = new AST.OddCondition();
                Condition.Right = parseExpression();
                return Condition;
            }

            return parseBinaryCondition();
        }

        public AST.BinaryCondition parseBinaryCondition()
        {
            AST.BinaryCondition Condition;

            var Left = parseExpression();

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
                    error("expected a Conditional operator");
                    skip();
                    return null;
            }

            next();

            Condition.Left = (Left);
            Condition.Right = parseExpression();

            return Condition;
        }

        public AST.Expression parseExpression()
        {
            AST.Expression Expression;

            if (consume(Token.ID.Plus))
            {
                Expression = parseTerm();
            }
            else if (consume(Token.ID.Minus))
            {
                var subExpression = new AST.NegationExpression();
                subExpression.Right = parseTerm();
                Expression = (subExpression);
            }
            else
            {
                Expression = parseTerm();
            }

            while (match(Token.ID.Plus) || match(Token.ID.Minus))
            {
                AST.BinaryExpression subExpression;

                if (match(Token.ID.Plus))
                {
                    subExpression = new AST.AdditionExpression();
                }
                else
                {
                    subExpression = new AST.SubtractionExpression();
                }

                next();

                subExpression.Left = (Expression);
                subExpression.Right = parseTerm();
                Expression = (subExpression);
            }

            return Expression;
        }

        public AST.Expression parseTerm()
        {
            var term = parseFactor();

            while (match(Token.ID.Multiply) || match(Token.ID.Divide))
            {
                AST.BinaryExpression subTerm;

                if (match(Token.ID.Multiply))
                {
                    subTerm = new AST.MultiplicationExpression();
                }
                else
                {
                    subTerm = new AST.DivisionExpression();
                }

                next();

                subTerm.Left = (term);
                subTerm.Right = parseFactor();
                term = (subTerm);
            }

            return term;
        }

        public AST.Expression parseFactor()
        {
            switch (id)
            {
                case Token.ID.Identifier:
                    {
                        return extractIdentifier();
                    }
                case Token.ID.Number:
                    {
                        return extractNumber();
                    }
                case Token.ID.LParen:
                    {
                        next();

                        var Expression = parseExpression();

                        if (!consume(Token.ID.RParen))
                        {
                            error("missing ')' after Expression");
                            skip();
                        }

                        return Expression;
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        public AST.Number extractNumber()
        {
            var value = (int)(lex.Value??0);
            next();
            return new AST.Number(value);
        }

        public AST.Identifier extractIdentifier()
        {
            var name = lex.Value as string;
            next();
            return new AST.Identifier(name);
        }


        protected void error(string message)
        {
            failed = true;
            diagnostic.WriteLine($"{lex.Line}: error: {message}");
        }
        public void skip()
        {
            while (id < Token.ID.Const)
            {
                next();
            }

        }

        public bool match(Token.ID expected) => id == expected;

        public bool consume(Token.ID expected)
        {
            if (match(expected))
            {
                next();
                return true;
            }

            return false;
        }

        public void next()
        {
            this.id = lex.NextToken();
        }
    }
}