using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Prompts
{
    public class ScorpionBreakout : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Scorpion Breakout");
            Description.SetDefault("Scorpions infest terraria\nDefeat Wasteland to stop this effect\nDisable this effect in the config");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }
    }
}