using PL0.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0
{
    public class Scope
    {
        public Dictionary<string, int> Constants { get; } = new();
        public Dictionary<string, int> Variables { get; } = new();
        public Dictionary<string, AST.Procedure> Procedures { get; } = new();
    }

    public class Evaluator :AST.Visitor
    {
        protected bool consequence = false;
        protected TextReader _reader;
        protected TextWriter _writer;
        protected Stack<int> stack { get; } = new();
        protected List<Scope> scopes = new();
        public Evaluator(TextReader reader, TextWriter writer)
        {
            this._reader = reader;
            this._writer = writer;   
        }
        public void visit(AST.Program program)
        {
            program?.Block?.accept(this);
        }

        public void visit(AST.Block block)
        {
            foreach (var  constant in block.Constants)
            {
                constant?.accept(this);
            }

            foreach (var  variable in block.Variables)
            {
                var  scope = scopes.Last();
                scope.Variables[variable?.Name??""]= 0;
            }

            foreach (var  procedure in block.Procedures)
            {
                if (procedure != null)
                {
                    var scope = scopes.Last();
                    scope.Procedures[procedure?.Identifier?.Name ?? ""] = procedure!;
                }
            }

            block?.Statement?.accept(this);
        }

        public void visit(AST.Constant constant)
        {
            var  scope = scopes.Last();
            scope.Constants[constant.Identifier?.Name??""] = constant!.Number!.Value;
        }

        public void visit(AST.Procedure procedure)
        {
            //scopes.emplace_back();
            var scope = new Scope();
            scopes.Add(scope);
            procedure.Block?.accept(this);
            scopes.RemoveAt(scopes.Count - 1);
        }

        public void visit(AST.AssignmentStatement statement)
        {

            var sv = new List<Scope>(this.scopes);
            sv.Reverse();

            foreach (var  scope in sv)
            {
                if (scope.Variables.TryGetValue(statement.Left?.Name??"",out var s))
                {
                    statement.Right?.accept(this);
                    scope.Variables[statement.Left?.Name??""] = stack.Peek();
                    return;
                }
            }

            throw new Exception("unrecognized variable name \"" + statement.Left?.Name + "\" during assignment");
        }

        public void visit(AST.CallStatement statement)
        {
            var sv = new List<Scope>(this.scopes);
            sv.Reverse();

            foreach (var scope in sv) {
                if (scope.Procedures.TryGetValue(statement.Callee?.Name??"",out var p))
                {
                    p.accept(this);
                    return;
                }
            }

            throw new Exception("unrecognized procedure name \"" + statement.Callee?.Name + "\" during call");
        }

        public void visit(AST.ReadStatement statement)
        {
            var sv = new List<Scope>(this.scopes);
            sv.Reverse();

            foreach (var  scope in sv)
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

        public void visit(AST.WriteStatement statement)
        {
            statement.Expression?.accept(this);
            
            this._writer.WriteLine(stack.Peek());
        }

        public void visit(AST.BeginStatement statement)
        {
            foreach (var  child in statement.Children)
            {
                child?.accept(this);
            }
        }

        public void visit(AST.IfStatement statement)
        {
            statement.Condition?.accept(this);
            if (consequence)
            {
                statement.Statement?.accept(this);
            }
        }

        public void visit(AST.WhileStatement statement)
        {
            statement.Condition?.accept(this);
            while (consequence)
            {
                statement.Statement?.accept(this);
                statement.Condition?.accept(this);
            }
        }

        public void visit(AST.OddCondition Condition)
        {
            Condition.Right?.accept(this);
            consequence = 0!=(stack.Peek() & 1);
            stack.Pop();
        }

        public void visit(AST.EqualCondition Condition)
        {
            Condition.Left?.accept(this);
            Condition.Right?.accept(this);

            var right = stack.Peek();
            stack.Pop();
            consequence = stack.Peek() == right;
            stack.Pop();
        }

        public void visit(AST.NotEqualCondition Condition)
        {
            Condition.Left?.accept(this);
            Condition.Right?.accept(this);

            var right = stack.Peek();
            stack.Pop();
            consequence = stack.Peek() != right;
            stack.Pop();
        }

        public void visit(AST.LessThanCondition Condition)
        {
            Condition.Left?.accept(this);
            Condition.Right?.accept(this);

            var right = stack.Peek();
            stack.Pop();
            consequence = stack.Peek() < right;
            stack.Pop();
        }

        public void visit(AST.LessEqualCondition Condition)
        {
            Condition.Left?.accept(this);
            Condition.Right?.accept(this);

            var right = stack.Peek();
            stack.Pop();
            consequence = stack.Peek() <= right;
            stack.Pop();
        }

        public void visit(AST.GreaterThanCondition Condition)
        {
            Condition.Left?.accept(this);
            Condition.Right?.accept(this);

            var right = stack.Peek();
            stack.Pop();
            consequence = stack.Peek() > right;
            stack.Pop();
        }

        public void visit(AST.GreaterEqualCondition Condition)
        {
            Condition.Left?.accept(this);
            Condition.Right?.accept(this);

             var right = stack.Peek();
            stack.Pop();
            consequence = stack.Peek() >= right;
            stack.Pop();
        }

        public void visit(AST.AdditionExpression Expression)
        {
            Expression.Left?.accept(this);
            Expression.Right?.accept(this);

             var right = stack.Peek();
            stack.Pop();
            int v = stack.Pop();
            v += right;
            stack.Push(v);
        }

        public void visit(AST.SubtractionExpression Expression)
        {
            Expression.Left?.accept(this);
            Expression.Right?.accept(this);

             var right = stack.Peek();
            stack.Pop();
            int v = stack.Pop();
            v -= right;
            stack.Push(v);
        }

        public void visit(AST.MultiplicationExpression Expression)
        {
            Expression.Left?.accept(this);
            Expression.Right?.accept(this);

             var right = stack.Peek();
            stack.Pop();
            int v = stack.Pop();
            v *= right;
            stack.Push(v);
        }

        public void visit(AST.DivisionExpression Expression)
        {
            Expression.Left?.accept(this);
            Expression.Right?.accept(this);

             var right = stack.Peek();
            stack.Pop();
            int v = stack.Pop();
            v /= right;
            stack.Push(v);
        }

        public void visit(AST.NegationExpression Expression)
        {
            Expression.Right?.accept(this);
            int v = stack.Pop();
            v = -v;
            stack.Push(v);

        }

        public void visit(AST.Identifier Identifier)
        {
            var sv = new List<Scope>(this.scopes);
            sv.Reverse();

            foreach ( var scope in sv) {
                if (scope.Constants.TryGetValue(Identifier?.Name??"",out var value))
                {
                    stack.Push(value);
                    return;
                }

                if (scope.Variables.TryGetValue(Identifier.Name,out var value2))
                {
                    stack.Push(value2);
                    return;
                }
            }

            throw new Exception( "unrecognized symbol name \"" + Identifier.Name + '"');
        }

        public void visit(AST.Number number)
        {
            stack.Push(number.Value);
        }

        public void visit(EmptyStatement empty)
        {
        }
    }
}
