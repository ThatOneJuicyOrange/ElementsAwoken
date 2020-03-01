using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.ScourgeFighter
{
    public class MiniFighter : ModNPC
    {
        public override void SetDefaults()
        {
            npc.aiStyle = 5;
            npc.damage = 30;
            npc.width = 30; //324
            npc.height = 26; //216
            npc.defense = 30;
            npc.lifeMax = 500;
            npc.knockBackResist = 0f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mini Fighter");
            Main.npcFrameCount[npc.type] = 1;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 40;
            npc.lifeMax = 750;
            if (MyWorld.awakenedMode)
            {
                npc.lifeMax = 1000;
                npc.damage = 60;
                npc.defense = 45;
            }
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            npc.ai[0]++;
            if (npc.ai[0] == 70)
            {
                float Speed = 20f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 20;  //projectile damage
                int type = mod.ProjectileType("ScourgeBeam");  //put your projectile
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 33);
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                npc.ai[0] = 0;
            }
            for (int num246 = 0; num246 < 2; num246++)
            {
                float num247 = 0f;
                float num248 = 0f;
                if (num246 == 1)
                {
                    num247 = npc.velocity.X * 0.5f;
                    num248 = npc.velocity.Y * 0.5f;
                }
                if (Main.rand.Next(12) == 0)
                {
                    /*int num249 = Dust.NewDust(new Vector2(npc.position.X + 3f + num247, npc.position.Y + 3f + num248) - npc.velocity * 0.5f, npc.width - 4, npc.height - 26, 6, 0f, 0f, 100, default(Color), 1f);
                    Main.dust[num249].scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                    Main.dust[num249].velocity *= 0.2f;
                    Main.dust[num249].noGravity = true;
                    num249 = Dust.NewDust(new Vector2(npc.position.X + 3f + num247, npc.position.Y + 3f + num248) - npc.velocity * 0.5f, npc.width - 4, npc.height - 26, 31, 0f, 0f, 100, default(Color), 0.5f);
                    Main.dust[num249].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
                    Main.dust[num249].velocity *= 0.05f;*/
                }
            }
            //STOP CLUMPING FOOLS
            for (int k = 0; k < 200; k++)
            {
                NPC other = Main.npc[k];
                if (k != npc.whoAmI && other.type == npc.type && other.active && Math.Abs(npc.position.X - other.position.X) + Math.Abs(npc.position.Y - other.position.Y) < npc.width)
                {
                    const float pushAway = 0.05f;
                    if (npc.position.X < other.position.X)
                    {
                        npc.velocity.X -= pushAway;
                    }
                    else
                    {
                        npc.velocity.X += pushAway;
                    }
                    if (npc.position.Y < other.position.Y)
                    {
                        npc.velocity.Y -= pushAway;
                    }
                    else
                    {
                        npc.velocity.Y += pushAway;
                    }
                }
            }
            if (!NPC.AnyNPCs(mod.NPCType("ScourgeFighter")))
            {
                npc.active = false;
            }
        }
    }
}