using PL0.AST;

namespace PL0
{
    public class DotGenerator : Visitor
    {
        public TextWriter Writer => this.writer;
        
        protected TextWriter writer;
        protected ulong id = 0UL;
        protected Stack<ulong> stack = new ();
        public DotGenerator(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Visit(Program program)
        {
            this.WriteLine("digraph G {");
            {
                this.WriteLine("node [shape=oval];");
                this.StartUnlinkedVertex("Program");
                program.Block?.Accept(this);
                this.EndVertex();
            }
            this.WriteLine("}");

        }

        public void Visit(Block block)
        {
            this.StartVertex("Block");

            this.StartVertex("Constants");
            foreach (var constant in block.Constants)
            {
                constant.Accept(this);
            }
            this.EndVertex();

            this.StartVertex("Variables");
            foreach (var variable in block.Variables)
            {
                variable.Accept(this);
            }
            this.EndVertex();

            this.StartVertex("Procedures");
            foreach (var procedure in block.Procedures)
            {
                procedure.Accept(this);
            }
            this.EndVertex();

            block.Statement?.Accept(this);

            this.EndVertex();
        }

        public void Visit(Constant constant)
        {
            this.StartVertex("Constant");
            constant.Identifier?.Accept(this);
            constant.Number?.Accept(this);
            this.EndVertex();
        }

        public void Visit(Procedure procedure)
        {
            this.StartVertex("Procedure");
            procedure.Block?.Accept(this);
            procedure.Identifier?.Accept(this);
            this.EndVertex();
        }

        public void Visit(AssignmentStatement statement)
        {
            this.StartVertex("AssignmentStatement");
            statement.Left?.Accept(this);
            statement.Right?.Accept(this);
            this.EndVertex();
        }

        public void Visit(CallStatement statement)
        {
            this.StartVertex("CallStatement");
            statement.Callee?.Accept(this);
            this.EndVertex();
        }

        public void Visit(ReadStatement statement)
        {
            this.StartVertex("ReadStatement");
            statement.Identifier?.Accept(this);
            this.EndVertex();
        }

        public void Visit(WriteStatement statement)
        {
            this.StartVertex("WriteStatement");
            statement.Expression?.Accept(this);
            this.EndVertex();
        }

        public void Visit(BeginStatement statement)
        {
            this.StartVertex("BeginStatement");
            foreach (var child in statement.Children)
            {
                child?.Accept(this);
            }
            this.EndVertex();
        }

        public void Visit(IfStatement statement)
        {
            this.StartVertex("IfStatement");
            statement.Condition?.Accept(this);
            statement.Statement?.Accept(this);
            this.EndVertex();
        }

        public void Visit(WhileStatement statement)
        {
            this.StartVertex("WhileStatement");
            statement.Condition?.Accept(this);
            statement.Statement?.Accept(this);
            this.EndVertex();
        }

        public void Visit(OddCondition condition)
        {
            this.StartVertex("OddCondition");
            condition.Right?.Accept(this);
            this.EndVertex();
        }

        public void Visit(EqualCondition condition)
        {
            this.StartVertex("EqualCondition");
            condition.Left?.Accept(this);
            condition.Right?.Accept(this);
            this.EndVertex();
        }

        public void Visit(NotEqualCondition condition)
        {
            this.StartVertex("NotEqualCondition");
            condition.Left?.Accept(this);
            condition.Right?.Accept(this);
            this.EndVertex();
        }

        public void Visit(LessThanCondition condition)
        {
            this.StartVertex("LessThanCondition");
            condition.Left?.Accept(this);
            condition.Right?.Accept(this);
            this.EndVertex();
        }

        public void Visit(LessEqualCondition condition)
        {
            this.StartVertex("LessEqualCondition");
            condition.Left?.Accept(this);
            condition.Right?.Accept(this);
            this.EndVertex();
        }

        public void Visit(GreaterThanCondition condition)
        {
            this.StartVertex("GreaterThanCondition");
            condition.Left?.Accept(this);
            condition.Right?.Accept(this);
            this.EndVertex();
        }

        public void Visit(GreaterEqualCondition condition)
        {
            this.StartVertex("GreaterEqualCondition");
            condition.Left?.Accept(this);
            condition.Right?.Accept(this);
            this.EndVertex();
        }

        public void Visit(AdditionExpression expression)
        {
            this.StartVertex("AdditionExpression");
            expression.Left?.Accept(this);
            expression.Right?.Accept(this);
            this.EndVertex();
        }

        public void Visit(SubtractionExpression expression)
        {
            this.StartVertex("SubtractionExpression");
            expression.Left?.Accept(this);
            expression.Right?.Accept(this);
            this.EndVertex();
        }

        public void Visit(MultiplicationExpression expression)
        {
            this.StartVertex("MultiplicationExpression");
            expression.Left?.Accept(this);
            expression.Right?.Accept(this);
            this.EndVertex();
        }

        public void Visit(DivisionExpression expression)
        {
            this.StartVertex("DivisionExpression");
            expression.Left?.Accept(this);
            expression.Right?.Accept(this);
            this.EndVertex();
        }

        public void Visit(NegationExpression expression)
        {
            this.StartVertex("NegationExpression");
            expression.Right?.Accept(this);
            this.EndVertex();
        }

        public void Visit(Identifier identifier)
        {
            this.StartVertex("Identifier");
            this.StartVertex(identifier.Name, "box");
            this.EndVertex();
            this.EndVertex();
        }

        public void Visit(Number number)
        {
            this.StartVertex("Number");

            this.StartVertex(number.Value, "box");
            this.EndVertex();

            this.EndVertex();
        }
        public void Visit(EmptyStatement empty)
        {
            this.StartVertex("Empty");
            this.EndVertex();
        }

        protected void WriteLine(string text)
        {
            this.writer.WriteLine(text);
        }

        protected void StartVertex(string name)
        {
            this.StartUnlinkedVertex(name);
            this.AddEdgeToParent();
        }
        protected void StartVertex(string name, string shape)
        {
            this.StartUnlinkedVertex(name, shape);
            this.AddEdgeToParent();
        }

        protected void StartVertex(int value, string shape)
        {
            this.StartUnlinkedVertex(value, shape);
            this.AddEdgeToParent();
        }
        protected void StartUnlinkedVertex(string name)
        {
            this.stack.Push(id);
            this.WriteLine($"{this.id++} [label=\"{name}\"]");
        }

        protected void StartUnlinkedVertex(string name, string shape)
        {
            this.stack.Push(id);
            this.WriteLine($"{this.id++} [label=\"{name}\" shape={shape}]");
        }

        protected void StartUnlinkedVertex(int value, string shape)
        {
            this.stack.Push(id);
            this.WriteLine($"{this.id++} [label=\"{value}\" shape={shape}]");
        }

        protected void EndVertex()
        {
            if (this.stack.Count > 0)
            {
                this.stack.Pop();
            }
        }

        protected void AddEdgeToParent()
        {
            var array = this.stack.ToArray();
            if (array.Length > 2)
            {
                var pre_last = array[^2];
                var last = array[^1];
                this.WriteLine($"{pre_last} -> {last};");
            }
        }
    }
}
