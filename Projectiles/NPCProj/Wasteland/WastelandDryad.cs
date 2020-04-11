using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles.NPCProj.Wasteland
{
    public class WastelandDryad : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 52;

            projectile.penetrate = 1;
            projectile.timeLeft = 12000;

            projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wasteland Dryad");
            Main.projFrames[projectile.type] = 2;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            if (projectile.ai[0] >= 360) projectile.frame = 1;
            else projectile.frame = 0;
            return true;
        }
        public override void AI()
        {
            projectile.ai[1]++;
            int wastelandID = NPC.FindFirstNPC(NPCType<NPCs.Bosses.Wasteland.Wasteland>());
            Vector2 desiredLoc = projectile.Center;
            if (wastelandID >= 0)
            {
                NPC parent = Main.npc[wastelandID];
                desiredLoc = parent.Center + new Vector2(200, -200 + (float)Math.Sin(projectile.ai[1] / 20) * 20f);
            }
            if (!NPC.AnyNPCs(NPCType<NPCs.Bosses.Wasteland.Wasteland>()))
            {
                projectile.ai[0] = 0;
                NPC dryad = Main.npc[NPC.FindFirstNPC(NPCID.Dryad)];
                desiredLoc = dryad.Center;
                Vector2 toTarget = new Vector2(desiredLoc.X - projectile.Center.X, desiredLoc.Y - projectile.Center.Y);
                toTarget.Normalize();
                projectile.velocity = toTarget * 9;
                if (Vector2.Distance(desiredLoc, projectile.Center) < 8)
                {
                    projectile.Kill();
                    dryad.alpha = 0;
                }
            }
            else
            {
                NPC dryad = Main.npc[NPC.FindFirstNPC(NPCID.Dryad)];
                if (projectile.ai[0] == 0)
                {
                    Vector2 toTarget = new Vector2(desiredLoc.X - projectile.Center.X, desiredLoc.Y - projectile.Center.Y);
                    toTarget.Normalize();
                    projectile.velocity = toTarget * 9;
                    if (Vector2.Distance(desiredLoc, projectile.Center) < 8) projectile.ai[0] = 1;
                }
                else
                {
                    NPC parent = Main.npc[wastelandID];
                    projectile.ai[0]++;
                    projectile.Center = desiredLoc;
                    if (projectile.ai[0] == 90) CombatText.NewText(projectile.getRect(), Color.GreenYellow, "Wait! Stop!", false, false);
                    else if (projectile.ai[0] == 180) CombatText.NewText(projectile.getRect(), Color.GreenYellow, "I can handle this!", false, false);
                    else if (projectile.ai[0] >= 360)
                    {
                        Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 75)];
                        Vector2 toTarget = new Vector2(parent.Center.X - projectile.Center.X, parent.Center.Y - projectile.Center.Y);
                        toTarget.Normalize();
                        dust.velocity = toTarget * 24f;
                        dust.noGravity = true;
                        dust.fadeIn = 1.2f;

                        Dust dust2 = Main.dust[Dust.NewDust(parent.position, parent.width, parent.height, 75)];
                        dust2.noGravity = true;
                        dust2.fadeIn = 0.8f;
                        dust2.scale *= 2f;
                    }
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            ProjectileUtils.OutwardsCircleDust(projectile, 75, 36, 5f);
        }
    }
}