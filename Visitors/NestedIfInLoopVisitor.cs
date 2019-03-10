using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    class NestedIfInLoopVisitor : AutoVisitor
    {
        public bool HaveNestedIf;

        public override void VisitForNode(ForNode c)
        {
            foreach (Node n in c.Block.StList)
                if (n is IfNode) HaveNestedIf = true;

            if (!HaveNestedIf) c.Block.Visit(this);
        }

        public override void VisitWhileNode(WhileNode c)
        {
            foreach (Node n in c.Block.StList)
                if (n is IfNode) HaveNestedIf = true;

            if (!HaveNestedIf) c.Block.Visit(this);
        }
    }
}
