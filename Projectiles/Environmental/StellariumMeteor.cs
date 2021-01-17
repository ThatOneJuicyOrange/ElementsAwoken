using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Environmental
{
    public class StellariumMeteor : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 32;

            projectile.scale = 1.0f;

            projectile.penetrate = 1;
            projectile.friendly = true;

            projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 16;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellarium Meteor");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1f, 0.3f, 0.3f);

            projectile.rotation += 0.1f;

            if (Main.rand.Next(20) == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 58, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 150, default(Color), 1.2f);
            }
            if (projectile.soundDelay == 0)
            {
                projectile.soundDelay = 20 + Main.rand.Next(40);
                Main.PlaySound(SoundID.Item9, projectile.position);
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            float rot = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            Texture2D texBack = ModContent.GetTexture("ElementsAwoken/Projectiles/Environmental/StellariumMeteorBack");
            Vector2 drawPos2 = projectile.position - projectile.velocity * 3 - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
            Texture2D tex = Main.projectileTexture[projectile.type];
            float num4 = (float)MyWorld.generalTimer / 30f;
            for (float num6 = 0f; num6 < 1f; num6 += 0.25f)
            {
                sb.Draw(texBack, drawPos2 + new Vector2(0f, 12f).RotatedBy((double)((num6 + num4) * 6.28318548f), default(Vector2)), texBack.Frame(), new Color(217, 115, 205) * 0.3f, rot, texBack.Size() / 2, 1f, SpriteEffects.None, 0f);
            }
            for (float num7 = 0f; num7 < 1f; num7 += 0.34f)
            {
                sb.Draw(texBack, drawPos2 + new Vector2(0f, 8f).RotatedBy((double)((num7 + num4) * 6.28318548f), default(Vector2)), texBack.Frame(), new Color(114, 217, 219) * 0.3f, rot, texBack.Size() / 2, 1f, SpriteEffects.None, 0f);
            }

            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = (new Color(203, 247, 218) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length)) * 0.5f;
                float scale = projectile.scale * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                sb.Draw(texBack, drawPos, null, color, rot, drawOrigin, scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);

            int item = Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, ModContent.ItemType<Items.ItemSets.Stellarium.StellariumMeteorItem>());
            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
        }
    }
}