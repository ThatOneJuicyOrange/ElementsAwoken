using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs
{
    public class EsseneceAlerts : GlobalNPC
    {
        public override bool PreNPCLoot(NPC npc)
        {
            if (npc.type == NPCID.EyeofCthulhu)
            {
                if (!NPC.downedBoss1)
                {
                    Main.NewText("You have activated a boss prompt! These can be disabled in the EA Config.", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B);
                }
            }
            return true;
        }
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.EyeofCthulhu)
            {
                if (!MyWorld.desertText)
                {
                    Main.NewText("The desert sands shift...", Color.Yellow.R, Color.Yellow.G, Color.Yellow.B);
                    MyWorld.desertText = true;
                }
            }

            if (npc.type == NPCID.SkeletronHead)
            {
                if (!MyWorld.fireText)
                {
                    Main.NewText("Roars echo from the underworld...", Color.Orange.R, Color.Orange.G, Color.Orange.B);
                    MyWorld.fireText = true;
                }
            }
            if (npc.type == NPCID.SkeletronPrime)
            {
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2)
                {
                    if (!MyWorld.skyText)
                    {
                        Main.NewText("The sky wind howls...", Color.Cyan.R, Color.Cyan.G, Color.Cyan.B);
                        MyWorld.skyText = true;
                    }
                }
            }
            if (npc.type == NPCID.TheDestroyer)
            {
                if (NPC.downedMechBoss1 && NPC.downedMechBoss3)
                {
                    if (!MyWorld.skyText)
                    {
                        Main.NewText("The sky wind howls...", Color.Cyan.R, Color.Cyan.G, Color.Cyan.B);
                        MyWorld.skyText = true;
                    }
                }
            }
            if (npc.type == NPCID.Spazmatism && NPC.AnyNPCs(NPCID.Retinazer))
            {
                if (NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {
                    if (!MyWorld.skyText)
                    {
                        Main.NewText("The sky wind howls...", Color.Cyan.R, Color.Cyan.G, Color.Cyan.B);
                        MyWorld.skyText = true;
                    }
                }
            }
            if (npc.type == NPCID.Retinazer && NPC.AnyNPCs(NPCID.Spazmatism))
            {
                if (NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {
                    if (!MyWorld.skyText)
                    {
                        Main.NewText("The sky wind howls...", Color.Cyan.R, Color.Cyan.G, Color.Cyan.B);
                        MyWorld.skyText = true;
                    }
                }
            }
            if (npc.type == NPCID.Plantera)
            {
                if (!MyWorld.frostText)
                {
                    Main.NewText("You hear cracking coming from the ice...", Color.LightBlue.R, Color.LightBlue.G, Color.LightBlue.B);
                    MyWorld.frostText = true;
                }
            }

            if (npc.type == NPCID.DukeFishron)
            {
                if (!MyWorld.waterText)
                {
                    //Main.NewText("The ocean bubbles...", Color.MediumBlue.R, Color.MediumBlue.G, Color.MediumBlue.B);
                    Main.NewText("The wrath of Aqueous stirs the ocean", Color.MediumBlue.R, Color.MediumBlue.G, Color.MediumBlue.B);
                    MyWorld.waterText = true;
                }
            }

            if (npc.type == NPCID.MoonLordCore)
            {
                if (!MyWorld.voidText)
                { 
                    Main.NewText("The depths of Terraria rumble...", Color.Red.R, Color.Red.G, Color.Red.B);
                    MyWorld.voidText = true;
                }
            }
        }
    }
}