using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Blightfire
{
    public class BlightfirePickaxe : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 56;
            item.height = 60;

            item.damage = 87;
            item.pick = 240;
            item.knockBack = 6f;

            item.scale = 1.3f;

            item.useTime = 5;
            item.useAnimation = 16;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 11;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blightfire Pickaxe");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Blightfire", 8);
            recipe.AddIngredient(ItemID.LunarBar, 2);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 75);
                Main.dust[dust].noGravity = true;
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Corroding"), 180);
        }
    }
}
