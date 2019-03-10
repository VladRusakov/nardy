using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    class NestedLoopVisitor : AutoVisitor
    {
        private bool inFirstLoop, inSecondLoop;
        public bool HaveNestedLoop;

        public override void VisitForNode(ForNode c)
        {
            if (!inFirstLoop) // если это внешний цикл
            {
                inFirstLoop = true;
                c.Block.Visit(this);
                inFirstLoop = false;
            }
            else // если мы уже во внешнем
            {
                if (!inSecondLoop) // если мы еще не во внутреннем
                {
                    inSecondLoop = true;
                    HaveNestedLoop = true;
                    c.Block.Visit(this);
                    inSecondLoop = false;
                }
            }
        }

        public override void VisitWhileNode(WhileNode c)
        {
            if (!inFirstLoop) // если это внешний цикл
            {
                inFirstLoop = true;
                c.Block.Visit(this);
                inFirstLoop = false;
            }
            else // если мы уже во внешнем
            {
                if (!inSecondLoop) // если мы еще не во внутреннем
                {
                    inSecondLoop = true;
                    HaveNestedLoop = true;
                    c.Block.Visit(this);
                    inSecondLoop = false;
                }
            }
        }
    }
}
