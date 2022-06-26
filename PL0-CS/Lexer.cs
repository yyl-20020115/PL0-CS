using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL0
{
    public class Lexer
    {
        protected TextReader reader;
        protected TextWriter writer;
        protected int cursor = 0;
        protected int lexeme = 0;
        protected int line = 0;
        protected int column = 0;
        protected object? value = null;
        protected string text = "";
        protected StringBuilder buffer = new();
        public TextWriter Writer => this.writer;
        public TextReader Reader => this.reader;
        public int Cursor => this.cursor;
        public int Lexeme => this.lexeme;
        public int Line => this.line;
        public int Column => this.column;
        public object? Value => this.value;
        public Lexer(TextReader reader, TextWriter writer)
        {
            this.writer = writer;
            this.reader = reader;
        }
        protected char GetCurrentChar()
        {
            //*++cursor
            this.reader.Read();
            var i = this.reader.Peek();
            var c = (char)i;
            if (i >= 0)
            {
                buffer.Append(c);
            }
            return c;
        }
        protected void CaptureText()
        {
            this.text = buffer.ToString();
            this.buffer.Clear();
        }
        protected char GetFirstChar()
        {
            var i = this.reader.Peek();
            var c = (char)i;
            if (i >= 0)
            {
                buffer.Append(c);
            }
            return c;
        }
        protected string GetCurrentText() =>
            //cursor,cursor -lexeme
            this.text ?? "";

        public Token.ID NextToken()
        {

        loop:
            this.CaptureText();
            column += (cursor - lexeme);
            lexeme = cursor;

#line 16 "<stdout>"
            {
                char yych = GetFirstChar();
                switch (yych)
                {
                    case '\0': goto yy1;
                    case '\t':
                    case '\v':
                    case '\f':
                    case '\r':
                    case ' ': goto yy4;
                    case '\n': goto yy6;
                    case '!': goto yy7;
                    case '#': goto yy8;
                    case '(': goto yy9;
                    case ')': goto yy10;
                    case '*': goto yy11;
                    case '+': goto yy12;
                    case ',': goto yy13;
                    case '-': goto yy14;
                    case '.': goto yy15;
                    case '/': goto yy16;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9': goto yy17;
                    case ':': goto yy19;
                    case ';': goto yy20;
                    case '<': goto yy21;
                    case '=': goto yy23;
                    case '>': goto yy24;
                    case '?': goto yy26;
                    case 'A':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'U':
                    case 'X':
                    case 'Y':
                    case 'Z':
                    case '_':
                    case 'a':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'q':
                    case 'r':
                    case 's':
                    case 'u':
                    case 'x':
                    case 'y':
                    case 'z': goto yy27;
                    case 'B':
                    case 'b': goto yy30;
                    case 'C':
                    case 'c': goto yy31;
                    case 'D':
                    case 'd': goto yy32;
                    case 'E':
                    case 'e': goto yy33;
                    case 'I':
                    case 'i': goto yy34;
                    case 'O':
                    case 'o': goto yy35;
                    case 'P':
                    case 'p': goto yy36;
                    case 'T':
                    case 't': goto yy37;
                    case 'V':
                    case 'v': goto yy38;
                    case 'W':
                    case 'w': goto yy39;
                    default: goto yy2;
                }
            yy1:
                ++cursor;
#line 25 "Lexer.re"
                { return Token.ID.EndOfFile; }
#line 113 "<stdout>"
            yy2:
                ++cursor;
            yy3:
#line 20 "Lexer.re"
                {
                    writer.WriteLine(line + ": error: unrecognized symbol '" + yych + "'");
                    return Token.ID.Unknown;
                }
#line 122 "<stdout>"
            yy4:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case '\t':
                    case '\v':
                    case '\f':
                    case '\r':
                    case ' ': goto yy4;
                    default: goto yy5;
                }
            yy5:
#line 29 "Lexer.re"
                { goto loop; }
#line 136 "<stdout>"
            yy6:
                ++cursor;
#line 28 "Lexer.re"
                { ++line; column = 0; goto loop; }
#line 141 "<stdout>"
            yy7:
                ++cursor;
#line 50 "Lexer.re"
                { return Token.ID.Write; }
#line 146 "<stdout>"
            yy8:
                ++cursor;
#line 52 "Lexer.re"
                { return Token.ID.NotEqual; }
#line 151 "<stdout>"
            yy9:
                ++cursor;
#line 61 "Lexer.re"
                { return Token.ID.LParen; }
#line 156 "<stdout>"
            yy10:
                ++cursor;
#line 62 "Lexer.re"
                { return Token.ID.RParen; }
#line 161 "<stdout>"
            yy11:
                ++cursor;
#line 59 "Lexer.re"
                { return Token.ID.Multiply; }
#line 166 "<stdout>"
            yy12:
                ++cursor;
#line 57 "Lexer.re"
                { return Token.ID.Plus; }
#line 171 "<stdout>"
            yy13:
                ++cursor;
#line 46 "Lexer.re"
                { return Token.ID.Comma; }
#line 176 "<stdout>"
            yy14:
                ++cursor;
#line 58 "Lexer.re"
                { return Token.ID.Minus; }
#line 181 "<stdout>"
            yy15:
                ++cursor;
#line 45 "Lexer.re"
                { return Token.ID.Period; }
#line 186 "<stdout>"
            yy16:
                ++cursor;
#line 60 "Lexer.re"
                { return Token.ID.Divide; }
#line 191 "<stdout>"
            yy17:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9': goto yy17;
                    default: goto yy18;
                }
            yy18:
#line 66 "Lexer.re"
                {
                    int.TryParse(this.GetCurrentText(), out var v);

                    //_snscanf_s(lexeme, (cursor - lexeme), "%d", &v);
                    value = v;
                    //value = boost::lexical_cast<int>(lexeme, cursor - lexeme);
                    return Token.ID.Number;
                }
#line 216 "<stdout>"
            yy19:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case '=': goto yy40;
                    default: goto yy3;
                }
            yy20:
                ++cursor;
#line 47 "Lexer.re"
                { return Token.ID.Semicolon; }
#line 227 "<stdout>"
            yy21:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case '=': goto yy41;
                    default: goto yy22;
                }
            yy22:
