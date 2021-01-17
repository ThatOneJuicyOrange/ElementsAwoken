using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles.NPCProj.TheKeeper
{
    public class KeeperBone : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 24;

            projectile.hostile = true;

            projectile.penetrate = 1;
            projectile.timeLeft = 180;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Keeper Bone");
        }
        public override void AI()
        {
            projectile.rotation += projectile.velocity.X * 0.05f;
            projectile.velocity.Y += 0.16f;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int i = 0; i < 2; i++)
            {
                Gore.NewGore(projectile.position, projectile.velocity, mod.GetGoreSlot("Gores/" + GetType().Name + i), projectile.scale);
            }
        }
    }
}