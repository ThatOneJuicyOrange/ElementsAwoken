using System;
using System.Collections.Generic;
using System.IO;
using ElementsAwoken.Projectiles.NPCProj;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.NPCs.VolcanicPlateau.Sulfur
{
    public class ToxipotAnt : ModNPC
    {
        private float aiTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private bool voidBreak = true;
        private bool changedStats = false;
        public override void SetDefaults()
        {
            npc.width = 74;
            npc.height = 42;

            npc.aiStyle = -1;

            npc.lifeMax = 3000;
            npc.damage = 120;
            npc.defense = 30;
            npc.knockBackResist = 0.3f;

            npc.lavaImmune = true;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "Toxipot ants are an unusual species. Evolved to appear similar to honeypot ants through convergent evolution, they store sulphur in their so-called 'pots', using the food stored in there to survive when out in the harsh and barren Toxic Dunes.";

            npc.HitSound = SoundID.NPCHit31;
            npc.DeathSound = SoundID.NPCDeath34;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toxipot Ant");
            Main.npcFrameCount[npc.type] = 16;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            if (npc.velocity.Y == 0) npc.frameCounter += (int)(Math.Abs(npc.velocity.X));
            if (npc.frameCounter > 6)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.life > npc.lifeMax * 0.5f)
            {
                if (npc.frame.Y > frameHeight * 7)
                {
                    npc.frame.Y = 0;
                }
            }
            else
            {
                if (npc.frame.Y < frameHeight * 8)
                {
                    npc.frame.Y = frameHeight * 8;
                }
                if (npc.frame.Y > frameHeight * 15)
                {
                    npc.frame.Y = frameHeight * 8;
                }
            }
        }
        public override void AI()
        {
            if (voidBreak && Main.netMode != NetmodeID.MultiplayerClient)
            {
                PlateauNPCs.TryVoidbreak(npc, NPCType<VoidbrokenToxipot>());
                voidBreak = false;
            }
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];

            aiTimer++;
            int num = 240;
            if (npc.life < npc.lifeMax * 0.5f)
            {
                num *= 2;
                if (!changedStats)
                {
                    npc.damage = (int)(npc.damage * 1.5f);
                    npc.defense = (int)(npc.defense * 0.75f);
                }
            }
            if (aiTimer >= num)
            {
                float dist = Vector2.Distance(player.Center, npc.Center);
                if (dist > 800) aiTimer = 0;
                npc.velocity.X *= 0.95f;
                int delay = Main.expertMode ? MyWorld.awakenedMode ? 16 : 14 : 12;
                int numProj = Main.expertMode ? MyWorld.awakenedMode ? 5 : 4 : 3;
                if (aiTimer % delay == 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 48, 0.6f, 0.7f);

                    int timeScale = Main.expertMode ? MyWorld.awakenedMode ? 12 : 10 : 8;
                    float time = dist / timeScale;
                    float grav = 0.26f;
                    float xSPD = (player.Center.X - npc.Center.X) / time;
                    float ySPD = (player.Center.Y - npc.Center.Y - 0.5f * grav * time * time) / time;
                    Projectile.NewProjectile(npc.Center.X + 28 * npc.spriteDirection, npc.Center.Y + 4, xSPD, ySPD, ProjectileType<SulfurSpit>(), npc.damage / 2, 0f, Main.myPlayer, grav, 0f);
                }
                Main.NewText(num + (num % delay) + delay * (numProj - 1));
                if (aiTimer > num + (num % delay) + delay * (numProj - 1)) aiTimer = 0;
            }
            else
            {
                float speed = 2f;
                if (Main.expertMode) speed *= 1.1f;
                if (MyWorld.awakenedMode) speed *= 1.1f;
                if (npc.life < npc.lifeMax * 0.5f) speed *= 2;
                NPCsGLOBAL.AdjustableFighterAI(npc, 0.15f, speed);
            }
        }
      
    }
}