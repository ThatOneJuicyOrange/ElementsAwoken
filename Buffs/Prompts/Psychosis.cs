using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Buffs.Prompts
{
    public class Psychosis : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Psychosis");
            Description.SetDefault("The force of the void drags you down\n10 decreased defence and 5% damage reduction\nCauses hallucinations\nDefeat the Void Leviathan to stop this effect");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (Main.rand.Next(20) == 0)
            {
                int num1 = Dust.NewDust(player.position, player.width, player.height, 14);
                Main.dust[num1].scale = 1.5f;
                Main.dust[num1].velocity *= 3f;
                Main.dust[num1].noGravity = true;
            }
            player.statDefense -= 10;
            player.meleeDamage *= 0.95f;
            player.minionDamage *= 0.95f;
            player.rangedDamage *= 0.95f;
            player.magicDamage *= 0.95f;
            player.thrownDamage *= 0.95f;
            if (Main.rand.Next(2000) == 0)
            {
                int choice = Main.rand.Next(12);
                if (choice == 0)
                {
                    Main.NewText("Death will consume all.", Color.Purple.R, Color.Purple.G, Color.Purple.B);
                    Main.NewText("Void Leviathan has awoken!", Color.MediumPurple.R, Color.MediumPurple.G, Color.MediumPurple.B);
                    Main.PlaySound(15, (int)player.Center.X, (int)player.Center.Y, 0, 1f, 0f);
                }
                else if (choice == 1)
                {
                    Main.NewText("Impending doom approaches...", Color.PaleGreen.R, Color.PaleGreen.G, Color.PaleGreen.B);

                }
                else if (choice == 2)
                {
                    Main.NewText("You're not ready for this", Color.Red.R, Color.Red.G, Color.Red.B);
                }
                else if (choice == 3)
                {
                    Main.NewText("Leave", Color.Red.R, Color.Red.G, Color.Red.B);
                }
                else if (choice == 4)
                {
                    Main.PlaySound(4, (int)player.Center.X, (int)player.Center.Y, 62, 1f, 0f);
                }
                else if (choice == 5)
                {
                    Main.PlaySound(4, (int)player.Center.X, (int)player.Center.Y, 59, 1f, 0f);
                }
                else if (choice == 6)
                {
                    Main.PlaySound(4, (int)player.Center.X, (int)player.Center.Y, 51, 1f, 0f);
                }
                else if (choice == 7)
                {
                    Main.PlaySound(15, (int)player.Center.X, (int)player.Center.Y, 0, 1f, 0f);
                }
                else if (choice == 8)
                {
                    Main.PlaySound(15, (int)player.Center.X, (int)player.Center.Y, 2, 1f, 0f);
                }
                else if (choice == 9)
                {
                    Main.PlaySound(29, (int)player.Center.X, (int)player.Center.Y, 105, 1f, 0f);
                }
                else if (choice == 10)
                {
                    Main.PlaySound(29, (int)player.Center.X, (int)player.Center.Y, 104, 1f, 0f);
                }
                else if (choice == 11)
                {
                    int guide = NPC.FindFirstNPC(NPCID.Guide);
                    if (guide >= 0 && Main.rand.Next(5) == 0)
                    {
                        Main.NewText(Main.npc[guide].GivenName + "the Guide was slain...", Color.Red.R, Color.Red.G, Color.Red.B);
                    }
                }
            }
            if (Main.rand.Next(1500) == 0)
            {
                int add1 = 0;
                int choice = Main.rand.Next(2);
                if (choice == 0)
                {
                    add1 = Main.rand.Next(750, 2000);
                }
                else if (choice == 1)
                {
                    add1 = Main.rand.Next(-2000, -750);
                }

                int type = 0;
                int choice2 = Main.rand.Next(6);
                if (choice2 == 0)
                {
                    type = 524; // ghoul
                }
                else if (choice2 == 1)
                {
                    type = 524; // ghoul

                    //type = 480; // medusa // can hurt u so dont do this one
                }
                else if (choice2 == 2)
                {
                    type = 258; // ladybug
                }
                else if (choice2 == 3)
                {
                    type = 93; // giant bat
                }
                else if (choice2 == 4)
                {
                    type = 78; // mummy
                }
                else if (choice2 == 5)
                {
                    type = 34; // cursed skull
                }
                int random = NPC.NewNPC((int)player.Center.X + add1, (int)player.Center.Y - 800, type);
                NPC npc = Main.npc[random];
                npc.color = new Color(66, 66, 66);
                npc.alpha = 200;
                npc.damage = 0;
                npc.lifeMax = 10;
                npc.life = 10;
                npc.DeathSound = SoundID.NPCDeath6;
            }
        }
    }
}