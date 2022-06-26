using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0.AST
{
    public class BeginStatement : Statement
    {
        public List<Statement> Children { get; } = new();
        public void accept(Visitor visitor) => visitor.visit(this);
    }
}
