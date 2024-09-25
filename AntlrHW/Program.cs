using System;
using System.IO;
using Antlr4.Runtime;

namespace AntlrHW
{
    class Program
    {
        public class ErrorListener<S> : Antlr4.Runtime.ConsoleErrorListener<S>
        {
            public bool had_error = false;

            public override void SyntaxError(TextWriter output, IRecognizer recognizer, S offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
            {
                had_error = true;
                base.SyntaxError(output, recognizer, offendingSymbol, line, charPositionInLine, msg, e);
            }
        }

        static void Main(string[] args)
        {
            var input = new AntlrInputStream("hello world");
            var lexer = new HelloLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new HelloParser(tokens);
            var listener = new ErrorListener<IToken>();
            parser.AddErrorListener(listener);
            var tree = parser.r();
	    System.Console.WriteLine(listener.had_error ? "Fail" : "Pass");
	    System.Console.WriteLine(tree.ToStringTree(parser));
        }

    }
}
