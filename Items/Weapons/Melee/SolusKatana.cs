using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class SolusKatana : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 62;
            item.melee = true;
            item.width = 70;
            item.height = 70;
            item.useTime = 15;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.knockBack = 5f;
            item.value = Item.buyPrice(0, 20, 0, 0);
            item.rare = 6;
            item.UseSound = SoundID.Item15;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SolusKunai");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solus Katana");
            Tooltip.SetDefault("A weapon used by a feared cultist...\nRight click to shoot 3 solus kunais");
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {

                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.FindBuffIndex(mod.BuffType("SolusKunaiCooldown")) == -1)
                {
                    item.useTime = 25;
                    item.useAnimation = 25;
                    player.AddBuff(mod.BuffType("SolusKunaiCooldown"), 120);

                    if (player.direction == 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 12f, 4f, mod.ProjectileType("SolusKunai"), 120, 5f, Main.myPlayer, 0f, 0f);
                        Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 91);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 11f, 6f, mod.ProjectileType("SolusKunai"), 120, 5f, Main.myPlayer, 0f, 0f);
                        Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 91);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 10f, 8f, mod.ProjectileType("SolusKunai"), 120, 5f, Main.myPlayer, 0f, 0f);
                        Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 91);
                    }
                    if (player.direction == -1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, -12f, 4f, mod.ProjectileType("SolusKunai"), 120, 5f, Main.myPlayer, 0f, 0f);
                        Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 91);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, -11f, 6f, mod.ProjectileType("SolusKunai"), 120, 5f, Main.myPlayer, 0f, 0f);
                        Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 91);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, -10f, 8f, mod.ProjectileType("SolusKunai"), 120, 5f, Main.myPlayer, 0f, 0f);
                        Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 91);
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                item.useTime = 15;
                item.useAnimation = 15;
            }
            return base.CanUseItem(player);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 200);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofFright, 8);
            recipe.AddIngredient(ItemID.Katana, 1);
            recipe.AddIngredient(ItemID.LivingFireBlock, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
