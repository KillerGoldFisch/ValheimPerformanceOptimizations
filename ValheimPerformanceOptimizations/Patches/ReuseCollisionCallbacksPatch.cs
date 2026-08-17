using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace ValheimPerformanceOptimizations.Patches
{
	/// <summary>
	/// -50kb saved per frame lmfao
	/// </summary>
	public static class ReuseCollisionCallbacksPatch
	{
		private static ConfigEntry<bool> _reuseCollisionCallbacks;

		static ReuseCollisionCallbacksPatch()
		{
			ValheimPerformanceOptimizations.OnInitialized += Initialize;
		}

		private static void Initialize(ConfigFile configFile, Harmony harmony)
		{
			const string key = "Reuse collision callbacks";
			const string description = "Significantly reduces GC in physics-heavy scenes. Disable if there are collision issues in mod-heavy scenarios.";

			_reuseCollisionCallbacks = configFile.Bind("General", key, true, description);
			_reuseCollisionCallbacks.SettingChanged += (_, _) => Apply();
			Apply();
		}

		private static void Apply()
		{
			Physics.reuseCollisionCallbacks = _reuseCollisionCallbacks.Value;
		}
	}
}
