using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.ToySlime
{
    public class LegoBrick : ModProjectile
    {
        public int type = 0;
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 16;
            projectile.aiStyle = 0;
            projectile.timeLeft = 600;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.magic = true;
            projectile.penetrate = 3;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lego Brick");
            Main.projFrames[projectile.type] = 4;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frame = type;
            return true;
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                type = Main.rand.Next(0, 3);
                projectile.localAI[0] = 1;
            }
            projectile.velocity.Y = projectile.velocity.Y + 0.15f;
            try
            {
                int num187 = (int)(projectile.position.X / 16f) - 1;
                int num188 = (int)((projectile.position.X + (float)projectile.width) / 16f) + 2;
                int num189 = (int)(projectile.position.Y / 16f) - 1;
                int num190 = (int)((projectile.position.Y + (float)projectile.height) / 16f) + 2;
                if (num187 < 0)
                {
                    num187 = 0;
                }
                if (num188 > Main.maxTilesX)
                {
                    num188 = Main.maxTilesX;
                }
                if (num189 < 0)
                {
                    num189 = 0;
                }
                if (num190 > Main.maxTilesY)
                {
                    num190 = Main.maxTilesY;
                }
                int num3;
                for (int num191 = num187; num191 < num188; num191 = num3 + 1)
                {
                    for (int num192 = num189; num192 < num190; num192 = num3 + 1)
                    {
                        if (Main.tile[num191, num192] != null && Main.tile[num191, num192].nactive() && (Main.tileSolid[(int)Main.tile[num191, num192].type] || (Main.tileSolidTop[(int)Main.tile[num191, num192].type] && Main.tile[num191, num192].frameY == 0)))
                        {
                            Vector2 vector18;
                            vector18.X = (float)(num191 * 16);
                            vector18.Y = (float)(num192 * 16);
                            if (projectile.position.X + (float)projectile.width > vector18.X && projectile.position.X < vector18.X + 16f && projectile.position.Y + (float)projectile.height > vector18.Y && projectile.position.Y < vector18.Y + 16f)
                            {
                                projectile.velocity.X = 0f;
                                projectile.velocity.Y = -0.2f;
                            }
                        }
                        num3 = num192;
                    }
                    num3 = num191;
                }
            }
            catch
            {
            }
        }
    }
}