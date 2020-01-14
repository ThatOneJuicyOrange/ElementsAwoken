using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Drakonite.Regular
{
    public class DrakonitePickaxe : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 56;
            item.height = 60;

            item.damage = 5;
            item.pick = 45;
            item.knockBack = 4.5f;

            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.value = Item.sellPrice(0, 0, 20, 0);
            item.rare = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drakonite Pickaxe");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Drakonite", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
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

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 100);
        }
    }
}
