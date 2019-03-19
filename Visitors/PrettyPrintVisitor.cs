using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    class PrettyPrintVisitor: Visitor
    {
        public string Text = "";
        private int Indent = 0;

        private Dictionary<ProgramTree.TypeOperation, String> opDictionary = new Dictionary<TypeOperation, string>();

        public PrettyPrintVisitor()
        {
            opDictionary.Add(ProgramTree.TypeOperation.Mult, "*");
            opDictionary.Add(ProgramTree.TypeOperation.Div, "/");
            opDictionary.Add(ProgramTree.TypeOperation.And, "&&");
            opDictionary.Add(ProgramTree.TypeOperation.Or, "||");
            opDictionary.Add(ProgramTree.TypeOperation.NEqual, "!=");
            opDictionary.Add(ProgramTree.TypeOperation.Equal, "==");
            opDictionary.Add(ProgramTree.TypeOperation.GEqual, ">=");
            opDictionary.Add(ProgramTree.TypeOperation.Greater, ">");
            opDictionary.Add(ProgramTree.TypeOperation.LEqual, "<=");
            opDictionary.Add(ProgramTree.TypeOperation.Less, "<");
            opDictionary.Add(ProgramTree.TypeOperation.Minus, "-");
            opDictionary.Add(ProgramTree.TypeOperation.Plus, "+");
            opDictionary.Add(ProgramTree.TypeOperation.Not, "!");
            opDictionary.Add(ProgramTree.TypeOperation.UMinus, "-");

        }

        private string IndentStr()
        {
            return new string(' ', Indent);
        }
        private void IndentPlus()
        {
            Indent += 2;
        }
        private void IndentMinus()
        {
            Indent -= 2;
        }
        public override void VisitIdNode(IdNode id)
        {
            Text += id.Name;
        }
        public override void VisitIntNumNode(IntNumNode num)
        {
            Text += num.Num.ToString();
        }
        public override void VisitBooleanNode(BooleanNode node)
        {
            Text += node.Value.ToString().ToLower();
        }

        public override void VisitBinOpNode(BinOpNode binop)
        {
            //Text += "(";
            binop.Left.Visit(this);
            Text += " " + opDictionary[binop.Op] + " ";
            binop.Right.Visit(this);
            //Text += ")";
        }
        public override void VisitAssignNode(AssignNode a)
        {
            Text += IndentStr();
            a.Id.Visit(this);
            Text += " = ";
            a.Expr.Visit(this);
        }
        public override void VisitWhileNode(WhileNode c)
        {
            Text += IndentStr() + "while (";
            c.Expr.Visit(this);
            Text += ")" + Environment.NewLine;
            c.Block.Visit(this);
        }

        public override void VisitForNode(ForNode c)
        {
            Text += IndentStr() + "for (";
            c.ExprStart.Visit(this);
            Text += " to ";
            c.ExprEnd.Visit(this);
            Text += ")" + Environment.NewLine;
            c.Block.Visit(this);
        }

        public override void VisitIfNode(IfNode c)
        {
            Text += IndentStr() + "if (";
            c.Expr.Visit(this);
            Text += ")" + Environment.NewLine;
            c.BlockIf.Visit(this);
            if (c.BlockElse != null)
            {
                Text += "else";
                c.BlockElse.Visit(this);
            }
        }

        public override void VisitBlockNode(BlockNode bl)
        {
            Text += IndentStr() + "{" + Environment.NewLine;
            IndentPlus();

            var Count = bl.StList.Count;

            if (Count > 0)
                bl.StList[0]?.Visit(this);
            for (var i = 1; i < Count; i++)
            {
                if (bl.StList[i] == null) continue;
                Text += ';';
                if (!(bl.StList[i] is EmptyNode))
                    Text += Environment.NewLine;
                bl.StList[i].Visit(this);
            }
            IndentMinus();
            Text += Environment.NewLine + IndentStr() + "}";
        }
        public override void VisitPrintNode(PrintNode w)
        {
            Text += IndentStr() + "println ";
            w.Expr.Visit(this);
            // TODO \n?
        }
        public override void VisitVarDefNode(VarDefNode w)
        {
            Text += IndentStr() + "var " + w.vars[0].Name;
            for (int i = 1; i < w.vars.Count; i++)
                Text += ',' + w.vars[i].Name;
        }
    }
}
