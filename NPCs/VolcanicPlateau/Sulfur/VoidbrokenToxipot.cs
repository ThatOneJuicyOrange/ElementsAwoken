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
    public class VoidbrokenToxipot : ModNPC
    {
        private bool changedStats = false;
        private float aiTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCType<ToxipotAnt>());
            npc.lifeMax = (int)(npc.lifeMax * 1.5f);
            npc.defense = (int)(npc.defense * 1.5f);
            npc.damage = (int)(npc.damage * 1.5f);
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "Voidpot ants are infected toxipot ants acting on the commands of an unknown force. They have a form of voidbroken fluid in the ‘pot’ known as Nihiline. The pot is not entirely solid, and creatures such as small fish and flare hornets can get trapped in it.";
            npc.GetGlobalNPC<PlateauNPCs>().voidBroken = true;
            npc.GetGlobalNPC<PlateauNPCs>().counterpart = NPCType<ToxipotAnt>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Voidpot Ant");
            Main.npcFrameCount[npc.type] = 8;
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
            if (npc.frame.Y > frameHeight * 7)
            {
                npc.frame.Y = 0;
            }
        }
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 0.4f, 0.2f, 0.4f);

            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            float speed = 3f;
            int num = 180;
            if (npc.life < npc.lifeMax * 0.5f)
            {
                num *= 2;

                if (!changedStats)
                {
                    npc.damage = (int)(npc.damage * 1.5f);
                    npc.defense = (int)(npc.defense * 0.75f);
                }

                if (Main.rand.NextBool(6))
                {
                    Dust dust = Main.dust[Dust.NewDust(npc.Top - new Vector2(npc.spriteDirection == 1 ? npc.width / 2 : 0,0), 34, 28, DustID.PinkFlame)];
                    dust.noGravity = true;
                    dust.scale *= 0.4f;
                    dust.fadeIn = 1.1f;
                    dust.velocity.Y = -Main.rand.NextFloat(2f, 5);
                }

                if (npc.velocity.X != 0 || npc.velocity.Y != 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(npc.Center + new Vector2(28 * npc.spriteDirection, 4), 2, 2, DustID.PinkFlame)];
                        dust.position -= npc.velocity * ((float)i * 0.25f);
                        dust.scale = 1.1f;
                        dust.velocity = Vector2.Zero;
                        dust.noGravity = true;
                    }
                }
            }


            aiTimer++;
            if (aiTimer >= num)
            {
                float dist = Vector2.Distance(player.Center, npc.Center);
                if (dist > 1200) aiTimer = 0;
                npc.velocity.X *= 0.95f;
                int delay = Main.expertMode ? MyWorld.awakenedMode ? 24 : 20 : 16;
                int numProj = Main.expertMode ? MyWorld.awakenedMode ? 7 : 5 : 4;
                if (aiTimer % delay == 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 48, 0.6f, 0.7f);

                    int timeScale = Main.expertMode ? MyWorld.awakenedMode ? 16 : 13 : 10;
                    float time = dist / timeScale;
                    float grav = 0.26f;
                    float xSPD = (player.Center.X - npc.Center.X) / time;
                    float ySPD = (player.Center.Y - npc.Center.Y - 0.5f * grav * time * time) / time;
                    Projectile.NewProjectile(npc.Center.X + 28 * npc.spriteDirection, npc.Center.Y + 4, xSPD, ySPD, ProjectileType<VoidSpit>(), npc.damage / 2, 0f, Main.myPlayer, grav, 0f);
                }
                Main.NewText(num + (num % delay) + delay * (numProj - 1));
                if (aiTimer > num + (num % delay) + delay * (numProj - 1)) aiTimer = 0;
            }
            else
            {
                if (Main.expertMode) speed *= 1.1f;
                if (MyWorld.awakenedMode) speed *= 1.1f;
                if (npc.life < npc.lifeMax * 0.5f) speed *= 2;
                NPCsGLOBAL.AdjustableFighterAI(npc, 0.25f, speed);
            }
        }

    }
}