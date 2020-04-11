using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Yoyos
{
    public class FusionP : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;

            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;

            projectile.light = 0.5f;

            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 480f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 18f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Inferno");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Slow, 200);
            target.AddBuff(BuffID.OnFire, 200);
            target.AddBuff(BuffID.VortexDebuff, 200);
            target.AddBuff(BuffID.Frostburn, 200);
            target.AddBuff(BuffID.Wet, 200);
            target.AddBuff(mod.BuffType("ExtinctionCurse"), 200);
        }
        public override void AI()
        {
  int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 63, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
            if (Main.rand.Next(7) == 0)
            {
                int type = mod.ProjectileType("FusionOrb1");
                switch (Main.rand.Next(6))
                {
                    case 0: type = mod.ProjectileType("FusionOrb1"); break;
                    case 1: type = mod.ProjectileType("FusionOrb2"); break;
                    case 2: type = mod.ProjectileType("FusionOrb3"); break;
                    case 3: type = mod.ProjectileType("FusionOrb4"); break;
                    case 4: type = mod.ProjectileType("FusionOrb5"); break;
                    case 5: type = mod.ProjectileType("FusionOrb6"); break;
                    default: break;
                }
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 16f, Main.rand.Next(-50, 50) * 0.25f, Main.rand.Next(-50, 50) * 0.25f, type, projectile.damage, 0, projectile.owner);
            }
        }
    }
}