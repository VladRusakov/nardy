using ProgramTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleLang.Visitors
{
    class OptWhileVisitor: AutoVisitor
    {
        public bool IsPerformed { get; set; }
        public void ReplaceStat(StatementNode from, StatementNode to)
        {
            var p = from.Parent;
            if (p is AssignNode || p is ExprNode || p is PrintNode || p is EmptyNode)
{
                throw new Exception("Родительский узел не содержит операторов");
            }
            if(to != null)
                to.Parent = p;
            if (p is BlockNode bln) // Можно переложить этот код на узлы!
            {
                //bln.StList = bln.StList.Select(bl => bl == from ? to : bl).ToList();
                for (var i = 0; i < bln.StList.Count; i++)
                    if (bln.StList[i] == from)
                    {
                        bln.StList[i] = to;
                        break;
                    }    
            }
            //else if (p is IfNode ifn)
            //{
            //    if (ifn.BlockIf == from) // Поиск подузла в Parent
            //        ifn.BlockIf = to;
            //    else if (ifn. == from)
            //        ifn.BlockElse = to;
            //}
        }

        

        public override void VisitWhileNode(WhileNode wn)
        {
            IsPerformed = false;
            if (wn.Expr is BooleanNode bnn && !bnn.Value)
            {
                ReplaceStat(wn, null);
                IsPerformed = true;
            }
                
        }
    }
}
