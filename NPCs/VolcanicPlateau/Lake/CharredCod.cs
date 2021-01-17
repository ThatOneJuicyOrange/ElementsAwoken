using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace ElementsAwoken.NPCs.VolcanicPlateau.Lake
{
    public class CharredCod : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 18;

            npc.aiStyle = -1;

            npc.defense = 5;
            npc.lifeMax = 50;
            npc.damage = 0;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.knockBackResist = 0.2f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.dontTakeDamageFromHostiles = true;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffType<Buffs.Debuffs.Incineration>()] = true;

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "Charred cod are a subspecies of cod that live in the volcanic lake. They live in schools and are one of the most peaceful species in the lake. This can likely be attributed to their minute brain size.";
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charred Cod");
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
        }
        public override void NPCLoot()
        {
            // Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Drakonite"), Main.rand.Next(1, 3));
        }

        
        public override void AI()
        {
            npc.spriteDirection = npc.direction;
            EAUtils.PushOtherEntities(npc,pushStrength:0.05f);
            if (npc.ai[0] == 0)
            {
                npc.ai[0] = -1;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < Main.rand.Next(2,8); i++)
                    {
                        NPC buddy = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<CharredCod>())];
                        buddy.ai[0] = npc.whoAmI + 1; // + 1 so its never 0
                    }
                }
            }
            if (npc.ai[0] > 0)
            {
                npc.direction = Math.Sign(npc.velocity.X);
                NPC parent = Main.npc[(int)npc.ai[0] - 1];
                if (npc.lavaWet)
                {
                    Vector2 toTarget = new Vector2(parent.Center.X - npc.Center.X, parent.Center.Y - npc.Center.Y); 
                    float dist = (float)Math.Sqrt((double)(toTarget.X * toTarget.X + toTarget.Y * toTarget.Y));
                    if (dist > 30)
                    {
                        dist = 0.2f * (dist / 600f);
                        toTarget.X *= dist;
                        toTarget.Y *= dist;
                        npc.velocity.X = (npc.velocity.X * 20f + toTarget.X) / 21f;
                        npc.velocity.Y = (npc.velocity.Y * 20f + toTarget.Y) / 21f;
                    }
                }
            }
            else
            {
                Vector2 target = new Vector2(npc.ai[2], npc.ai[3]);
                if (GetInstance<Config>().debugMode)
                {
                    Dust dust = Main.dust[Dust.NewDust(npc.Center, 2, 2, DustID.PinkFlame)];
                    dust.noGravity = true;
                    dust = Main.dust[Dust.NewDust(target, 2, 2, DustID.PinkFlame)];
                    dust.noGravity = true;
                }
                npc.ai[1]++;
                if (npc.ai[1] > 600 || npc.ai[2] == 0 || Math.Abs(npc.Center.X - npc.ai[2]) < 20 || Math.Abs(npc.Center.Y - npc.ai[3]) < 20)
                {
                    npc.ai[1] = 0;
                    FindLocation();
                }

                if (npc.lavaWet)
                {
                    npc.direction = Math.Sign(target.X - npc.Center.X);
                    npc.directionY = Math.Sign(target.Y - npc.Center.Y);

                    if (npc.velocity.X > 0f && npc.direction < 0) npc.velocity.X = npc.velocity.X * 0.95f;
                    if (npc.velocity.X < 0f && npc.direction > 0) npc.velocity.X = npc.velocity.X * 0.95f;
                    if (npc.velocity.Y > 0f && npc.directionY < 0) npc.velocity.Y = npc.velocity.Y * 0.95f;
                    if (npc.velocity.Y < 0f && npc.directionY > 0) npc.velocity.Y = npc.velocity.Y * 0.95f;
                    float spdX = 0.009f;
                    float spdY = 0.007f;
                    npc.velocity.X = npc.velocity.X + (float)npc.direction * spdX;
                    npc.velocity.Y = npc.velocity.Y + (float)npc.directionY * spdY;

                    float maxVelX = 12;
                    float maxVelY = 8;
                    if (npc.velocity.X > maxVelX)
                    {
                        npc.velocity.X = maxVelX;
                    }
                    if (npc.velocity.X < -maxVelX)
                    {
                        npc.velocity.X = -maxVelX;
                    }
                    if (npc.velocity.Y > maxVelY)
                    {
                        npc.velocity.Y = maxVelY;
                    }
                    if (npc.velocity.Y < -maxVelY)
                    {
                        npc.velocity.Y = -maxVelY;
                    }
                }
            }
            if (!npc.lavaWet)
            {
                npc.velocity.Y += 0.16f;
            }
            NPCsGLOBAL.GoThroughPlatforms(npc);
        }
        private void FindLocation(int attempts = 0)
        {
            npc.ai[2] = npc.Center.X + Main.rand.Next(-200, 201);
            npc.ai[3] = npc.Center.Y + Main.rand.Next(-200, 201);
            Tile tile = Framing.GetTileSafely((int)npc.ai[2] / 16, (int)npc.ai[3] / 16);
            if ((!tile.lava() || !Collision.CanHit(npc.Center, 2, 2, new Vector2(npc.ai[2], npc.ai[3]), 2, 2)) && attempts < 20) FindLocation(attempts + 1);
        }
    }
}
