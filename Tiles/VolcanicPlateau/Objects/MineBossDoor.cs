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

namespace ElementsAwoken.Tiles.VolcanicPlateau.Objects
{
    public class MineBossDoor : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 144;
            npc.height = 22;

            npc.aiStyle = -1;

            npc.lifeMax = 1;

            npc.GetGlobalNPC<NPCs.VolcanicPlateau.PlateauNPCs>().tomeClickable = false;
            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.dontTakeDamage = true;
            npc.immortal = true;
            npc.friendly = true;
            npc.noGravity = true;
            npc.behindTiles = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void AI()
        {
            if (npc.ai[0] == 0) // open
            {
                //npc.ai[1] = npc.Left.X;
                npc.ai[2] = npc.Left.X;
                int boss = NPC.FindFirstNPC(NPCType<NPCs.VolcanicPlateau.Bosses.MineBoss>());
                if (boss >= 0)
                {
                    if (Main.npc[boss].ai[2] != 0) npc.ai[0] = 1;
                }
            }
            else if (npc.ai[0] == 1) // closing
            {
                if (npc.Right.X > npc.ai[2])
                {
                    npc.position.X -= 3;
                }
                else
                {
                    npc.ai[0] = 2;
                }
            }
            else if (npc.ai[0] == 2)// closed
            {
                Player player = Main.LocalPlayer;
                if (npc.Hitbox.Intersects(player.Hitbox))
                {
                    int pushDir = Math.Sign(player.Center.Y - npc.Center.Y);
                    float push = (player.Center.Y + -pushDir * (player.height / 2)) - (npc.Center.Y + pushDir * (npc.height / 2));
                    player.position.Y -= push;
                }
                for (int p = 0; p < Main.maxProjectiles; p++)
                {
                    Projectile proj = Main.projectile[p];
                    if (npc.Hitbox.Intersects(proj.Hitbox) && proj.tileCollide && proj.active && proj.type != ProjectileType<Projectiles.NPCProj.MineBoss.IgneousRockSpawner>() && proj.type != ProjectileType<Projectiles.NPCProj.MineBoss.IgneousRockProj>())
                    {
                        proj.Kill();
                    }
                }
                for (int p = 0; p < Main.maxNPCs; p++)
                {
                    NPC other = Main.npc[p];
                    if (npc.Hitbox.Intersects(other.Hitbox) && other.active && !other.noTileCollide)
                    {
                        int pushDir = Math.Sign(other.Center.Y - npc.Center.Y);
                        float push = (other.Center.Y + -pushDir * (other.height / 2)) - (npc.Center.Y + pushDir * (npc.height / 2));
                        other.position.Y -= push;
                        other.velocity.Y = 0;
                    }
                }
                int boss = NPC.FindFirstNPC(NPCType<NPCs.VolcanicPlateau.Bosses.MineBoss>());
                if (boss >= 0)
                {
                    if (Main.npc[boss].ai[2] == 0)
                    {
                        npc.ai[0] = 3;
                    }
                }
                else
                {
                    npc.ai[0] = 3;
                }
            }
            else // closing
            {
                if (npc.Left.X < npc.ai[2])
                {
                    npc.position.X += 1;
                }
                else npc.ai[0] = 0;
            }
        }
    }
}
