using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Erius
{
    public class EriusAcidBall : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;

            projectile.hostile = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 300;
            
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Ball");
        }
        public override void AI()
        {
            projectile.rotation += 0.08f;
            projectile.velocity.Y += 0.16f;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Debuffs.AcidBurn>(), 200);
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                float alpha = 1 - ((float)k / (float)projectile.oldPos.Length);
                float scale = 1 - ((float)k / (float)projectile.oldPos.Length);
                Color color = Color.Lerp(Color.White, new Color(66, 135, 245), (float)k / (float)projectile.oldPos.Length) * alpha;
                sb.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale * scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/AcidHiss"));
            for (int i = 0; i < 6; i++)
            {
                float distance = i < 3 ? 20 : 10;
                Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.Center + Main.rand.NextVector2Square(-distance, distance), Vector2.Zero, ModContent.ProjectileType<AcidCloud>(), projectile.damage, 0, projectile.owner, i < 3 ? 0 : 1)];
                proj.localAI[0] = Main.rand.NextBool() ? -1 : 1;
                proj.rotation = Main.rand.NextFloat((float)Math.PI * 2);
            }
        }
    }
}