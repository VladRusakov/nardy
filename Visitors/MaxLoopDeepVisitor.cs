using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    class MaxLoopDeepVisitor : AutoVisitor
    {
        private bool inFirstLoop;
        public int Deep = 0;
        private int currentDeep = 0;

        public override void VisitForNode(ForNode c)
        {
            currentDeep++;
            if (Deep < currentDeep) Deep = currentDeep;
            c.Block.Visit(this);
            currentDeep--;
        }

        public override void VisitWhileNode(WhileNode c)
        {
            currentDeep++;
            if (Deep < currentDeep) Deep = currentDeep;
            c.Block.Visit(this);
            currentDeep--;
        }
    }
}
