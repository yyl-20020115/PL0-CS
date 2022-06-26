using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0.AST
{
    public abstract class BinaryCondition : Condition
    {
        public Expression? Left { get; set; }
        public Expression? Right { get; set; }

    }
}
