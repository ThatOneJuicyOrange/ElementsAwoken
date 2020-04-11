using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.Ancients.Gores
{
    public class KrecheusShard : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 30;

            projectile.timeLeft = 100000;
            projectile.scale *= 1.3f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
            Main.projFrames[projectile.type] = 2;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frame = (int)projectile.ai[0];
            return true;
        }
        public override void AI()
        {
            projectile.rotation += 0.1f;

            NPC parent = null;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].type == mod.NPCType("ShardBase"))
                {
                    parent = Main.npc[i];
                }
            }
            if (parent != null)
            {
                float movespeed = 5f;
                if (Vector2.Distance(parent.Center, projectile.Center) >= 200)
                {
                    movespeed = 12f;
                }
                Vector2 toTarget = new Vector2(parent.Center.X - projectile.Center.X, parent.Center.Y - projectile.Center.Y);
                toTarget = new Vector2(parent.Center.X - projectile.Center.X, parent.Center.Y - projectile.Center.Y);
                toTarget.Normalize();
                if (Vector2.Distance(parent.Center, projectile.Center) >= 40)
                {
                    projectile.velocity = toTarget * movespeed;
                }

                ProjectileUtils.PushOtherEntities(projectile, new List<int>() { mod.ProjectileType("KirveinShard"), mod.ProjectileType("XernonShard"), mod.ProjectileType("IzarisShard") });

            }
            else if (projectile.localAI[0] == 0)
            {
                projectile.localAI[0]++;
                ElementsAwoken.DebugModeText("Error: Shard Base not found");
            }
        }
    }
}