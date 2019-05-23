using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.VoidEventEnemies.Phase1
{
    public class ZergCaster : ModNPC
    {
        public float shootTimer = 180f;
        public float teleportTimer = 400f;
        public bool casting = false;
        public override void SetDefaults()
        {
            npc.npcSlots = 0.5f;
            aiType = NPCID.DarkCaster;
            npc.damage = 45;
            npc.width = 26; //324
            npc.height = 20; //216
            npc.defense = 25;
            npc.lifeMax = 300;
            npc.knockBackResist = 0.25f;
            npc.value = Item.buyPrice(0, 0, 20, 0);
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.buffImmune[24] = true;
            banner = npc.type;
            bannerItem = mod.ItemType("ZergCasterBanner");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zerg Caster");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 70;
            npc.lifeMax = 700;
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), 1); //Item spawn
            }
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidStone"), Main.rand.Next(3, 5)); //Item spawn
            }
            if (Main.rand.Next(40) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CastersCurse"), 1); //Item spawn
            }
        }
         public override void AI()
        {
            npc.velocity.X = 0f;
            Player P = Main.player[npc.target];
            if (shootTimer > 0f)
            {
                shootTimer -= 1f;
            }
            if (shootTimer <= 30)
            {
                casting = true;
            }
            else
            {
                casting = false;
            }
            if (Main.netMode != 1 && shootTimer == 0f)
            {
                float Speed = 8f;
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 6;
                int type = mod.ProjectileType("ZergFireball");
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                shootTimer = 120f;
            }
            teleportTimer--;
            if (teleportTimer <= 0f)
            {
                npc.ai[0] = 1f;
                int num1 = (int)Main.player[npc.target].position.X / 16;
                int num2 = (int)Main.player[npc.target].position.Y / 16;
                int num3 = (int)npc.position.X / 16;
                int num4 = (int)npc.position.Y / 16;
                int num5 = 20;
                /*int num6 = 0;
                bool flag1 = false;
                if ((double)Math.Abs(npc.position.X - Main.player[npc.target].position.X) + (double)Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 2000.0)
                {
                    num6 = 100;
                    flag1 = true;
                }*/
                int index1 = Main.rand.Next(num1 - num5, num1 + num5);
                for (int index2 = Main.rand.Next(num2 - num5, num2 + num5); index2 < num2 + num5; ++index2)
                {
                    if ((index2 < num2 - 4 || index2 > num2 + 4 || (index1 < num1 - 4 || index1 > num1 + 4)) && (index2 < num4 - 1 || index2 > num4 + 1 || (index1 < num3 - 1 || index1 > num3 + 1)) && Main.tile[index1, index2].nactive())
                    {
                        bool flag2 = true;

                        if (Main.tile[index1, index2 - 1].lava())
                            flag2 = false;
                        if (flag2 && Main.tileSolid[(int)Main.tile[index1, index2].type] && !Collision.SolidTiles(index1 - 1, index1 + 1, index2 - 4, index2 - 1))
                        {
                            npc.ai[1] = 20f;
                            npc.ai[2] = (float)index1;
                            npc.ai[3] = (float)index2;
                            //flag1 = true;
                            break;
                        }

                    }
                }
                Main.PlaySound(SoundID.Item8, npc.position);
                npc.position.X = (float)((double)npc.ai[2] * 16.0 - (double)(npc.width / 2) + 8.0);
                npc.position.Y = npc.ai[3] * 16f - (float)npc.height;
                for (int i = 0; i < 20; i++)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, 127);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].velocity *= 0.1f;
                }
                teleportTimer = 400f;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (!casting)
            {
                npc.frame.Y = 0 * frameHeight;
            }
            if (casting)
            {
                npc.frame.Y = 1 * frameHeight;
            }
        }
    }
}