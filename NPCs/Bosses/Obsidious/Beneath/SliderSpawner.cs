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
    public class SliderSpawner : ModNPC
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        private float inactive
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float spawn
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float aiTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private float aiTimer2
        {
            get => npc.ai[3];
            set => npc.ai[3] = value;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slider Spawner");
        }
        public override void SetDefaults()
        {
            npc.width = 16;
            npc.height = 16;

            npc.aiStyle = -1;

            npc.lifeMax = 500;
            npc.knockBackResist = 0f;

            npc.immortal = true;
            npc.dontTakeDamage = true;
            
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;

            npc.GivenName = " ";
        }
        public override void AI()
        {
            if (spawn == 0)
            {
                int num = Main.expertMode ? MyWorld.awakenedMode ? 7 : 5 : 3;
                int alreadyActive = 0;
                for (int p = 1; p < Main.maxNPCs; p++)
                {
                    NPC other = Main.npc[p];
                    if (other.active && other.type == ModContent.NPCType<MiniSlider>())
                    {
                        alreadyActive++;
                    }
                }
                for (int p = 0; p < num - alreadyActive; p++)
                {
                    int type = ModContent.NPCType<MiniSlider>();
                    if (p == (int)(num - alreadyActive) / 2 && !NPC.AnyNPCs(ModContent.NPCType<MediumSlider>()) && (NPC.downedPlantBoss || ModContent.GetInstance<Config>().debugMode)) type = ModContent.NPCType<MediumSlider>();

                    NPC slider = Main.npc[NPC.NewNPC((int)npc.Center.X - 600 + (1200 / num) * p, (int)npc.Center.Y + 344, type)];
                    if (inactive == 1) slider.ai[0] = 999;
                    slider.ai[1] = Main.rand.Next(-120, 0);
                    slider.netUpdate = true;
                }
                spawn++;
                if (inactive == 1) spawn++; // to make it not do anything unless the player gets far enough and it despawns
            }
            else if (spawn == 1)
            {
                aiTimer++;
                if (aiTimer > 1800)
                {
                    spawn = 0;
                    aiTimer = 0;
                }
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return false;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}
