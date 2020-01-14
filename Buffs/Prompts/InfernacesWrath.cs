using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;

namespace ElementsAwoken.Buffs.Prompts
{
    public class InfernacesWrath : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Infernace's Wrath");
            Description.SetDefault("Meteors rain from the sky\nWhen approaching hell, Infernace's Guardians may attack the player\nDefeat Infernace to stop this effect\nDisable this effect in the config");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }
    }
}