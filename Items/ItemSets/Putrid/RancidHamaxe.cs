using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.ItemSets.Putrid
{
    public class RancidHamaxe : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 38;

            item.damage = 60;
            item.axe = 31;
            item.hammer = 155;
            item.knockBack = 8f;

            item.scale = 1.3f;

            item.useTurn = true;
            item.melee = true;
            item.autoReuse = true;

            item.useTime = 7;
            item.useAnimation = 17;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rancid Hamaxe");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<PutridBar>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 46, 0, 0, 150);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}
