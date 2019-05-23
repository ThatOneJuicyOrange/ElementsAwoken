using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Wasteland
{
    public class WastelandDiggingSpoutReal : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.tileCollide = true;
            projectile.timeLeft = 400; // only lasts 360, its killed in wastelands code
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wasteland");
        }
        public override void AI()
        {
            projectile.velocity *= 0f;
            for (int k = 0; k < 3; k++)
            {
                int dust2 = Dust.NewDust(projectile.position, projectile.width, 32, 32, 0f, -16f, 100, default(Color), 1.5f);
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].velocity *= 1f;
            }

            int dust = Dust.NewDust(projectile.position, projectile.width, 32, 75, 0f, -16f, 100, default(Color), 1.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity.Y *= 16f;
            projectile.ai[1]++;
            /*NPC parent = Main.npc[(int)projectile.ai[0]];
            projectile.localAI[1]++;
            if (projectile.localAI[1] > 120)
            {
                if (parent != Main.npc[0])
                {
                    parent.position.X = projectile.Center.X - (float)parent.width / 2f;
                    parent.position.Y = projectile.Center.Y + 300;
                }
            }
            if (!parent.active)
            {
                projectile.Kill();
            }*/
            //Main.NewText("parent " + parent.FullName, Color.Orange.R, Color.Orange.G, Color.Orange.B);

        }

        public override void Kill(int timeLeft)
        {
            /*NPC parent = Main.npc[(int)projectile.ai[0]];
            if (parent != Main.npc[0])
            {
                parent.ai[1] = 1;
            }*/
        }
    }
}