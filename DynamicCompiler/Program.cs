using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DynamicCompiler
{
    class Program
    {

        static void Main(string[] args)
        {
            //1.CSharpCodePrivoder
            CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();

            //2.ICodeComplier
            ICodeCompiler objICodeCompiler = objCSharpCodePrivoder.CreateCompiler();

            //3.CompilerParameters
            CompilerParameters objCompilerParameters = new CompilerParameters();
            objCompilerParameters.ReferencedAssemblies.Add("System.dll");
            objCompilerParameters.GenerateExecutable = false;
            objCompilerParameters.GenerateInMemory = true;

            //4.CompilerResults
            CompilerResults cr = objICodeCompiler.CompileAssemblyFromSource(objCompilerParameters, GenerateCode());

            if (cr.Errors.HasErrors)
            {
                Console.WriteLine("编译错误");

                foreach (CompilerError err in cr.Errors)
                {
                    Console.WriteLine(err.ErrorText);
                }
            }
            else
            {
                //通过反射，调用HelloWorld的实例
                Assembly objAssembly = cr.CompiledAssembly;
                object objHelloWorld = objAssembly.CreateInstance(
                    "DynamicCompiler.Program");

                MethodInfo objMI = objHelloWorld.GetType().GetMethod("OutPut");

                Console.WriteLine(objMI.Invoke(objHelloWorld, null));

                //动态编译的assembly不会生成在文件夹下，是动态生成在内存中的，所以，即使命名空间和类名相同，定义了相同的方法，也相当于是在
                //不同的dll(assembly)中。
                MethodInfo objTest = objHelloWorld.GetType().GetMethod("TestFunction");

                Console.WriteLine(objTest.Invoke(objHelloWorld, null));
            }

            Console.ReadLine();
        }

        static string GenerateCode()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using System;");
            sb.Append(Environment.NewLine);
            sb.Append("namespace DynamicCompiler");
            sb.Append(Environment.NewLine);
            sb.Append("{");
            sb.Append(Environment.NewLine);
            sb.Append("    public class Program");
            sb.Append(Environment.NewLine);
            sb.Append("    {");

            //方法1
            sb.Append(Environment.NewLine);
            sb.Append("        public string OutPut()");
            sb.Append(Environment.NewLine);
            sb.Append("        {");
            sb.Append(Environment.NewLine);
            sb.Append("             return \"Hello world!\";");
            sb.Append(Environment.NewLine);
            sb.Append("        }");

            //方法2
            sb.Append(Environment.NewLine);
            sb.Append("        public void TestFunction()");
            sb.Append(Environment.NewLine);
            sb.Append("        {");
            sb.Append(Environment.NewLine);
            sb.Append("               Console.WriteLine(\"我是改变的测试方法\");Console.ReadKey(); ");
            sb.Append(Environment.NewLine);
            sb.Append("        }");

            sb.Append(Environment.NewLine);
            sb.Append("    }");
            sb.Append(Environment.NewLine);
            sb.Append("}");

            string code = sb.ToString();
            Console.WriteLine(code);
            Console.WriteLine();

            return code;
        }

        static void TestFunction()
        {
            Console.WriteLine("我是原有测试方法");
            Console.ReadKey();
        }
    }
}
