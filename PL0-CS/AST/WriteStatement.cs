using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0.AST
{
    public class WriteStatement : Statement
    {
        public void Accept(Visitor visitor)
            => visitor.Visit(this);
        public Expression? Expression { get; set; }
    }
}
