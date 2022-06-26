namespace PL0.AST
{
    public interface Visitor
    {
        void visit(Program program);
        void visit(Block block);
        void visit(Constant constant);
        void visit(Procedure procedure);
        void visit(AssignmentStatement statement);
        void visit(CallStatement statement);
        void visit(ReadStatement statement);
        void visit(WriteStatement statement);
        void visit(BeginStatement statement);
        void visit(IfStatement statement);
        void visit(WhileStatement statement);
        void visit(OddCondition condition);
        void visit(EqualCondition condition);
        void visit(NotEqualCondition condition);
        void visit(LessThanCondition condition);
        void visit(LessEqualCondition condition);
        void visit(GreaterThanCondition condition);
        void visit(GreaterEqualCondition condition);
        void visit(AdditionExpression expression);
        void visit(SubtractionExpression expression);
        void visit(MultiplicationExpression expression);
        void visit(DivisionExpression expression);
        void visit(NegationExpression expression);
        void visit(Identifier identifier);
        void visit(Number number);
        void visit(EmptyStatement empty);
    }

}
