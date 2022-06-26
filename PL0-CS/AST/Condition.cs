using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0.AST
{
    public abstract class Condition : Node
    {
        public abstract void accept(Visitor visitor);
    }
}
