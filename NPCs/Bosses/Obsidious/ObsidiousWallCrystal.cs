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
    public class ObsidiousWallCrystal : ModNPC
    {
        private float fadeTime = 15;
        public override void SetDefaults()
        {
            npc.width = 26;
            npc.height = 30;

            npc.aiStyle = -1;

            npc.lifeMax = 750;
            npc.damage = 0;
            npc.knockBackResist = 0f;

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.netAlways = true;
            npc.gfxOffY = -4;

            npc.HitSound = SoundID.DD2_WitherBeastCrystalImpact;

            NPCsGLOBAL.ImmuneAllEABuffs(npc);
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }

            bossBag = mod.ItemType("ObsidiousBag");
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0 && ModContent.GetInstance<Config>().debugMode && npc.ai[2] != 0)
            {
                for (int k = 0; k < Main.npc.Length; k++)
                {
                    NPC other = Main.npc[k];
                    if (other.active && other.type == npc.type)
                    {
                        other.life = 0;
                        other.checkDead();
                    }
                }
                Main.NewText("DEBUG: Removing other crystals for speed");
            }
        }
        public override void AI()
        {
            if (npc.ai[2] != 0)
            {
                npc.ai[1]++;
                if (npc.ai[1] > fadeTime)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 0);
                    npc.checkDead();
                }
            }
        }
        public override bool CheckDead()
        {
            if (npc.ai[1] < fadeTime)
            {
                Main.PlaySound(SoundID.DD2_WitherBeastDeath, npc.position);
                Main.PlaySound(SoundID.DD2_GhastlyGlaiveImpactGhost, npc.position);
                npc.life = npc.lifeMax;
                npc.ai[2] = 1;
                npc.dontTakeDamage = true;
                npc.netUpdate = true;
                npc.alpha = 255;
                return false;
            }
            return base.CheckDead();
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            NPC parent = Main.npc[(int)npc.ai[0]];
            if (parent.active && parent.type == ModContent.NPCType<ObsidiousArenaCrystal>())
            {
                Texture2D texture = ModContent.GetTexture("ElementsAwoken/NPCs/Bosses/Obsidious/ObsidiousChain");
                Vector2 position = npc.Center;
                Vector2 mountedCenter = parent.Center;
                Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
                Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
                float num1 = (float)texture.Height;
                Vector2 vector2_4 = parent.Center - position;
                float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
                bool flag = true;
                if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                    flag = false;
                if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                    flag = false;
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
                        //Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                        Color color = Color.Lerp(new Color(100, 100, 100, 0), new Color(90, 60, 100, 0), (float)(Math.Sin((float)MyWorld.generalTimer / 30f) + 1f) / 2f);// * (1f - npc.ai[1] / fadeTime);
                        if (npc.ai[1] != 0)
                        {
                            float max = Vector2.Distance(npc.Center, mountedCenter);
                            float curr = Vector2.Distance(npc.Center, position);
                            color *= curr / max - (npc.ai[1] / fadeTime);
                        }
                        for (int k = 0; k < 7; k++)
                        {
                            Vector2 newPos = position + new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
                            Main.spriteBatch.Draw(texture, newPos - Main.screenPosition, sourceRectangle, color, rotation, origin, 1f, SpriteEffects.None, 0f);
                        }
                    }
                }
            }
            return true;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}
