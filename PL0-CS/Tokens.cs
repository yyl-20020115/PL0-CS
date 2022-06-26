using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0.Token
{
    public enum ID : uint
    {
        // Weak symbols

        // Special markers
        Unknown,

        // Keywords
        Then,
        Do,

        // Operators and punctuation
        Assign,
        Equal,
        NotEqual,
        LessThan,
        LessEqual,
        GreaterThan,
        GreaterEqual,
        Plus,
        Minus,
        Multiply,
        Divide,
        LParen,
        RParen,

        // Identifiers and literals
        Number,
        Identifier,

        // Strong symbols

        // Keywords
        Const,
        Var,
        Procedure,
        Call,
        Begin,
        End,
        If,
        While,
        Odd,

        // Operators and punctuation
        Period,
        Comma,
        Semicolon,
        Read,
        Write,

        // Special markers
        EndOfFile,
    };

}
