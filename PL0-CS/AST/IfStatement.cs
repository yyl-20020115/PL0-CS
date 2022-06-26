using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0.AST
{
    public class IfStatement : Statement
    {
        public Condition? Condition { get; set; }
        public Statement? Statement { get; set; }
        public void Accept(Visitor visitor)
            => visitor.Visit(this);
    }
}
