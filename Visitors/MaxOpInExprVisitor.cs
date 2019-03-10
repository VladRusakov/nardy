using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    class MaxOpInExprVisitor : AutoVisitor
    {
        public int MaxCount = 0;
        private int CurrentCount = 0;

        public override void VisitBinOpNode(BinOpNode binOp)
        {
            CurrentCount++;
            binOp.Left.Visit(this);
            binOp.Right.Visit(this);
        }

        public override void VisitUnaryOpNode(UnaryOpNode unaryOp)
        {
            CurrentCount++;
            unaryOp.Expr.Visit(this);
        }

        public override void VisitAssignNode(AssignNode a)
        {
            if (CurrentCount > MaxCount) MaxCount = CurrentCount;
            CurrentCount = 0;
        }
    }
}
