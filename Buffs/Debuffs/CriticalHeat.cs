using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Debuffs
{
    public class CriticalHeat : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Critical Heat");
            Description.SetDefault("It is too hot to sustain Terrarian life during an eruption\nYou will gradually build up deadly heat and fire damage is far more potent\nEffects can be slowed or negated with fire protection");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            longerExpertDebuff = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().criticalHeat = true;
        }

    }
}