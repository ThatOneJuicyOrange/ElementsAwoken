using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Buffs.Debuffs;

namespace ElementsAwoken.Projectiles
{
    public class PutridCloud : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50; // slightly offset the width and height to make it rotate weird

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 600;
            projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Putrid Cloud");
            Main.projFrames[projectile.type] = 5;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 4)
                    projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation -= 0.08f * projectile.localAI[0];
            projectile.ai[1]++;
            int lowestAlpha = projectile.ai[0] == 1 ? 210 : 170;
            int diff = 255 - lowestAlpha;
            if (projectile.ai[1] < 90)
            {
                if (projectile.alpha > lowestAlpha) projectile.alpha -= diff / 20;
            }
            else
            {
                projectile.alpha++;
                if (projectile.alpha >= 255) projectile.Kill();
            }
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.Hitbox.Intersects(projectile.Hitbox) && !npc.townNPC) npc.AddBuff(BuffType<FastPoison>(), 60);
            }
        }

    }
}