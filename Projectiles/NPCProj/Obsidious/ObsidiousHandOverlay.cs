using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousHandOverlay : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 52;
            projectile.height = 76;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 100000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious");
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
    