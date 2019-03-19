﻿using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using SimpleScanner;
using SimpleParser;
using SimpleLang.Visitors;

namespace SimpleCompiler
{
    public class SimpleCompilerMain
    {
        public static void Main()
        {
            string FileName = @"..\..\a.txt";
            try
            {
                string Text = File.ReadAllText(FileName);

                Scanner scanner = new Scanner();
                scanner.SetSource(Text, 0);

                Parser parser = new Parser(scanner);

                var b = parser.Parse();
                if (!b)
                    Console.WriteLine("Ошибка");
                else
                {
                    Console.WriteLine("Синтаксическое дерево построено");

                    var fillParentVsitor = new FillParentVisitor();
                    parser.root.Visit(fillParentVsitor);



                    var opt1Visitor = new OptVisitor();
                    var opt14Visitor = new OptWhileVisitor();

                    bool isPerformed = true;
                    while (isPerformed)
                    {
                        parser.root.Visit(opt14Visitor);
                        while ((isPerformed = opt14Visitor.IsPerformed))
                        {
                            opt14Visitor.IsPerformed = false;
                            parser.root.Visit(opt14Visitor);
                        }

                        parser.root.Visit(opt1Visitor);
                        if (!opt1Visitor.IsPerformed) break;     

                    }


                  //  while(!opt14Visitor.IsPerformed)

                   // parser.root.Visit(optVisitior);

                    var pepeVisitor = new PrettyPrintVisitor();
                    parser.root.Visit(pepeVisitor);
                    Console.WriteLine(pepeVisitor.Text);
                  


                    //.Visit(avis);
                    //Console.WriteLine("Количество присваиваний = {0}", avis.Count);
                    //Console.WriteLine("-------------------------------");

                    //var pp = new PrettyPrintVisitor();
                    //parser.root.Visit(pp);
                    //Console.WriteLine(pp.Text);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл {0} не найден", FileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e);
            }

            Console.ReadLine();
        }

    }
}
