using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.TheCelestial
{
    public class TheCelestialMinion : ModNPC
    {
        private float spinAI
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float type
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float shootTimer
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 40;

            npc.aiStyle = -1;

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
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Watcher");
            Main.npcFrameCount[npc.type] = 16;
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
            npc.spriteDirection = npc.direction;

            npc.frameCounter++;
            if (npc.frameCounter > 6)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y >= ((type + 1) * frameHeight) * 4) npc.frame.Y = (int)(type * frameHeight) * 4;
            if (npc.frame.Y < (type * frameHeight) * 4) npc.frame.Y = (int)(type * frameHeight) * 4;
        }
        public override void AI()
        {
            npc.TargetClosest(false);
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
            shootTimer--;
            if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 12);
                float Speed = 4f;
                float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("TheCelestialLaser"), 60, 0f, Main.myPlayer, type);
                shootTimer = Main.rand.Next(60, 450);
                if (MyWorld.awakenedMode) shootTimer = Main.rand.Next(5, 240);
                else if (Main.expertMode) shootTimer = Main.rand.Next(20, 360);
                npc.netUpdate = true;
            }
            // spin
            NPC parent = Main.npc[(int)npc.ai[1]];
            spinAI += 3f; // speed
            int distance = 150;
            double rad = spinAI * (Math.PI / 180); // angle to radians
            npc.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - npc.width / 2;
            npc.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - npc.height / 2;
            if (!parent.active || parent.ai[1] > type) npc.active = false;
            if (!ModContent.GetInstance<Config>().lowDust)
            {
                int dustType = 6;
                switch ((int)type)
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
        }
    }
}