using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class EndlessTears : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Endless Tears");
            Description.SetDefault("The sadness pulls you down");  
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCsGLOBAL>().endlessTears = true;
            int num1 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.BlueCrystalShard);
            Main.dust[num1].scale = 0.5f;
            Main.dust[num1].velocity *= 0f;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().endlessTears = true;
            int num1 = Dust.NewDust(player.position, player.width, player.height, DustID.BlueCrystalShard);
            Main.dust[num1].scale = 0.5f;
            Main.dust[num1].velocity *= 0f;
        }

    }
}