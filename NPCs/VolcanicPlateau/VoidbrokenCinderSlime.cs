using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.NPCs.VolcanicPlateau
{
    public class VoidbrokenCinderSlime : ModNPC
    {
        private float aiTimer = 0;
        private float aiTimer2 = 0;
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCType<CinderSlime>());
            animationType = NPCID.BlueSlime;
            npc.lifeMax = (int)(npc.lifeMax * 1.5f);
            npc.defense = (int)(npc.defense * 1.5f);
            npc.damage = (int)(npc.damage * 1.5f);
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "The voidbroken cinder slimes are corrupted versions of cinder slimes that channel the Void. They utilise this ability to float at a greater strength, allowing them to rise up into the air and attack the Void’s enemies.";
            npc.GetGlobalNPC<PlateauNPCs>().voidBroken = true;
            npc.GetGlobalNPC<PlateauNPCs>().counterpart = NPCType<CinderSlime>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Voidbroken Cinder Slime");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax /= 2;
            npc.damage /= 2;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(MyWorld.awakenedPlateau ? BuffType<Buffs.Debuffs.Incineration>() :  BuffID.OnFire, MyWorld.awakenedPlateau ? 150 : 90, false);
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

        public override void NPCLoot()
        {
           // Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Drakonite"), Main.rand.Next(1, 3));
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (npc.velocity.Y != 0)
            {
                aiTimer++;
                if (aiTimer >= 20 && aiTimer < 60)
                {
                    npc.velocity.Y -= 0.45f;
                    Dust dust = Main.dust[Dust.NewDust(npc.BottomLeft + new Vector2(10,-4), npc.width - 20, 2,DustID.PinkFlame)];
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
                        Dust dust = Main.dust[Dust.NewDust(npc.BottomLeft + new Vector2(10, -4) + npc.velocity * 3, npc.width - 20, 2, DustID.PinkFlame)];
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
                        Dust dust = Main.dust[Dust.NewDust(npc.Center + new Vector2(-4, -4), 8, 8, DustID.PinkFlame)];
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
