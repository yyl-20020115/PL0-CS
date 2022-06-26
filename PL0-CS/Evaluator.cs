using PL0.AST;

namespace PL0
{
    public class Scope
    {
        public Dictionary<string, int> Constants { get; } = new();
        public Dictionary<string, int> Variables { get; } = new();
        public Dictionary<string, Procedure> Procedures { get; } = new();
    }

    public class Evaluator : Visitor
    {
        protected bool consequence = false;
        protected TextReader reader;
        protected TextWriter writer;
        protected Stack<int> stack = new();
        protected List<Scope> scopes = new();
        public TextWriter Writer => this.writer;
        public TextReader Reader => this.reader;
        public Stack<int> Stack => this.stack;
        public List<Scope> Scopes => this.scopes;
        public Evaluator(TextReader reader, TextWriter writer)
        {
            this.reader = reader;
            this.writer = writer;   
        }
        public void Visit(Program program)
        {
            program.Block?.Accept(this);
        }

        public void Visit(Block block)
        {
            foreach (var constant in block.Constants)
            {
                constant?.Accept(this);
            }

            foreach (var variable in block.Variables)
            {
                var scope = scopes.LastOrDefault();
                if (scope != null)
                {
                    scope.Variables[variable?.Name ?? ""] = 0;
                }
            }

            foreach (var procedure in block.Procedures)
            {
                if (procedure != null)
                {
                    var scope = scopes.LastOrDefault();
                    if (scope != null)
                    {
                        scope.Procedures[procedure?.Identifier?.Name ?? ""] = procedure!;
                    }
                }
            }

            block.Statement?.Accept(this);
        }

        public void Visit(Constant constant)
        {
            var  scope = scopes.Last();
            scope.Constants[constant.Identifier?.Name??""] = constant!.Number!.Value;
        }

        public void Visit(Procedure procedure)
        {
            var scope = new Scope();
            scopes.Add(scope);
            procedure.Block?.Accept(this);
            scopes.RemoveAt(scopes.Count - 1);
        }

        public void Visit(AssignmentStatement statement)
        {
            var sv = new List<Scope>(this.scopes);
            sv.Reverse();

            foreach (var scope in sv)
            {
                if (scope.Variables.TryGetValue(statement.Left?.Name??"",out var s))
                {
                    statement.Right?.Accept(this);
                    scope.Variables[statement.Left?.Name??""] = stack.Peek();
                    return;
                }
            }

            throw new Exception("unrecognized variable name \"" + statement.Left?.Name + "\" during assignment");
        }

        public void Visit(CallStatement statement)
        {
            var sv = new List<Scope>(this.scopes);
            sv.Reverse();

            foreach (var scope in sv) 
            {
                if (scope.Procedures.TryGetValue(statement.Callee?.Name??"",out var p))
                {
                    p.Accept(this);
                    return;
                }
            }

            throw new Exception($"unrecognized procedure name \"{ statement.Callee?.Name }\" during call");
        }

        public void Visit(AST.ReadStatement statement)
        {
            var sv = new List<Scope>(this.scopes);
            sv.Reverse();

            foreach (var scope in sv)
            {
                if (scope.Variables.TryGetValue(statement.Identifier?.Name??"",out var value))
                {
                    var text = Console.ReadLine();
                    int.TryParse(text, out value);
                    scope.Variables[statement.Identifier?.Name ?? ""] = value;

                    return;
                }
            }

            throw new Exception("unrecognized variable name \"" + statement.Identifier?.Name + "\" during call");
        }

        public void Visit(WriteStatement statement)
        {
            statement.Expression?.Accept(this);
            
            this.writer.WriteLine(stack.Peek());
        }

        public void Visit(BeginStatement statement)
        {
            foreach (var child in statement.Children)
            {
                child?.Accept(this);
            }
        }

        public void Visit(IfStatement statement)
        {
            statement.Condition?.Accept(this);
            if (consequence)
            {
                statement.Statement?.Accept(this);
            }
        }

