using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs.VolcanicPlateau.Bosses
{
    public class LavaSquid : ModNPC
    {
        private float createTentacles
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float direction
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float pulseAI
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float aiState
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        private float pulseMax = 45;
        public override void SetDefaults()
        {
            npc.width = 70;
            npc.height = 138;
            
            npc.aiStyle = -1;

            npc.damage = 120;
            npc.defense = 12;
            npc.lifeMax = 1200;
            npc.knockBackResist = 0.05f;

            npc.value = Item.buyPrice(0, 2, 0, 0);
            npc.noGravity = true;

            npc.HitSound = SoundID.NPCHit52;
            npc.DeathSound = SoundID.NPCDeath55;

            banner = npc.type;
            bannerItem = ModContent.ItemType<Items.Banners.InfernoSpiritBanner>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volkraken");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void FindFrame(int frameHeight)
        {

        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Player player = Main.player[npc.target];
            Vector2 eyePos = npc.Center + new Vector2(0, 40);

            Vector2 direction2 =  player.Center - eyePos;
            direction2.Normalize();

            Texture2D tex = ModContent.GetTexture("ElementsAwoken/NPCs/VolcanicPlateau/Bosses/LavaSquidPupil");
            Vector2 drawPos = eyePos + direction2 * 3 - Main.screenPosition;
            spriteBatch.Draw(tex, drawPos, null, drawColor, 0, tex.Size() / 2f, 1f, SpriteEffects.None, 0f);
        }

        public override void AI()
        {
            Lighting.AddLight(npc.Center, 1.0f, 0.2f, 0.7f);
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];

            if (createTentacles == 0)
            {
                for (int l = 0; l < 10; l++)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<LavaSquidTentacle>(), npc.whoAmI, npc.whoAmI, l);
                }
                createTentacles++;
            }
            int tilesAboveBlocks = 99999;
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
            if (tilesAboveBlocks < 8) npc.velocity.Y -= 0.04f;
            else
            {
                npc.velocity.Y += 0.04f;
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.8f);
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.OnFire, 400, true);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 6, hitDirection, -1f, 0, default(Color), 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 6, hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Items.Materials.Pyroplasm>(), Main.rand.Next(1, 4));
        }
    }
}