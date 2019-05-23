using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Ancients.Gores
{
    public class Lightbeam : ModProjectile
    {
        public float rotSpeed = 0.2f;
        public override void SetDefaults()
        {
            projectile.width = 600;
            projectile.height = 600;

            projectile.timeLeft = 100000;

            projectile.light = 3;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                projectile.rotation = Main.rand.NextFloat(0, 5);
                rotSpeed = Main.rand.NextFloat(-0.04f, 0.04f);
                projectile.scale = Main.rand.NextFloat(0.5f, 1.2f);
                projectile.alpha = Main.rand.Next(150, 220);

                projectile.localAI[0]++;
            }
            projectile.rotation += rotSpeed;

            if (!NPC.AnyNPCs(mod.NPCType("AncientAmalgamDeath")))
            {
                projectile.Kill();
            }
        }
    }
}