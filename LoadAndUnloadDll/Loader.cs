using System;
using System.Collections;
using System.Reflection;

namespace CVST.AppFramework.AssemblyLoader
{
	/// <summary>
	/// Class1 的摘要说明。
	/// </summary>
	public class Loader:IDisposable
	{
		public Loader()
		{
		}		

		private AppDomain domain = null;
		private Hashtable domains = new Hashtable();		
		private RemoteLoader rl = null;

		private void SetRemoteLoaderObject(string dllName)
		{
			AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationName = "ApplicationLoader";
            setup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
            //setup.PrivateBinPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "private");
            setup.CachePath = setup.ApplicationBase;
            setup.ShadowCopyFiles = "true"; //启用影像复制程序集
            setup.ShadowCopyDirectories = setup.ApplicationBase;
            AppDomain.CurrentDomain.SetShadowCopyFiles();

            domain = AppDomain.CreateDomain(dllName,null,setup);

            domains.Add(dllName,domain);
			object[] parms = {dllName};
			BindingFlags bindings = BindingFlags.CreateInstance |
				BindingFlags.Instance | BindingFlags.Public;
			try
			{
				rl = (CVST.AppFramework.AssemblyLoader.RemoteLoader)domain.CreateInstanceFromAndUnwrap(
					"LoadAndUnloadDll.exe","CVST.AppFramework.AssemblyLoader.RemoteLoader",true,bindings,
					null,parms,null,null,null);	
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public Assembly LoadAssembly(string dllName)
		{
			try
			{
				SetRemoteLoaderObject(dllName);
				return rl.LoadAssembly(dllName);
			}
			catch (Exception ex)
			{
				throw new AssemblyLoadFailureException();
			}
		}

		public string GetVersion(string dllName)
		{
			try
			{
				SetRemoteLoaderObject(dllName);
				return rl.GetAssemblyVersion(dllName);
			}
			catch (Exception ex)
			{
				throw new AssemblyLoadFailureException();
			}
		}

		public ArrayList LoadAssemblies(ArrayList assemblyFileList)
		{
			ArrayList assemblies = new ArrayList();
			foreach (string assemblyFile in assemblyFileList)
			{
				Assembly a = LoadAssembly(assemblyFile);
				assemblies.Add(a);
			}

			return assemblies;
		}

		public void Unload(string dllName)
		{
			if (domains.ContainsKey(dllName))
			{
				AppDomain appDomain = (AppDomain)domains[dllName];
				AppDomain.Unload(appDomain);
				domains.Remove(dllName);
			}			
		}

		public void Unload()
		{
			dispose(true);
		}

		~Loader()
		{
			dispose(false);
		}

		#region IDisposable 成员

		public void Dispose()
		{
			dispose(true);
		}

		#endregion

		private void dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (object o in domains.Keys)
				{
					string dllName = o.ToString();
					AppDomain appDomain = (AppDomain)domains[dllName];
					AppDomain.Unload(appDomain);								
				}
				domains.Clear();
			}
		}
	}
}
