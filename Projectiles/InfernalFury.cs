using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    class InfernalFury : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 86;
            projectile.height = 86;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.melee = true;
            projectile.tileCollide = false;

            projectile.penetrate = -1;
            projectile.timeLeft = 60000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal Fury");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (!player.active)
            {
                projectile.Kill();
            }
            else
            {
                projectile.timeLeft = 60000;
            }
            Lighting.AddLight(player.Center, 0.75f, 0.3f, 0.1f);
            projectile.Center = player.Center;

            projectile.rotation += projectile.ai[0] == 1 ? -0.075f : 0.05f;        
            projectile.scale = projectile.ai[0] == 1 ? 1.35f : 0.9f;

            bool hasChalice = false;
            int maxAccessoryIndex = 5 + player.extraAccessorySlots;
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
                if (player.armor[i].type == mod.ItemType("InfernalChalice"))
                {
                    hasChalice = true;
                }
            }
            if (!hasChalice)
            {
                projectile.Kill();
            }
        }
    }
}