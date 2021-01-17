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
    public class ForgottenBoneWisp : ModNPC
    {
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

            npc.knockBackResist = 0.2f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.gfxOffY = 6f;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffType<Buffs.Debuffs.Incineration>()] = true;

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "Bone wisps are a variety of common wisp that have fused themselves with bones. They can be found in the Scarlet Mines, where they hunt down miners and trespassers to add to their collection. They shoot bones of molten lava at whatever goes near them.";
                }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forgotten Bonewisp");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.frame.Y == 0)
            {
                npc.frame.Y += frameHeight * Main.rand.Next(1, 3);
            }
        }
        public override void AI()
        {
            EAUtils.PushOtherEntities(npc);
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            int tilesAboveBlocks = 99999;

            Vector2 targetPos = player.Center;
            targetPos.X -= Math.Sign(targetPos.X - npc.Center.X) * 60;

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
                npc.velocity.Y += toY * 0.02f;
            }
            // floaty, but with intention

            if (npc.ai[2] < 0) npc.ai[2]++;
            if ((Math.Abs(targetPos.X - npc.Center.X) < attackDist || npc.ai[2] > 0) && npc.ai[2] >= 0 && Math.Abs(targetPos.Y - npc.Center.Y) < 15)
            {
                npc.velocity *= 0.95f;
                npc.ai[2]++;
                if (npc.ai[2] >= 60)
                {
                    Main.PlaySound(SoundID.DD2_BetsyFireballShot, npc.position);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, npc.direction * 3, 0, ProjectileType<ForgottenWispBolt>(), npc.damage / 2, 0f, Main.myPlayer);
                    npc.ai[2] = -90;
                }
            }
            else if (Math.Abs(targetPos.X - npc.Center.X) < 600)
            {
                int toX = Math.Sign(targetPos.X - npc.Center.X);
                if (npc.velocity.X < 3 && npc.velocity.X > -3) npc.velocity.X += toX * 0.02f;
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
