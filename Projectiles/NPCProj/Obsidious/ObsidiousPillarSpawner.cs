using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousPillarSpawner : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blazing Glory");
        }
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 20;

            projectile.tileCollide = false;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindNPCsAndTiles.Add(index);
        }
        public override void AI()
        {
            projectile.ai[0]++;
            int num = Main.expertMode ? MyWorld.awakenedMode ? 90 : 120 : 180;
            if (projectile.ai[0] == num)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 122, pitchOffset: -0.5f);
                Main.PlaySound(4, (int)projectile.position.X, (int)projectile.position.Y, 6, pitchOffset: -0.5f);
                int num48 = Projectile.NewProjectile(projectile.Center.X, projectile.Bottom.Y, 0, -20, ModContent.ProjectileType<ObsidiousPillar>(), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                NetMessage.SendData(27, -1, -1, null, num48, 0f, 0f, 0f, 0, 0, 0);
                Main.projectile[num48].localAI[1] = projectile.localAI[1];
            }
            else if (projectile.ai[0] > num)
            {

                projectile.alpha += 10;
                if (projectile.alpha >= 255) projectile.Kill();
            }
            else
            {
                projectile.alpha -= 20;
                if (projectile.alpha < 0) projectile.alpha = 0;
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(default, BlendState.Additive);

            Texture2D tex = ModContent.GetTexture("ElementsAwoken/Extra/UpwardsLightBeam");

            float alphaScale = 1f - (float)projectile.alpha / 255f;
            spriteBatch.Draw(tex, projectile.Center -Main.screenPosition, null, new Color(255, 185, 138) * alphaScale * 0.7f, 0, new Vector2(tex.Width / 2, tex.Height), new Vector2(2f, alphaScale * 2f), projectile.ai[1] == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);

            spriteBatch.End();
            spriteBatch.Begin();
        }
    }
}