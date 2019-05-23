using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.VoidEventEnemies.Phase1
{
    public class Immolator : ModNPC
    {
        public float vectorX = 0f;
        public int changeLocationTimer = 0;
        public int shootTimer = 0;
        public override void SetDefaults()
        {
            npc.width = 26;
            npc.height = 20;

            npc.damage = 150;
            npc.defense = 35;
            npc.lifeMax = 1000;
            npc.knockBackResist = 0.25f;

            npc.npcSlots = 0.5f;

            npc.value = Item.buyPrice(0, 0, 20, 0);
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;

            npc.noGravity = true;
            npc.noTileCollide = true;

            npc.buffImmune[24] = true;

            banner = npc.type;
            bannerItem = mod.ItemType("ImmolatorBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Immolator");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 175;
            npc.lifeMax = 1500;
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), 1);
            }
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidStone"), Main.rand.Next(3, 5));
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            ++npc.frameCounter;
            if (npc.frameCounter >= 16.0)
                npc.frameCounter = 0.0;
            npc.frame.Y = frameHeight * (int)(npc.frameCounter / 4.0);
        }

        public override void AI()
        {
            Player P = Main.player[npc.target];

            changeLocationTimer--;
            if (changeLocationTimer <= 0)
            {
                switch (Main.rand.Next(2))
                {
                    case 0:
                        vectorX = 400f + Main.rand.Next(-100, 100);
                        break;
                    case 1:
                        vectorX = -400f + Main.rand.Next(-100, 100);
                        break;
                    default: break;
                }
                changeLocationTimer = 180;
            }
            // movement
            npc.TargetClosest(true);
            npc.spriteDirection = npc.direction;
            float speed = 0.1f;
            Vector2 vector75 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float targetX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) + vectorX - vector75.X;
            float targetY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 200f - vector75.Y;
            if (npc.velocity.X < targetX)
            {
                npc.velocity.X = npc.velocity.X + speed * 2;
            }
            else if (npc.velocity.X > targetX)
            {
                npc.velocity.X = npc.velocity.X - speed * 2;
            }
            if (npc.velocity.Y < targetY)
            {
                npc.velocity.Y = npc.velocity.Y + speed;
                if (npc.velocity.Y < 0f && targetY > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + speed;
                    return;
                }
            }
            else if (npc.velocity.Y > targetY)
            {
                npc.velocity.Y = npc.velocity.Y - speed;
                if (npc.velocity.Y > 0f && targetY < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - speed;
                    return;
                }
            }

            Vector2 direction = P.Center - npc.Center;
            if (direction.X >= 0f)
            {
                npc.spriteDirection = 1;
                npc.rotation = direction.ToRotation();
            }
            if (direction.X < 0f)
            {
                npc.spriteDirection = -1;
                npc.rotation = direction.ToRotation() + 3.14f;
            }

            shootTimer--;
            if (shootTimer <= 0)
            {
                float Speed = 10f;
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 35;
                int type = mod.ProjectileType("ImmolatorBall");
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 12);
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                shootTimer = 160;
            }
            if (Vector2.Distance(P.Center, npc.Center) >= 400 || !Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
            {
                npc.noTileCollide = true;
            }
            else
            {
                npc.noTileCollide = false;
            }
        }
    }
}