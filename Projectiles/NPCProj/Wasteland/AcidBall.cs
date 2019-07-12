using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Wasteland
{
    public class AcidBall : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;

            projectile.penetrate = 1;
            projectile.timeLeft = 120;

            projectile.tileCollide = true;
            projectile.hostile = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Blob");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            projectile.velocity.Y += 0.16f;

            for (int i = 0; i < 3; i++)
            {
                int dust1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 75, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity *= 0f;
            }
            if (projectile.timeLeft <= 60)
            {
                if (Main.rand.Next(60) == 0)
                {
                    projectile.Kill();
                }
            }
            if (Vector2.Distance(Main.player[Main.myPlayer].Center, projectile.Center) <= 75)
            {
                if (Main.rand.Next(15) == 0)
                {
                    projectile.Kill();
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("AcidAuraBase"), 0, 0f, projectile.owner, 0f, 0f);
            Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/AcidHiss"));
        }
    }
}