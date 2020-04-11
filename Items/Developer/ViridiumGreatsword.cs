using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Developer
{
    public class ViridiumGreatsword : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 80;
            item.height = 80; 
            
            item.damage = 300;
            item.knockBack = 6;

            item.UseSound = SoundID.Item1;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 1;

            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 11;

            item.autoReuse = true;
            item.useTurn = true;
            item.melee = true;

            item.shoot = mod.ProjectileType("ViridiumLightning");
            item.shootSpeed = 32f;

            item.GetGlobalItem<EATooltip>().developer = true;
            item.GetGlobalItem<EARarity>().rare = 12;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Viridium Greatsword");
            Tooltip.SetDefault("The greatsword is constantly brimming with energy\nMaybe it's because of the strange material it is made out of.\nThe blade seems to glow brighter whenever one is moving at high speed.\nAmadisLFE's developer weapon");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse != 2)
            {
                Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ElectricArcing"));

                int numberProjectiles = Main.rand.Next(2, 3);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    int velocity = (int)player.velocity.X;
                    if (velocity < 0)
                    {
                        velocity *= -1;
                    }

                    Vector2 vector94 = new Vector2(speedX, speedY);
                    float ai = (float)Main.rand.Next(100);
                    Vector2 vector95 = Vector2.Normalize(vector94.RotatedByRandom(0.78539818525314331)) * 4f;
                    Projectile.NewProjectile(position.X, position.Y, vector95.X, vector95.Y, mod.ProjectileType("ViridiumLightning"), damage, 0f, Main.myPlayer, vector94.ToRotation(), ai);
                }
            }
            return false;
        }
        public override bool AltFunctionUse(Player player)
        {
            if (player.FindBuffIndex(mod.BuffType("DashCooldown")) == -1)
            {
                return true;
            }
            return false;
        }
        public override void HoldItem(Player player)
        {
            if (Main.rand.Next(100) == 0)
            {
                Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ElectricArcing"));

                Vector2 vector94 = new Vector2(2, 2);
                float ai = (float)Main.rand.Next(100);
                Vector2 vector95 = Vector2.Normalize(vector94.RotatedByRandom(360)) * 2f;
                Projectile.NewProjectile(player.Center.X, player.Center.Y, vector95.X, vector95.Y, mod.ProjectileType("ViridiumLightningPassive"), 300, 0f, Main.myPlayer, (vector94.RotatedByRandom(360)).ToRotation(), ai);
            }
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.direction == 1 )
                {
                    item.noMelee = true;
                    item.noUseGraphic = true;
                    player.velocity.X += 25f;
                    item.useStyle = 5;
                    player.AddBuff(mod.BuffType("DashCooldown"), 300);
                }
                if (player.direction == -1)
                {
                    item.noMelee = true;
                    item.noUseGraphic = true;
                    player.velocity.X -= 25f;
                    item.useStyle = 5;
                    player.AddBuff(mod.BuffType("DashCooldown"), 300);
                }
                player.GetModPlayer<MyPlayer>().viridiumDash = true;
                player.GetModPlayer<MyPlayer>().dashDustTimer = 60;
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("ViridiumExplosion"), 400, 10f, player.whoAmI, 0.0f, 0.0f);

                for (int l = 0; l < 100; l++)
                {
                    int num = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 222, 0f, 0f, 100, default(Color), 2f);
                    Dust expr_A4_cp_0 = Main.dust[num];
                    expr_A4_cp_0.position.X = expr_A4_cp_0.position.X + (float)Main.rand.Next(-20, 21);
                    Dust expr_CB_cp_0 = Main.dust[num];
                    expr_CB_cp_0.position.Y = expr_CB_cp_0.position.Y + (float)Main.rand.Next(-20, 21);
                    Main.dust[num].velocity *= 0.4f;
                    Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                    Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(player.cWaist, player);
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                        Main.dust[num].noGravity = true;
                    }
                }
            }
            else
            {
                item.noMelee = false;
                item.noUseGraphic = false;
                item.useStyle = 1;
            }
            return base.CanUseItem(player);
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += Math.Abs(player.velocity.X) * 0.28f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddIngredient(null, "NeutronFragment", 8);
            recipe.AddIngredient(null, "VoiditeBar", 8);
            recipe.AddIngredient(null, "NinjaKatana", 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
