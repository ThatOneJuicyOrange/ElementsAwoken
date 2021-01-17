using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.NPCs.VolcanicPlateau
{
    public class VoidbrokenCaldera : ModNPC
    {
        private float aiTimer = 0;
        private float aiTimer2 = 0;
        private float aiTimer3 = 0;
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCType<RuinedCaldera>());
            animationType = NPCID.BlueSlime;
            npc.lifeMax = (int)(npc.lifeMax * 1.5f);
            npc.defense = (int)(npc.defense * 1.5f);
            npc.damage = (int)(npc.damage * 1.5f);
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "Voidbroken calderas are ruined calderas corrupted by the Void Leviathan’s incursions into the plateau. They fire voidbroken energy taking the form of stars and can summon voidbroken scorch slimes to their aid.";
            npc.GetGlobalNPC<PlateauNPCs>().voidBroken = true;
            npc.GetGlobalNPC<PlateauNPCs>().counterpart = NPCType<RuinedCaldera>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Voidbroken Caldera");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax /= 2;
            npc.damage /= 2;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffType<Buffs.Debuffs.Incineration>(), 300, false);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("NPCs/VolcanicPlateau/" + GetType().Name + "_Glow");
            Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, 4), frame, Color.White, npc.rotation, origin, npc.scale, effects, 0.0f);
        }
        public override void NPCLoot()
        {
           // Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Drakonite"), Main.rand.Next(1, 3));
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Plateau/" + GetType().Name + i), npc.scale);
                }
            }
        }
        public override void AI()
        {
            Player player = Main.player[npc.target];
            bool inLava = Collision.LavaCollision(npc.position, npc.width, npc.height + (MyWorld.plateauWeather == 2 ? 60 : 30));
            if ((npc.velocity.Y != 0 || inLava) && aiTimer3 < 2)
            {
                if (aiTimer == 0) npc.velocity.Y -= 3;
                aiTimer++;
                if (aiTimer == (inLava ? 120 : 40))
                {
                    if (inLava) aiTimer = 50;
                    npc.velocity.Y -= inLava ? 15 : 10f;
                    // Main.PlaySound(SoundID.Item14, npc.position);
                    Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, npc.position);
                    for (int l = 0; l < 20; l++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(npc.BottomLeft + new Vector2(34, -4), 42, 2, DustID.PinkFlame)];
                        dust.noGravity = true;
                        dust.scale *= 1.6f;
                        dust.velocity.Y = Main.rand.NextFloat(5, 10);
                    }
                }
                if (aiTimer > 300) aiTimer = 0;
            }
            else
            {
                aiTimer = 0;
                if (aiTimer3 < 0) aiTimer3 = 0;
            }
            if (npc.Bottom.Y < player.Top.Y &&
                Math.Abs(player.Center.X - npc.Center.X) < npc.width / 2 &&
                Math.Abs(player.Center.Y - npc.Center.Y) < 300 &&
                aiTimer3 == 0 && aiTimer >= 20) aiTimer3 = 1;
            if (aiTimer3 > 0)
            {
                aiTimer3++;
                npc.velocity = Vector2.Zero;
                Dust dust = Main.dust[Dust.NewDust(npc.BottomLeft + new Vector2(34, -4), 42, 2, DustID.PinkFlame)];
                dust.noGravity = true;
                dust.scale *= 1.6f;
                dust.velocity.Y = Main.rand.NextFloat(5, 10);
                if (aiTimer3 == 15)
                {
                    aiTimer3 = -5;
                    Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, npc.position);
                    npc.velocity.Y += 15;
                    for (int l = 0; l < 20; l++)
                    {
                        Dust dust2 = Main.dust[Dust.NewDust(npc.TopLeft + new Vector2(32, 0), 46, 18, DustID.PinkFlame)];
                        dust2.noGravity = true;
                        dust2.scale *= 1.6f;
                        dust2.velocity.Y = -Main.rand.NextFloat(5, 10);
                    }
                }
            }
            aiTimer2++;
            if (aiTimer2 == 900) Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 118, 1, -0.9f);
            else if (aiTimer2 > 900)
            {
                npc.ai[0] = -200;
                if (aiTimer2 % 6 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Main.PlaySound(SoundID.DD2_PhantomPhoenixShot, npc.position);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 12, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-12, -6), ProjectileType<VoidSpark>(), 90, 0f, Main.myPlayer, 1);
                }
                if (aiTimer2 % 20 == 0 && MyWorld.awakenedMode && Main.netMode != NetmodeID.MultiplayerClient && NPC.CountNPCS(NPCType<VoidbrokenCinderSlime>()) < 5)
                {
                    NPC slime = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 12, NPCType<VoidbrokenCinderSlime>())];
                    slime.SpawnedFromStatue = true;
                    slime.velocity = new Vector2(Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-12, -6));
                }
                if (aiTimer2 > 960) aiTimer2 = 0;
            }
        }
    }
}
