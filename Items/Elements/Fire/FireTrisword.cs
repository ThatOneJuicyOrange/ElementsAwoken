using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Fire
{
    public class FireTrisword : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 58;
            item.height = 58;

            item.damage = 27;
            item.knockBack = 6;

            item.useTurn = true;
            item.melee = true;
            item.autoReuse = true;

            item.useTime = 36;
            item.useAnimation = 18;
            item.UseSound = SoundID.Item1;
            item.useStyle = 1;

            item.value = Item.buyPrice(0, 7, 0, 0);
            item.rare = 4;

            item.shoot = mod.ProjectileType("TriswordBlade");
            item.shootSpeed = 8f; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blazing Trisword");
            Tooltip.SetDefault("Forged inside of an eternal fire");
        }
        public override bool OnlyShootOnSwing => false;

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Fire);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(SoundID.Item, player.position, 8);          
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float pi = 0.314159274f;
            int numProjectiles = 3;
            Vector2 vector14 = new Vector2(speedX, speedY);
            vector14.Normalize();
            vector14 *= 40f;
            bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector14, 0, 0);
            for (int num123 = 0; num123 < numProjectiles; num123++)
            {
                float num124 = (float)num123 - ((float)numProjectiles - 1f) / 2f;
                Vector2 vector15 = vector14.RotatedBy((double)(pi * num124), default(Vector2));
                if (!flag11)
                {
                    vector15 -= vector14;
                }
                int num125 = Projectile.NewProjectile(vector2.X + vector15.X, vector2.Y + vector15.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
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
