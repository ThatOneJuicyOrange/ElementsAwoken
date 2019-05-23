using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Generators
{
    public class StarlightCrank : ModItem
    {
        public int crankCooldown = 0;
        public override bool CloneNewInstances
        {
            get { return true; }
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 44;

            item.rare = 3;

            item.maxStack = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hand Crank");
            Tooltip.SetDefault("Right click in inventory to generate 3 energy");
        }
        public override void UpdateInventory(Player player)
        {
            crankCooldown--;
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>(mod);
            if (crankCooldown <= 0 && modPlayer.energy < modPlayer.maxEnergy)
            {
                Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/HandCrank"), 1, 0);
                modPlayer.energy += 1;
                crankCooldown = 20;
            }
            item.stack++;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "HandCrank", 1);
            recipe.AddIngredient(ItemID.SunplateBlock, 16);
            recipe.AddIngredient(null, "CopperWire", 8);
            recipe.AddIngredient(null, "Capacitor", 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
