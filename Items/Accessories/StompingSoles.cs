using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using ElementsAwoken.Projectiles.GlobalProjectiles;

namespace ElementsAwoken.Items.Accessories
{
    public class StompingSoles : ModItem
    {
        public float oldVelY = 0;
        public int fallingTimer = 0;
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 4;
            item.value = Item.sellPrice(0, 2, 50, 0);
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stomping Soles");
            Tooltip.SetDefault("Breaks blocks under the player when falling\nDisable visibility to disable explosions");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.velocity.Y > 0) fallingTimer++;
            else if (player.velocity.Y == 0 && fallingTimer > 40 && oldVelY > 3 && !hideVisual)
            {

                Explosion(player);
                Vector2 playerTile = player.BottomLeft / 16; 
                for (int i = 0; i < 4; i++)
                {
                    player.PickTile((int)playerTile.X - 1 + i, (int)playerTile.Y, 100);
                }
                fallingTimer = 0;
            }
            else if (player.velocity.Y < 0) fallingTimer = 0;
            oldVelY = player.velocity.Y; // setting what the old velocity is after so it is the OLD velocity and not current
        }
        private void Explosion(Player player)
        {
            Projectile exp = Main.projectile[Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("Explosion"), 50, 5f, player.whoAmI, 0f, 0f)];
            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 14);
            int num = ModContent.GetInstance<Config>().lowDust ? 10 : 20;
            for (int i = 0; i < num; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 31, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 1.4f;
            }
            int num2 = ModContent.GetInstance<Config>().lowDust ? 5 : 10;
            for (int i = 0; i < num2; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 6, 0f, 0f, 100, default(Color), 2.5f)];
                dust.noGravity = true;
                dust.velocity *= 5f;
                dust = Main.dust[Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 6, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 3f;
            }
            int num373 = Gore.NewGore(new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore85 = Main.gore[num373];
            gore85.velocity.X = gore85.velocity.X + 1f;
            Gore gore86 = Main.gore[num373];
            gore86.velocity.Y = gore86.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore87 = Main.gore[num373];
            gore87.velocity.X = gore87.velocity.X - 1f;
            Gore gore88 = Main.gore[num373];
            gore88.velocity.Y = gore88.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore89 = Main.gore[num373];
            gore89.velocity.X = gore89.velocity.X + 1f;
            Gore gore90 = Main.gore[num373];
            gore90.velocity.Y = gore90.velocity.Y - 1f;
            num373 = Gore.NewGore(new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore91 = Main.gore[num373];
            gore91.velocity.X = gore91.velocity.X - 1f;
            Gore gore92 = Main.gore[num373];
            gore92.velocity.Y = gore92.velocity.Y - 1f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MagmaCrystal", 4);
            recipe.AddIngredient(ItemID.Bomb, 3);
            recipe.AddIngredient(ItemID.Leather, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
