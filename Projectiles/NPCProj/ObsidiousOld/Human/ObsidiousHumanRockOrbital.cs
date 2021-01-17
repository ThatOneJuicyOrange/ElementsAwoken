using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.ObsidiousOld.Human
{
    public class ObsidiousHumanRockOrbital : ModProjectile
    {
        int type = 0;
        int rotType = 0;
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 100000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frame = type;
            return true;
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                type = Main.rand.Next(0, 3);
                rotType = Main.rand.Next(0, 1);
                projectile.localAI[0] = 1;
            }
            if (rotType == 0)
            {
                projectile.rotation += 3;
            }
            if (rotType == 1)
            {
                projectile.rotation -= 3;
            }

            NPC parent = Main.npc[(int)projectile.ai[1]];
            if (parent != Main.npc[0])
            {
                projectile.ai[0] += 3f; // speed
                int distance = 35;
                double rad = projectile.ai[0] * (Math.PI / 180); // angle to radians
                projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - projectile.width / 2;
                projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - projectile.height / 2;
            }
            if (!parent.active || parent.ai[1] != 1)
            {
                projectile.Kill();
            }
        }
    }
}
    