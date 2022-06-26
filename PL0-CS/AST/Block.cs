using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0.AST
{
    public class Block : Node
    {
        public List<Constant> Constants { get; } = new();
        public List<Identifier> Variables { get; } = new();
        public List<Procedure> Procedures { get; } = new();
        public Statement? Statement { get; set; }
        public void Accept(Visitor visitor)
            => visitor.Visit(this);
    }
}
