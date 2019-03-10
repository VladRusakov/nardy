using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    class OpCountVisitor : AutoVisitor
    {
        public int AssignCount = 0;
        public int IfCount = 0;
        public int ForCount = 0;
        public int WhileCount = 0;
        public int PrintCount = 0;
        public int VarDefCount = 0;


        public override void VisitAssignNode(AssignNode a)
        {
            AssignCount++;
        }

        public override void VisitForNode(ForNode c)
        {
            ForCount++;
            c.Block.Visit(this);
        }

        public override void VisitIfNode(IfNode c)
        {
            IfCount++;
            c.BlockIf.Visit(this);
            c.BlockElse?.Visit(this);
        }

        public override void VisitPrintNode(PrintNode w)
        {
            PrintCount++;
        }

        public override void VisitVarDefNode(VarDefNode w)
        {
            VarDefCount++;
        }

        public override void VisitWhileNode(WhileNode c)
        {
            WhileCount++;
            c.Block.Visit(this);
        }

    }
}
