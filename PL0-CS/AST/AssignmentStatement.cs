using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0.AST
{
    public class AssignmentStatement : Statement
    {
        public Identifier? Left { get; set; }
        public Expression? Right { get; set; }
        public virtual void Accept(Visitor visitor) => visitor.Visit(this);
    }
}
