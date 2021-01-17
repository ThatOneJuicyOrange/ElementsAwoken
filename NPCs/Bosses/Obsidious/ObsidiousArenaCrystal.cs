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

namespace ElementsAwoken.NPCs.Bosses.Obsidious
{
    public class ObsidiousArenaCrystal : ModNPC
    {
        //shockwave
        private int rippleCount = 2;
        private int rippleSize = 15;
        private int rippleSpeed = 30;
        private float distortStrength = 200f;

        private float laserAI = 0;
        private Vector2 startPos = Vector2.Zero;
        private int parentID
        {
            get => (int)npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float smashCount
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float shockwave
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float visuals
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 72;

            npc.aiStyle = -1;

            npc.lifeMax = 100;
            npc.damage = 0;
            npc.defense = 0;
            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.netAlways = true;
            npc.immortal = true;
            npc.dontTakeDamage = true;

            NPCsGLOBAL.ImmuneAllEABuffs(npc);
            // all vanilla buffs
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidious Crystal");
        }
        public override void AI()
        {
            if (startPos == Vector2.Zero) startPos = npc.Center;
            npc.GivenName = " ";
            NPC obsidious = Main.npc[parentID];
            if (smashCount >= 5)
            {
                int duration = 180;
                float amount = MathHelper.Lerp(0, 4, visuals / duration);
                npc.Center = startPos + Main.rand.NextVector2Square(-amount, amount);
                visuals++;
                if (visuals == duration)
                {
                    obsidious.ai[0] = 5000;
                    npc.alpha = 255;

                }
            }
            else
            {
                smashCount = 0;
                if (obsidious.active)
                {
                    if (obsidious.ai[1] == 3)
                    {
                        if (visuals < 30) visuals++;
                        if (obsidious.getRect().Intersects(npc.getRect()))
                        {
                            Main.PlaySound(SoundID.DD2_WitherBeastDeath, npc.position);

                            obsidious.ai[1] = 999;
                            smashCount++;
                            if (smashCount >= 5)
                            {
                                visuals = 0;
                                shockwave = -1;
                            }
                            else
                            {
                                visuals = 90;
                                shockwave = 1;
                            }
                            Vector2 away = obsidious.Center - npc.Center;
                            away.Normalize();
                            obsidious.velocity = away * 5;
                        }
                    }
                    else if (visuals > -30) visuals--;
                    if (obsidious.ai[1] == 20)
                    {
                        if (laserAI == 0)
                        {
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 1, 0, ModContent.ProjectileType<ObsidiousDeathray>(), 50, 0f, Main.myPlayer, 0, npc.whoAmI);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -1, 0, ModContent.ProjectileType<ObsidiousDeathray>(), 50, 0f, Main.myPlayer, 0, npc.whoAmI);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 1, ModContent.ProjectileType<ObsidiousDeathray>(), 50, 0f, Main.myPlayer, 0, npc.whoAmI);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, -1, ModContent.ProjectileType<ObsidiousDeathray>(), 50, 0f, Main.myPlayer, 0, npc.whoAmI);
                            laserAI++;
                        }
                    }
                }
                else npc.active = false;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (smashCount < 5)
            {
                NPC obsidious = Main.npc[parentID];
                if (obsidious.active)
                {
                    if (obsidious.ai[1] == 3)
                    {
                        Texture2D texture = Main.magicPixel;
                        Vector2 position = npc.Center;
                        Vector2 mountedCenter = obsidious.Center;
                        float num1 = 1;
                        Vector2 vector2_4 = mountedCenter - position;
                        float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
                        bool flag = true;
                        if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                            flag = false;
                        if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                            flag = false;
                        float num = 0;
                        while (flag)
                        {
                            if ((double)vector2_4.Length() < (double)num1 + 1.0)
                            {
                                flag = false;
                            }
                            else
                            {
                                num++;

                                Vector2 vector2_1 = vector2_4;
                                vector2_1.Normalize();
                                position += vector2_1 * num1;
                                vector2_4 = mountedCenter - position;
                                for (int k = 0; k < 3; k++)
                                {
                                    float waveLength = 20f + k * 10;
                                    //if (k == 1) waveLength *= -1;
                                    //Vector2 offset = new Vector2((float)Math.Sin(num / waveLength - (float)MyWorld.generalTimer / 10f) * (float)Math.Sin(num / (waveLength * 1.5f)) * 20, 0);
                                    Vector2 offset = new Vector2((float)Math.Sin(num / waveLength - (float)MyWorld.generalTimer / 10f) * (float)Math.Sin((float)MyWorld.generalTimer / 40f + k) * 20, 0);
                                    Color color = Color.Lerp(new Color(245, 212, 171), new Color(237, 168, 221), (float)Math.Sin(num / 20f - (float)MyWorld.generalTimer / 5f + k));
                                    Main.spriteBatch.Draw(texture, position - Main.screenPosition + offset.RotatedBy(rotation), new Rectangle?(new Rectangle(0, 0, 1, 1)), color * Math.Min(1, visuals / 30), rotation, Vector2.One / 2f, 2f, SpriteEffects.None, 0.0f);
                                }
                            }
                        }

                    }
                }
            }
            return true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (smashCount < 5)
            {
                NPC obsidious = Main.npc[parentID];
                if (obsidious.active)
                {
                    if (obsidious.ai[1] != 3)
                    {
                        Main.spriteBatch.End();
                        Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

                        var center = npc.Center - Main.screenPosition;
                        if (Main.LocalPlayer.gravDir == -1) center.Y -= (npc.Center.Y - Main.LocalPlayer.Center.Y) * 2;
                        float intensity = (float)Math.Sin(MyWorld.generalTimer / 60f);
                        int width = 150;
                        Vector2 size = new Vector2(1.5f * width, 1f * width);

                        Color color = Color.Lerp(new Color(245, 212, 171), new Color(240, 182, 210), intensity) * 0.7f * MathHelper.Clamp(-visuals / 30, 0, 1);

                        DrawData drawData = new DrawData(TextureManager.Load("Images/Misc/Perlin"), center, new Rectangle(0, 0, (int)size.X, (int)size.Y), color, npc.rotation, size / 2, npc.scale * (1f + intensity * 0.05f), SpriteEffects.None, 0);
                        GameShaders.Misc["ForceField"].UseColor(new Vector3(1f + intensity * 0.5f));
                        GameShaders.Misc["ForceField"].Apply(new DrawData?(drawData));
                        drawData.Draw(Main.spriteBatch);
                        Main.spriteBatch.End();
                        Main.spriteBatch.Begin();
                    }
                }
            }
            if (Main.hasFocus || Main.netMode != 0)
            {
                if (shockwave == 1 || shockwave == -1)
                {
                    if (!Filters.Scene["Shockwave"].IsActive())
                    {
                        Filters.Scene.Activate("Shockwave", npc.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(npc.Center);
                    }
                    shockwave++;
                }
                else if (shockwave > 1)
                {
                    float progress = shockwave / 180f;
                    Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f)).UseColor(rippleCount, rippleSize, rippleSpeed * progress * 4);
                    shockwave++;
                }
                else if (shockwave < -1)
                {
                    float progress = 1 - (shockwave / -180f);
                    Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f)).UseColor(rippleCount, rippleSize, rippleSpeed * progress * 4);
                    shockwave--;
                }
                if (shockwave == 180 || shockwave == -180)
                {
                    Filters.Scene["Shockwave"].Deactivate();
                    shockwave = 0;
                }
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}
