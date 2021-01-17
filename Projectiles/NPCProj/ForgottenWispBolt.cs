using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj
{
    public class ForgottenWispBolt : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;

            projectile.aiStyle = -1;
            projectile.alpha = 255;
            projectile.timeLeft = 45;

            projectile.hostile = true;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = false;

            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wisp Bolt");
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if(projectile.ai[1] != 1) player.AddBuff(BuffID.OnFire, 200);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (projectile.ai[1] == 1 ? 45 : 30) + target.defense;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if ((projectile.ai[1] == 0 && !target.GetGlobalNPC<NPCs.VolcanicPlateau.PlateauNPCs>().voidBroken) ||
                (projectile.ai[1] == 1 && target.GetGlobalNPC<NPCs.VolcanicPlateau.PlateauNPCs>().voidBroken)) return false;
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 3.14f;
            if (projectile.localAI[0] == 0) projectile.localAI[0] = 45;
            projectile.velocity *= 1.01f;
            projectile.ai[0]++;
            int dustType = projectile.ai[1] == 1 ? DustID.PinkFlame : 6;
            Dust dust = Main.dust[Dust.NewDust(projectile.position + new Vector2(0, (float)Math.Sin(projectile.ai[0] / 5) * 5), projectile.width, projectile.height, dustType, Scale:1.5f)];
            dust.velocity *= 0.1f;
            dust.noGravity = true;
            for (int l = -1; l < 2; l += 2)
            {
                Vector2 vel = new Vector2(Main.rand.NextFloat(0.1f, 0.3f), l * Main.rand.NextFloat(2.6f, 4f) * (1 - projectile.ai[0] / projectile.localAI[0])).RotatedBy((double)projectile.rotation, default(Vector2));
                Dust dust2 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, Scale:1.5f)];
                dust2.velocity = vel;
                dust2.noGravity = true;
            }
        }
    }
}