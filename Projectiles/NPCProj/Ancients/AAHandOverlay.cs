using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.Ancients
{
    public class AAHandOverlay : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 58;
            projectile.height = 72;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 100000;
            projectile.scale *= 1.3f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Amalgamate");
        }
        public override void AI()
        {
            NPC npc = Main.npc[(int)projectile.ai[1]];
            Vector2 offset = new Vector2(30, 0);
            if (npc != Main.npc[0])
            {
                projectile.rotation = npc.rotation;
                projectile.direction = npc.direction;
                projectile.spriteDirection = -npc.spriteDirection;
                projectile.alpha = (int)npc.ai[2];

                projectile.position.X = npc.position.X;
                projectile.position.Y = npc.position.Y;
            }
            if (!npc.active)
            {
                projectile.Kill();
            }
        }
    }
}
    