using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Minions
{
    public class MiniDragon : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 38;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.minionSlots = 1;
            projectile.timeLeft = 18000;
            Main.projFrames[projectile.type] = 6;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;       
            projectile.aiStyle = 62;
            aiType = 375;
            projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mini Dragon");
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.penetrate == 0)
            {
                projectile.Kill();
            }
            return false;
        }
        public override void AI()
        {
            bool flag64 = projectile.type == mod.ProjectileType("MiniDragon");
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            player.AddBuff(mod.BuffType("MiniDragonBuff"), 3600);
            if (player.dead)
            {
                modPlayer.miniDragon = false;
            }
            if (modPlayer.miniDragon)
            {
                projectile.timeLeft = 2;
            }

        }
    }
}