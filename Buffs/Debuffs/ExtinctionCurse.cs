using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class ExtinctionCurse : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Extinction Curse");
            Description.SetDefault("The forces of the abyss pull you deeper...");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCsGLOBAL>().extinctionCurse = true;
            int num1 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.PinkFlame);
            Main.dust[num1].scale = 2.9f;
            Main.dust[num1].velocity *= 3f;
            Main.dust[num1].noGravity = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().extinctionCurse = true;
            int num1 = Dust.NewDust(player.position, player.width, player.height, DustID.PinkFlame);
            Main.dust[num1].scale = 2.9f;
            Main.dust[num1].velocity *= 3f;
            Main.dust[num1].noGravity = true;
        }

    }
}