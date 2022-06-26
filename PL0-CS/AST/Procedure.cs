using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0.AST
{
    public class Procedure : Node
    {
        public Identifier? Identifier { get; set; }
        public Block? Block { get; set; }
        public void accept(Visitor visitor)
            => visitor.visit(this);
    }
}
