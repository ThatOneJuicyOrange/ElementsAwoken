using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class KirovAirshipBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Kirov Airship");
            Description.SetDefault("Bombardiers to your stations.");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("KirovAirship")] > 0)
            {
                modPlayer.kirovAirship = true;
            }
            if (!modPlayer.kirovAirship)
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