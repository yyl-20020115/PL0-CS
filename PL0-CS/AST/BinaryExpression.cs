using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0.AST
{
    public abstract class BinaryExpression : Expression
    {
        public Expression? Left { get; set; }
        public Expression? Right { get; set; }
        public abstract void Accept(Visitor visitor);

    }
}
