using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Tools
{
    public class GravityGunMKII : ModItem
    {
        public int energyConsume = 0;
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.useAnimation = 2;
            item.useTime = 2;
            item.useStyle = 5;

            item.noMelee = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 8;

            item.shootSpeed = 1f;
            item.shoot = mod.ProjectileType("Blank");
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tip = new TooltipLine(mod, "Elements Awoken:Energy", "Uses " + 5 + " energy");
            tooltips.Add(tip);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gravity Gun MKII");
            Tooltip.SetDefault("Able to pick up items and enemies");
        }
        public override void HoldItem(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();

            Vector2 mouse = Main.MouseWorld;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                bool immune = false;
                foreach (int k in ElementsAwoken.instakillImmune)
                {
                    if (nPC.type == k)
                    {
                        immune = true;
                    }
                }
                if (!immune && !nPC.boss && nPC.active)
                {
                    bool canSee = Collision.CanHit(player.Center, 2, 2, nPC.Center, 2, 2);
                    Rectangle grabBox = new Rectangle((int)nPC.TopLeft.X, (int)nPC.TopLeft.Y, nPC.width, nPC.height);
                    if (nPC.width < 30)
                    {
                        grabBox = new Rectangle((int)(nPC.Center.X - nPC.width * 3), (int)(nPC.Center.Y - nPC.height * 6), nPC.width * 6, nPC.height * 6); // making a larger hitbox (3x the size)
                    }
                    else if(nPC.width < 60)
                    {
                        grabBox = new Rectangle((int)(nPC.Center.X - nPC.width * 1.5f), (int)(nPC.Center.Y - nPC.height * 1.5f), (int)(nPC.width * 3f), (int)(nPC.height * 3f)); // making a larger hitbox (3x the size)
                    }
                    else
                    {
                        grabBox = new Rectangle((int)nPC.TopLeft.X, (int)nPC.TopLeft.Y, nPC.width, nPC.height);
                    }
                    bool mouseOn = grabBox.Contains((int)mouse.X, (int)mouse.Y);
                    if (mouseOn && canSee)
                    {
                        Dust.NewDust(nPC.position, nPC.width, nPC.height, 221);

                        if (Main.mouseLeft && modPlayer.energy > 5)
                        {
                            energyConsume--;
                            if (energyConsume <= 0)
                            {
                                modPlayer.energy -= 5;
                                energyConsume = 60;
                            }
                            Vector2 difference = player.Center - nPC.Center;
                            for (int k = 0; k < 20; k++)
                            {
                                Dust dust = Main.dust[Dust.NewDust(nPC.Center + difference * Main.rand.NextFloat(), 0, 0, 59)];
                                dust.velocity = Vector2.Zero;
                                dust.noGravity = true;
                            }
                            nPC.Center = mouse;
                        }
                    }
                }
            }
            for (int i = 0; i < Main.maxItems; i++)
            {
                Item otherItem = Main.item[i];

                bool canSee = Collision.CanHit(player.Center, 2, 2, otherItem.Center, 2, 2);
                Rectangle grabBox = new Rectangle((int)otherItem.TopLeft.X, (int)otherItem.TopLeft.Y, otherItem.width, otherItem.height);
                if (otherItem.width < 30)
                {
                    grabBox = new Rectangle((int)(otherItem.Center.X - otherItem.width * 3), (int)(otherItem.Center.Y - otherItem.height * 6), otherItem.width * 6, otherItem.height * 6); // making a larger hitbox (3x the size)
                }
                else
                {
                    grabBox = new Rectangle((int)(otherItem.Center.X - otherItem.width * 1.5f), (int)(otherItem.Center.Y - otherItem.height * 1.5f), (int)(otherItem.width * 3f), (int)(otherItem.height * 3f)); // making a larger hitbox (3x the size)
                }
                bool mouseOn = grabBox.Contains((int)mouse.X, (int)mouse.Y);
                if (mouseOn && canSee)
                {
                    Dust.NewDust(otherItem.position, otherItem.width, otherItem.height, 221);

                    if (Main.mouseLeft)
                    {
                        Vector2 difference = player.Center - otherItem.Center;
                        for (int k = 0; k < 20; k++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(otherItem.Center + difference * Main.rand.NextFloat(), 0, 0, 59)];
                            dust.velocity = Vector2.Zero;
                            dust.noGravity = true;
                        }
                        otherItem.Center = mouse;
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 16);
            recipe.AddIngredient(null, "Transistor", 20);
            recipe.AddIngredient(null, "Microcontroller", 5);
            recipe.AddIngredient(null, "GravityGun", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
