using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.TileBuffs
{
    public class StatueBuffGenihWat : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            DisplayName.SetDefault("Genih Wat's Presence");
            Description.SetDefault("15% increased magic damage\nIncreases Mana Regen by 5\nEnemies are more aggressive\nEnemies spawn more frequently");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.magicDamage *= 1.15f;
            player.manaRegen += 5;

            MyWorld.aggressiveEnemies = true;
        }
    }
}