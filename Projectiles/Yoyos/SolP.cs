using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ElementsAwoken.Projectiles.Yoyos
{
    public class SolP : ModProjectile
    {
        public bool hasOrbital = false;
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;

            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;

            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 480f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 20f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jupiter");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.9f, 0.3f, 0.6f);

            int maxDist = 200;
            float gravStength = 0.1f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                bool immune = false;
                foreach (int k in ElementsAwoken.instakillImmune)
                {
                    if (npc.type == k)
                    {
                        immune = true;
                    }
                }
                if (!immune && npc.CanBeChasedBy(this) && !npc.boss && npc.lifeMax < 10000 && Vector2.Distance(npc.Center, projectile.Center) < maxDist)
                {
                    Vector2 toTarget = new Vector2(projectile.Center.X - npc.Center.X, projectile.Center.Y - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity.X += toTarget.X * gravStength;
                    npc.velocity.Y += toTarget.Y * gravStength * 5;
                }
            }
            for (int i = 0; i < Main.maxItems; i++)
            {
                Item item = Main.item[i];
                if (item.active && Vector2.Distance(item.Center, projectile.Center) < maxDist)
                {
                    Vector2 toTarget = new Vector2(projectile.Center.X - item.Center.X, projectile.Center.Y - item.Center.Y);
                    toTarget.Normalize();
                    item.velocity += toTarget * gravStength;
                }
            }

            if (!hasOrbital && projectile.localAI[0] >= 20) // 0 is a timer
            {
                int swirlCount = 17;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 360 / swirlCount;
                    int orbital = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("SolOrbit"), projectile.damage, projectile.knockBack, 0, l * distance, projectile.whoAmI);
                    Projectile Orbital = Main.projectile[orbital];
                }
                hasOrbital = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
    }
}