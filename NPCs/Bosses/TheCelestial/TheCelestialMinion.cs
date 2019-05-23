using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.TheCelestial
{
    public class TheCelestialMinion : ModNPC
    {
        public float shootTimer1 = 180f;

        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 40;

            npc.npcSlots = 0f;

            npc.damage = NPC.downedMoonlord ? 50 : 19;
            npc.defense = NPC.downedMoonlord ? 40 : 30;
            npc.lifeMax = NPC.downedMoonlord ? 10000 : 2500;

            npc.knockBackResist = 0f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Watcher");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = NPC.downedMoonlord ? 17500 : 4000;
            npc.damage = NPC.downedMoonlord ? 110 : 75;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = NPC.downedMoonlord ? 25000 : 7500;
                npc.damage = NPC.downedMoonlord ? 140 : 90;
                npc.defense = NPC.downedMoonlord ? 60 : 35;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[2] == 0) // solar
            {
                npc.frame.Y = 0;
            }
            else if (npc.ai[2] == 1) // stardust
            {
                npc.frame.Y = 1 * frameHeight;
            }
            else if (npc.ai[2] == 2) // vortex
            {
                npc.frame.Y = 2 * frameHeight;
            }
            else if (npc.ai[2] == 3) // nebula
            {
                npc.frame.Y = 3 * frameHeight;
            }
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            Lighting.AddLight(npc.Center, 1f, 1f, 1f);
            if (npc.velocity.X >= 0f)
            {
                npc.spriteDirection = 1;
                Vector2 direction = Main.player[npc.target].Center - npc.Center;
                npc.rotation = direction.ToRotation();
            }
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = -1;
                Vector2 direction = Main.player[npc.target].Center - npc.Center;
                npc.rotation = direction.ToRotation() - 3.14f;
            }
            if (Main.rand.Next(200) == 0)
            {
                float Speed = 4f;
                int damage = 60;
                float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 12);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("TheCelestialLaser"), damage, 0f, 0, npc.ai[2]);
            }
            for (int i = 0; i < 2; i++)
            {
                int dustType = 6;
                switch ((int)npc.ai[2])
                {
                    case 0:
                        dustType = 6;
                        break;
                    case 1:
                        dustType = 197;
                        break;
                    case 2:
                        dustType = 229;
                        break;
                    case 3:
                        dustType = 242;
                        break;
                    default: break;
                }
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.1f;
            }
            // spin
            NPC parent = Main.npc[(int)npc.ai[1]];
            npc.ai[0] += 3f; // speed
            int distance = 150;
            double rad = npc.ai[0] * (Math.PI / 180); // angle to radians
            npc.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - npc.width / 2;
            npc.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - npc.height / 2;
            if (!parent.active || (npc.ai[2] == 0 && parent.ai[1] > 0) || (npc.ai[2] == 1 && parent.ai[1] > 1) || (npc.ai[2] == 2 && parent.ai[1] > 2) || (npc.ai[2] == 3 && parent.ai[1] > 3))
            {
                npc.active = false;
            }
            // old code
            /*
            NPC center = Main.npc[0];
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].type == mod.NPCType("TheCelestial"))
                {
                    center = Main.npc[i];
                    break;
                }
            }
            Vector2 offset = new Vector2(200, 0);
            if (center != Main.npc[0])
            {
                npc.ai[0] += 0.05f;
                npc.Center = center.Center + offset.RotatedBy(npc.ai[0] + npc.ai[1] * (Math.PI * 2 / 8));
            }
            if (!NPC.AnyNPCs(mod.NPCType("TheCelestial")))
            {
                npc.active = false;
            }
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].type == mod.NPCType("TheCelestial"))
                {
                    if (Main.npc[i].ai[1] > 1)
                    {
                        npc.active = false;
                    }
                    break;
                }
            }*/
        }
    }
}