using System;
using System.IO;
using System.Reflection;
using System.Collections;

namespace CVST.AppFramework.AssemblyLoader
{
	/// <summary>
	/// RemoteLoader 的摘要说明。
	/// </summary>
	public class RemoteLoader:MarshalByRefObject,IDisposable
	{
		public RemoteLoader(string dllName)
		{
			if (assembly == null)
			{
				assembly = Assembly.LoadFrom(dllName);
			}
		}

		~RemoteLoader()
		{
			dispose(true);
		}

		private Assembly assembly = null;

		public Assembly LoadAssembly(string dllName)
		{
			try
			{
				assembly = Assembly.LoadFrom(dllName);				
				return assembly;
			}
			catch (Exception)
			{
				throw new AssemblyLoadFailureException();
			}
		}

		public string GetAssemblyVersion(string dllName)
		{
			assembly = Assembly.LoadFrom(dllName);
			AssemblyName name = assembly.GetName();
			return name.Version.ToString();
		}
		
		public ArrayList LoadAssemblies(ArrayList dllFileList)
		{
			ArrayList assemblyList = new ArrayList();
			foreach (string dllFile in dllFileList)
			{
				FileInfo info = new FileInfo(dllFile);
				if (info.Extension == ".dll")
				{
					Assembly a = LoadAssembly(dllFile);
					assemblyList.Add(a);
				}
			}

			return assemblyList;
		}

		#region IDisposable 成员

		public void Dispose()
		{
			dispose(true);
		}		

		private void dispose(bool disposing)
		{
			if (disposing)
			{
				assembly = null;
				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.Collect(0);
			}
		}

		#endregion
	}
}
