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
    public class VoidbrokenFlamewisp : ModNPC
    {
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCType<MoltenFlamewisp>());
            npc.lifeMax = (int)(npc.lifeMax * 1.5f);
            npc.defense = (int)(npc.defense * 1.5f);
            npc.damage = (int)(npc.damage * 1.5f);

            npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "Voidbroken flamewisps are Void-touched chunks of obsidian with Nihiline, a substance with powerful preservative properties as well as the ability to corrupt creatures trapped in it, running through what appear to be veins. They hunt down creatures uncorrupted by the Void and act as ways for the Void to corrupt and cull unwanted creatures. They are colloquially known as the ‘Claws of the Void’ by demons.";
            npc.GetGlobalNPC<PlateauNPCs>().voidBroken = true;
            npc.GetGlobalNPC<PlateauNPCs>().counterpart = NPCType<MoltenFlamewisp>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Voidbroken Flamewisp");
        }
        public override void NPCLoot()
        {
             if (Main.rand.NextBool())Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Obsidian, Main.rand.Next(1, 3));
        }
        public override void AI()
        {
            EAUtils.PushOtherEntities(npc);
            npc.TargetClosest(true);

            Vector2 direction = Main.player[npc.target].Center - npc.Center;
            npc.rotation = direction.ToRotation();

            NPC voidBrokenTarget = null;
            Player player = Main.player[npc.target];
            int tilesAboveBlocks = 99999;
            float distance = 1000;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC other = Main.npc[i];
                if (other.active && !other.GetGlobalNPC<PlateauNPCs>().voidBroken && !other.lavaWet && Vector2.Distance(other.Center,npc.Center) < distance && other.CanBeChasedBy(npc, false))
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

            int attackDist = 300;
            // floaty
            //if (Math.Abs(targetPos.X - npc.Center.X) > attackDist * 1.2f)
            {
                npc.ai[0]++;
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
                if (tilesAboveBlocks < 8) npc.velocity.Y -= 0.1f;
                else
                {
                    npc.velocity.Y += 0.04f;
                }
            }

            // floaty, but with intention
            if (npc.ai[2] < 0) npc.ai[2]++;
            if (Vector2.Distance(npc.Center, targetPos) < attackDist)
            {
                npc.velocity *= 0.95f;
                if (npc.ai[2] >= 0)
                {
                    Main.PlaySound(SoundID.DD2_BetsyFireballShot, npc.position);

                    float rotation = (float)Math.Atan2(npc.Center.Y - targetPos.Y, npc.Center.X - targetPos.X);
                    float speed = 3f;
                    Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), ProjectileType<ForgottenWispBolt>(), npc.damage / 2, 0f, Main.myPlayer, 0f, 1f)];
                    proj.extraUpdates = 4;
                    proj.timeLeft = 90;
                    proj.localAI[0] = 90;
                    npc.ai[2] = -90;
                }
            }
            else
            {
                int toX = Math.Sign(targetPos.X - npc.Center.X);
                if (npc.velocity.X < 3 && npc.velocity.X > -3) npc.velocity.X += toX * 0.02f * velocityMult;
                if (toX > 0f && npc.velocity.X < 0 || toX < 0f && npc.velocity.X > 0) npc.velocity.X = npc.velocity.X * 0.98f;
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
