using System;
using System.Collections.Generic;
using System.IO;
using ElementsAwoken.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Events.VoidEvent.Enemies.Phase1
{
    public class ReaverSlime : ModNPC
	{
        private float jumpTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float jumpNum
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float aiTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        public override void SetDefaults()
		{
            npc.width = 32;
            npc.height = 22;

            npc.aiStyle = -1;

            npc.damage = 76;
			npc.defense = 20;
			npc.lifeMax = 1000;
            npc.knockBackResist = 0.3f;

            npc.value = Item.buyPrice(0, 0, 20, 0);

			npc.lavaImmune = false;
			npc.noGravity = false;
			npc.noTileCollide = false;

			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;

            banner = npc.type;
            bannerItem = mod.ItemType("ReaverSlimeBanner");

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reaver Slime");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
            npc.lifeMax = 2000;
            npc.defense = 50;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 3000;
                npc.defense = 65;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = NPCsGLOBAL.ReducePierceDamage(damage, projectile);
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("HandsOfDespair"), 180, false);
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (jumpTimer <= 40) npc.frameCounter++;
            if (npc.frameCounter > 8)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 1)  // so it doesnt go over
            {
                npc.frame.Y = 0;
            }
            if (aiTimer > 310)
            {
                npc.frame.Y = frameHeight * 2;
            }
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            if (aiTimer == 300)
            {
                npc.velocity.Y = -20f;
                npc.velocity.X = npc.velocity.X + (float)(2 * npc.direction);
            }
                if (aiTimer >= 300)
            {
                npc.noGravity = true;
                npc.velocity *= 0.95f;

                if (aiTimer % 60 == 0 && aiTimer != 300)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 17);

                    float Speed = 6f;
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1) - 2, mod.ProjectileType("ReaverGlob"), 30, 0f, 0);
                }
            }
            if (aiTimer > 600)
            {
                npc.noGravity = false;
                aiTimer = 0;
            }
            if (aiTimer < 300)
            {
                if (Vector2.Distance(P.Center, npc.Center) < 600 && Collision.CanHit(npc.position, npc.width, npc.height, P.position, P.width, P.height)) aiTimer++;
                SlimeAI();
            }
            else
            {
                aiTimer++;
            }
        }
        private void SlimeAI()
        {
            npc.TargetClosest(true);
            if (npc.wet)
            {
                if (npc.collideY)
                {
                    npc.velocity.Y = -2f;
                }
                if (npc.velocity.Y < 0f && npc.ai[3] == npc.position.X)
                {
                    npc.direction *= -1;
                }
                if (npc.velocity.Y > 0f)
                {
                    npc.ai[3] = npc.position.X;
                }

                if (npc.velocity.Y > 2f)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.9f;
                }
                npc.velocity.Y = npc.velocity.Y - 0.5f;
                if (npc.velocity.Y < -4f)
                {
                    npc.velocity.Y = -4f;
                }
            }

            if (npc.velocity.Y == 0f)
            {
                if (npc.collideY && npc.oldVelocity.Y != 0f && Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.position.X = npc.position.X - (npc.velocity.X + (float)npc.direction);
                }
                if (npc.ai[3] == npc.position.X)
                {
                    npc.direction *= -1;
                }
                npc.ai[3] = 0f;
                npc.velocity.X = npc.velocity.X * 0.8f;
                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                {
                    npc.velocity.X = 0f;
                }
                jumpTimer--; // jump speed

                if (jumpTimer <= 0)
                {
                    jumpNum++;
                    npc.netUpdate = true;
                    if (jumpNum == 4)
                    {
                        npc.velocity.Y = -8f;
                        npc.velocity.X = npc.velocity.X + (float)(3 * npc.direction);
                        jumpTimer = 90f;
                        npc.ai[3] = npc.position.X;
                        jumpNum = 0;
                    }
                    else
                    {
                        npc.velocity.Y = -6f;
                        npc.velocity.X = npc.velocity.X + (float)(2 * npc.direction);
                        jumpTimer = 40f;
                    }
                }
                else if (jumpTimer >= -30f)
                {
                    return;
                }
            }
            else if (npc.target < 255 && ((npc.direction == 1 && npc.velocity.X < 3f) || (npc.direction == -1 && npc.velocity.X > -3f)))
            {
                if (npc.collideX && Math.Abs(npc.velocity.X) == 0.2f)
                {
                    npc.position.X = npc.position.X - 1.4f * (float)npc.direction;
                }
                if (npc.collideY && npc.oldVelocity.Y != 0f && Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.position.X = npc.position.X - (npc.velocity.X + (float)npc.direction);
                }
                if ((npc.direction == -1 && (double)npc.velocity.X < 0.01) || (npc.direction == 1 && (double)npc.velocity.X > -0.01))
                {
                    npc.velocity.X = npc.velocity.X + 0.2f * (float)npc.direction;
                    return;
                }
                npc.velocity.X = npc.velocity.X * 0.93f;
            }
        }
        public override void NPCLoot()  //Npc drop
        {
            if (Main.rand.Next(6) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidJelly"), Main.rand.Next(1, 2)); //Item spawn
            }
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), 1); //Item spawn
            }
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidStone"), Main.rand.Next(3, 5)); //Item spawn
            }
        }
    }
}