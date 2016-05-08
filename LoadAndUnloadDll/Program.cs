using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Reflection;
using System;
using System.IO;

namespace LoadAndUnloadDll
{
    class Program
    {
        static void Main(string[] args)
        {
            //Assembly assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "SpeedDevelopTool.dll");

            string callingDomainName = AppDomain.CurrentDomain.FriendlyName;//Thread.GetDomain().FriendlyName;
            Console.WriteLine(callingDomainName);
            //AppDomain ad = AppDomain.CreateDomain("DLL Unload test");
            //ProxyObject obj = (ProxyObject)ad.CreateInstanceFromAndUnwrap(@"LoadAndUnloadDll.exe", "LoadAndUnloadDll.ProxyObject");
            //obj.LoadAssembly();
            //obj.Invoke("TestDll.Class1", "Test", "It's a test");
            //AppDomain.Unload(ad);
            //obj = null;

            //CVST.AppFramework.AssemblyLoader.Loader loader = new CVST.AppFramework.AssemblyLoader.Loader();
            //Assembly asb = loader.LoadAssembly(AppDomain.CurrentDomain.BaseDirectory + "CommonLib.dll");

            #region 字节数组不占用方式
            byte[] dllFile =CommonLib.Common.GetByteArrayFromFile(AppDomain.CurrentDomain.BaseDirectory + "SpeedDevelopTool.dll");
            Assembly assembly = Assembly.Load(dllFile);

            object instance = assembly.CreateInstance("CommonLib.Common");
            #endregion


            //无需卸载，使用的就不是这个dll，是拷贝后的镜像
            // loader.Unload();

            Console.ReadLine();
        }
    }
    class ProxyObject : MarshalByRefObject
    {
        Assembly assembly = null;
        public void LoadAssembly()
        {
            assembly = Assembly.LoadFile(@"TestDLL.dll");
        }
        public bool Invoke(string fullClassName, string methodName, params Object[] args)
        {
            if (assembly == null)
                return false;
            Type tp = assembly.GetType(fullClassName);
            if (tp == null)
                return false;
            MethodInfo method = tp.GetMethod(methodName);
            if (method == null)
                return false;
            Object obj = Activator.CreateInstance(tp);
            method.Invoke(obj, args);
            return true;
        }
    }
}