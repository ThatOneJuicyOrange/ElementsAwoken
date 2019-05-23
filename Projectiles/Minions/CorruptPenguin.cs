using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.Minions
{
    public class CorruptPenguin : ModProjectile
    {

        public override void SetDefaults()
        {
            /* projectile.width = 26;
             projectile.height = 26;

             projectile.netImportant = true;
             projectile.friendly = true;
             ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
             projectile.minion = true;
             projectile.tileCollide = false;

             projectile.minionSlots = 1;
             projectile.alpha = 75;
             projectile.aiStyle = 26;
             projectile.timeLeft = 18000;
             Main.projFrames[projectile.type] = 6;
             projectile.penetrate = -1;
             projectile.timeLeft *= 5;
             aiType = 266;*/


            projectile.netImportant = true;
            projectile.minion = true;

            projectile.width = 24;
            projectile.height = 42;

            projectile.aiStyle = 26;
            aiType = 266;

            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minionSlots = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupt Penguin");
            Main.projFrames[projectile.type] = 6;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            if (projectile.velocity.X == 0)
            {
                projectile.frame = 0;
            }
            return true;
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
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            player.AddBuff(mod.BuffType("CorruptPenguinBuff"), 3600);
            if (player.dead)
            {
                modPlayer.corruptPenguin = false;
            }
            if (modPlayer.corruptPenguin)
            {
                projectile.timeLeft = 2;
            }

        }
    }
}