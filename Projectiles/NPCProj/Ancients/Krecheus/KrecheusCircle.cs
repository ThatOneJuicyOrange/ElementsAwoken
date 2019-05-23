using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Ancients.Krecheus
{
    public class KrecheusCircle : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 15;
            projectile.height = 15;
            projectile.alpha = 255;
            projectile.timeLeft = 450;

            projectile.hostile = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;

            projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Krecheus");
        }
        public override void AI()
        {
            for (int i = 0; i < 8; i++)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("AncientRed"), 0f, 0f, 100, default(Color), 1f);
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale *= 0.5f;
            }
        }
    }
}