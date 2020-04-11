using System;
using System.Collections.Generic;
using ElementsAwoken.NPCs.Bosses.Infernace;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles.NPCProj.Infernace
{
    public class InfernaceSoul : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;
            projectile.tileCollide = false;
            projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Husbando");
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            for (int l = 0; l < 5; l++)
            {
                Vector2 position = projectile.Center + Main.rand.NextVector2Circular(projectile.width * 0.5f, projectile.height * 0.5f);
                Dust dust = Dust.NewDustPerfect(position, 6, Vector2.Zero);
                dust.velocity.Y = Main.rand.NextFloat(-6, -1);
                dust.noGravity = true;
                dust.fadeIn = 1.1f;
                dust = Dust.NewDustPerfect(position, 31, Vector2.Zero);
                dust.velocity.Y = Main.rand.NextFloat(-10, -5);
                dust.noGravity = true;
                dust.fadeIn = 0.9f;
            }
            projectile.velocity.Y -= 0.66f;
        }
    }
}