using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    public class AmuletOfDestruction : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.accessory = true;
            item.expert = true;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amulet of Despair");
            Tooltip.SetDefault("Extinction lasers rain down from the sky\nGreatly increased life regen when under 20% life\nImmunity to 'Extinction Curse' and 'Hands of Despair'\nYou charge up void energy overtime\nWhen the ability key is pressed:\n80% reduced movement speed and 20% increased damage\nProjectiles get shot in every direction");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statLife <= (player.statLifeMax2 * 0.2f))
            {
                player.lifeRegen += 10;
            }
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
            if (modPlayer.voidEnergyCharge < 3600 && modPlayer.voidEnergyTimer == 0)
            {
                modPlayer.voidEnergyCharge++;
            }
            player.buffImmune[mod.BuffType("ExtinctionCurse")] = true;
            player.buffImmune[mod.BuffType("HandsOfDespair")] = true;

            if (!hideVisual)
            {
                if (Main.rand.Next(12) == 0)
                {
                    float x = player.position.X + (float)Main.rand.Next(-400, 400);
                    float y = player.position.Y - (float)Main.rand.Next(500, 800);
                    Vector2 vector = new Vector2(x, y);
                    float num12 = player.Center.X - vector.X;
                    float num13 = player.Center.Y - vector.Y;
                    num12 += (float)Main.rand.Next(-100, 101);
                    float num = 23;
                    float num14 = (float)Math.Sqrt((double)(num12 * num12 + num13 * num13));
                    num14 = num / num14;
                    num12 *= num14;
                    num13 *= num14;
                    int num15 = Projectile.NewProjectile(x, y, num12, num13, mod.ProjectileType("AmuletProj"), 200, 5f, player.whoAmI, 0f, 0f);
                    Main.projectile[num15].ai[1] = player.position.Y;
                }
            }
        }
    }
}
