using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles;

namespace ElementsAwoken.Items.BossDrops.RadiantMaster
{
    [AutoloadEquip(EquipType.Face)]
    public class RadiantCrown : ModItem
    {
        private int cooldown = 0;
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 11;
            item.value = Item.sellPrice(0, 15, 0, 0);
            item.accessory = true;
            item.GetGlobalItem<EARarity>().awakened = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Crown");
            Tooltip.SetDefault("Double tap down to teleport you to the position of the mouse\nCreates a radiant storm around nearby enemies");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.radiantCrown = true;

            float range = 500f;
            cooldown--;
            if (cooldown <= 0)
            {
                for (int l = 0; l < Main.maxNPCs; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (!nPC.CanBeChasedBy(this) || Vector2.Distance(player.Center, nPC.Center) > range) continue;
                    if (player.whoAmI == Main.myPlayer)
                    {
                        Projectile proj = Main.projectile[Projectile.NewProjectile(nPC.Center, Vector2.Zero, ProjectileType<RadiantStorm>(), 200, 5f, player.whoAmI, 0f, 0f)];
                        proj.Bottom = nPC.Bottom;
                        break;
                    }
                }
                cooldown = 120;
            }
        }
    }
}
