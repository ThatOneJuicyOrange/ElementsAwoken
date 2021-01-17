using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.NPCs.VolcanicPlateau.Lake
{
    public class VoidbrokenScorchfin : ModNPC
    {
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCType<InsidiousScorchfin>());
            npc.width = 212;
            npc.height = 48;
            npc.lifeMax = (int)(npc.lifeMax * 1.5f);
            npc.defense = (int)(npc.defense * 1.5f);
            npc.damage = (int)(npc.damage * 1.5f);
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "Corrupted Voidfin are voidbroken versions of insidious scorchfins that populate the volcanic lake. These are not to be taken lightly and can spear an unwary adventurer with ease.";
            npc.GetGlobalNPC<PlateauNPCs>().voidBroken = true;
            npc.GetGlobalNPC<PlateauNPCs>().counterpart = NPCType<InsidiousScorchfin>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupted Voidfin");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax /= 2;
            npc.damage /= 2;
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            //player.AddBuff(BuffType<Buffs.Debuffs.Incineration>(), 300, false);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("NPCs/VolcanicPlateau/Lake/" + GetType().Name + "_Glow");
            Rectangle frame = new Rectangle(0, npc.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY) + new Vector2(0, 4), frame, Color.White, npc.rotation, origin, npc.scale, effects, 0.0f);
        }
        public override void NPCLoot()
        {
            // Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Drakonite"), Main.rand.Next(1, 3));
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (!npc.lavaWet && npc.ai[2] == 1) npc.velocity.Y *= 0.5f;// to stop the lava jitter
            return base.PreDraw(spriteBatch, drawColor);
        }     
        public override void AI()
        {
            InsidiousScorchfin.ScorchfinAI(npc,1.5f);
        }
    }
}
