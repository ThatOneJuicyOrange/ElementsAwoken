using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class DragonBladeBlast : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.timeLeft = 300;

            projectile.melee = true;
            projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Blade");
        }
        public override void AI()
        {
            projectile.Center = Main.player[projectile.owner].Center + new Vector2(0, 4);

            float max = 70;
            projectile.ai[0] += max / 8f;
            if (projectile.ai[0] > max) projectile.Kill();
            for (int i = 0; i < 120; i++)
            {
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                Vector2 offset = new Vector2((float)Math.Sin(angle) * projectile.ai[0], (float)Math.Cos(angle) * projectile.ai[0]);
                Dust dust = Main.dust[Dust.NewDust(projectile.Center + offset, 0, 0, 6, 0, 0, 100)];
                dust.noGravity = true;
            }
            if (projectile.ai[0] == max)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && npc.damage > 0 && !npc.boss && Vector2.Distance(npc.Center, projectile.Center) < max + 20)
                    {
                        npc.AddBuff(BuffID.OnFire, 120);
                        Vector2 toTarget = new Vector2(projectile.Center.X - npc.Center.X, projectile.Center.Y - npc.Center.Y);
                        toTarget.Normalize();
                        npc.velocity -= toTarget * 10 * npc.knockBackResist;
                        if (!npc.noGravity) npc.velocity.Y -= 4 * npc.knockBackResist;
                    }
                }
            }
        }
    }
}