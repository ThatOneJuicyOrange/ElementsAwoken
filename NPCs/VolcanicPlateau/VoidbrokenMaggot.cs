using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace ElementsAwoken.NPCs.VolcanicPlateau
{
    public class VoidbrokenMaggot : ModNPC
    {
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCType<CharredMaggot>());
            npc.lifeMax = (int)(npc.lifeMax * 1.5f);
            npc.defense = (int)(npc.defense * 1.5f);
            npc.damage = (int)(npc.damage * 1.5f);

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "Voidbroken maggots are emberfly larvae corrupted by the Void. They are composed of a soft fleshy substance resembling gelatine known as Obliveite. and are encased in a shell made out of Voidite, a combination of multiple different minerals corrupted by the Void Leviathan as it burrowed through the planet.";
            npc.GetGlobalNPC<PlateauNPCs>().voidBroken = true;
            npc.GetGlobalNPC<PlateauNPCs>().counterpart = NPCType<CharredMaggot>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Voidbroken Maggot");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax /= 2;
            npc.damage /= 2;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.ai[2] = frameHeight;
            npc.spriteDirection = npc.direction;
            npc.frameCounter += 1;
            if (npc.frameCounter > 15)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 2) npc.frame.Y = 0;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[1] > 0)
            {
                Texture2D texture = mod.GetTexture("NPCs/VolcanicPlateau/" + GetType().Name + "_Glow");
                Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
                Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
                SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                Color color = Color.White * (npc.ai[1] / 60);
                spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, 4), frame, color, npc.rotation, origin, npc.scale, effects, 0.0f);
            }
        }
        public override void NPCLoot()
        {
            // Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Drakonite"), Main.rand.Next(1, 3));
        }

        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (Main.rand.NextBool(5))
            {
                damage = 0;
                knockback = 0;
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 37, 1, -0.4f);
                Main.PlaySound(SoundID.DD2_FlameburstTowerShot,npc.position);
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 103, 1, -0.5f);
                npc.ai[1] = 60;
                int dir = Math.Sign(npc.Center.X - player.Center.X);
                int dirY = Math.Sign(npc.Center.Y - player.Center.Y);
                player.velocity.X -= dir * 12;
                player.velocity.Y -= dirY * 6f;
                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " got consumed by the void"), npc.damage / 3, 0);
                for (int l = 0; l < 30; l++)
                {
                    Dust dust = Main.dust[Dust.NewDust(new Vector2(npc.Top.X + (npc.width / 2) * npc.direction, npc.Top.Y), 2, npc.height, DustID.PinkFlame)];
                    dust.noGravity = true;
                    dust.scale *= 1.6f;
                    dust.velocity.X = -dir * Main.rand.NextFloat(8, 15);
                }
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.rand.NextBool(3))
            {
                damage = 0;
                knockback = 0;
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 37, 1, -0.4f);
                Main.PlaySound(SoundID.DD2_FlameburstTowerShot, npc.position);
                npc.ai[1] = 60;
            }
        }
        public override void AI()
        {
            CharredMaggot.MaggotAI(npc, 3f);
            if (Main.rand.NextBool(3) && npc.velocity.Y == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(npc.BottomLeft, npc.width, 2, DustID.PinkFlame)];
                dust.velocity *= 0.2f;
                dust.scale *= 0.5f;
                dust.noGravity = true;
                dust.fadeIn = 2f;
            }
        }
    }
}
