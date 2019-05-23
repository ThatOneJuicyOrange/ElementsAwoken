using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Fire
{
    public class FireScythe : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.DeathSickle);
            item.shootSpeed *= 1.1f;
            item.shoot = mod.ProjectileType("FireScytheProj");
            item.damage = 34;
            item.value = Item.buyPrice(0, 7, 0, 0);
            item.rare = 4;
            item.scale *= 0.9f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scythe of Eternal Flame");
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Fire);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
