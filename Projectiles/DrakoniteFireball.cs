using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class DrakoniteFireball : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;

            projectile.friendly = true;
            projectile.magic = true;
            projectile.ignoreWater = false;

            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.alpha = 0;
            projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drakonite Fireball");
        }
        public override void AI()
        {
            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Fire, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
            Main.dust[dust].velocity *= 0.2f;
            Main.dust[dust].scale *= 0.5f;
            Main.dust[dust].noGravity = true;
            projectile.ai[0]++;
            if (projectile.ai[0] > 20) projectile.velocity.Y += 0.07f;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            /* Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.position.X, projectile.position.Y, Main.rand.NextFloat(-3f,3f), Main.rand.NextFloat(-3f, 3f), mod.ProjectileType("GreekFire"), (int)(projectile.damage * 0.5f), projectile.knockBack * 0.35f, Main.myPlayer, 0f, 0f)];
             proj.timeLeft = 120;
             proj.magic = true;*/        
          Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("DrakoniteEruption"), (int)(projectile.damage * 0.5f), 0, Main.myPlayer, 0f, 0f);
        }
    }
}