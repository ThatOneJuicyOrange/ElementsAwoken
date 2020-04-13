using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Azana
{
    public class AzanaLaserWarning : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("warning");
        }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;

            projectile.aiStyle = -1;
            projectile.penetrate = -1;

            projectile.alpha = 255;
            projectile.timeLeft = 3600;
            projectile.tileCollide = false;
        }

        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            projectile.ai[0]++;
            if (projectile.ai[0] <= 20)
            {
                if (projectile.alpha <= 255) projectile.alpha -= 255 / 20;            
            }
            else if (projectile.ai[0] == 60)
            {
                Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, -24, ModContent.ProjectileType<AzanaInfectionPillar>(), projectile.damage, 0f, Main.myPlayer, 0, 40)];
                proj.localAI[1] = 125;
                proj.Center = projectile.Center;
            }
            else if (projectile.ai[0] >= 60)
            {
                projectile.alpha += 255 / 30;
                if (projectile.alpha >= 255) projectile.Kill();
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {      
            Main.spriteBatch.Draw(Main.magicPixel, projectile.position - new Vector2(3,4000)- Main.screenPosition, null, new Color(217, 107, 84) * (1 - ((float)projectile.alpha /255f)), projectile.rotation, Vector2.Zero, new Vector2(6,4), SpriteEffects.None, 0f);
            return false;
        }
    }
}