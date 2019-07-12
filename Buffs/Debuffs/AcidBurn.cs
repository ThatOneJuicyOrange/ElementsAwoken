using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class AcidBurn : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Acid Burn");
            Description.SetDefault("It feels like liquid fire.");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCsGLOBAL>(mod).acidBurn = true;
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
            player.GetModPlayer<MyPlayer>(mod).acidBurn = true;
            if (Main.rand.Next(10) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 74, 0f, 0f, 150)];
                dust.velocity.X = Main.rand.NextFloat(-0.2f, 0.2f);
                dust.velocity.Y = Main.rand.NextFloat(0.2f, 1f);
                dust.scale = Main.rand.NextFloat(0.4f, 1.2f);
                dust.noGravity = false;
                dust.fadeIn = 1f;
            }
        }

    }
}