using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Permafrost
{
    public class PermafrostMinion : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 24;

            npc.aiStyle = 86;

            npc.damage = 40;
            npc.defense = 12;
            npc.lifeMax = 1000;       
            npc.knockBackResist = 0.05f;

            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.HitSound = SoundID.NPCHit52;
            npc.DeathSound = SoundID.NPCDeath55;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Permafrost Spirit");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 2000;
            npc.damage = 60;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 4000;
                npc.damage = 75;
                npc.defense = 20;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.X < 0f)
            {
                npc.direction = -1;
            }
            else
            {
                npc.direction = 1;
            }
            if (npc.direction == 1)
            {
                npc.spriteDirection = 1;
            }
            if (npc.direction == -1)
            {
                npc.spriteDirection = -1;
            }
            npc.rotation = (float)Math.Atan2((double)(npc.velocity.Y * (float)npc.direction), (double)(npc.velocity.X * (float)npc.direction));
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }

        public override void AI()
        {
            Lighting.AddLight(npc.Center, 1.0f, 0.2f, 0.7f);

            int dust = Dust.NewDust(npc.position, npc.width, npc.height, 135);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;

            if (!NPC.AnyNPCs(mod.NPCType("Permafrost")))
            {
                npc.active = false;
            }
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Frostburn, 80, true);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 135, hitDirection, -1f, 0, default(Color), 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 135, hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }
    }
}