using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF = 0, // (EOF)
        SYMBOL_ERROR = 1, // (Error)
        SYMBOL_WHITESPACE = 2, // Whitespace
        SYMBOL_MINUS = 3, // '-'
        SYMBOL_LPAREN = 4, // '('
        SYMBOL_RPAREN = 5, // ')'
        SYMBOL_TIMES = 6, // '*'
        SYMBOL_COMMA = 7, // ','
        SYMBOL_DOT = 8, // '.'
        SYMBOL_DOTDOT = 9, // '..'
        SYMBOL_DIV = 10, // '/'
        SYMBOL_COLON = 11, // ':'
        SYMBOL_COLONEQ = 12, // ':='
        SYMBOL_SEMI = 13, // ';'
        SYMBOL_LBRACKET = 14, // '['
        SYMBOL_RBRACKET = 15, // ']'
        SYMBOL_CARET = 16, // '^'
        SYMBOL_PLUS = 17, // '+'
        SYMBOL_LT = 18, // '<'
        SYMBOL_LTEQ = 19, // '<='
        SYMBOL_LTGT = 20, // '<>'
        SYMBOL_EQ = 21, // '='
        SYMBOL_GT = 22, // '>'
        SYMBOL_GTEQ = 23, // '>='
        SYMBOL_AND = 24, // AND
        SYMBOL_ARRAY = 25, // ARRAY
        SYMBOL_BEGIN = 26, // BEGIN
        SYMBOL_CASE = 27, // CASE
        SYMBOL_CHARLITERAL = 28, // CharLiteral
        SYMBOL_CONST = 29, // CONST
        SYMBOL_DECLITERAL = 30, // DecLiteral
        SYMBOL_DIV2 = 31, // DIV
        SYMBOL_DO = 32, // DO
        SYMBOL_DOWNTO = 33, // DOWNTO
        SYMBOL_ELSE = 34, // ELSE
        SYMBOL_END = 35, // END
        SYMBOL_FILE = 36, // FILE
        SYMBOL_FLOATLITERAL = 37, // FloatLiteral
        SYMBOL_FOR = 38, // FOR
        SYMBOL_FORWARD = 39, // FORWARD
        SYMBOL_FUNCTION = 40, // FUNCTION
        SYMBOL_HEXLITERAL = 41, // HexLiteral
        SYMBOL_ID = 42, // id
        SYMBOL_IF = 43, // IF
        SYMBOL_MOD = 44, // MOD
        SYMBOL_NOT = 45, // NOT
        SYMBOL_OF = 46, // OF
        SYMBOL_OR = 47, // OR
        SYMBOL_PROCEDURE = 48, // PROCEDURE
        SYMBOL_PROGRAM = 49, // PROGRAM
        SYMBOL_RECORD = 50, // RECORD
        SYMBOL_REPEAT = 51, // REPEAT
        SYMBOL_STRINGLITERAL = 52, // StringLiteral
        SYMBOL_THEN = 53, // THEN
        SYMBOL_TO = 54, // TO
        SYMBOL_TYPE = 55, // TYPE
        SYMBOL_UNTIL = 56, // UNTIL
        SYMBOL_VAR = 57, // VAR
        SYMBOL_WHILE = 58, // WHILE
        SYMBOL_ACTUALS = 59, // <Actuals>
        SYMBOL_ARG = 60, // <Arg>
        SYMBOL_ARGUMENTLIST = 61, // <ArgumentList>
        SYMBOL_ARGUMENTS = 62, // <Arguments>
        SYMBOL_ASSIGNMENTSTATEMENT = 63, // <AssignmentStatement>
        SYMBOL_CASE2 = 64, // <Case>
        SYMBOL_CASELIST = 65, // <CaseList>
        SYMBOL_CASESTATEMENT = 66, // <CaseStatement>
        SYMBOL_COMPOUNDSTATEMENT = 67, // <CompoundStatement>
        SYMBOL_CONSTANT = 68, // <constant>
        SYMBOL_CONSTANTDEF = 69, // <ConstantDef>
        SYMBOL_CONSTANTDEFINITIONLIST = 70, // <ConstantDefinitionList>
        SYMBOL_CONSTANTDEFINITIONS = 71, // <ConstantDefinitions>
        SYMBOL_CONSTANTLIST = 72, // <ConstantList>
        SYMBOL_DECLARATIONS = 73, // <Declarations>
        SYMBOL_DIMENSION = 74, // <Dimension>
        SYMBOL_DIMENSIONLIST = 75, // <DimensionList>
        SYMBOL_EXPRESSION = 76, // <Expression>
        SYMBOL_EXPRESSIONLIST = 77, // <ExpressionList>
        SYMBOL_FACTOR = 78, // <Factor>
        SYMBOL_FIELD = 79, // <Field>
        SYMBOL_FIELDLIST = 80, // <FieldList>
        SYMBOL_FORSTATEMENT = 81, // <ForStatement>
        SYMBOL_FUNCTIONHEADER = 82, // <FunctionHeader>
        SYMBOL_IDLIST = 83, // <IdList>
        SYMBOL_IFSTATEMENT = 84, // <IfStatement>
        SYMBOL_PROCEDURECALL = 85, // <ProcedureCall>
        SYMBOL_PROCEDUREDEC = 86, // <ProcedureDec>
        SYMBOL_PROCEDUREDECLARATIONS = 87, // <ProcedureDeclarations>
        SYMBOL_PROCEDUREHEADER = 88, // <ProcedureHeader>
        SYMBOL_PROGRAM2 = 89, // <Program>
        SYMBOL_PROGRAMHEADER = 90, // <ProgramHeader>
        SYMBOL_REPEATSTATEMENT = 91, // <RepeatStatement>
        SYMBOL_SIMPLEEXPRESSION = 92, // <SimpleExpression>
        SYMBOL_STATEMENT = 93, // <Statement>
        SYMBOL_STATEMENTLIST = 94, // <StatementList>
        SYMBOL_TERM = 95, // <Term>
        SYMBOL_TYPEDEF = 96, // <TypeDef>
        SYMBOL_TYPEDEFINITIONLIST = 97, // <TypeDefinitionList>
        SYMBOL_TYPEDEFINITIONS = 98, // <TypeDefinitions>
        SYMBOL_TYPESPECIFIER = 99, // <TypeSpecifier>
        SYMBOL_VARIABLE = 100, // <Variable>
        SYMBOL_VARIABLEDEC = 101, // <VariableDec>
        SYMBOL_VARIABLEDECLARATIONLIST = 102, // <VariableDeclarationList>
        SYMBOL_VARIABLEDECLARATIONS = 103, // <VariableDeclarations>
        SYMBOL_WHILESTATEMENT = 104  // <WhileStatement>
    };

    enum RuleConstants : int
    {
        RULE_CONSTANT_DECLITERAL = 0, // <constant> ::= DecLiteral
        RULE_CONSTANT_STRINGLITERAL = 1, // <constant> ::= StringLiteral
        RULE_CONSTANT_FLOATLITERAL = 2, // <constant> ::= FloatLiteral
        RULE_CONSTANT_HEXLITERAL = 3, // <constant> ::= HexLiteral
        RULE_CONSTANT_CHARLITERAL = 4, // <constant> ::= CharLiteral
        RULE_PROGRAM_DOT = 5, // <Program> ::= <ProgramHeader> <Declarations> <CompoundStatement> '.'
        RULE_PROGRAMHEADER_PROGRAM_ID_SEMI = 6, // <ProgramHeader> ::= PROGRAM id ';'
        RULE_PROGRAMHEADER_PROGRAM_ID_LPAREN_RPAREN_SEMI = 7, // <ProgramHeader> ::= PROGRAM id '(' <IdList> ')' ';'
        RULE_DECLARATIONS = 8, // <Declarations> ::= <ConstantDefinitions> <TypeDefinitions> <VariableDeclarations> <ProcedureDeclarations>
        RULE_CONSTANTDEFINITIONS_CONST = 9, // <ConstantDefinitions> ::= CONST <ConstantDefinitionList>
        RULE_CONSTANTDEFINITIONS = 10, // <ConstantDefinitions> ::= 
        RULE_CONSTANTDEFINITIONLIST = 11, // <ConstantDefinitionList> ::= <ConstantDef>
        RULE_CONSTANTDEFINITIONLIST2 = 12, // <ConstantDefinitionList> ::= <ConstantDef> <ConstantDefinitionList>
        RULE_CONSTANTDEF_ID_EQ_SEMI = 13, // <ConstantDef> ::= id '=' <constant> ';'
        RULE_TYPEDEFINITIONS_TYPE = 14, // <TypeDefinitions> ::= TYPE <TypeDefinitionList>
        RULE_TYPEDEFINITIONS = 15, // <TypeDefinitions> ::= 
        RULE_TYPEDEFINITIONLIST = 16, // <TypeDefinitionList> ::= <TypeDef>
        RULE_TYPEDEFINITIONLIST2 = 17, // <TypeDefinitionList> ::= <TypeDef> <TypeDefinitionList>
        RULE_TYPEDEF_ID_EQ_SEMI = 18, // <TypeDef> ::= id '=' <TypeSpecifier> ';'
        RULE_VARIABLEDECLARATIONS_VAR = 19, // <VariableDeclarations> ::= VAR <VariableDeclarationList>
        RULE_VARIABLEDECLARATIONS = 20, // <VariableDeclarations> ::= 
        RULE_VARIABLEDECLARATIONLIST = 21, // <VariableDeclarationList> ::= <VariableDec>
        RULE_VARIABLEDECLARATIONLIST2 = 22, // <VariableDeclarationList> ::= <VariableDec> <VariableDeclarationList>
        RULE_VARIABLEDEC_COLON_SEMI = 23, // <VariableDec> ::= <IdList> ':' <TypeSpecifier> ';'
        RULE_PROCEDUREDECLARATIONS = 24, // <ProcedureDeclarations> ::= <ProcedureDec> <ProcedureDeclarations>
        RULE_PROCEDUREDECLARATIONS2 = 25, // <ProcedureDeclarations> ::= 
        RULE_PROCEDUREDEC_FORWARD_SEMI = 26, // <ProcedureDec> ::= <ProcedureHeader> FORWARD ';'
        RULE_PROCEDUREDEC_SEMI = 27, // <ProcedureDec> ::= <ProcedureHeader> <Declarations> <CompoundStatement> ';'
        RULE_PROCEDUREDEC_FORWARD_SEMI2 = 28, // <ProcedureDec> ::= <FunctionHeader> FORWARD ';'
        RULE_PROCEDUREDEC_SEMI2 = 29, // <ProcedureDec> ::= <FunctionHeader> <Declarations> <CompoundStatement> ';'
        RULE_PROCEDUREHEADER_PROCEDURE_ID_SEMI = 30, // <ProcedureHeader> ::= PROCEDURE id <Arguments> ';'
        RULE_FUNCTIONHEADER_FUNCTION_ID_COLON_SEMI = 31, // <FunctionHeader> ::= FUNCTION id <Arguments> ':' <TypeSpecifier> ';'
        RULE_ARGUMENTS_LPAREN_RPAREN = 32, // <Arguments> ::= '(' <ArgumentList> ')'
        RULE_ARGUMENTS = 33, // <Arguments> ::= 
        RULE_ARGUMENTLIST = 34, // <ArgumentList> ::= <Arg>
        RULE_ARGUMENTLIST_SEMI = 35, // <ArgumentList> ::= <Arg> ';' <ArgumentList>
        RULE_ARG_COLON = 36, // <Arg> ::= <IdList> ':' <TypeSpecifier>
        RULE_ARG_VAR_COLON = 37, // <Arg> ::= VAR <IdList> ':' <TypeSpecifier>
        RULE_COMPOUNDSTATEMENT_BEGIN_END = 38, // <CompoundStatement> ::= BEGIN <StatementList> END
        RULE_STATEMENTLIST = 39, // <StatementList> ::= <Statement>
        RULE_STATEMENTLIST_SEMI = 40, // <StatementList> ::= <Statement> ';' <StatementList>
        RULE_STATEMENT = 41, // <Statement> ::= <CompoundStatement>
        RULE_STATEMENT2 = 42, // <Statement> ::= <AssignmentStatement>
        RULE_STATEMENT3 = 43, // <Statement> ::= <ProcedureCall>
        RULE_STATEMENT4 = 44, // <Statement> ::= <ForStatement>
        RULE_STATEMENT5 = 45, // <Statement> ::= <WhileStatement>
        RULE_STATEMENT6 = 46, // <Statement> ::= <IfStatement>
        RULE_STATEMENT7 = 47, // <Statement> ::= <CaseStatement>
        RULE_STATEMENT8 = 48, // <Statement> ::= <RepeatStatement>
        RULE_STATEMENT9 = 49, // <Statement> ::= 
        RULE_ASSIGNMENTSTATEMENT_COLONEQ = 50, // <AssignmentStatement> ::= <Variable> ':=' <Expression>
        RULE_PROCEDURECALL_ID = 51, // <ProcedureCall> ::= id <Actuals>
        RULE_FORSTATEMENT_FOR_ID_COLONEQ_TO_DO = 52, // <ForStatement> ::= FOR id ':=' <Expression> TO <Expression> DO <Statement>
        RULE_FORSTATEMENT_FOR_ID_COLONEQ_DOWNTO_DO = 53, // <ForStatement> ::= FOR id ':=' <Expression> DOWNTO <Expression> DO <Statement>
        RULE_WHILESTATEMENT_WHILE_DO = 54, // <WhileStatement> ::= WHILE <Expression> DO <Statement>
        RULE_IFSTATEMENT_IF_THEN_ELSE = 55, // <IfStatement> ::= IF <Expression> THEN <Statement> ELSE <Statement>
        RULE_REPEATSTATEMENT_REPEAT_UNTIL = 56, // <RepeatStatement> ::= REPEAT <StatementList> UNTIL <Expression>
        RULE_CASESTATEMENT_CASE_OF_END = 57, // <CaseStatement> ::= CASE <Expression> OF <CaseList> END
        RULE_CASELIST = 58, // <CaseList> ::= <Case>
        RULE_CASELIST_SEMI = 59, // <CaseList> ::= <Case> ';' <CaseList>
        RULE_CASE_COLON = 60, // <Case> ::= <ConstantList> ':' <Statement>
        RULE_CONSTANTLIST = 61, // <ConstantList> ::= <constant>
        RULE_CONSTANTLIST_COMMA = 62, // <ConstantList> ::= <constant> ',' <ConstantList>
        RULE_EXPRESSION = 63, // <Expression> ::= <SimpleExpression>
        RULE_EXPRESSION_EQ = 64, // <Expression> ::= <SimpleExpression> '=' <SimpleExpression>
        RULE_EXPRESSION_LTGT = 65, // <Expression> ::= <SimpleExpression> '<>' <SimpleExpression>
        RULE_EXPRESSION_LT = 66, // <Expression> ::= <SimpleExpression> '<' <SimpleExpression>
        RULE_EXPRESSION_LTEQ = 67, // <Expression> ::= <SimpleExpression> '<=' <SimpleExpression>
        RULE_EXPRESSION_GT = 68, // <Expression> ::= <SimpleExpression> '>' <SimpleExpression>
        RULE_EXPRESSION_GTEQ = 69, // <Expression> ::= <SimpleExpression> '>=' <SimpleExpression>
        RULE_SIMPLEEXPRESSION = 70, // <SimpleExpression> ::= <Term>
        RULE_SIMPLEEXPRESSION_PLUS = 71, // <SimpleExpression> ::= <SimpleExpression> '+' <Term>
        RULE_SIMPLEEXPRESSION_MINUS = 72, // <SimpleExpression> ::= <SimpleExpression> '-' <Term>
        RULE_SIMPLEEXPRESSION_OR = 73, // <SimpleExpression> ::= <SimpleExpression> OR <Term>
        RULE_TERM = 74, // <Term> ::= <Factor>
        RULE_TERM_TIMES = 75, // <Term> ::= <Term> '*' <Factor>
        RULE_TERM_DIV = 76, // <Term> ::= <Term> '/' <Factor>
        RULE_TERM_DIV2 = 77, // <Term> ::= <Term> DIV <Factor>
        RULE_TERM_MOD = 78, // <Term> ::= <Term> MOD <Factor>
        RULE_TERM_AND = 79, // <Term> ::= <Term> AND <Factor>
        RULE_FACTOR_LPAREN_RPAREN = 80, // <Factor> ::= '(' <Expression> ')'
        RULE_FACTOR_PLUS = 81, // <Factor> ::= '+' <Factor>
        RULE_FACTOR_MINUS = 82, // <Factor> ::= '-' <Factor>
        RULE_FACTOR_NOT = 83, // <Factor> ::= NOT <Factor>
        RULE_FACTOR = 84, // <Factor> ::= <constant>
        RULE_FACTOR2 = 85, // <Factor> ::= <Variable>
        RULE_ACTUALS_LPAREN_RPAREN = 86, // <Actuals> ::= '(' <ExpressionList> ')'
        RULE_ACTUALS = 87, // <Actuals> ::= 
        RULE_EXPRESSIONLIST = 88, // <ExpressionList> ::= <Expression>
        RULE_EXPRESSIONLIST_COMMA = 89, // <ExpressionList> ::= <Expression> ',' <ExpressionList>
        RULE_VARIABLE_ID = 90, // <Variable> ::= id
        RULE_VARIABLE_DOT_ID = 91, // <Variable> ::= <Variable> '.' id
        RULE_VARIABLE_CARET = 92, // <Variable> ::= <Variable> '^'
        RULE_VARIABLE_LBRACKET_RBRACKET = 93, // <Variable> ::= <Variable> '[' <ExpressionList> ']'
        RULE_TYPESPECIFIER_ID = 94, // <TypeSpecifier> ::= id
        RULE_TYPESPECIFIER_CARET = 95, // <TypeSpecifier> ::= '^' <TypeSpecifier>
        RULE_TYPESPECIFIER_LPAREN_RPAREN = 96, // <TypeSpecifier> ::= '(' <IdList> ')'
        RULE_TYPESPECIFIER_DOTDOT = 97, // <TypeSpecifier> ::= <constant> '..' <constant>
        RULE_TYPESPECIFIER_ARRAY_LBRACKET_RBRACKET_OF = 98, // <TypeSpecifier> ::= ARRAY '[' <DimensionList> ']' OF <TypeSpecifier>
        RULE_TYPESPECIFIER_RECORD_END = 99, // <TypeSpecifier> ::= RECORD <FieldList> END
        RULE_TYPESPECIFIER_FILE_OF = 100, // <TypeSpecifier> ::= FILE OF <TypeSpecifier>
        RULE_DIMENSIONLIST = 101, // <DimensionList> ::= <Dimension>
        RULE_DIMENSIONLIST_COMMA = 102, // <DimensionList> ::= <Dimension> ',' <DimensionList>
        RULE_DIMENSION_DOTDOT = 103, // <Dimension> ::= <constant> '..' <constant>
        RULE_DIMENSION_ID = 104, // <Dimension> ::= id
        RULE_FIELDLIST = 105, // <FieldList> ::= <Field>
        RULE_FIELDLIST_SEMI = 106, // <FieldList> ::= <Field> ';' <FieldList>
        RULE_FIELD_COLON = 107, // <Field> ::= <IdList> ':' <TypeSpecifier>
        RULE_IDLIST_ID = 108, // <IdList> ::= id
        RULE_IDLIST_ID_COMMA = 109  // <IdList> ::= id ',' <IdList>
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox lstErrors;
        List<string> reserved_words_const = new List<string>(new string[] { "uses", "var", "function", "procedure" });
        List<string> reserved_words_var = new List<string>(new string[] { "function", "procedure", "begin", "end" });
        List<string> keywords = new List<string>(new string[]
        {
            "and","div","file","in","of","record","array","do",
            "for","label","or","repeat","until","begin","downto","else",
            "function","mod","packed","set","var","case","goto","nil",
            "procedure","then","while","const","end","if","not","program"
            ,"read", "write", "readln", "writeln","to","with",
             "(",")","+","-","{","}","/","*",",",";", "=", ":=", ":", "."  });
        Dictionary<string, string> mapping = new Dictionary<string, string>();
        int const_flag = 0; //to detect const keyword
        int var_flag = 0; //to detect var keyword
        int assginment_flag = 0;
        int colon_flag = 0;
        string last_token = ""; //to track PROGRAM keyword 
        List<string> constants = new List<string>(); //to store constants
        List<string> vars = new List<string>(); //to store vars
        List<string> words = new List<string>(); //keep track of words for constants
        List<string> allwords = new List<string>(); //keep track of all words
       
        public MyParser(string filename, ListBox lstErrors)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open,
                                               FileAccess.Read,
                                               FileShare.Read);
            Init(stream);
            stream.Close();
            this.lstErrors = lstErrors;
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;
            parser.OnShift += new LALRParser.ShiftHandler(ShiftEvent);
            parser.OnReduce += new LALRParser.ReduceHandler(ReduceEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
            parser.OnAccept += new LALRParser.AcceptHandler(AcceptEvent);
            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            parser.Parse(source);

        }

        private void TokenReadEvent(LALRParser parser, TokenReadEventArgs args)
        {
            try
            {
                args.Token.UserObject = CreateObject(args.Token);
            }
            catch (Exception e)
            {
                args.Continue = false;
                //todo: Report message to UI?
            }
        }

        private Object CreateObject(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF:
                    //(EOF)
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ERROR:
                    //(Error)
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE:
                    //Whitespace
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_MINUS:
                    //'-'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_LPAREN:
                    //'('
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_RPAREN:
                    //')'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_TIMES:
                    //'*'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_COMMA:
                    //','
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_DOT:
                    //'.'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_DOTDOT:
                    //'..'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_DIV:
                    //'/'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_COLON:
                    //':'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_COLONEQ:
                    //':='
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_SEMI:
                    //';'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_LBRACKET:
                    //'['
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_RBRACKET:
                    //']'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CARET:
                    //'^'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PLUS:
                    //'+'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_LT:
                    //'<'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_LTEQ:
                    //'<='
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_LTGT:
                    //'<>'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_EQ:
                    //'='
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_GT:
                    //'>'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_GTEQ:
                    //'>='
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_AND:
                    //AND
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ARRAY:
                    //ARRAY
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_BEGIN:
                    //BEGIN
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CASE:
                    //CASE
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CHARLITERAL:
                    //CharLiteral
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CONST:
                    //CONST
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_DECLITERAL:
                    //DecLiteral
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_DIV2:
                    //DIV
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_DO:
                    //DO
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_DOWNTO:
                    //DOWNTO
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ELSE:
                    //ELSE
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_END:
                    //END
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FILE:
                    //FILE
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FLOATLITERAL:
                    //FloatLiteral
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FOR:
                    //FOR
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FORWARD:
                    //FORWARD
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FUNCTION:
                    //FUNCTION
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_HEXLITERAL:
                    //HexLiteral
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ID:
                    //id
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_IF:
                    //IF
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_MOD:
                    //MOD
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_NOT:
                    //NOT
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_OF:
                    //OF
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_OR:
                    //OR
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PROCEDURE:
                    //PROCEDURE
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM:
                    //PROGRAM
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_RECORD:
                    //RECORD
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_REPEAT:
                    //REPEAT
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_STRINGLITERAL:
                    //StringLiteral
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_THEN:
                    //THEN
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_TO:
                    //TO
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_TYPE:
                    //TYPE
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_UNTIL:
                    //UNTIL
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_VAR:
                    //VAR
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_WHILE:
                    //WHILE
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ACTUALS:
                    //<Actuals>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ARG:
                    //<Arg>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ARGUMENTLIST:
                    //<ArgumentList>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ARGUMENTS:
                    //<Arguments>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ASSIGNMENTSTATEMENT:
                    //<AssignmentStatement>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CASE2:
                    //<Case>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CASELIST:
                    //<CaseList>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CASESTATEMENT:
                    //<CaseStatement>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_COMPOUNDSTATEMENT:
                    //<CompoundStatement>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CONSTANT:
                    //<constant>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CONSTANTDEF:
                    //<ConstantDef>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CONSTANTDEFINITIONLIST:
                    //<ConstantDefinitionList>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CONSTANTDEFINITIONS:
                    //<ConstantDefinitions>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CONSTANTLIST:
                    //<ConstantList>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_DECLARATIONS:
                    //<Declarations>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_DIMENSION:
                    //<Dimension>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_DIMENSIONLIST:
                    //<DimensionList>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION:
                    //<Expression>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSIONLIST:
                    //<ExpressionList>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FACTOR:
                    //<Factor>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FIELD:
                    //<Field>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FIELDLIST:
                    //<FieldList>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FORSTATEMENT:
                    //<ForStatement>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FUNCTIONHEADER:
                    //<FunctionHeader>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_IDLIST:
                    //<IdList>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_IFSTATEMENT:
                    //<IfStatement>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PROCEDURECALL:
                    //<ProcedureCall>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PROCEDUREDEC:
                    //<ProcedureDec>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PROCEDUREDECLARATIONS:
                    //<ProcedureDeclarations>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PROCEDUREHEADER:
                    //<ProcedureHeader>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM2:
                    //<Program>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PROGRAMHEADER:
                    //<ProgramHeader>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_REPEATSTATEMENT:
                    //<RepeatStatement>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_SIMPLEEXPRESSION:
                    //<SimpleExpression>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT:
                    //<Statement>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTLIST:
                    //<StatementList>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_TERM:
                    //<Term>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_TYPEDEF:
                    //<TypeDef>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_TYPEDEFINITIONLIST:
                    //<TypeDefinitionList>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_TYPEDEFINITIONS:
                    //<TypeDefinitions>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_TYPESPECIFIER:
                    //<TypeSpecifier>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_VARIABLE:
                    //<Variable>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEDEC:
                    //<VariableDec>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEDECLARATIONLIST:
                    //<VariableDeclarationList>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEDECLARATIONS:
                    //<VariableDeclarations>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_WHILESTATEMENT:
                    //<WhileStatement>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        private void ReduceEvent(LALRParser parser, ReduceEventArgs args)
        {
            try
            {
                args.Token.UserObject = CreateObject(args.Token);
            }
            catch (Exception e)
            {
                args.Continue = false;
                //todo: Report message to UI?
            }
        }

        public static Object CreateObject(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_CONSTANT_DECLITERAL:
                    //<constant> ::= DecLiteral
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_CONSTANT_STRINGLITERAL:
                    //<constant> ::= StringLiteral
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_CONSTANT_FLOATLITERAL:
                    //<constant> ::= FloatLiteral
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_CONSTANT_HEXLITERAL:
                    //<constant> ::= HexLiteral
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_CONSTANT_CHARLITERAL:
                    //<constant> ::= CharLiteral
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_PROGRAM_DOT:
                    //<Program> ::= <ProgramHeader> <Declarations> <CompoundStatement> '.'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_PROGRAMHEADER_PROGRAM_ID_SEMI:
                    //<ProgramHeader> ::= PROGRAM id ';'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_PROGRAMHEADER_PROGRAM_ID_LPAREN_RPAREN_SEMI:
                    //<ProgramHeader> ::= PROGRAM id '(' <IdList> ')' ';'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_DECLARATIONS:
                    //<Declarations> ::= <ConstantDefinitions> <TypeDefinitions> <VariableDeclarations> <ProcedureDeclarations>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_CONSTANTDEFINITIONS_CONST:
                    //<ConstantDefinitions> ::= CONST <ConstantDefinitionList>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_CONSTANTDEFINITIONS:
                    //<ConstantDefinitions> ::= 
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_CONSTANTDEFINITIONLIST:
                    //<ConstantDefinitionList> ::= <ConstantDef>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_CONSTANTDEFINITIONLIST2:
                    //<ConstantDefinitionList> ::= <ConstantDef> <ConstantDefinitionList>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_CONSTANTDEF_ID_EQ_SEMI:
                    //<ConstantDef> ::= id '=' <constant> ';'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TYPEDEFINITIONS_TYPE:
                    //<TypeDefinitions> ::= TYPE <TypeDefinitionList>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TYPEDEFINITIONS:
                    //<TypeDefinitions> ::= 
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TYPEDEFINITIONLIST:
                    //<TypeDefinitionList> ::= <TypeDef>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TYPEDEFINITIONLIST2:
                    //<TypeDefinitionList> ::= <TypeDef> <TypeDefinitionList>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TYPEDEF_ID_EQ_SEMI:
                    //<TypeDef> ::= id '=' <TypeSpecifier> ';'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATIONS_VAR:
                    //<VariableDeclarations> ::= VAR <VariableDeclarationList>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATIONS:
                    //<VariableDeclarations> ::= 
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATIONLIST:
                    //<VariableDeclarationList> ::= <VariableDec>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATIONLIST2:
                    //<VariableDeclarationList> ::= <VariableDec> <VariableDeclarationList>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_VARIABLEDEC_COLON_SEMI:
                    //<VariableDec> ::= <IdList> ':' <TypeSpecifier> ';'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_PROCEDUREDECLARATIONS:
                    //<ProcedureDeclarations> ::= <ProcedureDec> <ProcedureDeclarations>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_PROCEDUREDECLARATIONS2:
                    //<ProcedureDeclarations> ::= 
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_PROCEDUREDEC_FORWARD_SEMI:
                    //<ProcedureDec> ::= <ProcedureHeader> FORWARD ';'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_PROCEDUREDEC_SEMI:
                    //<ProcedureDec> ::= <ProcedureHeader> <Declarations> <CompoundStatement> ';'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_PROCEDUREDEC_FORWARD_SEMI2:
                    //<ProcedureDec> ::= <FunctionHeader> FORWARD ';'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_PROCEDUREDEC_SEMI2:
                    //<ProcedureDec> ::= <FunctionHeader> <Declarations> <CompoundStatement> ';'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_PROCEDUREHEADER_PROCEDURE_ID_SEMI:
                    //<ProcedureHeader> ::= PROCEDURE id <Arguments> ';'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_FUNCTIONHEADER_FUNCTION_ID_COLON_SEMI:
                    //<FunctionHeader> ::= FUNCTION id <Arguments> ':' <TypeSpecifier> ';'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_ARGUMENTS_LPAREN_RPAREN:
                    //<Arguments> ::= '(' <ArgumentList> ')'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_ARGUMENTS:
                    //<Arguments> ::= 
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_ARGUMENTLIST:
                    //<ArgumentList> ::= <Arg>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_ARGUMENTLIST_SEMI:
                    //<ArgumentList> ::= <Arg> ';' <ArgumentList>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_ARG_COLON:
                    //<Arg> ::= <IdList> ':' <TypeSpecifier>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_ARG_VAR_COLON:
                    //<Arg> ::= VAR <IdList> ':' <TypeSpecifier>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_COMPOUNDSTATEMENT_BEGIN_END:
                    //<CompoundStatement> ::= BEGIN <StatementList> END
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_STATEMENTLIST:
                    //<StatementList> ::= <Statement>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_STATEMENTLIST_SEMI:
                    //<StatementList> ::= <Statement> ';' <StatementList>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_STATEMENT:
                    //<Statement> ::= <CompoundStatement>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_STATEMENT2:
                    //<Statement> ::= <AssignmentStatement>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_STATEMENT3:
                    //<Statement> ::= <ProcedureCall>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_STATEMENT4:
                    //<Statement> ::= <ForStatement>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_STATEMENT5:
                    //<Statement> ::= <WhileStatement>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_STATEMENT6:
                    //<Statement> ::= <IfStatement>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_STATEMENT7:
                    //<Statement> ::= <CaseStatement>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_STATEMENT8:
                    //<Statement> ::= <RepeatStatement>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_STATEMENT9:
                    //<Statement> ::= 
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_ASSIGNMENTSTATEMENT_COLONEQ:
                    //<AssignmentStatement> ::= <Variable> ':=' <Expression>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_PROCEDURECALL_ID:
                    //<ProcedureCall> ::= id <Actuals>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_FORSTATEMENT_FOR_ID_COLONEQ_TO_DO:
                    //<ForStatement> ::= FOR id ':=' <Expression> TO <Expression> DO <Statement>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_FORSTATEMENT_FOR_ID_COLONEQ_DOWNTO_DO:
                    //<ForStatement> ::= FOR id ':=' <Expression> DOWNTO <Expression> DO <Statement>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_WHILESTATEMENT_WHILE_DO:
                    //<WhileStatement> ::= WHILE <Expression> DO <Statement>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_IFSTATEMENT_IF_THEN_ELSE:
                    //<IfStatement> ::= IF <Expression> THEN <Statement> ELSE <Statement>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_REPEATSTATEMENT_REPEAT_UNTIL:
                    //<RepeatStatement> ::= REPEAT <StatementList> UNTIL <Expression>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_CASESTATEMENT_CASE_OF_END:
                    //<CaseStatement> ::= CASE <Expression> OF <CaseList> END
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_CASELIST:
                    //<CaseList> ::= <Case>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_CASELIST_SEMI:
                    //<CaseList> ::= <Case> ';' <CaseList>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_CASE_COLON:
                    //<Case> ::= <ConstantList> ':' <Statement>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_CONSTANTLIST:
                    //<ConstantList> ::= <constant>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_CONSTANTLIST_COMMA:
                    //<ConstantList> ::= <constant> ',' <ConstantList>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSION:
                    //<Expression> ::= <SimpleExpression>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSION_EQ:
                    //<Expression> ::= <SimpleExpression> '=' <SimpleExpression>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSION_LTGT:
                    //<Expression> ::= <SimpleExpression> '<>' <SimpleExpression>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSION_LT:
                    //<Expression> ::= <SimpleExpression> '<' <SimpleExpression>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSION_LTEQ:
                    //<Expression> ::= <SimpleExpression> '<=' <SimpleExpression>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSION_GT:
                    //<Expression> ::= <SimpleExpression> '>' <SimpleExpression>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSION_GTEQ:
                    //<Expression> ::= <SimpleExpression> '>=' <SimpleExpression>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_SIMPLEEXPRESSION:
                    //<SimpleExpression> ::= <Term>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_SIMPLEEXPRESSION_PLUS:
                    //<SimpleExpression> ::= <SimpleExpression> '+' <Term>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_SIMPLEEXPRESSION_MINUS:
                    //<SimpleExpression> ::= <SimpleExpression> '-' <Term>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_SIMPLEEXPRESSION_OR:
                    //<SimpleExpression> ::= <SimpleExpression> OR <Term>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TERM:
                    //<Term> ::= <Factor>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TERM_TIMES:
                    //<Term> ::= <Term> '*' <Factor>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TERM_DIV:
                    //<Term> ::= <Term> '/' <Factor>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TERM_DIV2:
                    //<Term> ::= <Term> DIV <Factor>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TERM_MOD:
                    //<Term> ::= <Term> MOD <Factor>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TERM_AND:
                    //<Term> ::= <Term> AND <Factor>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_FACTOR_LPAREN_RPAREN:
                    //<Factor> ::= '(' <Expression> ')'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_FACTOR_PLUS:
                    //<Factor> ::= '+' <Factor>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_FACTOR_MINUS:
                    //<Factor> ::= '-' <Factor>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_FACTOR_NOT:
                    //<Factor> ::= NOT <Factor>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_FACTOR:
                    //<Factor> ::= <constant>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_FACTOR2:
                    //<Factor> ::= <Variable>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_ACTUALS_LPAREN_RPAREN:
                    //<Actuals> ::= '(' <ExpressionList> ')'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_ACTUALS:
                    //<Actuals> ::= 
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSIONLIST:
                    //<ExpressionList> ::= <Expression>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSIONLIST_COMMA:
                    //<ExpressionList> ::= <Expression> ',' <ExpressionList>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_VARIABLE_ID:
                    //<Variable> ::= id
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_VARIABLE_DOT_ID:
                    //<Variable> ::= <Variable> '.' id
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_VARIABLE_CARET:
                    //<Variable> ::= <Variable> '^'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_VARIABLE_LBRACKET_RBRACKET:
                    //<Variable> ::= <Variable> '[' <ExpressionList> ']'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TYPESPECIFIER_ID:
                    //<TypeSpecifier> ::= id
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TYPESPECIFIER_CARET:
                    //<TypeSpecifier> ::= '^' <TypeSpecifier>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TYPESPECIFIER_LPAREN_RPAREN:
                    //<TypeSpecifier> ::= '(' <IdList> ')'
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TYPESPECIFIER_DOTDOT:
                    //<TypeSpecifier> ::= <constant> '..' <constant>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TYPESPECIFIER_ARRAY_LBRACKET_RBRACKET_OF:
                    //<TypeSpecifier> ::= ARRAY '[' <DimensionList> ']' OF <TypeSpecifier>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TYPESPECIFIER_RECORD_END:
                    //<TypeSpecifier> ::= RECORD <FieldList> END
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_TYPESPECIFIER_FILE_OF:
                    //<TypeSpecifier> ::= FILE OF <TypeSpecifier>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_DIMENSIONLIST:
                    //<DimensionList> ::= <Dimension>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_DIMENSIONLIST_COMMA:
                    //<DimensionList> ::= <Dimension> ',' <DimensionList>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_DIMENSION_DOTDOT:
                    //<Dimension> ::= <constant> '..' <constant>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_DIMENSION_ID:
                    //<Dimension> ::= id
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_FIELDLIST:
                    //<FieldList> ::= <Field>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_FIELDLIST_SEMI:
                    //<FieldList> ::= <Field> ';' <FieldList>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_FIELD_COLON:
                    //<Field> ::= <IdList> ':' <TypeSpecifier>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_IDLIST_ID:
                    //<IdList> ::= id
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_IDLIST_ID_COMMA:
                    //<IdList> ::= id ',' <IdList>
                    //todo: Create a new object using the stored user objects.
                    return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void AcceptEvent(LALRParser parser, AcceptEventArgs args)
        {
            //todo: Use your fully reduced args.Token.UserObject
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '" + args.Token.ToString() + "'";
            lstErrors.Items.Add(message);
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '" + args.UnexpectedToken.ToString() + "'"
                + " in line: " + args.UnexpectedToken.Location.LineNr;
            lstErrors.Items.Add(message);
            string message2 = "Expected Token: '" + args.ExpectedTokens.ToString() + "'";
            lstErrors.Items.Add(message2);
            //todo: Report message to UI?
        }

        private void ShiftEvent(LALRParser parser, ShiftEventArgs args)
        {
           /*
           rule: Cannot reassign Constants
           rule logic :-
               - if const keyword found, turn const_flag on
               - find the first identifier
               - push the string in the constants list
               - if you find semi colon, start with new identifer
               - keep repeating till you find another keyword
           */
            if (args.Token.Text.ToLower() == "const")
            {
                const_flag = 1;
                //return;
            }
            else if (reserved_words_const.Contains(args.Token.Text.ToLower()))
            {
                const_flag = 0;
                //return;
            }

            if (const_flag == 1)
            {
                // PI = 3.14;
                if (args.Token.Text == "=")
                {
                    //lstErrors.Items.Add("equal found");
                    assginment_flag = 1;
                }
                else if (args.Token.Text == ";")
                {
                    //lstErrors.Items.Add("semicolon found");
                    assginment_flag = 0;
                }

                else if(keywords.Contains(args.Token.Text.ToLower()) == false)
                {
                    if (assginment_flag == 0)
                    {
                        if (constants.Contains(args.Token.Text.ToLower()))
                        {
                            string message = "Semantic error (constant reassignment) caused by token: " + args.Token.Text;
                            lstErrors.Items.Add(message);
                        }
                        else
                            constants.Add(args.Token.Text.ToLower());
                    }

                    //else
                    // numeric value of constant
                    // do nothing
                }
            }
            else
            {
                //if trying to reassign a constant variable -> raise error 
                if (args.Token.Text == ":=")
                {
                    for (int i = 0; i < constants.Count; i++)
                    {
                        if (constants[i] == words[words.Count - 1] && words.Count - 1 >= 0)
                        {
                            string message = "Semantic error (constant reassignment) caused by token: " + words[words.Count - 1];
                            lstErrors.Items.Add(message);
                        }
                    }
                }
                words.Add(args.Token.Text.ToLower());
            }

            //------------------------------------------------------//
           /*
           rule: Cannot use variables without declaration
           rule logic :-
              - Get all variables in the vars list
              - Check for undeclared variables (not keywords)
           */
            if (args.Token.Text.ToLower() == "var")
            {
                var_flag = 1;
            }
            else if (reserved_words_var.Contains(args.Token.Text.ToLower()))
            {
                var_flag = 0;

            }

            if (var_flag == 1)
            {
                //update mapping
                if(last_token == ":")
                {
                    for(int i = allwords.Count-1; i>=0; i--)
                    {
                        if (allwords[i] == "var" || allwords[i] == ";")
                            break;
                        else
                            mapping[allwords[i]] = args.Token.Text.ToLower();
                    }    
                }
                // width, height : Integer;
                if (args.Token.Text == ":")
                {
                    //lstErrors.Items.Add("colon found");
                    colon_flag = 1;
                }
                else if (args.Token.Text == ";")
                {
                    //lstErrors.Items.Add("semicolon found");
                    colon_flag = 0;
                }

                else
                {
                    if (colon_flag == 0 && args.Token.Text != ",")
                        vars.Add(args.Token.Text.ToLower());
                    //else
                    // variables datatype
                    // do nothing
                }
            }
            //not var_flag
            else
            {
                string candidate = args.Token.Text;
                int num;
                bool check = int.TryParse(candidate, out num);


                //undeclared variable
                if (keywords.Contains(args.Token.Text.ToLower()) == false && vars.Contains(args.Token.Text.ToLower()) == false &&
                    constants.Contains(args.Token.Text.ToLower()) == false && check==false && (candidate[0].ToString() != "'") &&
                    last_token.ToLower() != "program")
                { 
                        string message = "Semantic error (undeclared variable) caused by token: " + args.Token.Text;
                        lstErrors.Items.Add(message);
                }
                else
                {
                    //to check type mismatch
                    try
                    {
                        if (last_token == ":=")
                        {
                            int numm;
                            bool value_type = int.TryParse(args.Token.Text, out numm);
                            string type = mapping[allwords[allwords.Count - 2]];

                            //string = int
                            if (value_type == true)
                            {
                                string message = "semantic error (type mismatch) caused by token: " + args.Token.Text;
                                lstErrors.Items.Add(message);
                            }
                            else if (value_type == false && type == "integer")
                            {
                                string message = "semantic error (type mismatch) caused by token: " + args.Token.Text;
                                lstErrors.Items.Add(message);
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            //--------------------------------------------------------//
            /*
            rule: Cannot assign string to integer and vice versa
            rule logic :-
                - Map variables to its datatypes
                - If you find (variable = value)
                - check on variable type from map, raise error if value is not compitable
            */
             //check rule 3

            last_token = args.Token.Text;
            //Don't take , : into consideration for mapping
            if (last_token != "," && last_token !=":")
                allwords.Add(last_token);

            //Check Mapping
            /*foreach (KeyValuePair<string, string> entry in mapping)
            {
                // do something with entry.Value or entry.Key
                string ms1 = entry.Key;
                string ms2 = entry.Value;
                string ms = ms1 + " " + ms2;
                lstErrors.Items.Add(ms);
            }*/

        }
    }
}
