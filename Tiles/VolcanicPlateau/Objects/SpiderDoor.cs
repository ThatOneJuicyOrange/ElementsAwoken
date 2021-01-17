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
    public class SpiderDoor : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 48;
            npc.height = 80;

            npc.aiStyle = -1;

            npc.lifeMax = 1;

            npc.knockBackResist = 0f;

            npc.GetGlobalNPC<NPCs.VolcanicPlateau.PlateauNPCs>().tomeClickable = false;
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
            int erius = NPC.FindFirstNPC(NPCType<NPCs.VolcanicPlateau.Sulfur.Erius>());
            if (erius >= 0)
            {
                if (Main.npc[erius].ai[1] != 0 && npc.Top.Y + 10 < npc.ai[2]) npc.ai[0] = 2;
            }
            if (npc.ai[0] == 1 || MyWorld.downedErius)
            {
                // open
                npc.ai[3]++;
                if (npc.Bottom.Y > npc.ai[2])
                {
                    if (npc.soundDelay <= 0 && !MyWorld.downedErius)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/StoneSlide"));
                        npc.soundDelay = 600;
                    }
                    if (npc.ai[3] % 2 == 0) npc.position.Y -= 1;
                    npc.Center = new Vector2(npc.ai[1] + Main.rand.NextFloat(-2, 2), npc.Center.Y);
                }
                if (npc.ai[3] > 300)
                {
                    npc.ai[0] = 2;
                    npc.ai[3] = 0;
                }
            }
            else if (npc.ai[0] == 0)
            {
                npc.soundDelay = 0;

                Player player = Main.LocalPlayer;
                if (npc.Hitbox.Intersects(player.Hitbox))
                {
                    int pushDir = Math.Sign(player.Center.X - npc.Center.X);
                    float push = (player.Center.X + -pushDir * (player.width / 2)) - (npc.Center.X + pushDir * (npc.width / 2));
                    player.position.X -= push;
                    player.velocity.X = 0;
                }
                for (int p = 0; p < Main.maxProjectiles; p++)
                {
                    Projectile proj = Main.projectile[p];
                    if (npc.Hitbox.Intersects(proj.Hitbox) && proj.tileCollide && proj.active)
                    {
                        proj.Kill();
                    }
                }
                for (int p = 0; p < Main.maxNPCs; p++)
                {
                    NPC other = Main.npc[p];
                    if (npc.Hitbox.Intersects(other.Hitbox) && other.active && !other.noTileCollide)
                    {
                        int pushDir = Math.Sign(other.Center.X - npc.Center.X);
                        float push = (other.Center.X + -pushDir * (other.width / 2)) - (npc.Center.X + pushDir * (npc.width / 2));
                        other.position.X -= push;
                        other.velocity.X = 0;
                    }
                }
                npc.ai[1] = npc.Center.X;
                npc.ai[2] = npc.Top.Y + 10;
            }
            else
            {
                // close
                if (npc.Top.Y + 10 < npc.ai[2])
                {
                    if (npc.soundDelay <= 0)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/StoneSlide"));
                        npc.soundDelay = 600;
                    }
                    if (npc.Top.Y + 10 == npc.ai[2] - 1) Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/StoneFall"));
                    if (npc.ai[3] % 2 == 0) npc.position.Y += 1;
                    npc.Center = new Vector2(npc.ai[1] + Main.rand.NextFloat(-2, 2), npc.Center.Y);
                }
                else
                {
                    npc.ai[0] = 0;
                    npc.Center = new Vector2(npc.ai[1], npc.Center.Y);
                }
            }
        }
    }
}
