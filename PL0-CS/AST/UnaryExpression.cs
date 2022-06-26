﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0.AST
{
    public abstract class UnaryExpression : Expression
    {
        public Expression? Right { get; set; }
        public abstract void accept(Visitor visitor);
    }
}
