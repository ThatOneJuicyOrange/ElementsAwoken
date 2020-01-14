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
    public class AqueousSummon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;
            item.maxStack = 20;
            item.rare = 6;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Shell");
            Tooltip.SetDefault("It glows mysteriously\nSummons Aqueous on use");
        }

        public override bool CanUseItem(Player player)
        {
            if (player.ZoneBeach && !NPC.AnyNPCs(mod.NPCType("Aqueous")))
            {
                Main.NewText("Time for your reign to end, murderer", Color.Cyan.R, Color.Cyan.G, Color.Cyan.B);
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Aqueous"));
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 10);
            recipe.AddIngredient(ItemID.Ectoplasm, 6);
            recipe.AddIngredient(ItemID.SoulofMight, 7);
            recipe.AddIngredient(ItemID.SoulofNight, 3);
            recipe.AddIngredient(ItemID.SoulofLight, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
