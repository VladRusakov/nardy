using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    class NestedLoopInIfVisitor : AutoVisitor
    {
        public bool HaveNestedLoop;

        public override void VisitIfNode(IfNode c)
        {
            if (HaveNestedLoop) return;

            foreach (var n in c.BlockIf.StList)
                if (n is ForNode || n is WhileNode) HaveNestedLoop = true;

            if (c.BlockElse != null)
            {
                foreach (var n in c.BlockIf.StList)
                    if (n is ForNode || n is WhileNode) HaveNestedLoop = true;
            }

            if (!HaveNestedLoop)
            {
                c.BlockIf.Visit(this);
                c.BlockElse?.Visit(this);
            }
        }
    }
}
