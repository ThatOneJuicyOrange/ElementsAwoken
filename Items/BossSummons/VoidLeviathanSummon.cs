using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossSummons
{
    public class VoidLeviathanSummon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;
            item.rare = 10;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = false;
            item.shoot = mod.ProjectileType("VoidWormSpawn");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Embryo of Existence");
            Tooltip.SetDefault("Summons the guardian of the void");
        }


        public override bool CanUseItem(Player player)
        {
            return !Main.dayTime &&
            !NPC.AnyNPCs(mod.NPCType("VoidLeviathanHead")); ;
        }
        public override bool UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("VoidLeviathanHead"));
            else NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, mod.NPCType("VoidLeviathanHead"), 0f, 0f, 0, 0, 0); 
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NeutronFragment", 10);
            recipe.AddIngredient(null, "VoidEssence", 15);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddIngredient(ItemID.LunarBar, 5);
            recipe.AddIngredient(ItemID.Ectoplasm, 6);
            recipe.AddIngredient(ItemID.SoulofMight, 7);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
