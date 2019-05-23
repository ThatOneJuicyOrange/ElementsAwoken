using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Minions
{
    public class ScorpionMinion : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.OneEyedPirate);
            aiType = ProjectileID.OneEyedPirate;
            projectile.width = 26;
            projectile.height = 26;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.minionSlots = 1;
            projectile.timeLeft = 18000;
            Main.projFrames[projectile.type] = 15;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            //aiType = 393;
            //projectile.tileCollide = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scorpion");
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
            bool flag64 = projectile.type == mod.ProjectileType("ScorpionMinion");
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            player.AddBuff(mod.BuffType("ScorpionMinionBuff"), 3600);
            if (player.dead)
            {
                modPlayer.scorpionMinion = false;
            }
            if (modPlayer.scorpionMinion)
            {
                projectile.timeLeft = 2;
            }

        }
    }
}