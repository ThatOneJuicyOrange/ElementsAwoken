using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class AncientStar : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Starfury);
            aiType = ProjectileID.Starfury;
            projectile.scale *= 1.5f;
            projectile.damage *= 2;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Star");
        }
        public override bool PreKill(int timeLeft)
        {
            projectile.type = ProjectileID.Starfury;
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 2; i++)
            {
                int explode = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 16f, Main.rand.Next(-10, 11) * .25f, Main.rand.Next(-10, -5) * .25f, mod.ProjectileType("AncientStar2"), (int)(projectile.damage * .5f), 0, projectile.owner);
                Main.projectile[explode].aiStyle = 1;
                Main.projectile[explode].scale = 1f;
                Main.projectile[explode].tileCollide = true;
            }
            return true;
        }
    }
}