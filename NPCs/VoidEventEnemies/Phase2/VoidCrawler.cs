using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.VoidEventEnemies.Phase2
{
    public class VoidCrawler : ModNPC
	{
        public int jumpCooldown = 0;

        public override void SetDefaults()
		{
			npc.npcSlots = 0.5f;

			npc.width = 100;
			npc.height = 50;

            npc.damage = 80;
            npc.defense = 75;
			npc.lifeMax = 1500;
			npc.knockBackResist = 0.1f;

			animationType = 257;
            npc.aiStyle = 3;
            aiType = NPCID.AnomuraFungus;

            npc.value = Item.buyPrice(0, 0, 20, 0);

            npc.HitSound = SoundID.NPCHit29;
			npc.DeathSound = SoundID.NPCDeath31;

            banner = npc.type;
            bannerItem = mod.ItemType("VoidCrawlerBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Crawler");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 2000;
            npc.damage = 120;
            npc.defense = 90;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Slow, 180, false);
            player.AddBuff(mod.BuffType("HandsOfDespair"), 180, false);
        }

        public override void AI()
        {
            Lighting.AddLight(npc.Center, 0.1f, 0.1f, 0.5f);
            Player P = Main.player[npc.target];
            npc.TargetClosest(true);

            if (Main.rand.Next(500) == 0)
            {
                Main.PlaySound(3, (int)npc.position.X, (int)npc.position.Y, 29, 1f, 0f);
            }
            // jump up if the player is above and near:
            // x check, must be within 400 pix
            jumpCooldown--;
            float jumpspeed = 10.5f;
            if (Math.Abs(npc.Center.X - P.Center.X) <= 100)
            {
                if (npc.Bottom.Y > P.Bottom.Y) // under the player
                {
                    if (npc.velocity.Y == 0 && jumpCooldown <= 0) // grounded
                    {
                        npc.velocity.Y -= jumpspeed;
                        jumpCooldown = 15;
                    }
                }
            }
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), 1);
            }
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Darkstone"), Main.rand.Next(3, 5));
            }
        }
    }
}