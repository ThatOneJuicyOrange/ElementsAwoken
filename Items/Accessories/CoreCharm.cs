using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.Accessories
{
    public class CoreCharm : ModItem
    {
        public int shootTimer = 120;

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 4;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Core Charm");
            Tooltip.SetDefault("Occasionally fires 2 homing fire skulls at nearby enemies");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(7, 4));
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            shootTimer--;
            float maxDistance = 500f;
            if (player.whoAmI == Main.myPlayer)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= maxDistance && Collision.CanHit(player.Center, player.width, player.height, nPC.Center, nPC.width,nPC.height))
                    {
                        if (shootTimer <= 0)
                        {
                            int numberProjectiles = 2;
                            float Speed = 12f;
                            float rotation = (float)Math.Atan2(player.Center.Y - nPC.Center.Y, player.Center.X - nPC.Center.X);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(20));
                                Projectile.NewProjectile(player.Center.X, player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FireSkull"), 20, 0f, Main.myPlayer, 0f, 0f);
                            }
                            shootTimer = 120;
                            return;
                        }
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MagmaCrystal", 8);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ItemID.Bone, 16);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
