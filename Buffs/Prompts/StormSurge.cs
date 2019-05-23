using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Prompts
{
    public class StormSurge : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Storm Surge");
            Description.SetDefault("Waternados sprout from the ground\nRain happens more frequently\nDefeat Aqueous to stop this effect");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }
    }
}