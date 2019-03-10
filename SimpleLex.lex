%using SimpleParser;
%using QUT.Gppg;
%using System.Linq;

%namespace SimpleScanner

Alpha 	[a-zA-Z_]
Digit   [0-9] 
AlphaDigit {Alpha}|{Digit}
BOOLVAL true|false
INTNUM  {Digit}+
REALNUM {INTNUM}\.{INTNUM}
ID {Alpha}{AlphaDigit}* 

%%

{INTNUM} { 
  return (int)Tokens.INUM; 
}

{REALNUM} { 
  return (int)Tokens.RNUM;
}

{BOOLVAL} { 
  return (int)Tokens.BOOL; 
}

{ID}  { 
  int res = ScannerHelper.GetIDToken(yytext);
  return res;
}

"=" { return (int)Tokens.ASSIGN; }
";"  { return (int)Tokens.SEMICOLON; }
"+"  { return (int)Tokens.PLUS; }
"-"  { return (int)Tokens.MINUS; }
"*"  { return (int)Tokens.MULT; }
"/"  { return (int)Tokens.DIV; }
"("  { return (int)Tokens.LPAREN; }
")"  { return (int)Tokens.RPAREN; }
"{"  { return (int)Tokens.LBRACE; }
"}"  { return (int)Tokens.RBRACE; }
"||"  { return (int)Tokens.OR; }
"!"  { return (int)Tokens.NOT; }
"&&"  { return (int)Tokens.AND; }
"<"  { return (int)Tokens.LESS; }
">"  { return (int)Tokens.GREATER; }
"=="  { return (int)Tokens.EQUEAL; }
"!="  { return (int)Tokens.NEQUEAL; }
">="  { return (int)Tokens.GEQUEAL; }
"<="  { return (int)Tokens.LEQUEAL; }
"["  { return (int)Tokens.LCROCHET; }
"]"  { return (int)Tokens.RCROCHET; }
","  { return (int)Tokens.COMMA; }


[^ \r\n] {
	LexError();
	return (int)Tokens.EOF; // конец разбора
}

%{
  yylloc = new LexLocation(tokLin, tokCol, tokELin, tokECol); // позици€ символа (терминального или нетерминального), возвращаема€ @1 @2 и т.д.
%}

%%

public override void yyerror(string format, params object[] args) // обработка синтаксических ошибок
{
  var ww = args.Skip(1).Cast<string>().ToArray();
  string errorMsg = string.Format("({0},{1}): ¬стречено {2}, а ожидалось {3}", yyline, yycol, args[0], string.Join(" или ", ww));
  throw new SyntaxException(errorMsg);
}

public void LexError()
{
	string errorMsg = string.Format("({0},{1}): Ќеизвестный символ {2}", yyline, yycol, yytext);
    throw new LexException(errorMsg);
}

class ScannerHelper 
{
  private static Dictionary<string,int> keywords;

  static ScannerHelper() 
  {
    keywords = new Dictionary<string,int>();
    keywords.Add("for",(int)Tokens.FOR);
	keywords.Add("to",(int)Tokens.TO);
	keywords.Add("while",(int)Tokens.WHILE);
	keywords.Add("if",(int)Tokens.IF);
	keywords.Add("Println",(int)Tokens.PRINTLN);
	keywords.Add("real",(int)Tokens.TYPERNUM);
	keywords.Add("int",(int)Tokens.TYPEINUM);
	keywords.Add("bool",(int)Tokens.TYPEBOOL);
  }
  public static int GetIDToken(string s)
  {
    if (keywords.ContainsKey(s.ToLower())) // €зык нечувствителен к регистру
      return keywords[s];
    else
      return (int)Tokens.ID;
  }
}