#line 53 "Lexer.re"
                { return Token.ID.LessThan; }
#line 237 "<stdout>"
            yy23:
                ++cursor;
#line 51 "Lexer.re"
                { return Token.ID.Equal; }
#line 242 "<stdout>"
            yy24:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case '=': goto yy42;
                    default: goto yy25;
                }
            yy25:
#line 55 "Lexer.re"
                { return Token.ID.GreaterThan; }
#line 252 "<stdout>"
            yy26:
                ++cursor;
#line 49 "Lexer.re"
                { return Token.ID.Read; }
#line 257 "<stdout>"
            yy27:
                yych = GetCurrentChar();
            yy28:
                switch (yych)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                    case '_':
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z': goto yy27;
                    default: goto yy29;
                }
            yy29:
#line 76 "Lexer.re"
                {
                    value = this.GetCurrentText();
                    return Token.ID.Identifier;
                }
#line 333 "<stdout>"
            yy30:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'E':
                    case 'e': goto yy43;
                    default: goto yy28;
                }
            yy31:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'A':
                    case 'a': goto yy44;
                    case 'O':
                    case 'o': goto yy45;
                    default: goto yy28;
                }
            yy32:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'O':
                    case 'o': goto yy46;
                    default: goto yy28;
                }
            yy33:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'N':
                    case 'n': goto yy48;
                    default: goto yy28;
                }
            yy34:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'F':
                    case 'f': goto yy49;
                    default: goto yy28;
                }
            yy35:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'D':
                    case 'd': goto yy51;
                    default: goto yy28;
                }
            yy36:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'R':
                    case 'r': goto yy52;
                    default: goto yy28;
                }
            yy37:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'H':
                    case 'h': goto yy53;
                    default: goto yy28;
                }
            yy38:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'A':
                    case 'a': goto yy54;
                    default: goto yy28;
                }
            yy39:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'H':
                    case 'h': goto yy55;
                    default: goto yy28;
                }
            yy40:
                ++cursor;
#line 48 "Lexer.re"
                { return Token.ID.Assign; }
#line 410 "<stdout>"
            yy41:
                ++cursor;
#line 54 "Lexer.re"
                { return Token.ID.LessEqual; }
#line 415 "<stdout>"
            yy42:
                ++cursor;
#line 56 "Lexer.re"
                { return Token.ID.GreaterEqual; }
#line 420 "<stdout>"
            yy43:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'G':
                    case 'g': goto yy56;
                    default: goto yy28;
                }
            yy44:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'L':
                    case 'l': goto yy57;
                    default: goto yy28;
                }
            yy45:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'N':
                    case 'n': goto yy58;
                    default: goto yy28;
                }
            yy46:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                    case '_':
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z': goto yy27;
                    default: goto yy47;
                }
            yy47:
