using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.BossSummons
{
    public class AncientsSummon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 14;

            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
            item.maxStack = 20;

            item.shoot = mod.ProjectileType("AncientSpawn");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Resurrection Sigil");
            Tooltip.SetDefault("Summons The Ancients on use");
        }

        public override bool CanUseItem(Player player)
        {
            return
            !NPC.AnyNPCs(mod.NPCType("Izaris")) &&
            !NPC.AnyNPCs(mod.NPCType("Kirvein")) &&
            !NPC.AnyNPCs(mod.NPCType("Krecheus")) &&
            !NPC.AnyNPCs(mod.NPCType("Xernon"))&& 
            !NPC.AnyNPCs(mod.NPCType("AncientAmalgam"));
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ChaoticFlare", 2);
            recipe.AddIngredient(null, "CrystalAmalgamate", 1);
            recipe.AddIngredient(null, "DiscordantBar", 4);
            recipe.AddTile(null, "ChaoticCrucible");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
