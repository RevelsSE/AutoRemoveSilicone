using System;
using System.Reflection;
using HarmonyLib;
using VRage.Plugins;

namespace AutoRemoveSilicone
{
	public class Plugin : IDisposable, IPlugin
	{
		public void Dispose()
		{
		}

		public void Init(object gameInstance)
		{
			new Harmony("AutoRemoveSilicone").PatchAll(Assembly.GetExecutingAssembly());
		}

		public void Update()
		{
		}
	}
}