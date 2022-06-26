using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0.AST
{
    public class Number : Expression
    {
        public int Value { get; set; }

        public void Accept(Visitor visitor)
            => visitor.Visit(this);
        public Number(int value)
        {
            Value = value;  
        }
    }
}
