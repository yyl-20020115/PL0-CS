using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0.AST
{
    public class Constant : Node
    {
        public Identifier? Identifier { get; set; }
        public Number? Number { get; set; } 
        
        public void Accept(Visitor visitor)
            => visitor.Visit(this);
    }
}
