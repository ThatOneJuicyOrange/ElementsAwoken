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
    public class InfernaceWifeSoul : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.tileCollide = false;
            projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Waifu");
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
            int between = 270;
            projectile.ai[0]++;
            if (projectile.ai[0] == between) CombatText.NewText(projectile.getRect(), Color.OrangeRed, "Welcome home...", false, false);
            else if (projectile.ai[0] == between *2 ) CombatText.NewText(projectile.getRect(), Color.OrangeRed, "It's been a while.", false, false);
            else if (projectile.ai[0] == between * 3) CombatText.NewText(projectile.getRect(), Color.OrangeRed, "I'm sorry it had to end this way.", false, false);
            else if (projectile.ai[0] == between * 4 && projectile.ai[1] == 1) CombatText.NewText(projectile.getRect(), Color.OrangeRed, "Be safe, Furosia", false, false);
            else if (projectile.ai[0] > between * 4) projectile.velocity.Y -= 0.66f;
        }
    }
}