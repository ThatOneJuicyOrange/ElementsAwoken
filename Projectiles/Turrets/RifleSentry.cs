using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.ID;

namespace ElementsAwoken.Projectiles.Turrets
{
    public class RifleSentry : TurretBase
    {
        public int energyDrainCD = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(energyDrainCD);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            energyDrainCD = reader.ReadInt32();
        }
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 28;

            projectile.timeLeft = Projectile.SentryLifeTime;
            projectile.timeLeft *= 10;

            projectile.manualDirectionChange = true;
            //projectile.hide = true;
            projectile.netImportant = true;
            projectile.sentry = true;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;

            projectile.penetrate = -1;

            shootCDAmount = 60;
            maxRange = 900f;
            baseTex = "Projectiles/Turrets/RifleSentryBase";
        }

        public override void Shoot(NPC target)
        {
            Vector2 shootVel = target.Center - projectile.Center;
            if (shootVel == Vector2.Zero) shootVel = new Vector2(0f, 1f);
            shootVel.Normalize();
            shootVel *= 7f;

            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 11);
            Projectile.NewProjectile(projectile.Center - new Vector2(0,14), shootVel, ProjectileID.Bullet, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
        }
        public override void ExtraAI()
        {
            Player player = Main.player[projectile.owner];
            PlayerEnergy energyPlayer = player.GetModPlayer<PlayerEnergy>();
            energyDrainCD--;
            if (energyPlayer.energy < 1)
            {
                projectile.ai[0] = 0;
                projectile.rotation = 0;
                projectile.spriteDirection = 1;
            }
            else if (energyDrainCD <= 0)
            {
                energyPlayer.energy--;
                energyDrainCD = 120;
            }
        }
    }
}
    