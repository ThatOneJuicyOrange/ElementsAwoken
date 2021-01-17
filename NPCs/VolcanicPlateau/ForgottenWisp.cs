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

namespace ElementsAwoken.NPCs.VolcanicPlateau
{
    public class ForgottenWisp : ModNPC
    {
        private bool voidBreak = true;
        public override void SetDefaults()
        {
            npc.width = 18;
            npc.height = 18;

            npc.aiStyle = -1;

            npc.defense = 10;
            npc.lifeMax = 75;
            npc.damage = 18;
            npc.knockBackResist = 0.4f;

            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath6;

            npc.value = Item.buyPrice(0, 0, 2, 0);
            npc.knockBackResist = 0.2f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.gfxOffY = -4f;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffType<Buffs.Debuffs.Incineration>()] = true;

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "The forgotten wisps here are small, playful balls of ectoplasm that possessed small chunks of obsidian in the wake of the Void Leviathan’s invasion. They flutter about, attacking their foes with wisps of flame. They are also known to target the enemies known as the 'Voidbroken'.";
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forgotten Wisp");
        }
        public override void NPCLoot()
        {
             if (Main.rand.NextBool())Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Obsidian, Main.rand.Next(1, 3));
        }
        public override void AI()
        {
            if (voidBreak && Main.netMode != NetmodeID.MultiplayerClient)
            {
                PlateauNPCs.TryVoidbreak(npc, NPCType<VoidbrokenWisp>());
                voidBreak = false;
            }
            EAUtils.PushOtherEntities(npc);
            npc.TargetClosest(true);
            NPC voidBrokenTarget = null;
            Player player = Main.player[npc.target];
            int tilesAboveBlocks = 99999;
            float distance = 1000;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC other = Main.npc[i];
                if (other.active && other.GetGlobalNPC<PlateauNPCs>().voidBroken && !other.lavaWet && Vector2.Distance(other.Center,npc.Center) < distance)
                {
                    voidBrokenTarget = other;
                    distance = Vector2.Distance(other.Center, npc.Center);
                }
            }
            Vector2 targetPos = player.Center;
            bool targetVB = false;
            if (voidBrokenTarget != null)
            {
                targetPos = voidBrokenTarget.Center;
                targetVB = true;
                npc.direction = Math.Sign(voidBrokenTarget.Center.X - npc.Center.X);
            }
            targetPos.X -= Math.Sign(targetPos.X - npc.Center.X) * 60;
            float velocityMult = targetVB ? 2f : 1f;

            int attackDist = 120;
            // floaty
            if (Math.Abs(targetPos.X - npc.Center.X) > attackDist*2)
            {
                npc.ai[0]++;
                if (npc.ai[0] < npc.ai[1])
                {
                    Point wispTile = npc.Bottom.ToTileCoordinates();
                    for (int i = 0; i < 10; i++)
                    {
                        Tile t = Framing.GetTileSafely(wispTile.X, wispTile.Y + i);
                        if (t.active() && Main.tileSolid[t.type])
                        {
                            tilesAboveBlocks = i;
                            break;
                        }
                    }
                    if (tilesAboveBlocks < 5) npc.velocity.Y -= 0.1f;
                    else npc.velocity.Y -= 0.04f;
                }
                else npc.velocity.Y += 0.16f;
                if (npc.ai[0] > npc.ai[1] + 10)
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = Main.rand.Next(30, 40);
                }
            }
            else
            {
                int toY = Math.Sign(targetPos.Y - npc.Center.Y);
                npc.velocity.Y += toY * 0.02f * velocityMult;
            }
            // floaty, but with intention

            if (npc.ai[2] < 0) npc.ai[2]++;
            if ((Math.Abs(targetPos.X - npc.Center.X) < attackDist || npc.ai[2] > 0) && npc.ai[2] >= 0 && Math.Abs(targetPos.Y - npc.Center.Y) < 15)
            {
                npc.velocity *= 0.95f;
                npc.ai[2]++;
                if (npc.ai[2] >= (targetVB ? 10 : 60))
                {
                    Main.PlaySound(SoundID.DD2_BetsyFireballShot, npc.position);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, npc.direction * 3, 0, ProjectileType<ForgottenWispBolt>(), npc.damage / 2, 0f, Main.myPlayer);
                    npc.ai[2] = -90;
                }
            }
            else if (Math.Abs(targetPos.X - npc.Center.X) < 600)
            {
                int toX = Math.Sign(targetPos.X - npc.Center.X);
                if (npc.velocity.X < 3 && npc.velocity.X > -3) npc.velocity.X += toX * 0.02f * velocityMult;
                if (toX > 0f && npc.velocity.X < 0 || toX < 0f && npc.velocity.X > 0) npc.velocity.X = npc.velocity.X * 0.98f;
            }
            else
            {
                int duration = 180;
                npc.ai[3]++;
                int max = 3;
                if (npc.velocity.X < max && npc.velocity.X > -max) npc.velocity.X += Math.Sign(npc.ai[3]) * 0.06f;
                if (npc.ai[3] > 0f && npc.velocity.X < 0 || npc.ai[3] < 0f && npc.velocity.X > 0) npc.velocity.X = npc.velocity.X * 0.98f;
                if (npc.ai[3] > duration - (targetPos.X - npc.Center.X < 0 ? duration / 2 : 0)) npc.ai[3] = -duration + (targetPos.X - npc.Center.X > 0 ? duration / 2 : 0);
            }

            float maxDist = npc.width / 2;
            int amount = 3;
            for (int i = 0; i < amount; i++)
            {
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                Dust dust = Main.dust[Dust.NewDust(npc.Center + offset - new Vector2(4, 4), 0, 0, 6, 0, 0, 100)];
                dust.noGravity = true;
                dust.velocity *= 0.5f;
                dust.velocity += npc.velocity;
            }
            if (Main.rand.NextBool(5))
            {
                Dust dust2 = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 6)];
                dust2.noGravity = true;
                dust2.scale *= 1.6f;
                dust2.velocity.Y = -Main.rand.NextFloat(5, 10);
            }
        }
    }
}
