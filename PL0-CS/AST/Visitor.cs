namespace PL0.AST
{
    public interface Visitor
    {
        void Visit(Program program);
        void Visit(Block block);
        void Visit(Constant constant);
        void Visit(Procedure procedure);
        void Visit(AssignmentStatement statement);
        void Visit(CallStatement statement);
        void Visit(ReadStatement statement);
        void Visit(WriteStatement statement);
        void Visit(BeginStatement statement);
        void Visit(IfStatement statement);
        void Visit(WhileStatement statement);
        void Visit(OddCondition condition);
        void Visit(EqualCondition condition);
        void Visit(NotEqualCondition condition);
        void Visit(LessThanCondition condition);
        void Visit(LessEqualCondition condition);
        void Visit(GreaterThanCondition condition);
        void Visit(GreaterEqualCondition condition);
        void Visit(AdditionExpression expression);
        void Visit(SubtractionExpression expression);
        void Visit(MultiplicationExpression expression);
        void Visit(DivisionExpression expression);
        void Visit(NegationExpression expression);
        void Visit(Identifier identifier);
        void Visit(Number number);
        void Visit(EmptyStatement empty);
    }

}
