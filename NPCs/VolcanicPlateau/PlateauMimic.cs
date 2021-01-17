using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.NPCs.VolcanicPlateau
{
    public class PlateauMimic : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 28;
            npc.height = 44;
            npc.aiStyle = 87;
            npc.damage = 90;
            npc.defense = 34;
            npc.lifeMax = 3500;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.value = 30000f;
            npc.knockBackResist = 0.1f;
            npc.buffImmune[20] = true;
            npc.buffImmune[24] = true;
            npc.buffImmune[39] = true;
            npc.rarity = 2;

            animationType = 475;

            NPCID.Sets.TrailCacheLength[npc.type] = 10;
            NPCID.Sets.TrailingMode[npc.type] = 2;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "Volcanic mimics are powerful soul - imbued chests that have absorbed souls of bright, the primordial essence of inferno.They can leap at foes and have the ability to deflect projectiles.";
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volcanic Mimic");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BigMimicHallow];
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if ((int)npc.ai[0] == 4 || npc.ai[0] == 5f || npc.ai[0] == 6f)
            {
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (npc.spriteDirection == 1) spriteEffects = SpriteEffects.FlipHorizontally;
                for (int num237 = 1; num237 < npc.oldPos.Length; num237++)
                {
                    Vector2[] arg_10968_0_cp_0 = npc.oldPos;
                    Color color64 = drawColor;
                    color64.R = (byte)(0.5 * (double)color64.R * (double)(10 - num237) / 20.0);
                    color64.G = (byte)(0.5 * (double)color64.G * (double)(10 - num237) / 20.0);
                    color64.B = (byte)(0.5 * (double)color64.B * (double)(10 - num237) / 20.0);
                    color64.A = (byte)(0.5 * (double)color64.A * (double)(10 - num237) / 20.0);
                    Texture2D tex = Main.npcTexture[npc.type];
                    Main.spriteBatch.Draw(tex, new Vector2(npc.oldPos[num237].X - Main.screenPosition.X + (float)(npc.width / 2) - (float)tex.Width * npc.scale / 2f + npc.Center.X * npc.scale, npc.oldPos[num237].Y - Main.screenPosition.Y + (float)npc.height - (float)tex.Height * npc.scale / (float)Main.npcFrameCount[npc.type] + 4f +  npc.Center.Y * npc.scale), new Rectangle?(npc.frame), color64, npc.rotation,  npc.Center, npc.scale, spriteEffects, 0f);
                }
            }
            return true;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            int num = 0;
            int num584 = 31;
            if (npc.life > 0)
            {
                int num585 = 0;
                while ((double)num585 < damage / (double)npc.lifeMax * 50.0)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, num584, 0f, 0f, 0, default(Color), 1f);
                    num = num585;
                    num585 = num + 1;
                }
                return;
            }
            for (int num586 = 0; num586 < 20; num586 = num + 1)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, num584, 0f, 0f, 0, default(Color), 1f);
                num = num586;
            }
            int num587 = Gore.NewGore(npc.Center, new Vector2((float)hitDirection, 0f), 61, npc.scale);
            Gore gore = Main.gore[num587];
            gore.velocity *= 0.3f;
            num587 = Gore.NewGore(npc.Center, new Vector2((float)hitDirection, 0f), 62, npc.scale);
            gore = Main.gore[num587];
            gore.velocity *= 0.3f;
            num587 = Gore.NewGore(npc.Center, new Vector2((float)hitDirection, 0f), 63, npc.scale);
            gore = Main.gore[num587];
            gore.velocity *= 0.3f;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffType<Buffs.Debuffs.Incineration>(), 300, false);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
           /* Texture2D texture = mod.GetTexture("NPCs/VolcanicPlateau/" + GetType().Name + "_Glow");
            Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, 4), frame, Color.White, npc.rotation, origin, npc.scale, effects, 0.0f);*/
        }
        public override void NPCLoot()
        {
            // Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Drakonite"), Main.rand.Next(1, 3));
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GreaterHealingPotion, Main.rand.Next(5, 11));
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GreaterManaPotion, Main.rand.Next(5, 16));
        }
    }
}
