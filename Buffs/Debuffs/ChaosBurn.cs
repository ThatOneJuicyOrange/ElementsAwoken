using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class ChaosBurn : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Chaos Burn");
            Description.SetDefault("Your cells are being torn to shreds");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCsGLOBAL>(mod).extinctionCurse = true;
            int num1 = Dust.NewDust(npc.position, npc.width, npc.height, 127);
            Main.dust[num1].scale = 1.9f;
            Main.dust[num1].velocity *= 2f;
            Main.dust[num1].noGravity = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>(mod).extinctionCurse = true;
            int num1 = Dust.NewDust(player.position, player.width, player.height, 127);
            Main.dust[num1].scale = 1.9f;
            Main.dust[num1].velocity *= 2f;
            Main.dust[num1].noGravity = true;
        }

    }
}