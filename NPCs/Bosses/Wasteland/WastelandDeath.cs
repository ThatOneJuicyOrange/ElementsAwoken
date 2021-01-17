using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Events;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Projectiles.NPCProj.Wasteland;
using ElementsAwoken.Items.Essence;

namespace ElementsAwoken.NPCs.Bosses.Wasteland
{    public class WastelandDeath : ModNPC
    {
        public override string Texture { get { return "ElementsAwoken/NPCs/Bosses/Wasteland/Wasteland"; } }

        private float aiTimer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }

        public override void SetDefaults()
        {
            npc.width = 140;
            npc.height = 130;

            npc.aiStyle = -1;

            npc.damage = 0;
            npc.defense = 0;
            npc.lifeMax = 10;          
            npc.knockBackResist = 0f;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath36;

            npc.noTileCollide = true;
            npc.behindTiles = true;
            npc.immortal = true;
            npc.dontTakeDamage = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wasteland");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void AI()
        {
            npc.velocity.Y = 3f;
            for (int k = 0; k < 10; k++)
            {
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, 32);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].velocity *= 0.1f;
            }
            if (aiTimer >= 180) npc.active = false;
        }
    }
}