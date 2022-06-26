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
        public string Text => this.text;
        public string FullText = "";
        public Lexer(TextReader reader, TextWriter error_writer)
        {
            this.FullText = reader.ReadToEnd();
            this.reader = new StringReader(this.FullText) ;
            
            this.writer = error_writer;
        }
        //*++cursor
        protected char GetNextChar()
        {
            this.reader.Read();
            var i = this.reader.Peek();
            var c = (char)i;
            if (i >= 0)
            {
                ++this.cursor;
                buffer.Append(c);
            }
            else //-1
            {
                c = '\0';
            }
            return c;
        }

        protected char GetFirstChar()
        {
            var i = this.reader.Peek();
            var c = (char)i;
            if (i >= 0)
            {
                buffer.Append(c);
            }
            else //-1
            {
                c = '\0';
            }
            return c;
        }
        protected string GetCurrentText()
        {
           return this.text = this.FullText.Substring(this.lexeme, (cursor - lexeme));
        }

        //cursor,cursor -lexeme

        public Token.ID NextToken()
        {
        loop:
            column += (cursor - lexeme);
            lexeme = cursor;
            this.text = this.buffer.ToString();
            this.buffer.Clear();
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
                GetNextChar();
                { return Token.ID.EndOfFile; }
            yy2:
                GetNextChar();
            yy3:
                {
                    writer.WriteLine(line + ": error: unrecognized symbol '" + yych + "'");
                    return Token.ID.Unknown;
                }
            yy4:
                yych = GetNextChar();
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
                { goto loop; }
            yy6:
                GetNextChar();
                { ++line; column = 0; goto loop; }
            yy7:
                GetNextChar();
                { return Token.ID.Write; }
            yy8:
                GetNextChar();
                { return Token.ID.NotEqual; }
            yy9:
                GetNextChar();
                { return Token.ID.LParen; }
            yy10:
                GetNextChar();
                { return Token.ID.RParen; }
            yy11:
                GetNextChar();
                { return Token.ID.Multiply; }
            yy12:
                GetNextChar();
                { return Token.ID.Plus; }
            yy13:
                GetNextChar();
                { return Token.ID.Comma; }
            yy14:
                GetNextChar();
                { return Token.ID.Minus; }
            yy15:
                GetNextChar();
                { return Token.ID.Period; }
            yy16:
                GetNextChar();
                { return Token.ID.Divide; }
            yy17:
                yych = GetNextChar();
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
                {
                    value = int.TryParse(this.GetCurrentText().Trim(), out var v) ? v : (object)0;

                    return Token.ID.Number;
                }
            yy19:
                yych = GetNextChar();
                switch (yych)
                {
                    case '=': goto yy40;
                    default: goto yy3;
                }
            yy20:
                GetNextChar();
                { return Token.ID.Semicolon; }
            yy21:
                yych = GetNextChar();
                switch (yych)
                {
                    case '=': goto yy41;
                    default: goto yy22;
                }
            yy22:
                { return Token.ID.LessThan; }
            yy23:
                GetNextChar();
                { return Token.ID.Equal; }
            yy24:
                yych = GetNextChar();
                switch (yych)
                {
                    case '=': goto yy42;
                    default: goto yy25;
                }
            yy25:

                { return Token.ID.GreaterThan; }
            yy26:
                GetNextChar();
                { return Token.ID.Read; }
            yy27:
                yych = GetNextChar();
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
                {
                    value = this.GetCurrentText().Trim();
                    return Token.ID.Identifier;
                }
            yy30:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'E':
                    case 'e': goto yy43;
                    default: goto yy28;
                }
            yy31:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'A':
                    case 'a': goto yy44;
                    case 'O':
                    case 'o': goto yy45;
                    default: goto yy28;
                }
            yy32:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'O':
                    case 'o': goto yy46;
                    default: goto yy28;
                }
            yy33:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'N':
                    case 'n': goto yy48;
                    default: goto yy28;
                }
            yy34:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'F':
                    case 'f': goto yy49;
                    default: goto yy28;
                }
            yy35:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'D':
                    case 'd': goto yy51;
                    default: goto yy28;
                }
            yy36:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'R':
                    case 'r': goto yy52;
                    default: goto yy28;
                }
            yy37:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'H':
                    case 'h': goto yy53;
                    default: goto yy28;
                }
            yy38:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'A':
                    case 'a': goto yy54;
                    default: goto yy28;
                }
            yy39:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'H':
                    case 'h': goto yy55;
                    default: goto yy28;
                }
            yy40:
                GetNextChar();
                { return Token.ID.Assign; }
            yy41:
                GetNextChar();
                { return Token.ID.LessEqual; }
            yy42:
                GetNextChar();
                { return Token.ID.GreaterEqual; }
            yy43:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'G':
                    case 'g': goto yy56;
                    default: goto yy28;
                }
            yy44:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'L':
                    case 'l': goto yy57;
                    default: goto yy28;
                }
            yy45:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'N':
                    case 'n': goto yy58;
                    default: goto yy28;
                }
            yy46:
                yych = GetNextChar();
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
                { return Token.ID.Do; }
            yy48:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'D':
                    case 'd': goto yy59;
                    default: goto yy28;
                }
            yy49:
                yych = GetNextChar();
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
                { return Token.ID.If; }
            yy51:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'D':
                    case 'd': goto yy61;
                    default: goto yy28;
                }
            yy52:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'O':
                    case 'o': goto yy63;
                    default: goto yy28;
                }
            yy53:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'E':
                    case 'e': goto yy64;
                    default: goto yy28;
                }
            yy54:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'R':
                    case 'r': goto yy65;
                    default: goto yy28;
                }
            yy55:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'I':
                    case 'i': goto yy67;
                    default: goto yy28;
                }
            yy56:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'I':
                    case 'i': goto yy68;
                    default: goto yy28;
                }
            yy57:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'L':
                    case 'l': goto yy69;
                    default: goto yy28;
                }
            yy58:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'S':
                    case 's': goto yy71;
                    default: goto yy28;
                }
            yy59:
                yych = GetNextChar();
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
                { return Token.ID.End; }
            yy61:
                yych = GetNextChar();
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
                { return Token.ID.Odd; }
            yy63:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'C':
                    case 'c': goto yy72;
                    default: goto yy28;
                }
            yy64:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'N':
                    case 'n': goto yy73;
                    default: goto yy28;
                }
            yy65:
                yych = GetNextChar();
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
                { return Token.ID.Var; }
            yy67:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'L':
                    case 'l': goto yy75;
                    default: goto yy28;
                }
            yy68:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'N':
                    case 'n': goto yy76;
                    default: goto yy28;
                }
            yy69:
                yych = GetNextChar();
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
                { return Token.ID.Call; }
            yy71:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'T':
                    case 't': goto yy78;
                    default: goto yy28;
                }
            yy72:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'E':
                    case 'e': goto yy80;
                    default: goto yy28;
                }
            yy73:
                yych = GetNextChar();
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
                { return Token.ID.Then; }
            yy75:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'E':
                    case 'e': goto yy81;
                    default: goto yy28;
                }
            yy76:
                yych = GetNextChar();
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
                { return Token.ID.Begin; }
            yy78:
                yych = GetNextChar();
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
                { return Token.ID.Const; }
            yy80:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'D':
                    case 'd': goto yy83;
                    default: goto yy28;
                }
            yy81:
                yych = GetNextChar();
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
                { return Token.ID.While; }
            yy83:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'U':
                    case 'u': goto yy84;
                    default: goto yy28;
                }
            yy84:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'R':
                    case 'r': goto yy85;
                    default: goto yy28;
                }
            yy85:
                yych = GetNextChar();
                switch (yych)
                {
                    case 'E':
                    case 'e': goto yy86;
                    default: goto yy28;
                }
            yy86:
                yych = GetNextChar();
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
                { return Token.ID.Procedure; }
            }
        }
    }
}
