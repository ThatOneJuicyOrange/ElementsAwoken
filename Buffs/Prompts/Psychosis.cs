using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Buffs.Prompts
{
    public class Psychosis : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Psychosis");
            Description.SetDefault("The force of the void drags you down\n10 decreased defence and 5% damage reduction\nCauses hallucinations\nDefeat the Void Leviathan to stop this effect\nDisable this effect in the config");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 10;
            player.meleeDamage *= 0.95f;
            player.minionDamage *= 0.95f;
            player.rangedDamage *= 0.95f;
            player.magicDamage *= 0.95f;
            player.thrownDamage *= 0.95f;
        }
    }
}