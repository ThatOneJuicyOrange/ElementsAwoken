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
            item.width = 24;
            item.height = 28;

            item.damage = 28;
            item.knockBack = 7f;

            item.useStyle = 1;
            item.useAnimation = 25;
            item.useTime = 50;
            item.UseSound = SoundID.Item1;

            item.melee = true;
            item.autoReuse = true;

            item.shootSpeed = 8f;
            item.shoot = mod.ProjectileType("FireScytheProj");

            item.value = Item.buyPrice(0, 7, 0, 0);
            item.rare = 4;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scythe of Eternal Flame");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 71);
            return true;
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
