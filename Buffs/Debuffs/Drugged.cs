using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class Drugged : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Drugged");
            Description.SetDefault("You ate some fermented berries");
            Main.debuff[Type] = true;
            //Main.buffNoTimeDisplay[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }

    }
}