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
    public class VoidbrokenWisp : ModNPC
    {
        public override string Texture { get { return "ElementsAwoken/NPCs/VolcanicPlateau/ForgottenWisp"; } }
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCType<ForgottenWisp>());
            npc.lifeMax = (int)(npc.lifeMax * 1.5f);
            npc.defense = (int)(npc.defense * 1.5f);
            npc.damage = (int)(npc.damage * 1.5f);

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "Voidbroken wisps are small wisps consumed by the Void Leviathan’s power. They hunt down uncorrupted creatures and will sometimes get into duels with non-Voidbroken members of their kind.";
            npc.GetGlobalNPC<PlateauNPCs>().voidBroken = true;
            npc.GetGlobalNPC<PlateauNPCs>().counterpart = NPCType<ForgottenWisp>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Voidbroken Wisp");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax /= 2;
            npc.damage /= 2;
        }
        public override void NPCLoot()
        {
             if (Main.rand.NextBool())Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Obsidian, Main.rand.Next(1, 3));
        }
        public override void AI()
        {
            EAUtils.PushOtherEntities(npc);
            npc.TargetClosest(true);
            NPC voidBrokenTarget = null;
            Player player = Main.player[npc.target];
            int tilesAboveBlocks = 99999;
            float distance = 1000;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC other = Main.npc[i];
                if (other.active && !other.GetGlobalNPC<PlateauNPCs>().voidBroken && !other.lavaWet && Vector2.Distance(other.Center, npc.Center) < distance && other.CanBeChasedBy(npc, false))
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
            float velocityMult = targetVB ? 3f : 1.5f;

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
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, npc.direction * 3, 0, ProjectileType<ForgottenWispBolt>(), npc.damage / 2, 0f, Main.myPlayer, 0,1);
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
                Dust dust = Main.dust[Dust.NewDust(npc.Center + offset - new Vector2(4, 4), 0, 0, DustID.PinkFlame, 0, 0, 100)];
                dust.noGravity = true;
                dust.velocity *= 0.5f;
                dust.velocity += npc.velocity;
            }
            if (Main.rand.NextBool(5))
            {
                Dust dust2 = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, DustID.PinkFlame)];
                dust2.noGravity = true;
                dust2.scale *= 1.6f;
                dust2.velocity.Y = -Main.rand.NextFloat(5, 10);
            }
        }
    }
}
