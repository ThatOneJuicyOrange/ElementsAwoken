using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheGuardian
{
    public class GuardianCircle : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 408;
            projectile.height = 408;

            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.alpha = 60;
            projectile.penetrate = -1;
            projectile.timeLeft = 100000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian");
        }
        public override void AI()
        {
            if (!NPC.AnyNPCs(mod.NPCType("TheGuardianFly")))
            {
                projectile.Kill();
            }
            //NPC parent = Main.npc[NPC.FindFirstNPC(mod.NPCType("TheGuardianFly"))];
            NPC parent = Main.npc[(int)projectile.ai[1]];

            projectile.Center = parent.Center;

            projectile.rotation += 0.05f;
        }
    }
}