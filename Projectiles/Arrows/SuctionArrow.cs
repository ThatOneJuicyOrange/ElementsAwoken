using ElementsAwoken.Buffs.Debuffs;
using ElementsAwoken.NPCs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles.Arrows
{
    public class SuctionArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;

            projectile.timeLeft = 400;

            projectile.penetrate = -1;

            projectile.arrow = true;
            projectile.friendly = true;
            projectile.ranged = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Suction Arrow");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.ai[1] != 0)
            {
                NPC stick = Main.npc[(int)projectile.ai[0]];
                if (stick.active)
                {
                    projectile.Center = stick.Center - projectile.velocity * 2f;
                    projectile.gfxOffY = stick.gfxOffY;
                    projectile.localAI[0]++;
                    //stick.AddBuff(BuffType<VariableLifeRegen>(), 20);
                    //stick.GetGlobalNPC<NPCsGLOBAL>().lifeDrainAmount = 5;
                }
                else projectile.Kill();
            }
            else
            {
                projectile.velocity.Y += 0.13f;
            }
        }
        private void DeleteOldest(NPC target)
        {
            int lowestTimeLeftID = projectile.whoAmI;
            int numStuck = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile currentProjectile = Main.projectile[i];
                if (i != projectile.whoAmI 
                    && currentProjectile.active 
                    && currentProjectile.owner == Main.myPlayer 
                    && currentProjectile.type == projectile.type 
                    && currentProjectile.ai[1] == 1 
                    && currentProjectile.ai[0] == target.whoAmI)
                {
                    if (currentProjectile.timeLeft < Main.projectile[lowestTimeLeftID].timeLeft) lowestTimeLeftID = currentProjectile.whoAmI;
                    numStuck++;
                    if (numStuck > 3)
                        break;
                }
            }
            if (numStuck > 3) Main.projectile[lowestTimeLeftID].Kill();
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[1] == 1 && projectile.localAI[0] % 60 != 0) return false;
            return base.CanHitNPC(target);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[1] == 0)
            {
                projectile.ai[0] = target.whoAmI;
                projectile.ai[1] = 1;
                projectile.velocity = (target.Center - projectile.Center) * 0.75f;
                projectile.netUpdate = true;
                DeleteOldest(target);
            }
        }
    }
}