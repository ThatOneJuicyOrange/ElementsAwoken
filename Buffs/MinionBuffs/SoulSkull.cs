using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class SoulSkull : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Soulskull");
            Description.SetDefault("Ascended from the void, the skull breaths Soulfire that inflicts Soul Inferno");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("SoulSkull")] > 0)
            {
                modPlayer.soulSkull = true;
            }
            if (!modPlayer.soulSkull)
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