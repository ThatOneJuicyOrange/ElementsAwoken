using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Permafrost
{
    public class PermaOrbital : ModNPC
    {
        public float shootTimer1 = 180f;

        public override void SetDefaults()
        {
            npc.width = 50;
            npc.height = 50;

            npc.damage = 20;
            npc.defense = 18;
            npc.lifeMax = 3000;

            npc.knockBackResist = 0f;
            npc.npcSlots = 0f;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;

            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[mod.BuffType("IceBound")] = true;
            npc.buffImmune[mod.BuffType("EndlessTears")] = true;

            npc.noTileCollide = true;
            npc.noGravity = true;
            animationType = NPCID.Harpy;

            npc.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Perma Orbital");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 4000;
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            if (npc.alpha > 0)
            {
                npc.alpha -= 255 / 60;
            }
           /* shootTimer1--;
            if (shootTimer1 <= 0)
            {
                float Speed = 6f;
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 35;
                int type = mod.ProjectileType("PermafrostBolt");
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer, 0, 1);
                shootTimer1 = Main.rand.Next(60, 180);
            }*/

            NPC parent = Main.npc[(int)npc.ai[0]];

            npc.ai[2] += 1f; // wave
            npc.ai[1] += 2f; // speed
            int distance = 100 + (int)(Math.Sin(npc.ai[2] / 60) * 30);
            double rad = npc.ai[1] * (Math.PI / 180); // angle to radians
            npc.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - npc.width / 2;
            npc.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - npc.height / 2;
            if (!parent.active)
            {
                npc.active = false;
            }
        }
    }
}