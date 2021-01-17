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
    public class CharredMaggot : ModNPC
    {
        private bool voidBreak = true;
        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 20;

            npc.aiStyle = -1;

            npc.defense = 15;
            npc.lifeMax = 50;
            npc.damage = 20;

            npc.HitSound = SoundID.NPCHit7;
            npc.DeathSound = SoundID.NPCDeath21;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.knockBackResist = 0.2f;

            npc.lavaImmune = true;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffType<Buffs.Debuffs.Incineration>()] = true;

            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "The charred maggots within this plateau are baby fireflies. They adapted to the harsh conditions of the plateau by growing shells to protect them from the heat. Searching for food, they patrol these lands endlessly.";
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charred Maggot");
            Main.npcFrameCount[npc.type] = 3;
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
                npc.ai[1] = 60;
                int dir = Math.Sign(npc.Center.X - player.Center.X);
                int dirY = Math.Sign(npc.Center.Y - player.Center.Y);
                player.velocity.X -= dir * 7;
                player.velocity.Y -= dirY * 3f;
                for (int l = 0; l < 20; l++)
                {
                    Dust dust = Main.dust[Dust.NewDust(new Vector2(npc.Top.X + (npc.width / 2) * npc.direction, npc.Top.Y), 2, npc.height, 6)];
                    dust.noGravity = true;
                    dust.scale *= 1.6f;
                    dust.velocity.X = -dir * Main.rand.NextFloat(5, 10);
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
        public static void MaggotAI(NPC npc, float speed)
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (npc.ai[1] > 0) npc.ai[1]--;
            if (npc.frame.Y == 2 * npc.ai[2]) npc.ai[0] = 1;
            else if (npc.frame.Y == 0 && npc.ai[0] == 1)
            {
                npc.velocity.X += npc.direction * speed;
                npc.ai[0] = 0;
            }
            npc.velocity.X *= 0.95f;
            if (npc.velocity.Y == 0)
            {
                NPCsGLOBAL.StepUpTiles(npc);
                Point tilePoint = new Vector2(npc.Center.X + (npc.width / 2 + 1) * npc.direction, npc.Center.Y).ToTileCoordinates();
                Tile t = Framing.GetTileSafely(tilePoint);
                if (t.nactive() && Main.tileSolid[t.type])
                {
                    npc.velocity.Y -= 8;
                }
            }
        }
        public override void AI()
        {
            if (voidBreak && Main.netMode != NetmodeID.MultiplayerClient)
            {
                PlateauNPCs.TryVoidbreak(npc, NPCType<VoidbrokenMaggot>());
                voidBreak = false;
            }
            MaggotAI(npc, 1.5f);
        }
    }
}
