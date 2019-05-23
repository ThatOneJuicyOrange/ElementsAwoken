using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tools
{
    public class Hamaxe : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 5;
            item.melee = true;
            item.width = 56;
            item.height = 60;
            item.useTime = 6;
            item.useAnimation = 20;
            item.useTurn = true;
            item.axe = 9;
            item.hammer = 45;
            item.useStyle = 1;
            item.knockBack = 4.5f;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hamaxe");
            Tooltip.SetDefault("It reeks of rotten meat");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FleshClump", 8);
            recipe.AddIngredient(ItemID.Bone, 24);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(9) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 5);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}
