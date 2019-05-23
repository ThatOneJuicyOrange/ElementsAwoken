using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace ElementsAwoken.NPCs.VoidEventEnemies.Phase2
{
    public class VoidGolem : ModNPC
	{
        public int jumpCooldown = 0;
		public override void SetDefaults()
		{
			npc.width = 18;
			npc.height = 40;

			npc.damage = 50;
			npc.defense = 100;
			npc.lifeMax = 2500;
            npc.knockBackResist = 0.05f;

            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath8;
            npc.value = Item.buyPrice(0, 0, 20, 0);

            aiType = NPCID.Skeleton;
            npc.aiStyle = 3;

            //animationType = NPCID.PossessedArmor;
            npc.buffImmune[24] = true;

            //banner = npc.type;
            //bannerItem = mod.ItemType("DragonWarriorBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Golem");
            Main.npcFrameCount[npc.type] = 7;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 4000;
            npc.damage = 75;
            npc.defense = 120;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Slow, 180, false);
            player.AddBuff(mod.BuffType("HandsOfDespair"), 180, false);
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
        public override void AI()
        {
            Lighting.AddLight(npc.Center, 0.1f, 0.1f, 0.5f);
            Player P = Main.player[npc.target];
            npc.TargetClosest(true);
            // jump up if the player is above and near:
            // x check, must be within 400 pix
            jumpCooldown--;
            float jumpspeed = 9.5f;
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
            if (Vector2.Distance(P.Center, npc.Center) <= 200)
            {
                ElementsAwoken.screenshakeAmount = 4;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            //npc.frameCounter += 1;
            // moving
            if (npc.velocity.X != 0f)
            {
                npc.frameCounter += (double)Math.Abs(npc.velocity.X);
            }

            // on the ground 
            if (npc.velocity.Y == 0f)
            {
                if (npc.direction == 1)
                {
                    npc.spriteDirection = 1;
                }
                if (npc.direction == -1)
                {
                    npc.spriteDirection = -1;
                }
            }
            // moving  not jumping 
            if (npc.velocity.Y == 0f || npc.velocity.X != 0f)//(npc.direction == -1 && npc.velocity.X > 0f) || (npc.direction == 1 && npc.velocity.X < 0f))
            {
                if (npc.frameCounter > 8)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
                if (npc.frame.Y > frameHeight * 6)  // so it doesnt go over
                {
                    npc.frame.Y = frameHeight * 1;
                }
            }
            // jumping
            if (npc.velocity.Y != 0f)
            {
                npc.frameCounter = 0.0;
                npc.frame.Y = 0;
            }
        }
    }
}
