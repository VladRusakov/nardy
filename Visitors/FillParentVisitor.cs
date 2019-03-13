using ProgramTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleLang.Visitors
{
    class FillParentVisitor : AutoVisitor
    {
        private Stack<Node> parentNodes = new Stack<Node>();

        public FillParentVisitor()
        {
            parentNodes.Push(null);
        }

        public override void VisitAssignNode(AssignNode a)
        {
            a.Parent = parentNodes.Peek();
            parentNodes.Push(a);
            base.VisitAssignNode(a);
            parentNodes.Pop();
        }

        public override void VisitBinOpNode(BinOpNode binop)
        {
            binop.Parent = parentNodes.Peek();
            parentNodes.Push(binop);
            base.VisitBinOpNode(binop);
            parentNodes.Pop();
        }

        public override void VisitBlockNode(BlockNode bl)
        {
            bl.Parent = parentNodes.Peek();
            parentNodes.Push(bl);
            base.VisitBlockNode(bl);
            parentNodes.Pop();
        }

        public override void VisitBooleanNode(BooleanNode num)
        {
            num.Parent = parentNodes.Peek();
            parentNodes.Push(num);
            base.VisitBooleanNode(num);
            parentNodes.Pop();
        }

        public override void VisitEmptyNode(EmptyNode w)
        {
            w.Parent = parentNodes.Peek();
            parentNodes.Push(w);
            base.VisitEmptyNode(w);
            parentNodes.Pop();
        }

        public override void VisitForNode(ForNode c)
        {
            c.Parent = parentNodes.Peek();
            parentNodes.Push(c);
            base.VisitForNode(c);
            parentNodes.Pop();
        }

        public override void VisitIdNode(IdNode id)
        {
            id.Parent = parentNodes.Peek();
            parentNodes.Push(id);
            base.VisitIdNode(id);
            parentNodes.Pop();
        }

        public override void VisitIfNode(IfNode c)
        {
            c.Parent = parentNodes.Peek();
            parentNodes.Push(c);
            base.VisitIfNode(c);
            parentNodes.Pop();
        }

        public override void VisitIntNumNode(IntNumNode num)
        {
            num.Parent = parentNodes.Peek();
            parentNodes.Push(num);
            base.VisitIntNumNode(num);
            parentNodes.Pop();
        }

        public override void VisitPrintNode(PrintNode w)
        {
            w.Parent = parentNodes.Peek();
            parentNodes.Push(w);
            base.VisitPrintNode(w);
            parentNodes.Pop();
        }

        public override void VisitRealNumNode(RealNumNode num)
        {
            num.Parent = parentNodes.Peek();
            parentNodes.Push(num);
            base.VisitRealNumNode(num);
            parentNodes.Pop();
        }

        public override void VisitUnaryOpNode(UnaryOpNode unaryOp)
        {
            unaryOp.Parent = parentNodes.Peek();
            parentNodes.Push(unaryOp);
            base.VisitUnaryOpNode(unaryOp);
            parentNodes.Pop();
        }

        public override void VisitVarDefNode(VarDefNode w)
        {
            w.Parent = parentNodes.Peek();
            parentNodes.Push(w);
            base.VisitVarDefNode(w);
            parentNodes.Pop();
        }

        public override void VisitWhileNode(WhileNode c)
        {
            c.Parent = parentNodes.Peek();
            parentNodes.Push(c);
            base.VisitWhileNode(c);
            parentNodes.Pop();
        }
    }
}
