using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Volcanox
{
    public class Firefly : ModNPC
    {
        public override void SetDefaults()
        {
            npc.aiStyle = 86;
            npc.damage = 150;
            npc.width = 40; 
            npc.height = 24;
            npc.defense = 12;
            npc.lifeMax = 1000;
            npc.knockBackResist = 0.05f;
            npc.HitSound = SoundID.NPCHit52;
            npc.DeathSound = SoundID.NPCDeath55;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Firefly");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 2000;
            npc.damage = 175;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 5000;
                npc.damage = 200;
                npc.defense = 20;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = -1;
                npc.direction = -1;
            }
            else
            {
                npc.direction = 1;
                npc.spriteDirection = 1;
            }
            npc.rotation = (float)Math.Atan2((double)(npc.velocity.Y * (float)npc.direction), (double)(npc.velocity.X * (float)npc.direction));

            npc.frameCounter += 1;
            if (npc.frameCounter > 5)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * (Main.npcFrameCount[npc.type] - 1))
            {
                npc.frame.Y = 0;
            }
        }

        public override void AI()
        {
            Lighting.AddLight(npc.Center, 1.0f, 0.2f, 0.7f);

            int dust = Dust.NewDust(npc.position, npc.width, npc.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 400, true);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 6, hitDirection, -1f, 0, default(Color), 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 6, hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }
    }
}