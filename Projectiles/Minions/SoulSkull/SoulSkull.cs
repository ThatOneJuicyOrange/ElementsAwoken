using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Minions.SoulSkull
{
    public class SoulSkull : SoulSkullINFO
    {
        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            Main.projPet[projectile.type] = true;
            projectile.minion = true;
            projectile.netImportant = true;
            projectile.minionSlots = 1;
            projectile.penetrate = 1;
            projectile.timeLeft = 18000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            inertia = 30f;
            shoot = mod.ProjectileType("Soulflames");
            shootSpeed = 18f;
            shootCool = 22f;
            ProjectileID.Sets.LightPet[projectile.type] = true;
            Main.projPet[projectile.type] = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Skull");
        }
        public override void CheckActive()
        {
            bool flag64 = projectile.type == mod.ProjectileType("SoulSkull");
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(mod.BuffType("SoulSkull"), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.soulSkull = false;
                }
                if (modPlayer.soulSkull)
                {
                    projectile.timeLeft = 2;
                }
            }
            if (projectile.direction == -1)
            {
                int num154 = Dust.NewDust(new Vector2(projectile.Center.X - 6, projectile.Center.Y + 4), 6, 6, 173, Main.rand.Next(2, 10), -15, 100, default(Color), 1f);
                Main.dust[num154].velocity *= 0.6f;
                Main.dust[num154].scale *= 1.4f;
                Main.dust[num154].noGravity = true;
            }
            if (projectile.direction == 1)
            {
                int num154 = Dust.NewDust(new Vector2(projectile.Center.X + 14, projectile.Center.Y + 4), 6, 6, 173, -Main.rand.Next(2, 10), -15, 100, default(Color), 1f);
                Main.dust[num154].velocity *= 0.6f;
                Main.dust[num154].scale *= 1.4f;
                Main.dust[num154].noGravity = true;
            }
        }

        public override void CreateDust()
        {
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.6f, 0.9f, 0.3f);
        }
    }
}