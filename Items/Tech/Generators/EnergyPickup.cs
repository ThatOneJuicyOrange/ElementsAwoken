using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ElementsAwoken.Items.Tech.Generators
{
    public class EnergyPickup : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 44;

            item.rare = 0;

            item.maxStack = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Battery");
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            float num = (float)Main.rand.Next(90, 111) * 0.01f;
            num *= Main.essScale;
            Lighting.AddLight((int)((item.position.X + (float)(item.width / 2)) / 16f), (int)((item.position.Y + (float)(item.height / 2)) / 16f), 0.1f * num, 0.5f * num, 0.2f * num);
        }
        public override bool OnPickup(Player player)
        {
            int amount = 5;
            int downedBosses = 0;
            if (NPC.downedSlimeKing)
            {
                downedBosses++;
            }
            if (NPC.downedBoss1)
            {
                downedBosses++;
            }
            if (NPC.downedBoss2)
            {
                downedBosses++;
            }
            if (NPC.downedQueenBee)
            {
                downedBosses++;
            }
            if (NPC.downedBoss3)
            {
                downedBosses++;
            }
            if (Main.hardMode)
            {
                downedBosses++;
            }
            if (NPC.downedMechBossAny)
            {
                downedBosses++;
            }
            if (NPC.downedPlantBoss)
            {
                downedBosses++;
            }
            if (NPC.downedMoonlord)
            {
                downedBosses++;
            }
            if (MyWorld.downedVoidLeviathan)
            {
                downedBosses++;
            }
            amount *= downedBosses;
            CombatText.NewText(player.getRect(), Color.LightBlue, amount, false, false);
            PlayerEnergy energyPlayer = player.GetModPlayer<PlayerEnergy>(mod);
            energyPlayer.energy += amount;
            return false;
        }
    }
}
