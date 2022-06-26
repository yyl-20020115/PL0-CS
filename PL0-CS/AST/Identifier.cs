using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0.AST
{
    public class Identifier : Expression
    {
        public string Name { get; set; } = "";

        public void accept(Visitor visitor)
            => visitor.visit(this);
        public Identifier(string name)
        {
            Name = name;
        }
    }
}
