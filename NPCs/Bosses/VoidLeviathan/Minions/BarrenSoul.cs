using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.VoidLeviathan.Minions
{
    public class BarrenSoul : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 26;
            npc.height = 20;

            npc.damage = 20;
            npc.defense = 20;
            npc.lifeMax = 1000;
            npc.knockBackResist = 0f;

            npc.noGravity = true;
            npc.immortal = true;
            npc.dontTakeDamage = true;

            animationType = 5;
            npc.HitSound = SoundID.NPCHit55;
            npc.DeathSound = SoundID.NPCDeath59;
        }
        public override void SetStaticDefaults()    
        {
            DisplayName.SetDefault("Barren Soul");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.5f);
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];

            NPC parent = Main.npc[(int)npc.ai[1]];
            npc.ai[0] += 3f; // speed
            int distance = 125;
            double rad = npc.ai[0] * (Math.PI / 180); // angle to radians
            npc.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - npc.width / 2;
            npc.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - npc.height / 2;
            if (!parent.active)
            {
                npc.active = false;
            }

            if (Main.rand.Next(400) == 0)
            {
                float Speed = 10f;
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 12);
                float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                int num54 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), mod.ProjectileType("ExtinctionBlast"), 120, 0f, 0);
            }
            npc.rotation *= 0f;
            Lighting.AddLight(npc.Center, 0.4f, 0.2f, 0.4f);
        }
    }
}