using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.TileBuffs
{
    public class StatueBuffOinite : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            DisplayName.SetDefault("Oinite's Presence");
            Description.SetDefault("Perfectly balanced, like all things should be\nDoubles all buff and debuff duration");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>(mod).oiniteStatue = true;
        }
    }
}