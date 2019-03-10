%{
// ��� ���������� ����������� � ����� GPPGParser, �������������� ����� ������, ������������ �������� gppg
    public Parser(AbstractScanner<int, LexLocation> scanner) : base(scanner) { }
%}

%output = SimpleYacc.cs

%namespace SimpleParser

%token INUM RNUM BOOL ID ASSIGN COMMA SEMICOLON RBRACE LBRACE LPAREN RPAREN IF ELSE PLUS MINUS 
%token LCROCHET RCROCHET MULT DIV FOR TO WHILE OR NOT AND PRINTLN TYPEINUM TYPERNUM TYPEBOOL
%token LESS GREATER GEQUAL LEQUAL NEQUAL EQUAL
%%

progr   : block { root = $1; }
		;

stlist	: statement 
			{ 
				$$ = new BlockNode($1); 
			}
		| stlist statement 
			{ 
				$1.Add($3);
				$$ = $1; 
			}
		;

statement: assign SEMICOLON { $$ = $1; }
		| block   SEMICOLON { $$ = $1; }
		| ifstat 			{ $$ = $1; }
		| forstat 			{ $$ = $1; }
		| while 			{ $$ = $1; }
		| println SEMICOLON { $$ = $1; }
		| vardef SEMICOLON 	{ $$ = $1; }
		;

assign 	: ident ASSIGN expr { $$ = new AssignNode($1 as IdNode, $3); }
		;

block	: LBRACE stlist RBRACE { $$ = $2; }
		;
		
ifstat : IF LPAREN expr RPAREN block { $$ = new IfNode($3, $5, null); }
	|IF LPAREN expr RPAREN block ELSE block { $$ = new IfNode($3, $5, $7); }
	;
	
forstat : FOR LPAREN expr TO INUM RPAREN block
			{$$ = new ForNode($3, $5, $7); }
		;
	
while 	: WHILE LPAREN expr RPAREN block
		{$$ = new WhileNode($3, $5); }
		;

println : PRINTLN expr {$$ = new PrintNode($2);}
		;

ident 	: ID
		{
			if (!InDefSect)
				if (!SymbolTable.vars.ContainsKey($1))
					throw new Exception("("+@1.StartLine+","+@1.StartColumn+"): ���������� "+$1+" �� �������");
			$$ = new IdNode($1); 
			//| ID LCROCHET expr RCROCHET
		}	
		;

vardef	: typename { InDefSect = true; } varlist
		{ 
			foreach (var v in ($3 as VarDefNode).vars)
				SymbolTable.NewVarDef(v.Name, $1); // ��� ���������� ��� ������
			InDefSect = false;	
		}
		;
		
typename: TYPEINUM { $$ = TypeData.Inum; }
		| TYPERNUM { $$ = TypeData.Real; }
		| TYPEBOOL { $$ = TypeData.Bool; }
		;

varlist	: ident 		
		{ 
			$$ = new VarDefNode($1 as IdNode); 
		}
		| varlist COMMA ident
		{ 
			($1 as VarDefNode).Add($3 as IdNode);
			$$ = $1;
		}
		;

expr 	: A
		| expr OR A { $$ = new BinOpNode($1,$3, TypeOperation.Or); }
		; 

A 		: compare { $$ = $1; }
		| A AND compare { $$ = new BinOpNode($1,$3, TypeOperation.And); }
		;

compare : arithm { $$ = $1; }
		| arithm relation arithm 
		{
			$$ = new BinOpNode($1, $3, $2)
		}
		;
		
relation: LESS   { $$ = TypeOperation.Less;}
		| GREATER{ $$ = TypeOperation.Greater; }
		| GEQUAL { $$ = TypeOperation.GEqual; }
		| LEQUAL { $$ = TypeOperation.LEqual; }
		| NEQUAL { $$ = TypeOperation.NEqual; }
		| EQUAL  { $$ = TypeOperation.Equal; } 
		;
		
arithm 	: T	{ $$ = $1; }
		| arithm PLUS T { $$ = new BinOpNode($1,$3, TypeOperation.Plus); }
		| arithm MINUS T { $$ = new BinOpNode($1,$3,TypeOperation.Minus); }
		;
		
T    	: N { $$ = $1; }
		| T MULT N { $$ = new BinOpNode($1,$3,TypeOperation.Mult); }
		| T DIV N  { $$ = new BinOpNode($1,$3,TypeOperation.Div); }
		;

N 		: F { $$ = $1; }
		| NOT F { $$ = new UnaryNode($2, TypeOperation.Not); }
		;

F 		: E { $$ = $1; }
		| MINUS E { $$ = new UnaryNode($2, TypeOperation.UMinus);}
		;
	
E    	: ident { $$ = $1 as IdNode; }
		| INUM  { $$ = new IntNumNode($1); }
		| RNUM  { $$ = new RealNumNode($1); }
		| BOOL  { $$ = new BooleanNode($1); }
		| LPAREN expr RPAREN { $$ = $2; }
		;
%%
