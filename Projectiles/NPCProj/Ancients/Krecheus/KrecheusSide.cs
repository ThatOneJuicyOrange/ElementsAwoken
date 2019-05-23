using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Ancients.Krecheus
{
    public class KrecheusSide : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 48;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Krecheus");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 0.785f;

            int num49 = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("AncientRed"), 0f, 0f, 100, default(Color), 1f);
            Main.dust[num49].velocity *= 0.3f;
            Main.dust[num49].fadeIn = 0.9f;
            Main.dust[num49].noGravity = true;
        }
    }
}