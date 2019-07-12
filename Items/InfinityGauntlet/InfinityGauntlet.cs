using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Map;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.InfinityGauntlet
{
    public class InfinityGauntlet : ModItem
    {
        public int gauntletMode = 0;
        public int pushTimer = 0;

        public override bool CloneNewInstances
        {
            get { return true; }
        }

        public override void SetDefaults()
        {
            item.damage = 105;
            item.magic = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 8;
            item.useAnimation = 16;
            Item.staff[item.type] = true;
            item.useStyle = 5;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(2, 0, 0, 0);
            item.rare = 10;
            item.mana = 18;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("AncientStar");
            item.shootSpeed = 18f;
            item.useTurn = true;
            // under here is everything that is changed in the different modes
            item.channel = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Gauntlet");
            Tooltip.SetDefault("");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void UpdateInventory(Player player)
        {
            player.buffImmune[BuffID.WindPushed] = true;
            player.moveSpeed *= 1.1f;

            player.meleeDamage *= 1.05f;
            player.magicDamage *= 1.05f;
            player.rangedDamage *= 1.05f;
            player.minionDamage *= 1.05f;
            player.thrownDamage *= 1.05f;
            if (Main.rand.Next(90) == 0)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= 600)
                    {
                        nPC.AddBuff(BuffID.OnFire, 180, false);
                        return;
                    }
                }
            }

            player.wingTimeMax = (int)(player.wingTimeMax * 1.2f);

            pushTimer--;
            if (pushTimer <= 0)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (!nPC.friendly && nPC.active && nPC.damage > 0 && !nPC.boss && Vector2.Distance(nPC.Center, player.Center) < 300)
                    {
                        Vector2 toTarget = new Vector2(player.Center.X - nPC.Center.X, player.Center.Y - nPC.Center.Y);
                        toTarget.Normalize();
                        nPC.velocity -= toTarget * 8f;
                        if (!nPC.noGravity)
                        {
                            nPC.velocity.Y -= 7.5f;
                        }
                    }
                }
                pushTimer = 300;
            }

            player.magicCrit += 5;
            player.meleeCrit += 5;
            player.rangedCrit += 5;
            player.thrownCrit += 5;
            if (Main.rand.Next(90) == 0)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= 600)
                    {
                        nPC.AddBuff(BuffID.Frostburn, 180, false);
                        return;
                    }
                }
            }

            player.ignoreWater = true;
            //player.statManaMax2 += 50; // makes it beep

            if (Main.rand.Next(1600) == 0)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    bool immune = false;
                    foreach (int i in ElementsAwoken.instakillImmune)
                    {
                        if (nPC.type == i)
                        {
                            immune = true;
                        }
                    }
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= 600 && nPC.lifeMax < 30000 && !immune)
                    {
                        nPC.StrikeNPCNoInteraction(nPC.lifeMax, 0f, -nPC.direction, true);
                        for (int d = 0; d < 100; d++)
                        {
                            int dust = Dust.NewDust(nPC.position, nPC.width, nPC.height, 219);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].scale = 1f;
                            Main.dust[dust].velocity *= 2f;
                        }
                        return; // to only kill 1 
                    }
                }
            }

            if (Main.mapFullscreen && player.FindBuffIndex(mod.BuffType("InfinityPortalCooldown")) == -1 && gauntletMode == 2 && Main.mouseRight && Main.mouseRightRelease)
            {
                // thx heros mod for help with this code
                Vector2 worldMapSize = new Vector2(Main.maxTilesX * 16, Main.maxTilesY * 16);
                Vector2 cursorPosition = new Vector2((Main.mouseX - Main.screenWidth / 2) / 16, (Main.mouseY - Main.screenHeight / 2) / 16) * 16 / Main.mapFullscreenScale;
                Vector2 targetPos = (Main.mapFullscreenPos + cursorPosition) * 16;

                // to stop the player teleporting out of the map
                if (targetPos.X < 0) targetPos.X = 0;
                else if (targetPos.X + player.width > worldMapSize.X) targetPos.X = worldMapSize.X - player.width;
                if (targetPos.Y < 0) targetPos.Y = 0;
                else if (targetPos.Y + player.height > worldMapSize.Y) targetPos.Y = worldMapSize.Y - player.height;
                
                Point tilePos = (targetPos / 16).ToPoint();
                Tile tile = Framing.GetTileSafely(tilePos.X, tilePos.Y);
                MapTile mapTile = Main.Map[tilePos.X, tilePos.Y];
                Main.NewText(mapTile.Type);
                if (ValidTile(tile) && DiscoveredArea(mapTile))
                {
                    player.position = targetPos;
                    player.velocity = Vector2.Zero;
                    if (Main.netMode != 0)
                    {
                        NetMessage.SendData(65, -1, -1, null, 0, player.whoAmI, targetPos.X, targetPos.Y, 1, 0, 0);
                    }

                    if (!Config.debugMode)
                    {
                        player.AddBuff(mod.BuffType("InfinityPortalCooldown"), 1800);
                    }
                    else
                    {
                        player.AddBuff(mod.BuffType("InfinityPortalCooldown"), 30);
                    }
                }
            }
        }
        private bool ValidTile(Tile tile)
        {
            if (Main.tileSolid[tile.type] && tile.active())
            {
                return false;
            }
            return true;
        }
        private bool DiscoveredArea(MapTile mapTile)
        {
            if (mapTile.Type == 0)
            {
                return false;
            }
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.mana = 0;
                gauntletMode++;
                if (gauntletMode >= 6)
                {
                    gauntletMode = 0;
                }
                Main.PlaySound(12, (int)player.position.X, (int)player.position.Y, 0);
                string text = "";
                switch (gauntletMode)
                {
                    case 0:
                        text = "Desert";

                        item.useTime = 8;
                        item.useAnimation = 16;
                        break;
                    case 1:
                        text = "Fire";

                        item.useTime = 8;
                        item.useAnimation = 16;
                        break;
                    case 2:
                        text = "Sky";

                        item.useTime = 20;
                        item.useAnimation = 20;
                        break;
                    case 3:
                        text = "Frost";

                        item.useTime = 8;
                        item.useAnimation = 16;
                        break;
                    case 4:
                        text = "Water";

                        item.useTime = 2;
                        item.useAnimation = 16;
                        break;
                    case 5:
                        text = "Void";

                        item.useTime = 8;
                        item.useAnimation = 16;
                        break;
                    default:
                        return base.CanUseItem(player);
                }
                Main.NewText(text, Color.White.R, Color.White.G, Color.White.B);
            }
            else
            {
                item.mana = 18;
                if (gauntletMode == 4)
                {
                    if (player.FindBuffIndex(mod.BuffType("InfinityBubbleCooldown")) == -1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (gauntletMode == 5)
                {
                    if (player.FindBuffIndex(mod.BuffType("InfinityVoidCooldown")) == -1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return base.CanUseItem(player);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string baseTooltip = "The forces of the elements combined...\nHas the effects of all the infinity stones\nRight click to switch modes\n";

            string text = "";
            switch (gauntletMode)
            {
                case 0:
                    text = "Desert: Rains desert shards";
                    break;
                case 1:
                    text = "Fire: Fires a fireball";
                    break;
                case 2:
                    text = "Sky: Teleport to a discovered location on the map";
                    break;
                case 3:
                    text = "Frost: Summons a shield around the player";
                    break;
                case 4:
                    text = "Water: Turns projectiles into bubbles";
                    break;
                case 5:
                    text = "Void: Kills half of all non boss monsters with under 10000 life";
                    break;
            }
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name.StartsWith("Tooltip"))
                {
                    line2.text = baseTooltip + text;
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse != 2)
            {
                if (gauntletMode == 0)
                {
                    int numberProjectiles = 6 + Main.rand.Next(2);
                    for (int index = 0; index < numberProjectiles; ++index)
                    {
                        Vector2 vector2_1 = new Vector2((float)((double)player.position.X + (double)player.width * 0.5 + (double)(Main.rand.Next(201) * -player.direction) + ((double)Main.mouseX + (double)Main.screenPosition.X - (double)player.position.X)), (float)((double)player.position.Y + (double)player.height * 0.5 - 600.0));   //this defines the projectile width, direction and position
                        vector2_1.X = (float)(((double)vector2_1.X + (double)player.Center.X) / 2.0) + (float)Main.rand.Next(-200, 201);
                        vector2_1.Y -= (float)(100 * index);
                        float num12 = (float)Main.mouseX + Main.screenPosition.X - vector2_1.X;
                        float num13 = (float)Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
                        if ((double)num13 < 0.0) num13 *= -1f;
                        if ((double)num13 < 20.0) num13 = 20f;
                        float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                        float num15 = item.shootSpeed / num14;
                        float num16 = num12 * num15;
                        float num17 = num13 * num15;
                        float SpeedX = num16 + (float)Main.rand.Next(-80, 81) * 0.02f;  //this defines the projectile X position speed and randomnes
                        float SpeedY = num17 + (float)Main.rand.Next(-80, 81) * 0.02f;  //this defines the projectile Y position speed and randomnes
                        Projectile.NewProjectile(vector2_1.X, vector2_1.Y, SpeedX, SpeedY, mod.ProjectileType("DesertCrystal"), damage / 2, knockBack, Main.myPlayer, 0.0f, (float)Main.rand.Next(5));
                    }
                }
                else if (gauntletMode == 1)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("InfinityFireball"), damage, knockBack, player.whoAmI);
                }
                else if (gauntletMode == 2)
                {
                    //Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("SkylineBolt"), damage * 3, knockBack, player.whoAmI);
                    Main.mapFullscreen = true;
                }
                else if (gauntletMode == 3)
                {
                    if (player.FindBuffIndex(mod.BuffType("FrostShield")) == -1)
                    {
                        player.AddBuff(mod.BuffType("FrostShield"), 1200);
                    }
                }
                else if (gauntletMode == 4)
                {
                    if (player.FindBuffIndex(mod.BuffType("InfinityBubbleCooldown")) == -1)
                    {
                        for (int l = 0; l < 20; l++)
                        {
                            for (int k = 0; k < Main.projectile.Length; k++)
                            {
                                Projectile other = Main.projectile[k];
                                if (other.active && other.type != mod.ProjectileType("InfinityBubble") && other.hostile && !other.friendly)
                                {
                                    other.Kill();
                                    for (int i = 0; i < Main.rand.Next(4); k++)
                                    {
                                        Projectile.NewProjectile(other.position.X, other.position.Y, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), mod.ProjectileType("InfinityBubble"), 0, 0, player.whoAmI);
                                    }
                                }
                            }
                        }
                        if (!Config.debugMode)
                        {
                            player.AddBuff(mod.BuffType("InfinityBubbleCooldown"), 1200);
                        }
                        else
                        {
                            player.AddBuff(mod.BuffType("InfinityBubbleCooldown"), 30);
                        }
                    }
                }
                else if (gauntletMode == 5)
                {
                    if (player.FindBuffIndex(mod.BuffType("InfinityVoidCooldown")) == -1)
                    {
                        bool kill = false;
                        bool immune = false;
                        for (int k = 0; k < Main.npc.Length; k++)
                        {
                            NPC other = Main.npc[k];
                            if (other.active && !other.friendly && !other.boss && other.damage > 0 && other.lifeMax < 10000)
                            {
                                foreach (int p in ElementsAwoken.instakillImmune)
                                {
                                    if (other.type == p)
                                    {
                                        immune = true;
                                    }
                                }
                                if (!immune)
                                {
                                    kill = !kill;
                                    if (kill)
                                    {
                                        if (Main.netMode == 0)
                                        {
                                            other.active = false;
                                        }
                                        else
                                        {
                                            player.ApplyDamageToNPC(other, other.lifeMax, knockback: 0f, direction: 0, crit: true);
                                        }
                                        for (int d = 0; d < 100; d++)
                                        {
                                            int dust = Dust.NewDust(other.position, other.width, other.height, 219);
                                            Main.dust[dust].noGravity = true;
                                            Main.dust[dust].scale = 1f;
                                            Main.dust[dust].velocity *= 2f;
                                        }
                                    }
                                }
                            }
                        }
                        if (!Config.debugMode)
                        {
                            player.AddBuff(mod.BuffType("InfinityVoidCooldown"), 2700);//2700
                        }
                        else
                        {
                            player.AddBuff(mod.BuffType("InfinityVoidCooldown"), 30);
                        }
                    }
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EmptyGauntlet", 1);
            recipe.AddIngredient(null, "AridStone", 1);
            recipe.AddIngredient(null, "PyroStone", 1);
            recipe.AddIngredient(null, "MoonStone", 1);
            recipe.AddIngredient(null, "FrigidStone", 1);
            recipe.AddIngredient(null, "AquaticStone", 1);
            recipe.AddIngredient(null, "DeathStone", 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
