using ElementsAwoken.Projectiles.Other;
using ElementsAwoken.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ElementsAwoken.NPCs
{
    public class EsseneceAlerts : GlobalNPC
    {
        public override bool PreNPCLoot(NPC npc)
        {
            if (npc.type == NPCID.WallofFlesh)
            {
                if (!Main.hardMode)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Projectile.NewProjectile(Main.LocalPlayer.Center.X, Main.LocalPlayer.Center.Y, 0f, 0f, ModContent.ProjectileType<PlateauCinematicStarter>(), 0, 0, Main.myPlayer, 0f, 0f);
                    }
                    else
                    {
                        NetMessage.BroadcastChatMessage(NetworkText.FromLiteral("The Volcanic Plateau awakens"), Color.OrangeRed);
                    }
                }
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                if (!NPC.downedBoss1)
                {
                    PromptInfoUI.Visible = true;
                    string text = "The desert sands shift...";
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.Yellow);
                    else NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.Yellow);
                }
            }

            if (npc.type == NPCID.SkeletronHead)
            {
                if (!NPC.downedBoss3)
                {
                    string text = "Roars echo from the underworld...";
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.Orange);
                    else NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.Orange);
                }
            }
            if (npc.type == NPCID.SkeletronPrime)
            {
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && !NPC.downedMechBoss3)
                {
                    string text = "The sky wind howls...";
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.Cyan);
                    else NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.Cyan);
                }
            }
            if (npc.type == NPCID.TheDestroyer)
            {
                if (NPC.downedMechBoss1 && NPC.downedMechBoss3 && !NPC.downedMechBoss2)
                {
                    string text = "The sky wind howls...";
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.Cyan);
                    else NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.Cyan);
                }
            }
            if (npc.type == NPCID.Spazmatism && NPC.AnyNPCs(NPCID.Retinazer))
            {
                if (NPC.downedMechBoss2 && NPC.downedMechBoss3 && !NPC.downedMechBoss1)
                {
                    string text = "The sky wind howls...";
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.Cyan);
                    else NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.Cyan);
                }
            }
            if (npc.type == NPCID.Retinazer && NPC.AnyNPCs(NPCID.Spazmatism))
            {
                if (NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {
                    string text = "The sky wind howls...";
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.Cyan);
                    else NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.Cyan);
                }
            }
            if (npc.type == NPCID.Plantera)
            {
                if (!NPC.downedPlantBoss)
                {
                    string text = "You hear cracking coming from the ice...";
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.LightBlue);
                    else NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.LightBlue);
                }
            }

            if (npc.type == NPCID.DukeFishron)
            {
                if (!NPC.downedFishron)
                {
                    string text = "The wrath of Aqueous stirs the ocean";
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.MediumBlue);
                    else NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.MediumBlue);
                }
            }

            if (npc.type == NPCID.MoonLordCore)
            {
                if (!NPC.downedMoonlord)
                {
                    string text = "The depths of Terraria rumble...";
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.Red);
                    else NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.Red);
                }
            }
            return base.PreNPCLoot(npc);
        }
    }
}