using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class EnchantedTrio : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Enchanted Trio");
            Description.SetDefault("The enchanted weapons of Terraira defend you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("EnchantedTrio0")] > 0)
            {
                modPlayer.enchantedTrio = true;
            }
            if (!modPlayer.enchantedTrio)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}