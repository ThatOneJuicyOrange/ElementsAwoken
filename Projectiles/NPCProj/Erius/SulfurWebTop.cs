using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.Erius
{
    public class SulfurWebTop : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 18;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sulfur Web");
        }
        public override void AI()
        {
            if (projectile.ai[0] == 0)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, ModContent.ProjectileType<SulfurWeb>(), 0, 0f, Main.myPlayer,0,projectile.whoAmI);  
                    projectile.ai[0]++;
            }
        }
    }
}