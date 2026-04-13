using HarmonyLib;
using Sandbox.Game.Gui;
using Sandbox.Game.Weapons;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using VRage.Utils;

namespace AutoRemoveSilicone
{
    [HarmonyPatch]
	public class TerminalPatches
	{
		public static void ShipDrillControls()
		{
			MyTerminalControlOnOffSwitch<MyShipDrill> myTerminalControlOnOffSwitch = new MyTerminalControlOnOffSwitch<MyShipDrill>("AutoVoidSilicone", MyStringId.GetOrCompute("Void Silicone"));
			myTerminalControlOnOffSwitch.Getter = (MyShipDrill x) => SiliconeRemover.Enabled && StiliconeRemover.VoidMode == SiliconeRemover.Mode.Silicone;
			myTerminalControlOnOffSwitch.Setter = delegate (MyShipDrill x, bool v)
			{
				SiliconeRemover.EnableMode(SiliconeRemover.Mode.Silicone);
			};
			myTerminalControlOnOffSwitch.DynamicTooltipGetter = delegate (MyShipDrill drill)
			{
				return $"Toggle voiding of silicone.";
			};
			myTerminalControlOnOffSwitch.SupportsMultipleBlocks = false;
			MyTerminalControlFactory.AddControl<MyShipDrill>(myTerminalControlOnOffSwitch);
			MyTerminalAction<MyShipDrill> myTerminalAction = new MyTerminalAction<MyShipDrill>("VoidSilicone", new StringBuilder("Toggle Silicone voiding"), "");
			myTerminalAction.Action = delegate (MyShipDrill x) { SiliconeRemover.EnableMode(SiliconeRemover.Mode.Silicone); } ;
			myTerminalAction.Writer = delegate (MyShipDrill block, StringBuilder builder)
			{
				builder.Append($"{SiliconeRemover.VoidMode} {SiliconeRemover.Counter / 10:N0}");
			};
			myTerminalAction.ValidForGroups = false;
			MyTerminalControlFactory.AddAction<MyShipDrill>(myTerminalAction);

			
		}

		[HarmonyTranspiler]
		[HarmonyPatch(typeof(MyShipDrill), "CreateTerminalControls")]
		public static IEnumerable<CodeInstruction> ShipDrillTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			int index = instructions.Count() - 1;
			int num;
			for (int i = 0; i < index; i = num + 1)
			{
				yield return instructions.ElementAt(i);
				num = i;
			}
			yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(TerminalPatches), "ShipDrillControls", null, null));
			yield return new CodeInstruction(OpCodes.Ret, null);
			yield break;
		}
	}
}