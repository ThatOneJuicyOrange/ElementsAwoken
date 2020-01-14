using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.VoidLeviathan
{
    public class VoidOrbDestroyed : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 86;
            projectile.height = 86;

            projectile.tileCollide = false;

            projectile.scale = 1.2f;

            projectile.timeLeft = 300;
            projectile.alpha = 60;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void's Orb");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1f, 0.2f, 0.55f);
            projectile.ai[0]++;
            int deathDuration = 20;
            projectile.scale += 3f / deathDuration;
            projectile.alpha += (255 - 60) / deathDuration;
            if (projectile.alpha >= 255) projectile.Kill();
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("ExtinctionCurse"), 80, true);
        }
    }
}