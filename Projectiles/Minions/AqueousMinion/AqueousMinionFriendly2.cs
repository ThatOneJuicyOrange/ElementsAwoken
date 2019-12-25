using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Minions.AqueousMinion
{
    public class AqueousMinionFriendly2 : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.aiStyle = 66;
            projectile.minionSlots = 0f;
            projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            aiType = 66;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aqueous Minion");
        }
        public override void AI()
        {
            projectile.position.X = Main.player[projectile.owner].Center.X + - 100;
            projectile.position.Y = Main.player[projectile.owner].Center.Y - 50;
            //projectile.rotation += projectile.velocity.X * 0.04f;
            bool flag64 = projectile.type == mod.ProjectileType("AqueousMinionFriendly2");
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            //player.AddBuff(mod.BuffType("AqueousMinions"), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.aqueousMinions = false;
                }
                if (modPlayer.aqueousMinions)
                {
                    projectile.timeLeft = 2;
                }
            }
            if (player.FindBuffIndex(mod.BuffType("AqueousMinions")) == -1)
            {
                projectile.Kill();
            }
            if (projectile.owner == Main.myPlayer)
            {
                if (projectile.ai[0] != 0f)
                {
                    projectile.ai[0] -= 1f;
                    return;
                }
                float num396 = projectile.position.X;
                float num397 = projectile.position.Y;
                float num398 = 700f;
                bool flag11 = false;
                for (int nPC = 0; nPC < 200; nPC++)
                {
                    if (Main.npc[nPC].CanBeChasedBy(projectile, true))
                    {
                        float num400 = Main.npc[nPC].position.X + (float)(Main.npc[nPC].width / 2);
                        float num401 = Main.npc[nPC].position.Y + (float)(Main.npc[nPC].height / 2);
                        float num402 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num400) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num401);
                        if (num402 < num398 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[nPC].position, Main.npc[nPC].width, Main.npc[nPC].height))
                        {
                            num398 = num402;
                            num396 = num400;
                            num397 = num401;
                            flag11 = true;
                        }
                        Vector2 direction = Main.npc[nPC].Center - projectile.Center;
                        projectile.rotation = direction.ToRotation();
                    }
                }
                if (flag11)
                {
                    float num403 = 30f; //modify the speed the projectile are shot.  Lower number = slower projectile.
                    Vector2 vector29 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                    float num404 = num396 - vector29.X;
                    float num405 = num397 - vector29.Y;
                    float num406 = (float)Math.Sqrt((double)(num404 * num404 + num405 * num405));
                    num406 = num403 / num406;
                    num404 *= num406;
                    num405 *= num406;
                    Projectile.NewProjectile(projectile.Center.X - 4f, projectile.Center.Y, num404, num405, mod.ProjectileType("WaterballFriendly"), 60, projectile.knockBack, projectile.owner, 0f, 0f);
                    projectile.ai[0] = 50f;
                    return;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.penetrate == 0)
            {
                projectile.Kill();
            }
            return false;
        }
    }
}