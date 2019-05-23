using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Melee
{
    public class AsuterosaberuDX : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 56;
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
            item.shoot = mod.ProjectileType("AstralTear");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Asuterosaberu DX");
            Tooltip.SetDefault("Right click to tear a hole in reality");
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 63, 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
            Main.dust[dust].scale *= 2f;
            Main.dust[dust].noGravity = true;
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
                if (player.FindBuffIndex(mod.BuffType("AstralTearCooldown")) == -1)
                {
                    item.useTime = 25;
                    item.useAnimation = 25;
                    player.AddBuff(mod.BuffType("AstralTearCooldown"), 240);

                    if (player.direction == 1)
                    {
                        Projectile.NewProjectile(player.Center.X + 80, player.Center.Y - 18, 12f, 4f, mod.ProjectileType("AstralTear"), 120, 5f, Main.myPlayer, 0f, 0f);
                        Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 91);
                    }
                    if (player.direction == -1)
                    {
                        Projectile.NewProjectile(player.Center.X - 80, player.Center.Y - 18, -12f, 4f, mod.ProjectileType("AstralTear"), 120, 5f, Main.myPlayer, 0f, 0f);
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
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofSight, 8);
            recipe.AddIngredient(null, "Infamy", 1);
            recipe.AddIngredient(ItemID.RainbowBrick, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
