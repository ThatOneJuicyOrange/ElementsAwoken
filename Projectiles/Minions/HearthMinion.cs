using System;
using ElementsAwoken.Buffs.MinionBuffs;
using ElementsAwoken.Projectiles.Minions.MinionProj;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles.Minions
{   
    public class HearthMinion : MinionINFO
    {
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 34;

            projectile.minionSlots = 2;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.tileCollide = false;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.minion = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

            inertia = 30f;
            shoot = ProjectileType<HearthBeam>();
            shootSpeed = 14f;
            shootCool = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hearth");
            Main.projFrames[projectile.type] = 4;
        }
        public override void CheckActive()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(BuffType<HearthMinionBuff>(), 3600);
            if (player.dead)  modPlayer.hearthMinion = false;
            if (modPlayer.hearthMinion)  projectile.timeLeft = 2;
        }

        public override void CreateDust()
        {
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.6f, 0.9f, 0.3f);
        }
        public override void ShootExtraAction()
        {
            Main.PlaySound(SoundID.Item20, projectile.position);
        }
        public override void SelectFrame()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 3;
            }
        }
    }
}