using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Ancients.Krecheus
{
    public class KrecheusPortal : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.timeLeft = 450;

            projectile.hostile = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;

            projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Krecheus");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
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
            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("AncientRed"), 0f, 0f, 100, default(Color), 1f);
            Main.dust[dust].velocity *= 0f;
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale *= 0.5f;

            Player player = Main.player[Main.myPlayer];
            projectile.localAI[1]--;
            if (projectile.localAI[1] <= 0)
            {
                float Speed = 12f;
                float rotation = (float)Math.Atan2(projectile.Center.Y - player.Center.Y, projectile.Center.X - player.Center.X);
                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("KrecheusSpike"), projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                projectile.localAI[1] = Main.rand.Next(8, 12);
            }
        }        
    }
}