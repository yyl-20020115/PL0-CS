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
        public readonly TextWriter Writer;
        protected ulong id = 0UL;
        protected Stack<ulong> stack = new ();
        public DotGenerator(TextWriter writer)
        {
            this.Writer = writer;
        }

        public void visit(AST.Program program)
        {
            this.writeLine("digraph G {");
            {
                this.writeLine("node [shape=oval];");
                startUnlinkedVertex("Program");
                program?.Block?.accept(this);
                endVertex();
            }
            this.writeLine("}");

        }

        public void visit(Block block)
        {
            startVertex("Block");

            startVertex("Constants");
            foreach (var constant in block.Constants)
            {
                constant.accept(this);
            }
            endVertex();

            startVertex("Variables");
            foreach (var variable in block.Variables)
            {
                variable.accept(this);
            }
            endVertex();

            startVertex("Procedures");
            foreach (var procedure in block.Procedures)
            {
                procedure.accept(this);
            }
            endVertex();

            block.Statement?.accept(this);

            endVertex();
        }

        public void visit(Constant constant)
        {
            startVertex("Constant");
            constant?.Identifier?.accept(this);
            constant?.Number?.accept(this);
            endVertex();
        }

        public void visit(Procedure procedure)
        {
            startVertex("Procedure");
            procedure?.Block?.accept(this);
            procedure?.Identifier?.accept(this);
            endVertex();
        }

        public void visit(AssignmentStatement statement)
        {
            startVertex("AssignmentStatement");
            statement?.Left?.accept(this);
            statement?.Right?.accept(this);
            endVertex();
        }

        public void visit(CallStatement statement)
        {
            startVertex("CallStatement");
            statement?.Callee?.accept(this);
            endVertex();
        }

        public void visit(ReadStatement statement)
        {
            startVertex("ReadStatement");
            statement?.Identifier?.accept(this);
            endVertex();
        }

        public void visit(WriteStatement statement)
        {
            startVertex("WriteStatement");
            statement?.Expression?.accept(this);
            endVertex();
        }

        public void visit(BeginStatement statement)
        {
            startVertex("BeginStatement");
            foreach (var child in statement.Children)
            {
                child?.accept(this);
            }
            endVertex();
        }

        public void visit(IfStatement statement)
        {
            startVertex("IfStatement");
            statement?.Condition?.accept(this);
            statement?.Statement?.accept(this);
            endVertex();
        }

        public void visit(WhileStatement statement)
        {
            startVertex("WhileStatement");
            statement?.Condition?.accept(this);
            statement?.Statement?.accept(this);
            endVertex();
        }

        public void visit(OddCondition condition)
        {
            startVertex("OddCondition");
            condition?.Right?.accept(this);
            endVertex();
        }

        public void visit(EqualCondition condition)
        {
            startVertex("EqualCondition");
            condition?.Left?.accept(this);
            condition?.Right?.accept(this);
            endVertex();
        }

        public void visit(NotEqualCondition condition)
        {
            startVertex("NotEqualCondition");
            condition?.Left?.accept(this);
            condition?.Right?.accept(this);
            endVertex();
        }

        public void visit(LessThanCondition condition)
        {
            startVertex("LessThanCondition");
            condition?.Left?.accept(this);
            condition?.Right?.accept(this);
            endVertex();
        }

        public void visit(LessEqualCondition condition)
        {
            startVertex("LessEqualCondition");
            condition?.Left?.accept(this);
            condition?.Right?.accept(this);
            endVertex();
        }

        public void visit(GreaterThanCondition condition)
        {
            startVertex("GreaterThanCondition");
            condition?.Left?.accept(this);
            condition?.Right?.accept(this);
            endVertex();
        }

        public void visit(GreaterEqualCondition condition)
        {
            startVertex("GreaterEqualCondition");
            condition?.Left?.accept(this);
            condition?.Right?.accept(this);
            endVertex();
        }

        public void visit(AdditionExpression expression)
        {
            startVertex("AdditionExpression");
            expression?.Left?.accept(this);
            expression?.Right?.accept(this);
            endVertex();
        }

        public void visit(SubtractionExpression expression)
        {
            startVertex("SubtractionExpression");
            expression?.Left?.accept(this);
            expression?.Right?.accept(this);
            endVertex();
        }

        public void visit(MultiplicationExpression expression)
        {
            startVertex("MultiplicationExpression");
            expression?.Left?.accept(this);
            expression?.Right?.accept(this);
            endVertex();
        }

        public void visit(DivisionExpression expression)
        {
            startVertex("DivisionExpression");
            expression?.Left?.accept(this);
            expression?.Right?.accept(this);
            endVertex();
        }

        public void visit(NegationExpression expression)
        {
            startVertex("NegationExpression");
            expression?.Right?.accept(this);
            endVertex();
        }

        public void visit(Identifier identifier)
        {
            startVertex("Identifier");
            startVertex(identifier.Name, "box");
            endVertex();
            endVertex();
        }

        public void visit(Number number)
        {
            startVertex("Number");

            startVertex(number.Value, "box");
            endVertex();

            endVertex();
        }
        public void visit(EmptyStatement empty)
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
            this.Writer.WriteLine(text);
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
