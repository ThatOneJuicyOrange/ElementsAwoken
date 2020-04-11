using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class ArchaicProtection : ModProjectile
    {
        float circleScale = 0f;
        float circleAlpha = 0.7f;
        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 46;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.alpha = 60;
            projectile.penetrate = -1;
            projectile.timeLeft = 2000;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Archaic Protection");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            projectile.Center = player.Center - new Vector2(0, 20);
            if (modPlayer.archaicProtectionTimer <= 0) projectile.Kill();

            Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, GetDustID())];
            dust.noGravity = true;
            dust.velocity *= 1.5f;


            if (projectile.localAI[0] == 0)
            {
                DustBoom();
                Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ChargeShort"), 1f);
                projectile.localAI[0]++;
            }

            projectile.ai[0]++;
            int duration = 15;
            int num = 35;
            if (projectile.ai[0] == num) Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/Explosion"), 0.8f, -0.1f);
            if (projectile.ai[0] > num)
            {
                circleScale = ((projectile.ai[0] - num) / duration) * 0.6f;
                if (projectile.ai[0] > num + duration / 2) circleAlpha = MathHelper.Lerp(0.7f, 0f, (projectile.ai[0] - (num + duration /2)) / (duration / 2));
            }
            if (projectile.ai[0] > num + duration)
            {
                projectile.ai[0] = 0;
                circleScale = 0f;
                circleAlpha = 0.7f;
                Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ChargeShort"), 1f);
            }

            int maxDist = 300;
            // calculated from the players feet so projectiles that are on the ground get pushed away instead of into the ground
            if (projectile.ai[0] == num + duration / 2)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && npc.damage > 0 && !npc.boss && Vector2.Distance(npc.Center, player.Center) < maxDist)
                    {
                        Vector2 toTarget = new Vector2(player.Bottom.X - npc.Center.X, player.Bottom.Y - npc.Center.Y);
                        toTarget.Normalize();
                        npc.velocity -= toTarget * 25 * npc.knockBackResist;
                    }
                }
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.active && proj.hostile && Vector2.Distance(proj.Center, player.Center) < maxDist)
                    {
                        Vector2 toTarget = new Vector2(player.Bottom.X - proj.Center.X, player.Bottom.Y - proj.Center.Y);
                        toTarget.Normalize();
                        proj.velocity -= toTarget * 10;
                    }
                }
                for (int i = 0; i < Main.maxItems; i++)
                {
                    Item item = Main.item[i];
                    if (item.active && Vector2.Distance(item.Center, player.Center) < maxDist)
                    {
                        Vector2 toTarget = new Vector2(player.Bottom.X - item.Center.X, player.Bottom.Y - item.Center.Y);
                        toTarget.Normalize();
                        item.velocity -= toTarget * 10;
                    }
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            DustBoom();
        }
        private void DustBoom()
        {
            for (int i = 0; i < 31; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, GetDustID())];
                dust.noGravity = true;
                dust.velocity *= 1.5f;
            }
        }
        private int GetDustID()
        {
            switch (Main.rand.Next(4))
            {
                case 0:
                    return mod.DustType("AncientRed");
                case 1:
                    return mod.DustType("AncientGreen");
                case 2:
                    return mod.DustType("AncientBlue");
                case 3:
                    return mod.DustType("AncientPink");
                default:
                    return mod.DustType("AncientRed");
            }
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Texture2D tex = ModContent.GetTexture("ElementsAwoken/Extra/Circle");
            sb.Draw(tex, projectile.Center - Main.screenPosition - (tex.Size() * circleScale) / 2, null, Color.White * circleAlpha, projectile.rotation, Vector2.Zero, circleScale, SpriteEffects.None, 0f);

            return true;
        }
    }
}