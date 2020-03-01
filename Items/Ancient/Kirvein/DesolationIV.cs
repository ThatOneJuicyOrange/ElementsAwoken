using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace ElementsAwoken.Items.Ancient.Kirvein
{
    public class DesolationIV : ModItem
    {
        public int discCooldown = 0;

        public int mode = 0;
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 44;

            item.damage = 210;
            item.knockBack = 2f;

            item.useTime = 11;
            item.useAnimation = 11;
            item.useStyle = 5;
            item.UseSound = SoundID.Item5;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 50, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 14;

            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.shootSpeed = 14f;
            item.useAmmo = AmmoID.Arrow;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (discCooldown > 0)
            {
                for (var j = 0; j < 10; ++j)
                {

                    string text = "" + discCooldown / 60;
                    Vector2 textScale = new Vector2(Main.hotbarScale[j], Main.hotbarScale[j]);
                    Item otherItem = Main.player[Main.myPlayer].inventory[j];
                    if (otherItem == item)
                    {

                        /*if (Main.playerInventory)
                        {
                            pos -= new Vector2(0, 5);
                            textScale = new Vector2(0.8f, 0.8f);
                        }*/
                        ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontItemStack, text, position + new Vector2(23f, 20f) * Main.inventoryScale, Color.Red, 0f, Vector2.Zero, new Vector2(Main.inventoryScale), -1f, Main.inventoryScale);
                    }
                }
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desolation IV");
            Tooltip.SetDefault("Turns normal arrows into shattering crystalline arrows\n70% chance not to consume ammo\nRight Click to change modes");
        }
        public override bool AltFunctionUse(Player player)
        {
                return true;
        }
        public override void UpdateInventory(Player player)
        {
            discCooldown--;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() > .30f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                mode++;
                if (mode > 3)
                {
                    mode = 0;
                }
                string text = "";
                if (mode == 0) text = "Wave";
                else if (mode == 1) text = "Rapid";
                else if (mode == 2) text = "Charge";
                else if (mode == 3) text = "Disc";

                CombatText.NewText(player.getRect(), Color.DarkGreen, text, false, false);
                
                Main.PlaySound(12, (int)player.position.X, (int)player.position.Y, 0);
                switch (mode)
                {
                    case 0:
                        item.useTime = 11;
                        item.useAnimation = 11;
                        item.channel = false;
                        item.noUseGraphic = false;
                        item.autoReuse = true;
                        break;
                    case 1:
                        item.useTime = 3;
                        item.useAnimation = 9;
                        item.channel = false;
                        item.noUseGraphic = false;
                        item.autoReuse = true;
                        break;
                    case 2:
                        item.useTime = 11;
                        item.useAnimation = 11; 
                        item.channel = true;
                        item.noUseGraphic = true;
                        item.autoReuse = false;
                        break;
                    case 3:
                        item.useTime = 11;
                        item.useAnimation = 11;
                        item.channel = false;
                        item.noUseGraphic = false;
                        item.autoReuse = true;
                        break;
                    default:
                        return base.CanUseItem(player);
                }
            }
            else
            {
                if (mode == 3)
                {
                    if (discCooldown <= 0)
                    {
                        return base.CanUseItem(player);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse != 2)
            {
                if (mode == 0)
                {
                    if (type == ProjectileID.WoodenArrowFriendly)
                    {
                        type = mod.ProjectileType("DesolationArrow");
                    }
                    //tsunami code from 'Player'
                    Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
                    float pi = 0.314159274f;
                    int numProjectiles = 7;
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
                        Main.projectile[num125].noDropItem = true;
                    }
                }
                else if (mode == 1)
                {
                    if (type == ProjectileID.WoodenArrowFriendly)
                    {
                        type = mod.ProjectileType("DesolationArrow");
                    }
                    Projectile.NewProjectile(position.X + Main.rand.Next(-20,20), position.Y + Main.rand.Next(-20, 20), speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
                }
                else if (mode == 2)
                {
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DesolationIVHeld"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
                }
                else
                {
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DesolationDisc"), damage, knockBack, player.whoAmI, 0.1f, Main.rand.Next(-1, 2));
                    discCooldown = 420;
                }
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesolationIII", 1);
            recipe.AddIngredient(null, "AncientShard", 5);
            recipe.AddIngredient(null, "VoiditeBar", 4);
            recipe.AddIngredient(null, "DiscordantBar", 20);
            recipe.AddTile(null, "ChaoticCrucible");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
