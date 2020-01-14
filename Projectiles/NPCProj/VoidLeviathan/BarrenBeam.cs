using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.VoidLeviathan
{
    public class BarrenBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Barren Beam");
        }
        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 36;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.timeLeft = 3600;
            projectile.tileCollide = false;
        }
		internal const float charge = 220f;
        public float LaserLength { get { return projectile.localAI[1]; } set { projectile.localAI[1] = value; } }
        public const float LaserLengthMax = 5000f;
		int multiplier = 1;
		public override bool ShouldUpdatePosition ()
		{
			return false;
		}
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.ai[1] += 6f * multiplier;
            /*if (projectile.ai[1] == charge)
            {
                projectile.hostile = true;
            }*/
            if (projectile.ai[1] >= charge + 60f && multiplier == 1)
            {
                multiplier = -1;
            }
            if (multiplier == -1 && projectile.ai[1] <= 0)
                projectile.Kill();
            projectile.gfxOffY = player.gfxOffY;

            projectile.rotation = projectile.velocity.ToRotation() - 1.57079637f;
            projectile.velocity = Vector2.Normalize(projectile.velocity);

            float[] sampleArray = new float[2];
            Collision.LaserScan(projectile.Center, projectile.velocity, 0, LaserLengthMax, sampleArray);
            float sampledLength = 0f;
            for (int i = 0; i < sampleArray.Length; i++)
            {
                sampledLength += sampleArray[i];
            }
            sampledLength /= sampleArray.Length;
            float amount = 0.75f; // last prism is 0.75 rather than 0.5?
            LaserLength = MathHelper.Lerp(LaserLength, sampledLength, amount);

            #region Dusts
            Vector2 endPoint = projectile.Center + projectile.velocity * (projectile.localAI[1] - 14f);
            for (int i = 0; i < 2; i++)
            {
                float num809 = projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                float num810 = (float)Main.rand.NextDouble() * 2f + 2f;
                Vector2 vector79 = new Vector2((float)Math.Cos((double)num809) * num810, (float)Math.Sin((double)num809) * num810);
                int num811 = Dust.NewDust(endPoint, 0, 0, DustID.PinkFlame, vector79.X, vector79.Y, 0, default(Color), 1f);
                Main.dust[num811].noGravity = true;
                Main.dust[num811].scale = 1.7f;
            }
            if (Main.rand.Next(5) == 0)
            {
                Vector2 value29 = projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)projectile.width;
                int num812 = Dust.NewDust(endPoint + value29 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
                Dust dust3 = Main.dust[num812];
                dust3.velocity *= 0.5f;
                Main.dust[num812].velocity.Y = -Math.Abs(Main.dust[num812].velocity.Y);
            }
            #endregion

            projectile.ai[0]++;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float collisionPoint = 0f;
            return (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + projectile.velocity * LaserLength, projHitbox.Width, ref collisionPoint));
        }
        public override bool? CanCutTiles()
        {
            DelegateMethods.tilecut_0 = Terraria.Enums.TileCuttingContext.AttackProjectile;
            Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * LaserLength, (float)projectile.width * projectile.scale * 2, new Utils.PerLinePoint(CutTilesAndBreakWalls));
            return true;
        }

        private bool CutTilesAndBreakWalls(int x, int y)
        {
            return DelegateMethods.CutTiles(x, y);
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            //damage = (int)(target.statLifeMax2 * 0.75f) + target.statDefense;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (projectile.ai[1] < charge) return false;
            return base.CanHitPlayer(target);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.velocity == Vector2.Zero)
            {
                return false;
            }
            Texture2D tailTex = mod.GetTexture("Projectiles/NPCProj/VoidLeviathan/BarrenBeamTail");
            Texture2D beamTex = Main.projectileTexture[projectile.type];
            Texture2D headTex = mod.GetTexture("Projectiles/NPCProj/VoidLeviathan/BarrenBeamHead");
            float num228 = LaserLength;
            Color color44 = Color.White * 0.8f * (projectile.ai[1] >= charge ? 1f : 0.5f) ;
            Vector2 arg_AF99_2 = projectile.Center + new Vector2(0, projectile.gfxOffY) - Main.screenPosition;
            spriteBatch.Draw(tailTex, arg_AF99_2, null, color44, projectile.rotation, tailTex.Size() / 2f, new Vector2(Math.Min(projectile.ai[1], charge) / charge, 1f), SpriteEffects.None, 0f);
            num228 -= (float)(tailTex.Height / 2 + headTex.Height) * projectile.scale;
            Vector2 value20 = projectile.Center + new Vector2(0, projectile.gfxOffY);
            value20 += projectile.velocity * projectile.scale * (float)tailTex.Height / 2f;
            if (num228 > 0f)
            {
                float num229 = 0f;
                Rectangle rectangle7 = new Rectangle(0, 16 * (projectile.timeLeft / 3 % 5), beamTex.Width, 16);
                while (num229 + 1f < num228)
                {
                    if (num228 - num229 < (float)rectangle7.Height)
                    {
                        rectangle7.Height = (int)(num228 - num229);
                    }
                    Main.spriteBatch.Draw(beamTex, value20 - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(rectangle7), color44, projectile.rotation, new Vector2((float)(rectangle7.Width / 2), 0f), new Vector2(Math.Min(projectile.ai[1], charge) / charge, 1f), SpriteEffects.None, 0f);
                    num229 += (float)rectangle7.Height * projectile.scale;
                    value20 += projectile.velocity * (float)rectangle7.Height * projectile.scale;
                    rectangle7.Y += 16;
                    if (rectangle7.Y + rectangle7.Height > beamTex.Height)
                    {
                        rectangle7.Y = 0;
                    }
                }
            }
            Main.spriteBatch.Draw(headTex, value20 - Main.screenPosition, null, color44, projectile.rotation, headTex.Frame(1, 1, 0, 0).Top(), new Vector2(Math.Min(projectile.ai[1], charge) / charge, 1f), SpriteEffects.None, 0f);
            return false;
        }
    }
}