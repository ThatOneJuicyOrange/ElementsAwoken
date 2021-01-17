using Microsoft.Xna.Framework;
using System;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.NPCs.MovingPlatforms
{
    public class LavaPlatform : MovingPlatformBase
    {
        public override void SetPlatformDefaults()
        {
            npc.width = 54;
            npc.height = 52;

            npc.direction = Main.rand.NextBool() ? -1 : 1;
            npc.GivenName = " ";
            npc.spriteDirection = Main.rand.NextBool() ? -1 : 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lava Platform");
        }

        public override void PlatformAI(bool hasPlayerOn)
        {
            if (npc.ai[2] == 0) npc.ai[2] = Main.rand.NextFloat(0.2f, 2);
            npc.velocity.X = npc.direction * npc.ai[2];
            bool pointLavaWet = Collision.LavaCollision(npc.position, npc.width, 26); // any less than 20 will result in the player snapping to lava with water walking boots
            if (hasPlayerOn)
            {
                if (npc.ai[1] == 0) npc.velocity.Y += 1.7f;
                npc.ai[1] = 1;
            }
            else npc.ai[1] = 0;
            if (pointLavaWet)
            {
                npc.ai[0] = 0;
                npc.velocity.Y -= 0.16f;
                if (npc.velocity.Y > 0) npc.velocity.Y *= 0.95f;
                npc.rotation = npc.rotation.AngleLerp(0, 0.05f);

                if ((double)npc.velocity.Y > 0.4 || (double)npc.velocity.Y < -0.4)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.95f;
                }
            }
            else if (npc.lavaWet)
            {
                npc.ai[0] = 1;
                npc.rotation = npc.rotation.AngleLerp(0, 0.05f);
                npc.velocity.Y *= 0.95f;

                if ((double)npc.velocity.Y > 0.4 || (double)npc.velocity.Y < -0.4)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.95f;
                }
            }
            else
            {
                npc.ai[0] = 2;
                npc.velocity.Y += 0.16f;
                if (npc.velocity.Y < 0) npc.velocity.Y *= 0.95f;
                npc.rotation = npc.velocity.Y * 0.2f * Math.Sign(npc.velocity.X);
            }
            if (npc.collideX)
            {
                npc.velocity.X *= -1f;
                npc.direction *= -1;
                npc.netUpdate = true;
            }
        }
        private bool WillCollide()
        {
            for (int j = 0; j < npc.height / 16; j++)
            {
                int velDir = Math.Sign(npc.velocity.X);
                for (int x = 0; x < 3; x++)
                { 
                    Dust ash = Main.dust[Dust.NewDust(new Vector2((int)(npc.Center.X + npc.width / 2 * velDir + velDir * 16), (int)npc.position.Y + j * 16), 16, 16, 6)];
                    ash.velocity = Vector2.Zero;
                }
                Tile t = Framing.GetTileSafely((int)(npc.Center.X + npc.width / 2 * velDir + velDir * 16) / 16, (int)npc.position.Y / 16 + j);
                if (t.active() && Main.tileSolid[t.type]) return true;
            }
            return false;
        }
    }
}