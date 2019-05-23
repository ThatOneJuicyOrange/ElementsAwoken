using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Other
{
    public class HandsOfDespair : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 76;
            projectile.height = 60;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 60000;
            projectile.light = 0.5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hands of Despair");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            Player parent = Main.player[(int)projectile.ai[1]];
            NPC parent1 = Main.npc[(int)projectile.ai[1]];

            //Player player = Main.player[projectile.owner];

            if (projectile.ai[0] > 0)
            {
                projectile.position.X = parent1.Center.X - (projectile.width / 2);
                projectile.position.Y = parent1.position.Y + 5;
                if (parent1.FindBuffIndex(mod.BuffType("HandsOfDespair")) == -1)
                {
                    projectile.alpha++;
                }
                if (!parent1.active)
                {
                    projectile.Kill();
                }
                projectile.hostile = true;
                projectile.friendly = false;
            }
            else
            {
                projectile.position.X = parent.Center.X - (projectile.width / 2);
                projectile.position.Y = parent.position.Y + 5;
                if (parent.FindBuffIndex(mod.BuffType("HandsOfDespair")) == -1)
                {
                    projectile.alpha += 10;
                }
                if (!parent.active)
                {
                    projectile.Kill();
                }
                projectile.hostile = false;
                projectile.friendly = true;
            }

            if (projectile.alpha >= 255)
            {
                projectile.Kill();
            }
        }
    }
}