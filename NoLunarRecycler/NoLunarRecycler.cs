using BepInEx;
using MonoMod.RuntimeDetour;
using RoR2;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System;
using R2API.Utils;

namespace Plugin
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGUID = "Flexlolo.NoLunarRecycler";
        public const string PluginAuthor = "Flexlolo";
        public const string PluginName = "No Lunar Recycler";
        public const string PluginVersion = "1.0.0";

        public void Awake()
        {
            On.RoR2.PurchaseInteraction.GetInteractability += PurchaseInteraction_GetInteractability;
            On.RoR2.PurchaseInteraction.OnInteractionBegin += PurchaseInteraction_OnInteractionBegin;
        }

        public Interactability PurchaseInteraction_GetInteractability(On.RoR2.PurchaseInteraction.orig_GetInteractability orig, PurchaseInteraction self, Interactor activator)
        {
            if (SceneInfo.instance.sceneDef.nameToken == "MAP_BAZAAR_TITLE")
            {
                if (self.name.ToLower() == "lunarrecycler")
                {
                    return Interactability.ConditionsNotMet;
                }
            }
            
            return orig(self, activator);
        }

        public void PurchaseInteraction_OnInteractionBegin(On.RoR2.PurchaseInteraction.orig_OnInteractionBegin orig, PurchaseInteraction self, Interactor activator)
        {
            if (SceneInfo.instance.sceneDef.nameToken == "MAP_BAZAAR_TITLE")
            {
                if (self.name.ToLower() == "lunarrecycler")
                {
                    return;
                }
            }

            orig(self, activator);
        }
    }
}