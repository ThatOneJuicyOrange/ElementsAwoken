using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Projectiles.NPCProj
{
    public class ToadTongue : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;

            projectile.tileCollide = false;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Giant Toad");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.ai[0] <= 0f)
            {
                projectile.alpha -= 200;
                if (projectile.alpha <= 0)
                {
                    projectile.alpha = 0;
                    projectile.ai[0] = 1f;
                    if (projectile.ai[1] == 0f)
                    {
                        projectile.ai[1] += 1f;
                        projectile.position += projectile.velocity * 1f;
                    }
                    if (Main.myPlayer == projectile.owner)
                    {
                        int num47 = projectile.type;
                        float mult = 1;
                        if (projectile.ai[1] >= 30f + Main.rand.Next(0,6))
                        {
                            num47 = ProjectileType<ToadTongueTip>();
                            mult = 1.2f;
                        }
                        int num48 = Projectile.NewProjectile(projectile.Center.X + projectile.velocity.X * mult * (projectile.scale * 0.98f), projectile.Center.Y + projectile.velocity.Y * mult * (projectile.scale * 0.98f), projectile.velocity.X, projectile.velocity.Y, num47, projectile.damage, projectile.knockBack, projectile.owner, 0f, projectile.ai[1] + 1f);
                        NetMessage.SendData(27, -1, -1, null, num48, 0f, 0f, 0f, 0, 0, 0);
                        Main.projectile[num48].localAI[1] = projectile.localAI[1];
                        Main.projectile[num48].localAI[0] = projectile.localAI[0] - 2;
                        Main.projectile[num48].scale= projectile.scale * 0.98f;
                        return;
                    }
                }
            }
            else
            {
                projectile.ai[0]++;
                if (projectile.ai[0] > projectile.localAI[0])
                {
                    projectile.alpha += 60;
                    if (projectile.alpha >= 255)
                    {
                        projectile.Kill();
                        return;
                    }
                }
            }
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600);
            target.AddBuff(BuffID.Venom, 300);
        }
    }
}