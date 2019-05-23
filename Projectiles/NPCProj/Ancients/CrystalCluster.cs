using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.Ancients
{
    public class CrystalCluster : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 44;
            projectile.ignoreWater = true;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Amalgamate");
        }
        public override bool CanHitPlayer(Player target)
        {
            if (projectile.localAI[0] <= 90)
            {
                return false;
            }

            return base.CanHitPlayer(target);

        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                projectile.scale = 0.1f;
            }
            
            projectile.localAI[0]++;
            if (projectile.localAI[0] <= 60)
            {
                projectile.scale += 0.01f;
            }
            else
            {
                projectile.scale += 0.05f;
            }
            if (projectile.scale >= 1.5f)
            {
                projectile.scale = 1.5f;

                projectile.localAI[1]++;
                if (projectile.localAI[1] > 15)
                {
                    projectile.Kill();
                    Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
                }
            }
            if (Main.rand.Next(3) == 0)
            {
                int dustBoxWidth = (int)(projectile.width * projectile.scale);
                int dust = Dust.NewDust(projectile.Center - new Vector2(dustBoxWidth / 2, dustBoxWidth / 2), dustBoxWidth, dustBoxWidth, GetDustID(), 0f, 0f, 100, default(Color));
                Main.dust[dust].noGravity = true;
            }
        }
        private int GetDustID()
        {
            int dustType = mod.DustType("AncientRed");
            switch (Main.rand.Next(4))
            {
                case 0:
                    return mod.DustType("AncientRed");
                case 1:
                    return mod.DustType("AncientGreen");
                case 2:
                    return mod.DustType("AncientBlue");
                case 3:
                    return mod.DustType("AncientPink");
                default:
                    return mod.DustType("AncientRed");
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 12; k++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, GetDustID(), 0f, 0f, 100, default(Color));
                Main.dust[dust].noGravity = true;
            }
        }
    }
}
    