using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Aqueous
{
    public class AquaticReaper : ModNPC
    {
        public override void SetDefaults()
        {
            npc.noGravity = true;
            npc.width = 24;
            npc.height = 24;
            npc.damage = 100;
            npc.defense = 100;
            npc.lifeMax = 100;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0f;
            npc.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aqueous Minion");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 111, hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }
        public override void AI()
        {
            npc.noTileCollide = true;
            int num1029 = 90;
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
            {
                npc.TargetClosest(false);
                npc.direction = 1;
                npc.netUpdate = true;
            }
            if (npc.ai[0] == 0f)
            {
                float[] var_9_32483_cp_0 = npc.ai;
                int var_9_32483_cp_1 = 1;
                float num244 = var_9_32483_cp_0[var_9_32483_cp_1];
                var_9_32483_cp_0[var_9_32483_cp_1] = num244 + 1f;
                int arg_324A9_0 = npc.type;
                npc.noGravity = true;
                npc.dontTakeDamage = true;
                npc.velocity.Y = npc.ai[3];
                if (npc.ai[1] >= (float)num1029)
                {
                    npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                    if (!Collision.SolidCollision(npc.position, npc.width, npc.height))
                    {
                        npc.ai[1] = 1f;
                    }
                    Main.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 19, 1f, 0f);
                    npc.TargetClosest(true);
                    npc.spriteDirection = npc.direction;
                    Vector2 vector127 = Main.player[npc.target].Center - npc.Center;
                    float speed = 40f;
                    vector127.Normalize();
                    npc.velocity = vector127 * speed;
                    npc.rotation = npc.velocity.ToRotation();
                    if (npc.direction == -1)
                    {
                        npc.rotation += 3.14159274f;
                    }
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 1f)
            {
                npc.noGravity = true;
                if (!Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    if (npc.ai[1] < 1f)
                    {
                        npc.ai[1] = 1f;
                    }
                }
                else
                {
                    npc.alpha -= 15;
                    if (npc.alpha < 150)
                    {
                        npc.alpha = 150;
                    }
                }
                if (npc.ai[1] >= 1f)
                {
                    npc.alpha -= 60;
                    if (npc.alpha < 0)
                    {
                        npc.alpha = 0;
                    }
                    npc.dontTakeDamage = false;
                    float[] var_9_32858_cp_0 = npc.ai;
                    int var_9_32858_cp_1 = 1;
                    float num244 = var_9_32858_cp_0[var_9_32858_cp_1];
                    var_9_32858_cp_0[var_9_32858_cp_1] = num244 + 1f;
                    if (Collision.SolidCollision(npc.position, npc.width, npc.height))
                    {
                        if (npc.DeathSound != null)
                        {
                            Main.PlaySound(npc.DeathSound, npc.position);
                        }
                        npc.life = 0;
                        npc.HitEffect(0, 10.0);
                        npc.active = false;
                        return;
                    }
                }
                if (npc.ai[1] >= 60f)
                {
                    npc.noGravity = false;
                }
                npc.rotation = npc.velocity.ToRotation();
                if (npc.direction == -1)
                {
                    npc.rotation += 3.14159274f;
                    return;
                }
            }
        }
    }
}