using System;
using System.Collections.Generic;
using ElementsAwoken.Projectiles.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Azana
{
    public class AzanaTarget : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;

            projectile.hostile = true;
            projectile.tileCollide = false;

            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 120;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Target");
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item95, projectile.Center);

            int numProj = Main.expertMode ? MyWorld.awakenedMode ? 10 : 8 : 6;
            for (int i = 0; i < numProj; i++)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 750, Main.rand.NextFloat(-2,2), Main.rand.NextFloat(14,22), mod.ProjectileType("AzanaGlob"), projectile.damage, projectile.knockBack, projectile.owner);
            }
        }
        public override void AI()
        {
            projectile.rotation += 0.1f;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Texture2D shell = ModContent.GetTexture("ElementsAwoken/Projectiles/NPCProj/Azana/AzanaTarget1");
            Vector2 shellOrigin = new Vector2(shell.Width * 0.5f, shell.Height * 0.5f);
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, Main.projectileTexture[projectile.type].Height * 0.5f);
            Vector2 shellPos = projectile.Center  - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            Vector2 drawPos = projectile.Center  - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            Color color = projectile.GetAlpha(lightColor);
            sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, 0, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            sb.Draw(shell, shellPos, null, color, projectile.rotation, shellOrigin, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}