using System;
using System.Reflection;
using HarmonyLib;
using VRage.Plugins;

namespace AutoRemoveSilicon
{
	public class Plugin : IDisposable, IPlugin
	{
		public void Dispose()
		{
		}

		public void Init(object gameInstance)
		{
			new Harmony("AutoRemoveSilicon").PatchAll(Assembly.GetExecutingAssembly());
		}

		public void Update()
		{
		}
	}
}