using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.TileBuffs
{
    public class StatueBuffOrange : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            DisplayName.SetDefault("ThatOneJuicyOrange_ Presence");
            Description.SetDefault("15% increased damage\n50% increased damage taken\nIncreases the cooldown of healing potions");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.meleeDamage *= 1.15f;
            player.magicDamage *= 1.15f;
            player.minionDamage *= 1.15f;
            player.rangedDamage *= 1.15f;
            player.thrownDamage *= 1.15f;

            player.GetModPlayer<MyPlayer>(mod).damageTaken *= 1.5f;
            player.potionDelayTime = (int)(player.potionDelayTime * 1.25);
            player.restorationDelayTime = (int)(player.restorationDelayTime * 1.25);
        }
    }
}