﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    // базовая логика обхода без действий
    // Если нужны действия или другая логика обхода, то соответствующие методы надо переопределять
    // При переопределении методов для задания действий необходимо не забывать обходить подузлы
    class AutoVisitor: Visitor
    {
        public override void VisitBinOpNode(BinOpNode binop) 
        {
            binop.Left.Visit(this);
            binop.Right.Visit(this);
        }

        public override void VisitUnaryOpNode(UnaryOpNode unaryOp)
        {
            unaryOp.Expr.Visit(this);
        }

        public override void VisitAssignNode(AssignNode a) 
        {
            // для каких-то визиторов порядок может быть обратный - вначале обойти выражение, потом - идентификатор
            a.Id.Visit(this);
            a.Expr.Visit(this);
        }
        public override void VisitWhileNode(WhileNode c) 
        {
            c.Expr.Visit(this);
            c.Block.Visit(this);
        }

        public override void VisitForNode(ForNode c)
        {
            c.ExprStart.Visit(this);
            c.ExprEnd.Visit(this);
            c.Block.Visit(this);
        }

        public override void VisitBlockNode(BlockNode bl) 
        {
            for(int i = 0; i < bl.StList.Count; i++)
                bl.StList[i]?.Visit(this);
        }
        public override void VisitPrintNode(PrintNode w) 
        {
            w.Expr.Visit(this);
        }
        public override void VisitVarDefNode(VarDefNode w) 
        {
            foreach (var v in w.vars)
                v.Visit(this);
        }
        
        public override void VisitIfNode(IfNode c)
        {
            c.Expr.Visit(this);
            c.BlockIf.Visit(this);
            c.BlockElse?.Visit(this);
        }
       
    }
}
