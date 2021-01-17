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
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.NPCs.VolcanicPlateau.Sulfur
{
    public class EriusBaby : ModNPC
    {
        public override string Texture { get { return "ElementsAwoken/NPCs/VolcanicPlateau/Sulfur/Erius"; } }
        public override void SetDefaults()
        {
            npc.width = 124;
            npc.height = 52;

            npc.aiStyle = -1;

            npc.defense = 20;
            npc.lifeMax = 100;
            npc.damage = 15;
            npc.knockBackResist = 0f;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(0, 0, 0, 50);
            npc.knockBackResist = 0f;

            npc.scale *= 0.5f;
            npc.lavaImmune = true;
            npc.noGravity = false;

            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffType<Buffs.Debuffs.AcidBurn>()] = true;

            //npc.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            npc.GetGlobalNPC<PlateauNPCs>().tomeText = "";
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sulfuric Spider");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += (int)Math.Abs(npc.velocity.X);
            if (Math.Abs(npc.velocity.Y) < 0.02f)
            {
                if (npc.frameCounter > 15)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
                if (npc.frame.Y > frameHeight * 3)
                {
                    npc.frame.Y = 0;
                }
            }
            else
            {
                npc.frame.Y = frameHeight * 4;
            }
        }
        public override void NPCLoot()
        {
             //if (Main.rand.NextBool())Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Items.Placeable.Tiles.Plateau.SulfuricSedimentItem>(), Main.rand.Next(1, 3));
        }

        public override void AI()
        {
            npc.spriteDirection = npc.direction;
            if (npc.direction == 0) npc.direction = 1;
            if (Math.Abs(npc.velocity.X) < 0.02f) npc.direction = -npc.direction;
            npc.velocity.X = npc.direction * npc.ai[1];
        }
    }
}
