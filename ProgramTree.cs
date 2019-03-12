using System.Collections.Generic;
using SimpleLang.Visitors;

namespace ProgramTree
{
    public enum TypeOperation { Or, And, Less, Greater, GEqual, LEqual,
    NEqual, Equal, Plus, Minus, Mult, Div, Not, UMinus};

    public abstract class Node // базовый класс для всех узлов    
    {
        public abstract void Visit(Visitor v);
    }

    public abstract class ExprNode : Node // базовый класс для всех выражений
    {
    }

    public class IdNode : ExprNode
    {
        public string Name { get; set; }
        public IdNode(string name) { Name = name; }
        public override void Visit(Visitor v)
        {
            v.VisitIdNode(this);
        }
    }

    //public class IdArrayNode : ExprNode
    //{
    //    public string Name { get; set; }
    //    public IdArrayNode(string name) { Name = name; }
    //    public override void Visit(Visitor v)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}

    public class IntNumNode : ExprNode
    {
        public int Num { get; set; }
        public IntNumNode(int num) { Num = num; }
        public override void Visit(Visitor v)
        {
            v.VisitIntNumNode(this);
        }
    }

    public class RealNumNode : ExprNode
    {
        public double Num { get; set; }
        public RealNumNode(double num) { Num = num; }
        public override void Visit(Visitor v)
        {
            v.VisitRealNumNode(this);
        }
    }

    public class BooleanNode : ExprNode
    {
        public bool Value { get; set; }
        public BooleanNode(bool value) { Value = value; }
        public override void Visit(Visitor v)
        {
            v.VisitBooleanNode(this);
        }
    }

    public class BinOpNode : ExprNode
    {
        public ExprNode Left { get; set; }
        public ExprNode Right { get; set; }
        public TypeOperation Op { get; set; }
        public BinOpNode(ExprNode Left, ExprNode Right, TypeOperation op)
        {
            this.Left = Left;
            this.Right = Right;
            this.Op = op;
        }
        public override void Visit(Visitor v)
        {
            v.VisitBinOpNode(this);
        }
    }

    public class UnaryOpNode : ExprNode
    {
        public TypeOperation Op { get; set; }
        public ExprNode Expr { get; set; }

        public UnaryOpNode(ExprNode expr, TypeOperation op)
        {
            Expr = expr;
            Op = op;
        }

        public override void Visit(Visitor v)
        {
            v.VisitUnaryOpNode(this);
        }
    }




    public abstract class StatementNode : Node // базовый класс для всех операторов
    {
    }

    public class AssignNode : StatementNode
    {
        public IdNode Id { get; set; }
        public ExprNode Expr { get; set; }
        public AssignNode(IdNode id, ExprNode expr)
        {
            Id = id;
            Expr = expr;
        }
        public override void Visit(Visitor v)
        {
            v.VisitAssignNode(this);
        }
    }

    public class WhileNode : StatementNode
    {
        public ExprNode Expr { get; set; }
        public BlockNode Block { get; set; }
        public WhileNode(ExprNode expr, BlockNode block)
        {
            Expr = expr;
            Block = block;
        }
        public override void Visit(Visitor v)
        {
            v.VisitWhileNode(this);
        }
    }

    public class ForNode : StatementNode
    {
        public ExprNode ExprStart { get; set; }
        public ExprNode ExprEnd { get; set; }
        public BlockNode Block { get; set; }
        public ForNode(ExprNode exprStart, ExprNode exprEnd, BlockNode block)
        {
            ExprStart = exprStart;
            ExprEnd = exprEnd;
            Block = block;
        }
        public override void Visit(Visitor v)
        {
            v.VisitForNode(this);

        }
    }

    public class IfNode : StatementNode
    {
        public ExprNode Expr { get; set; }
        public BlockNode BlockIf { get; set; }
        public BlockNode BlockElse { get; set; }
        public IfNode(ExprNode expr, BlockNode blockIf, BlockNode blockElse)
        {
            Expr = expr;
            BlockIf = blockIf;
            BlockElse = blockElse;
        }
        public override void Visit(Visitor v)
        {
            v.VisitIfNode(this);
        }
    }

    public class BlockNode : StatementNode
    {
        public List<StatementNode> StList = new List<StatementNode>();
        public BlockNode(StatementNode stat)
        {
            Add(stat);
        }
        public void Add(StatementNode stat)
        {
            StList.Add(stat);
        }
        public override void Visit(Visitor v)
        {
            v.VisitBlockNode(this);
        }
    }

    public class PrintNode : StatementNode
    {
        public ExprNode Expr { get; set; }
        public PrintNode(ExprNode Expr)
        {
            this.Expr = Expr;
        }
        public override void Visit(Visitor v)
        {
            v.VisitPrintNode(this);
        }
    }

    public class EmptyNode : StatementNode
    {
        public override void Visit(Visitor v)
        {
            v.VisitEmptyNode(this);
        }
    }

    public class VarDefNode : StatementNode
    {
        public List<IdNode> vars = new List<IdNode>();
        public VarDefNode(IdNode id)
        {
            Add(id);
        }

        public void Add(IdNode id)
        {
            vars.Add(id);
        }

        public override void Visit(Visitor v)
        {
            v.VisitVarDefNode(this);
        }
    }
}