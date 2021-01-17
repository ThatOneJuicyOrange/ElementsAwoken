using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.NPCs.VolcanicPlateau
{
    public class CinderSlime : ModNPC
    {
        private float aiTimer = 0;
        private float aiTimer2 = 0;
        private bool voidBreak = true;
        public override void SetDefaults()
        {
            npc.width = 38;
            npc.height = 26;

            npc.aiStyle = 1;
            aiType = 1;
            animationType = NPCID.BlueSlime;

            npc.damage = MyWorld.awakenedPlateau ? 40 : 15;
            npc.defense = MyWorld.awakenedPlateau ? 12 : 6;
            npc.lifeMax = MyWorld.awakenedPlateau ? 230 : 50;

            npc.HitSound = SoundID.NPCHit7;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.knockBackResist = 0.5f;

            npc.lavaImmune = true;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffType<Buffs.Debuffs.Incineration>()] = true;

            banner = npc.type;
            bannerItem = mod.ItemType("DragonSlimeBanner");
            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "The cinder slimes that frequent this area are an offshoot of the lava slimes living throughout the rest of the underworld, formed when Mount Magmadi erupted and buried them all under tons of rubble.";
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Plateau/" + GetType().Name + i), npc.scale);
                }
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cinder Slime");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = MyWorld.awakenedPlateau ? 60 : 30;
            npc.defense = MyWorld.awakenedPlateau ? 14 : 8;
            npc.lifeMax = MyWorld.awakenedPlateau ? 400 : 75;
            if (MyWorld.awakenedMode)
            {
                npc.damage = MyWorld.awakenedPlateau ? 80 : 38;
                npc.defense = MyWorld.awakenedPlateau ? 16 : 10;
                npc.lifeMax = MyWorld.awakenedPlateau ? 600 : 100;
            }
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(MyWorld.awakenedPlateau ? BuffType<Buffs.Debuffs.Incineration>() :  BuffID.OnFire, MyWorld.awakenedPlateau ? 150 : 90, false);
        }

        public override void NPCLoot()
        {
           // Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Drakonite"), Main.rand.Next(1, 3));
        }

        public override void AI()
        { 
            if (voidBreak && Main.netMode != NetmodeID.MultiplayerClient)
            {
                PlateauNPCs.TryVoidbreak(npc, NPCType<VoidbrokenCinderSlime>());
                voidBreak = false;
            }
            Player player = Main.player[npc.target];
            if (npc.velocity.Y != 0)
            {
                aiTimer++;
                if (aiTimer >= 20 && aiTimer < 60)
                {
                    npc.velocity.Y -= 0.35f;
                    Dust dust = Main.dust[Dust.NewDust(npc.BottomLeft + new Vector2(10,-4), npc.width - 20, 2,6)];
                    dust.noGravity = true;
                    dust.scale *= 1.6f;
                    dust.velocity.Y = Main.rand.NextFloat(2,5);
                }
            }
            else aiTimer = 0;
            int stuckTime = 200;
            if (Math.Abs(npc.velocity.X) < 0.5f && aiTimer2 < stuckTime)
            {
                aiTimer2++;
                if (aiTimer2 == stuckTime)
                {
                    npc.velocity.X = -Math.Sign(player.Center.X - npc.Center.X) * 12;
                }
            }
            if (aiTimer2 >= stuckTime)
            {
                aiTimer2++;
                if (aiTimer2 == stuckTime + 20)
                {
                    npc.velocity.Y -= Main.rand.Next(10,15);
                    npc.netUpdate = true;
                    for (int l = 0; l < 20; l++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(npc.BottomLeft + new Vector2(10, -4) + npc.velocity * 3, npc.width - 20, 2, 6)];
                        dust.noGravity = true;
                        dust.scale *= 1.6f;
                        dust.velocity.Y = Main.rand.NextFloat(2, 5);
                    }
                }
                if (aiTimer2 == stuckTime + 60)
                {
                    npc.velocity.X = Math.Sign(player.Center.X - npc.Center.X) * Main.rand.Next(4, 8);
                    npc.netUpdate = true;
                    aiTimer2 = 0;
                    for (int l = 0; l < 20; l++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(npc.Center + new Vector2(-4, -4), 8, 8, 6)];
                        dust.noGravity = true;
                        dust.scale *= 1.6f;
                        dust.velocity.X = Main.rand.NextFloat(2, 5) * -Math.Sign(npc.velocity.X);
                    }
                }
            }
            else if (Math.Abs(npc.velocity.X) > 0.5f) aiTimer2 = 0;
        }
    }
}
