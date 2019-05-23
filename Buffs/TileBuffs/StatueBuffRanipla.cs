using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.TileBuffs
{
    public class StatueBuffRanipla : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            DisplayName.SetDefault("Ranipla's Presence");
            Description.SetDefault("Worth is irrelevant, just do it.\n25% increased speed\n15% increased summon damage\n25% reduced jump height and wingtime");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed *= 1.25f;
            player.minionDamage *= 1.15f;

            Player.jumpHeight = (int)(Player.jumpHeight * 0.75f);
            player.wingTimeMax = (int)(player.wingTimeMax * 0.75f);
        }
    }
}