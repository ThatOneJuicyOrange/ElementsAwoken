using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class Choking : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Choking");
            Description.SetDefault("Sulphur fills your lungs\nLife regeneration is minimal\nYou slowly lose breath\nCan be overcome with a Gas Mask");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            longerExpertDebuff = true;
            canBeCleared = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().choking = true;
        }

    }
}