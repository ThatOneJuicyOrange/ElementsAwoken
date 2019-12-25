using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Generators
{
    public class HandCrank : ModItem
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

            item.rare = 0;

            item.maxStack = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hand Crank");
            Tooltip.SetDefault("Right click in inventory to generate 1 energy");
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
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
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
            recipe.AddIngredient(ItemID.Wood, 16);
            recipe.AddIngredient(ItemID.StoneBlock, 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
