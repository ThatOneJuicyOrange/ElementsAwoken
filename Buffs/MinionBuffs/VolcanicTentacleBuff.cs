using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class VolcanicTentacleBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Volcanic Tentacle");
            Description.SetDefault("It clings onto you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("VolcanicTentacle")] > 0)
            {
                modPlayer.volcanicTentacle = true;
            }
            if (!modPlayer.volcanicTentacle)
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