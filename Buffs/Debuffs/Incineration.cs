using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class Incineration : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Incineration");
            Description.SetDefault("2000 Kelvin\nDefence and attack damage is draining away\n50% potent in the Volcanic Plateau\nTwice as potent during an eruption");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCsGLOBAL>().acidBurn = true;
            if (Main.rand.Next(10) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 74, 0f, 0f, 150)];
                dust.velocity.X = Main.rand.NextFloat(-0.2f, 0.2f);
                dust.velocity.Y = Main.rand.NextFloat(0.2f, 1f);
                dust.scale = Main.rand.NextFloat(0.4f, 1.2f);
                dust.noGravity = false;
                dust.fadeIn = 1f;
            }
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().incineration = true;
            for (int s = 0; s < 3; s++)
            {
                Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, Main.rand.NextBool() ? 6 : 127, 0f, 0f)];
                dust.velocity.X = Main.rand.NextFloat(-0.2f, 0.2f);
                dust.velocity.Y = Main.rand.NextFloat(-5f, -1f);
                dust.scale = Main.rand.NextFloat(0.4f, 1.2f);
                dust.noGravity = true;
                dust.fadeIn = 1f;
                dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 32, 0f, 0f, 150)];
                dust.velocity.X = Main.rand.NextFloat(-0.2f, 0.2f);
                dust.velocity.Y = Main.rand.NextFloat(-9f, -4f);
                dust.scale = Main.rand.NextFloat(0.4f, 1.2f);
                dust.noGravity = true;
                dust.fadeIn = 1f;
            }
        }

    }
}