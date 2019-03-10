using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    public abstract class Visitor
    {
        // Expr
        public virtual void VisitIdNode(IdNode id) { }
        public virtual void VisitIntNumNode(IntNumNode num) { }
        public virtual void VisitRealNumNode(RealNumNode num) { }
        public virtual void VisitBooleanNode(BooleanNode num) { }
        public virtual void VisitBinOpNode(BinOpNode binOp) { }
        public virtual void VisitUnaryOpNode(UnaryOpNode unaryOp) { }

        // Statement
        public virtual void VisitAssignNode(AssignNode a) { }
        public virtual void VisitWhileNode(WhileNode c) { }
        public virtual void VisitForNode(ForNode c) { }
        public virtual void VisitIfNode(IfNode c) { }
        public virtual void VisitBlockNode(BlockNode bl) { }
        public virtual void VisitPrintNode(PrintNode w) { }
        public virtual void VisitEmptyNode(EmptyNode w) { }
        public virtual void VisitVarDefNode(VarDefNode w) { }
    }
}
