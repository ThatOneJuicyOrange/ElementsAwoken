using ElementsAwoken.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class WispBolt : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;

            projectile.friendly = true;
            projectile.tileCollide = false;

            projectile.penetrate = 1;
            projectile.timeLeft = 30;
            projectile.light = 1f;
            projectile.extraUpdates = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wisp Bolt");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Incineration>(), 300);
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            /*for (int i = 0; i < 3; i++)
            {
                Dust fire = Main.dust[Dust.NewDust(projectile.Center, projectile.width, projectile.height, 6, projectile.velocity.X * 0.6f, projectile.velocity.Y * 0.6f, 130, default(Color), 1)];
                fire.velocity *= 0.6f;
                fire.noGravity = true;
            }
            if (Main.rand.NextBool(3))
            {
                Dust smoke = Main.dust[Dust.NewDust(projectile.Center, projectile.width, projectile.height, 31, projectile.velocity.X * 0.6f, projectile.velocity.Y * 0.6f, 130, default(Color), 1)];
                smoke.velocity *= 0.6f;
            }*/
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 3.14f;
            if (projectile.localAI[0] == 0) projectile.localAI[0] = 45;
            projectile.velocity *= 1.01f;
            projectile.ai[0]++;
            int dustType = modPlayer.wispDust;
            Dust dust = Main.dust[Dust.NewDust(projectile.position + new Vector2(0, (float)Math.Sin(projectile.ai[0] / 5) * 5), projectile.width, projectile.height, dustType, Scale: 1.5f)];
            dust.velocity *= 0.1f;
            dust.noGravity = true;
            for (int l = -1; l < 2; l += 2)
            {
                Vector2 vel = new Vector2(Main.rand.NextFloat(0.1f, 0.3f), l * Main.rand.NextFloat(2.6f, 4f) * (1 - projectile.ai[0] / projectile.localAI[0])).RotatedBy((double)projectile.rotation, default(Vector2));
                Dust dust2 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, Scale: 1.5f)];
                dust2.velocity = vel;
                dust2.noGravity = true;
            }
        }
    }
}