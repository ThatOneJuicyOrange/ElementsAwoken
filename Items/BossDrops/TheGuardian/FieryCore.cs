using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.TheGuardian
{
    public class FieryCore : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.accessory = true;
            item.expert = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fiery Core");
            Tooltip.SetDefault("The ancient temple artifact of fire\n10% increased damage when in hell\nImmunity to On Fire and Burning\nYou set nearby creatures on fire and inflict fire on hit");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Main.player[Main.myPlayer].ZoneUnderworldHeight)
            {
                player.meleeDamage *= 1.10f;
                player.thrownDamage *= 1.10f;
                player.rangedDamage *= 1.10f;
                player.magicDamage *= 1.10f;
                player.minionDamage *= 1.10f;
            }
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Burning] = true;
            float num2 = 500f;
            int random = Main.rand.Next(10);
            if (player.whoAmI == Main.myPlayer)
            {
                if (random == 0)
                {
                    for (int l = 0; l < 200; l++)
                    {
                        NPC nPC = Main.npc[l];
                        if (nPC.active && !nPC.friendly && !nPC.boss && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= num2 && nPC.life <= 30)
                        {
                            Projectile.NewProjectile(nPC.Center.X, nPC.Center.Y, 0f, 0f, 612, 50, 4, Main.myPlayer);
                        }
                        if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= num2)
                        {
                            nPC.AddBuff(BuffID.OnFire, 180, false);
                        }
                    }
                }
            }
        }
    }
}
