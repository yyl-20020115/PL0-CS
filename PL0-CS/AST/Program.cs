using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0.AST
{
    public class Program : Node
    {
        public Block? Block { get; set; }
        public void accept(Visitor visitor)
            => visitor.visit(this);
    }
}
