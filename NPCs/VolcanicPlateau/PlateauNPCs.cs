using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.NPCs.VolcanicPlateau;
using ElementsAwoken.NPCs.VolcanicPlateau.Sulfur;

namespace ElementsAwoken.NPCs.VolcanicPlateau
{
    public class PlateauNPCs : GlobalNPC
    {
        public string tomeText = "";
        public float floatScale = 1f;
        public bool voidBroken = false;
        public bool tomeClickable = true;
        public int counterpart = 0;

        public static int voidBreakChance = 25;
        public override bool InstancePerEntity => true;
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo) // run on clients only
        {
            MyPlayer modPlayer = spawnInfo.player.GetModPlayer<MyPlayer>();
            if (modPlayer.zonePlateau) 
            {
                pool.Clear();
                pool.Add(NPCType<CinderSlime>(), 0.1f);
                if (MyWorld.awakenedPlateau)
                {
                    pool.Add(NPCType<RuinedCaldera>(), 0.05f);
                    pool.Add(NPCType<PlateauMimic>(), 0.002f); // needs to be roughly 0.5% chance
                    if (modPlayer.zoneSulphur)
                    {
                        if (NPC.downedMoonlord)
                        {
                            pool.Add(NPCType<ToxipotAnt>(), 0.05f);
                        }
                        else
                        {
                            pool.Add(NPCType<SulfuricBurster>(), 0.05f);
                        }
                    }
                }
                else
                {
                    pool.Add(NPCType<ForgottenWisp>(), 0.1f);
                    pool.Add(NPCType<CharredMaggot>(), 0.1f);
                }
            }
        }
        //Changing the spawn rate
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.zonePlateau)
            {
                if (!MyWorld.awakenedPlateau)
                {
                    spawnRate = 400;
                    maxSpawns = 15;
                } 
                else
                {
                    spawnRate = 200;
                    maxSpawns = 50;
                }
            }
        }
        public override void AI(NPC npc)
        {
            if (EAWorldGen.generatedPlateaus && MyWorld.plateauWeather == 2 && !npc.noGravity)
            {
                Vector2 plateauPos = new Vector2(EAWorldGen.plateauLoc.X, EAWorldGen.plateauLoc.Y);
                Vector2 npcTile = npc.position / 16;
                if (npcTile.X > plateauPos.X - 20 && npcTile.Y > plateauPos.Y - 20 && npcTile.X < plateauPos.X + EAWorldGen.plateauWidth + 20)
                {
                    if (npc.velocity.Y < 0) npc.velocity.Y -= 0.2f * floatScale;
                    if (npc.velocity.Y > 0) npc.velocity.Y -= 0.26f * floatScale;
                }
            }
        }
        public static void TryVoidbreak(NPC npc, int voidbrokenID)
        {
            if (Main.rand.NextBool(voidBreakChance) || MyWorld.plateauWeather == 3)
            {
                npc.active = false;
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, voidbrokenID);
            }
        }
    }
}