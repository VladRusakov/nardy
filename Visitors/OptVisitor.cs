using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    class OptVisitor : AutoVisitor
    {
        public void ReplaceExpr(ExprNode from, ExprNode to)
        {
            var p = from.Parent;
            to.Parent = p;
            if (p is AssignNode assn)
            {
                assn.Expr = to;
            }
            else if (p is BinOpNode binopn)
            {
                if (binopn.Left == from)
                    binopn.Left = to;
                else if (binopn.Right == from)
                    binopn.Right = to;
            }
            else if (p is BlockNode)
            {
                throw new Exception("Родительский узел не содержит выражений");
            }
        }

        public override void VisitBinOpNode(BinOpNode binop)
        {
            if (binop.Op == ProgramTree.TypeOperation.Mult 
                && binop.Left is IntNumNode innLeft
                && innLeft.Num == 1)
            {
                binop.Right.Visit(this);
                ReplaceExpr(binop, binop.Right);
            }
            else if (binop.Op == ProgramTree.TypeOperation.Mult
                 && binop.Right is IntNumNode innRight
                 && innRight.Num == 1)
            {
                binop.Right.Visit(this);
                ReplaceExpr(binop, binop.Left);
            } 
            else if (binop.Op == ProgramTree.TypeOperation.Div
                     && binop.Right is IntNumNode innRightDiv
                     && innRightDiv.Num == 1)
            {
                binop.Right.Visit(this);
                ReplaceExpr(binop, binop.Left);
            }
            else
            {
                base.VisitBinOpNode(binop);
            }

        }
    }
}