        public void Visit(WhileStatement statement)
        {
            statement.Condition?.Accept(this);
            while (consequence)
            {
                statement.Statement?.Accept(this);
                statement.Condition?.Accept(this);
            }
        }

        public void Visit(OddCondition Condition)
        {
            Condition.Right?.Accept(this);
            consequence = 0!=(stack.Peek() & 1);
            stack.Pop();
        }

        public void Visit(EqualCondition Condition)
        {
            Condition.Left?.Accept(this);
            Condition.Right?.Accept(this);

            var right = stack.Peek();
            this.stack.Pop();
            consequence = stack.Peek() == right;
            this.stack.Pop();
        }

        public void Visit(NotEqualCondition Condition)
        {
            Condition.Left?.Accept(this);
            Condition.Right?.Accept(this);

            var right = stack.Peek();
            this.stack.Pop();
            consequence = stack.Peek() != right;
            this.stack.Pop();
        }

        public void Visit(LessThanCondition Condition)
        {
            Condition.Left?.Accept(this);
            Condition.Right?.Accept(this);

            var right = stack.Peek();
            this.stack.Pop();
            consequence = stack.Peek() < right;
            this.stack.Pop();
        }

        public void Visit(LessEqualCondition Condition)
        {
            Condition.Left?.Accept(this);
            Condition.Right?.Accept(this);

            var right = stack.Peek();
            this.stack.Pop();
            consequence = stack.Peek() <= right;
            this.stack.Pop();
        }

        public void Visit(GreaterThanCondition Condition)
        {
            Condition.Left?.Accept(this);
            Condition.Right?.Accept(this);

            var right = stack.Peek();
            this.stack.Pop();
            consequence = stack.Peek() > right;
            this.stack.Pop();
        }

        public void Visit(GreaterEqualCondition Condition)
        {
            Condition.Left?.Accept(this);
            Condition.Right?.Accept(this);

             var right = stack.Peek();
            this.stack.Pop();
            consequence = stack.Peek() >= right;
            this.stack.Pop();
        }

        public void Visit(AdditionExpression Expression)
        {
            Expression.Left?.Accept(this);
            Expression.Right?.Accept(this);

            var right = stack.Peek();
            this.stack.Pop();
            int v = stack.Pop();
            v += right;
            this.stack.Push(v);
        }

        public void Visit(SubtractionExpression Expression)
        {
            Expression.Left?.Accept(this);
            Expression.Right?.Accept(this);

            var right = stack.Peek();
            this.stack.Pop();
            int v = stack.Pop();
            v -= right;
            this.stack.Push(v);
        }

        public void Visit(MultiplicationExpression Expression)
        {
            Expression.Left?.Accept(this);
            Expression.Right?.Accept(this);

            var right = stack.Peek();
            this.stack.Pop();
            int v = stack.Pop();
            v *= right;
            this.stack.Push(v);
        }

        public void Visit(DivisionExpression Expression)
        {
            Expression.Left?.Accept(this);
            Expression.Right?.Accept(this);

            var right = stack.Peek();
            this.stack.Pop();
            int v = stack.Pop();
            v /= right;
            this.stack.Push(v);
        }

        public void Visit(NegationExpression Expression)
        {
            Expression.Right?.Accept(this);
            int v = stack.Pop();
            v = -v;
            this.stack.Push(v);
        }

        public void Visit(Identifier Identifier)
        {
            var sv = new List<Scope>(this.scopes);
            sv.Reverse();

            foreach (var scope in sv) {
                if (scope.Constants.TryGetValue(Identifier?.Name??"",out var value))
                {
                    this.stack.Push(value);
                    return;
                }

                if (scope.Variables.TryGetValue(Identifier?.Name??"",out var value2))
                {
                    this.stack.Push(value2);
                    return;
                }
            }

            throw new Exception( "unrecognized symbol name \"" + Identifier.Name + '"');
        }

        public void Visit(Number number)
        {
            this.stack.Push(number.Value);
        }

        public void Visit(EmptyStatement empty)
        {
        }
    }
}