#line 41 "Lexer.re"
                { return Token.ID.Do; }
#line 513 "<stdout>"
            yy48:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'D':
                    case 'd': goto yy59;
                    default: goto yy28;
                }
            yy49:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                    case '_':
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z': goto yy27;
                    default: goto yy50;
                }
            yy50:
#line 38 "Lexer.re"
                { return Token.ID.If; }
#line 592 "<stdout>"
            yy51:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'D':
                    case 'd': goto yy61;
                    default: goto yy28;
                }
            yy52:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'O':
                    case 'o': goto yy63;
                    default: goto yy28;
                }
            yy53:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'E':
                    case 'e': goto yy64;
                    default: goto yy28;
                }
            yy54:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'R':
                    case 'r': goto yy65;
                    default: goto yy28;
                }
            yy55:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'I':
                    case 'i': goto yy67;
                    default: goto yy28;
                }
            yy56:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'I':
                    case 'i': goto yy68;
                    default: goto yy28;
                }
            yy57:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'L':
                    case 'l': goto yy69;
                    default: goto yy28;
                }
            yy58:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'S':
                    case 's': goto yy71;
                    default: goto yy28;
                }
            yy59:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                    case '_':
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z': goto yy27;
                    default: goto yy60;
                }
            yy60:
#line 37 "Lexer.re"
                { return Token.ID.End; }
#line 720 "<stdout>"
            yy61:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                    case '_':
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z': goto yy27;
                    default: goto yy62;
                }
            yy62:
#line 42 "Lexer.re"
                { return Token.ID.Odd; }
#line 792 "<stdout>"
            yy63:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'C':
                    case 'c': goto yy72;
                    default: goto yy28;
                }
            yy64:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'N':
                    case 'n': goto yy73;
                    default: goto yy28;
                }
            yy65:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                    case '_':
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z': goto yy27;
                    default: goto yy66;
                }
            yy66:
#line 33 "Lexer.re"
                { return Token.ID.Var; }
#line 878 "<stdout>"
            yy67:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'L':
                    case 'l': goto yy75;
                    default: goto yy28;
                }
            yy68:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'N':
                    case 'n': goto yy76;
                    default: goto yy28;
                }
            yy69:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                    case '_':
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z': goto yy27;
                    default: goto yy70;
                }
            yy70:
#line 35 "Lexer.re"
                { return Token.ID.Call; }
#line 964 "<stdout>"
            yy71:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'T':
                    case 't': goto yy78;
                    default: goto yy28;
                }
            yy72:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'E':
                    case 'e': goto yy80;
                    default: goto yy28;
                }
            yy73:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                    case '_':
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z': goto yy27;
                    default: goto yy74;
                }
            yy74:
#line 39 "Lexer.re"
                { return Token.ID.Then; }
#line 1050 "<stdout>"
            yy75:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'E':
                    case 'e': goto yy81;
                    default: goto yy28;
                }
            yy76:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                    case '_':
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z': goto yy27;
                    default: goto yy77;
                }
            yy77:
#line 36 "Lexer.re"
                { return Token.ID.Begin; }
#line 1129 "<stdout>"
            yy78:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                    case '_':
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z': goto yy27;
                    default: goto yy79;
                }
            yy79:
#line 32 "Lexer.re"
                { return Token.ID.Const; }
#line 1201 "<stdout>"
            yy80:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'D':
                    case 'd': goto yy83;
                    default: goto yy28;
                }
            yy81:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                    case '_':
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z': goto yy27;
                    default: goto yy82;
                }
            yy82:
#line 40 "Lexer.re"
                { return Token.ID.While; }
#line 1280 "<stdout>"
            yy83:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'U':
                    case 'u': goto yy84;
                    default: goto yy28;
                }
            yy84:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'R':
                    case 'r': goto yy85;
                    default: goto yy28;
                }
            yy85:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case 'E':
                    case 'e': goto yy86;
                    default: goto yy28;
                }
            yy86:
                yych = GetCurrentChar();
                switch (yych)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                    case '_':
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z': goto yy27;
                    default: goto yy87;
                }
            yy87:
#line 34 "Lexer.re"
                { return Token.ID.Procedure; }
#line 1373 "<stdout>"
            }
#line 80 "Lexer.re"

        }
    }
}
