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
    public class DesolationIII : ModItem
    {
        public int discCooldown = 0;

        public int mode = 0;
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 44;

            item.damage = 120;
            item.knockBack = 2f;

            item.useTime = 13;
            item.useAnimation = 13;
            item.useStyle = 5;
            item.UseSound = SoundID.Item5;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 3;

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
            DisplayName.SetDefault("Desolation III");
            Tooltip.SetDefault("Turns normal arrows into shattering crystalline arrows\nRight to change modes");
        }
        public override bool AltFunctionUse(Player player)
        {
                return true;
        }
        public override void UpdateInventory(Player player)
        {
            discCooldown--;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                mode++;
                if (mode >= 2)
                {
                    mode = 0;
                }
                Main.PlaySound(12, (int)player.position.X, (int)player.position.Y, 0);
                switch (mode)
                {
                    case 0:
                        item.channel = false;
                        item.noUseGraphic = false;
                        item.autoReuse = true;
                        break;
                    case 1:
                        item.channel = true;
                        item.noUseGraphic = true;
                        item.autoReuse = false;
                        break;
                    case 2:
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
                if (mode == 2)
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
                    int numProjectiles = 5;
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
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DesolationIIIHeld"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
                }
                else
                {
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DesolationDisc"), damage, knockBack, player.whoAmI, 0.1f, Main.rand.Next(-1, 2));
                    discCooldown = 600;
                }
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesolationII", 1);
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddIngredient(ItemID.LunarBar, 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
