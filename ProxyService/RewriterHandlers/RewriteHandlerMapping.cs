using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using ProxyService.RewriterHandlers.Configuration;

namespace ProxyService.RewriterHandlers
{
	public class RewriteHandlerMapping
	{
		private static Dictionary<string, IRewriter> mappingDic;
		private static RewriteHandlerConfigurationSection _section;
		public RewriteHandlerMapping(Dictionary<string, IRewriter> dic)
		{
			if (dic == null)
				throw new InvalidOperationException("Failed to load content type handlers");
			mappingDic = dic;
		}

		public RewriteHandlerMapping()
		{
			mappingDic = new Dictionary<string, IRewriter>();
		}

		private static RewriteHandlerConfigurationSection Section
		{
			get 
			{
				if (_section == null)
				{
					_section = (RewriteHandlerConfigurationSection)ConfigurationManager.GetSection("RewriteHandleConfiguration");
				}
				return _section;
			}
		}

		public IRewriter this[string contentType]
		{
			get 
			{
				if (!mappingDic.ContainsKey(contentType))
				{
					if (Section.Handles[contentType] == null)
						return null;

					string typeFullname = Section.Handles[contentType].TypeFullName;
					var rules = Section.Handles[contentType].RewriteRules;

					var instance = LoadAssembly(typeFullname, rules);
					mappingDic.Add(contentType, instance);
				}
				return mappingDic[contentType];
			}
		}

		private IRewriter LoadAssembly(string typeFullName, RewriteRuleCollection rules)
		{
			System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(typeFullName));

			string assemblyName = typeFullName.Substring(typeFullName.IndexOf(',') + 1);
			string typeName = typeFullName.Substring(0, typeFullName.IndexOf(','));

			try
			{
				Assembly assembly = Assembly.Load(assemblyName);
				Type type = assembly.GetType(typeName);

				var instance = (IRewriter)Activator.CreateInstance(type, rules);
				if (instance == null)
					throw new ApplicationException(string.Format("{0}, {1} doesn't inherit from  {2}", typeName, assembly, "IRewriter"));
				return instance;
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Failed to load handler", ex);
			}
		}
	}

}
