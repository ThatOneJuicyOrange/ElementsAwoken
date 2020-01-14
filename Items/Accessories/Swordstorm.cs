using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Accessories
{
    public class Swordstorm : ModItem
    {
        public float shootTimer = 60f;

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = 10000;
            item.rare = 1;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Swordstorm Charm");
            Tooltip.SetDefault("Shoots out swords on hit\nSwords do extra damage in hardmode and expert mode");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (shootTimer > 0f)
            {
                shootTimer -= 1f;
            }
            bool hardMode = Main.hardMode;
            bool expertMode = Main.expertMode;
            if (player.immune && shootTimer == 0f)
            {   
                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 8);
                float spread = 45f * 0.0174f;
                double startAngle = Math.Atan2(player.velocity.X, player.velocity.Y) - spread / 2;
                double deltaAngle = spread / 8f;
                double offsetAngle;
                int hardMult = hardMode ? 30 : 0;
                int expertMult = hardMode ? 10 : 0;
                int newDamage = 15 + expertMult + hardMult;
                if (player.whoAmI == Main.myPlayer)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), mod.ProjectileType("Sword"), newDamage, 1.25f, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), mod.ProjectileType("Sword"), newDamage, 1.25f, Main.myPlayer, 0f, 0f);
                    }
                }
                shootTimer = 60f;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddRecipeGroup("ElementsAwoken:SilverSword");
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
