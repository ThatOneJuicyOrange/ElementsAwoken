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
    public class RuinedCaldera : ModNPC
    {
        private float aiTimer = 0;
        private float aiTimer2 = 0;
        private float aiTimer3 = 0;
        private bool voidBreak = true;
        public override void SetDefaults()
        {
            npc.width = 110;
            npc.height = 56;

            npc.aiStyle = 1;
            aiType = 1;
            animationType = NPCID.BlueSlime;

            npc.damage = 40;
            npc.defense = 20;
            npc.lifeMax = 600;

            npc.HitSound =  mod.GetLegacySoundSlot(SoundType.Music, "Sounds/NPC/PlateauHit");
            npc.DeathSound = SoundID.NPCDeath43;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.knockBackResist = 0.1f;

            npc.lavaImmune = true;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffType<Buffs.Debuffs.Incineration>()] = true;

            banner = npc.type;
            bannerItem = mod.ItemType("DragonSlimeBanner");
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "Ruined calderas are a species of slime made out of solidified lava that were formed with the eruption of Mount Magmadi. With the Void Leviathan's attacking the plateau, some were corrupted and now attack anything within range of themselves. They can leap at foes and fire smaller scorch slimes at their enemies.";
            npc.GetGlobalNPC<PlateauNPCs>().floatScale = 0.85f;
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ruined Caldera");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 65;
            npc.defense = 24;
            npc.lifeMax = 800;
            if (MyWorld.awakenedMode)
            {
                npc.damage = 80;
                npc.defense = 30;
                npc.lifeMax = 1200;
            }
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

        public override void AI()
        {
            if (voidBreak && Main.netMode != NetmodeID.MultiplayerClient)
            {
                PlateauNPCs.TryVoidbreak(npc, NPCType<VoidbrokenCaldera>());
                voidBreak = false;
            }
            Player player = Main.player[npc.target];
            bool inLava = Collision.LavaCollision(npc.position, npc.width, npc.height + (MyWorld.plateauWeather == 2 ? 60 : 30));
            if ((npc.velocity.Y != 0 || inLava) && aiTimer3 < 2)
            {
                if (aiTimer == 0)
                {
                    npc.velocity.Y -= 3;
                    npc.velocity.X *= 2;
                }
                aiTimer++;
                if (aiTimer == (inLava ? 120 : 40))
                {
                    if (inLava) aiTimer = 50;
                    npc.velocity.Y -= inLava ? 15 : 10f;
                    // Main.PlaySound(SoundID.Item14, npc.position);
                    Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, npc.position);
                    for (int l = 0; l < 20; l++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(npc.BottomLeft + new Vector2(34, -4), 42, 2, 6)];
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
                Dust dust = Main.dust[Dust.NewDust(npc.BottomLeft + new Vector2(34, -4), 42, 2, 6)];
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
                        Dust dust2 = Main.dust[Dust.NewDust(npc.TopLeft + new Vector2(32, 0), 46, 18, 6)];
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
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 12, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-12, -6), ProjectileType<SolarFragmentProj>(), 50, 0f, Main.myPlayer, 1);
                }
                if (aiTimer2 % 12 == 0 && MyWorld.awakenedMode && Main.netMode != NetmodeID.MultiplayerClient && NPC.CountNPCS(NPCType<CinderSlime>()) < 10)
                {
                    NPC slime = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 12, NPCType<CinderSlime>())];
                    slime.SpawnedFromStatue = true;
                    slime.velocity = new Vector2(Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-12, -6));
                }
                if (aiTimer2 > 960) aiTimer2 = 0;
            }
        }
    }
}
