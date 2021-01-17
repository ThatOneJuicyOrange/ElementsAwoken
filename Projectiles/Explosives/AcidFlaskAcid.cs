using ElementsAwoken.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Explosives
{
    public class AcidFlaskAcid : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;

            projectile.friendly = true;
            projectile.tileCollide = false;

            projectile.penetrate = 5;
            projectile.timeLeft = 45;
            projectile.light = 1f;
            projectile.extraUpdates = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid");
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void AI()
        {
            if (projectile.velocity.Y < 6) projectile.velocity.Y += 0.08f;

            Dust dust = Main.dust[Dust.NewDust(projectile.Center, projectile.width, projectile.height, 74, projectile.velocity.X * 0.6f, projectile.velocity.Y * 0.6f)];
            dust.velocity *= 0.6f;
            dust.scale *= 1.6f;
            dust.fadeIn = 1f;
            dust.noGravity = true;
            dust.noLight = true;

            int i = (int)projectile.Center.X / 16;
            int j = (int)projectile.Center.Y / 16;
            Tile t = Framing.GetTileSafely(i, j);
            if (t.active() && ProjectileUtils.CanExplosionKillTile(i, j))
            {
                projectile.penetrate--;
                if (projectile.penetrate <= 0) projectile.Kill();
                WorldGen.KillTile(i, j, false, false, false);
                if (!Main.tile[i, j].active() && Main.netMode != 0)
                {
                    NetMessage.SendData(17, -1, -1, null, 0, (float)i, (float)j, 0f, 0, 0, 0);
                }
            }
        }
    }
}