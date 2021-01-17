using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Environmental
{
    public class SulfurVentSpawn : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 2;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.timeLeft = 210;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sulfuric Gas");
        }
        public override void AI()
        {
            projectile.ai[0] += 1.5f;
            if (projectile.ai[0] % 6 ==0)
            {
                float width = MathHelper.Lerp(8, 30, projectile.ai[0] / 210f);
                Projectile.NewProjectile(projectile.Center + new Vector2(Main.rand.NextFloat(-width, width), -projectile.ai[0]), Vector2.Zero, ModContent.ProjectileType<SulfurCloud>(), projectile.damage, projectile.knockBack, projectile.owner);
            }
            if (projectile.soundDelay <= 0)
            {
                Main.PlaySound(SoundID.Item34, projectile.position);
                projectile.soundDelay = 20;
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
    }
}
