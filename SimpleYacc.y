%{
// Ёти объ€влени€ добавл€ютс€ в класс GPPGParser, представл€ющий собой парсер, генерируемый системой gppg
    public Parser(AbstractScanner<int, LexLocation> scanner) : base(scanner) { }
%}

%output = SimpleYacc.cs

%namespace SimpleParser

%token INUM RNUM BOOL ID ASSIGN COMMA SEMICOLON RBRACE LBRACE LPAREN RPAREN IF ELSE PLUS MINUS 
%token LCROCHET RCROCHET MULT DIV FOR TO WHILE OR NOT AND PRINTLN TYPEINUM TYPERNUM TYPEBOOL
%token LESS GREATER GEQUEAL LEQUEAL NEQUEAL EQUEAL
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
		
ifstat : IF LPAREN expr RPAREN block { $$ = new IfNode($3, $5,null); }
	|IF LPAREN expr RPAREN block ELSE block { $$ = new IfNode($3, $5, $7); }
	;
	
forstat : FOR LPAREN expr TO INUM RPAREN block
		;
	
while 	: WHILE LPAREN expr RPAREN block
		;

println : PRINTLN expr
		;

ident 	: ID
		| ID LCROCHET expr RCROCHET
		;

vardef	: typename varlist
		;
		
typename: TYPEINUM
		| TYPERNUM
		| TYPEBOOL
		;

varlist	: ident
		| varlist COMMA ident
		;

expr 	: A
		| expr OR A
		;

A 		: compare 
		| A AND compare
		;

compare : arithm
		| arithm relation arithm
		;
		
relation: LESS
		| GREATER 
		| GEQUEAL 
		| LEQUEAL 
		| NEQUEAL 
		| EQUEAL
		;
		
arithm 	: T	
		| arithm PLUS T { $$ = new BinOpNode($1,$3,'+'); }
		| arithm MINUS T { $$ = new BinOpNode($1,$3,'-'); }
		;
		
T    	: N
		| T MULT N { $$ = new BinOpNode($1,$3,'*'); }
		| T DIV N { $$ = new BinOpNode($1,$3,'/'); }
		;

N 		: F 
		| NOT F { $$ = new UnaryNode($2,'!'); }
		;
	
F    	: ident
		| INUM 
		| RNUM
		| BOOL
		| LPAREN expr RPAREN
		;
%%
