using ElementsAwoken.Projectiles.NPCProj.Obsidious;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.Bosses.Obsidious.Beneath
{
    public class SliderKey : ModNPC
    {
        private int parentID
        {
            get => (int)npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float aiTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float aiTimer2
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float aiTimer3
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious Key");
        }
        public override void SetDefaults()
        {
            npc.width = 24;
            npc.height = 26;

            npc.aiStyle = -1;

            npc.lifeMax = 1;
            npc.damage = 0;
            npc.knockBackResist = 0f;

            npc.immortal = true;
            npc.dontTakeDamage = true;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.netAlways = true;
            npc.gfxOffY = -4;

            npc.GivenName = " ";
        }
        public override void AI()
        {
            NPC parent = Main.npc[parentID];
            if (!parent.active || parent.type != ModContent.NPCType<MediumSlider>()) npc.active = false;
            Vector2 desiredLoc = new Vector2(parent.Center.X, parent.Center.Y + parent.height / 2 + 40);
            int stringLength = 80;

            Player player = Main.player[parent.target];
            npc.noTileCollide = EAUtils.FindNumTilesNearby(ModContent.TileType<Tiles.Objects.ObsidiousDoor>(), (int)player.Center.X / 16, (int)player.Center.Y / 16, 5) > 0;

            Vector2 to = parent.Bottom - npc.Center;
            to.Normalize();
            if (Vector2.Distance(npc.Center, parent.Bottom) > stringLength)
            {
                npc.Center = parent.Bottom + -to * stringLength;
                npc.velocity *= 0.96f;
                npc.velocity += to * 3;
            }

            Point keyWorld = npc.position.ToTileCoordinates();
            Tile t = Framing.GetTileSafely(keyWorld.X, keyWorld.Y);
            if (t.type == ModContent.TileType<Tiles.Objects.ObsidiousDoor>())
            {
                int frame = t.frameX / 18;
                WorldGen.KillTile(keyWorld.X, keyWorld.Y);
                for (int p = 0; p < 7; p++)
                {
                    WorldGen.PlaceTile(keyWorld.X - frame + p, keyWorld.Y, TileID.Platforms, true, true, style: 13);
                }
                npc.active = false;
                Main.PlaySound(SoundID.Unlock, npc.position);
                Main.PlaySound(SoundID.Item67, npc.position);
                for (int p = 1; p <= 2; p++)
                {
                    float strength = 3 + p * 1.2f;
                    int numDusts = p * 20;
                    EAUtils.OutwardsCircleDust(npc.Center, 6, numDusts, strength, randomiseVel: true);
                }
                parent.ai[0] = 999;
                parent.velocity = Vector2.Zero;
                for (int p = 1; p < Main.maxNPCs; p++)
                {
                    NPC other = Main.npc[p];
                    if (other.active && other.type == ModContent.NPCType<MiniSlider>())
                    {
                        other.ai[0] = 999;
                    }
                }
            }

            Vector2 toTarget = new Vector2(desiredLoc.X - npc.Center.X, desiredLoc.Y - npc.Center.Y);
            toTarget.Normalize();
            npc.velocity.X += toTarget.X * 0.25f;
            float dist = MathHelper.Distance(desiredLoc.X, npc.Center.X);
            if (dist < 20) npc.velocity.X *= 0.95f + (dist / 20f) * 0.05f;
            if (parent.ai[0] == 4)
            {
                npc.velocity.Y = parent.velocity.Y;
                if (Math.Abs(npc.velocity.Y) > 0.3f) aiTimer3 = parent.velocity.Y;
                aiTimer = 1;
            }
            else aiTimer = 0;
            if (aiTimer == 0 && aiTimer2 == 1) 
                npc.velocity.Y = aiTimer3 * 4f;
            aiTimer2 = aiTimer;
            
            npc.velocity.Y += 0.16f;

            NPCsGLOBAL.GoThroughPlatforms(npc);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            NPC parent = Main.npc[(int)npc.ai[0]];
            if (parent.active)
            {
                for (int k = -1; k <= 1; k += 2)
                {
                    Texture2D texture = Main.magicPixel;
                    Vector2 mountedCenter = parent.Center + new Vector2(parent.width / 3 * k, parent.height / 2 - 6);
                    Vector2 position = npc.Center;
                    float num1 = 1;
                    Vector2 vector2_4 = mountedCenter - position;
                    float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
                    bool flag = true;
                    if (float.IsNaN(position.X) && float.IsNaN(position.Y)) flag = false;
                    if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y)) flag = false;
                    while (flag)
                    {
                        if ((double)vector2_4.Length() < (double)num1 + 1.0)
                        {
                            flag = false;
                        }
                        else
                        {
                            Vector2 vector2_1 = vector2_4;
                            vector2_1.Normalize();
                            position += vector2_1 * num1;
                            vector2_4 = mountedCenter - position;
                            Color color = new Color(130, 85, 64) * ((float)drawColor.R / 255);
                            color.A = 255;
                            Main.spriteBatch.Draw(texture, position - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, 1, 1)), color, rotation, Vector2.One / 2f, 2f, SpriteEffects.None, 0.0f);
                        }
                    }
                }
            }
            return true;
        }
    }
}
