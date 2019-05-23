using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Drakonite.Regular
{
    public class DrakoniteKnife : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 12;
            item.damage = 12;
            item.thrown = true;
            item.noMelee = true;
            item.consumable = true;
            item.noUseGraphic = true;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.useTime = 15;
            item.knockBack = 1.75f;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.height = 30;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 0, 20, 0);
            item.rare = 1;
            item.shoot = mod.ProjectileType("DrakoniteKnifeP");
            item.shootSpeed = 11f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drakonite Knife");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Drakonite", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 40);
            recipe.AddRecipe();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Fire);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}
