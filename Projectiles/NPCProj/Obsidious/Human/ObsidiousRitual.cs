using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Obsidious.Human
{
    public class ObsidiousRitual : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 108;
            projectile.height = 108;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.hostile = true;
            projectile.timeLeft = 100000;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious");
        }

        public override void AI()
        {
            NPC npc = Main.npc[(int)projectile.ai[1]];
            if (npc != Main.npc[0])
            {
                projectile.Center = npc.Center;
            }
            if (!npc.active)
            {
                projectile.Kill();
            }
            else
            {
                projectile.timeLeft = 100000;
            }
            projectile.rotation += 0.1f;
            projectile.alpha = npc.alpha;
        }

    }
}
