using PL0.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0
{
    public class DotGenerator :AST.Visitor
    {
        public TextWriter Writer=>this.writer;
        
        protected TextWriter writer;
        protected ulong id = 0UL;
        protected Stack<ulong> stack = new ();
        public DotGenerator(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Visit(AST.Program program)
        {
            this.writeLine("digraph G {");
            {
                this.writeLine("node [shape=oval];");
                startUnlinkedVertex("Program");
                program?.Block?.Accept(this);
                endVertex();
            }
            this.writeLine("}");

        }

        public void Visit(Block block)
        {
            startVertex("Block");

            startVertex("Constants");
            foreach (var constant in block.Constants)
            {
                constant.Accept(this);
            }
            endVertex();

            startVertex("Variables");
            foreach (var variable in block.Variables)
            {
                variable.Accept(this);
            }
            endVertex();

            startVertex("Procedures");
            foreach (var procedure in block.Procedures)
            {
                procedure.Accept(this);
            }
            endVertex();

            block.Statement?.Accept(this);

            endVertex();
        }

        public void Visit(Constant constant)
        {
            startVertex("Constant");
            constant?.Identifier?.Accept(this);
            constant?.Number?.Accept(this);
            endVertex();
        }

        public void Visit(Procedure procedure)
        {
            startVertex("Procedure");
            procedure?.Block?.Accept(this);
            procedure?.Identifier?.Accept(this);
            endVertex();
        }

        public void Visit(AssignmentStatement statement)
        {
            startVertex("AssignmentStatement");
            statement?.Left?.Accept(this);
            statement?.Right?.Accept(this);
            endVertex();
        }

        public void Visit(CallStatement statement)
        {
            startVertex("CallStatement");
            statement?.Callee?.Accept(this);
            endVertex();
        }

        public void Visit(ReadStatement statement)
        {
            startVertex("ReadStatement");
            statement?.Identifier?.Accept(this);
            endVertex();
        }

        public void Visit(WriteStatement statement)
        {
            startVertex("WriteStatement");
            statement?.Expression?.Accept(this);
            endVertex();
        }

        public void Visit(BeginStatement statement)
        {
            startVertex("BeginStatement");
            foreach (var child in statement.Children)
            {
                child?.Accept(this);
            }
            endVertex();
        }

        public void Visit(IfStatement statement)
        {
            startVertex("IfStatement");
            statement?.Condition?.Accept(this);
            statement?.Statement?.Accept(this);
            endVertex();
        }

        public void Visit(WhileStatement statement)
        {
            startVertex("WhileStatement");
            statement?.Condition?.Accept(this);
            statement?.Statement?.Accept(this);
            endVertex();
        }

        public void Visit(OddCondition condition)
        {
            startVertex("OddCondition");
            condition?.Right?.Accept(this);
            endVertex();
        }

        public void Visit(EqualCondition condition)
        {
            startVertex("EqualCondition");
            condition?.Left?.Accept(this);
            condition?.Right?.Accept(this);
            endVertex();
        }

        public void Visit(NotEqualCondition condition)
        {
            startVertex("NotEqualCondition");
            condition?.Left?.Accept(this);
            condition?.Right?.Accept(this);
            endVertex();
        }

        public void Visit(LessThanCondition condition)
        {
            startVertex("LessThanCondition");
            condition?.Left?.Accept(this);
            condition?.Right?.Accept(this);
            endVertex();
        }

        public void Visit(LessEqualCondition condition)
        {
            startVertex("LessEqualCondition");
            condition?.Left?.Accept(this);
            condition?.Right?.Accept(this);
            endVertex();
        }

        public void Visit(GreaterThanCondition condition)
        {
            startVertex("GreaterThanCondition");
            condition?.Left?.Accept(this);
            condition?.Right?.Accept(this);
            endVertex();
        }

        public void Visit(GreaterEqualCondition condition)
        {
            startVertex("GreaterEqualCondition");
            condition?.Left?.Accept(this);
            condition?.Right?.Accept(this);
            endVertex();
        }

        public void Visit(AdditionExpression expression)
        {
            startVertex("AdditionExpression");
            expression?.Left?.Accept(this);
            expression?.Right?.Accept(this);
            endVertex();
        }

        public void Visit(SubtractionExpression expression)
        {
            startVertex("SubtractionExpression");
            expression?.Left?.Accept(this);
            expression?.Right?.Accept(this);
            endVertex();
        }

        public void Visit(MultiplicationExpression expression)
        {
            startVertex("MultiplicationExpression");
            expression?.Left?.Accept(this);
            expression?.Right?.Accept(this);
            endVertex();
        }

        public void Visit(DivisionExpression expression)
        {
            startVertex("DivisionExpression");
            expression?.Left?.Accept(this);
            expression?.Right?.Accept(this);
            endVertex();
        }

        public void Visit(NegationExpression expression)
        {
            startVertex("NegationExpression");
            expression?.Right?.Accept(this);
            endVertex();
        }

        public void Visit(Identifier identifier)
        {
            startVertex("Identifier");
            startVertex(identifier.Name, "box");
            endVertex();
            endVertex();
        }

        public void Visit(Number number)
        {
            startVertex("Number");

            startVertex(number.Value, "box");
            endVertex();

            endVertex();
        }
        public void Visit(EmptyStatement empty)
        {
            startVertex("Empty");
            endVertex();
        }


        protected void startVertex(string name)
        {
            startUnlinkedVertex(name);
            addEdgeToParent();
        }
        protected void startVertex(string name, string shape)
        {
            startUnlinkedVertex(name, shape);
            addEdgeToParent();
        }

        protected void startVertex(int value, string shape)
        {
            startUnlinkedVertex(value, shape);
            addEdgeToParent();

        }
        protected void writeLine(string text)
        {
            this.writer.WriteLine(text);
        }
        protected void startUnlinkedVertex(string name)
        {
            this.stack.Push(id);
            this.writeLine($"{this.id++} [label=\"{name}\"]");
        }

        protected void startUnlinkedVertex(string name, string shape)
        {
            this.stack.Push(id);
            this.writeLine($"{this.id++} [label=\"{name}\" shape={shape}]");
        }

        protected void startUnlinkedVertex(int value, string shape)
        {
            this.stack.Push(id);
            this.writeLine($"{this.id++} [label=\"{value}\" shape={shape}]");
        }

        protected void endVertex()
        {
            if (this.stack.Count > 0)
            {
                this.stack.Pop();
            }
        }

        protected void addEdgeToParent()
        {
            if (this.stack.Count > 0)
            {
                var array = this.stack.ToArray();
                var pre_last = array[array.Length-2];
                var last = array[array.Length - 1];
                this.writeLine($"{pre_last} -> {last};");
            }
        }

    }
}
