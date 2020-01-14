using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class CorroderBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Corroder");
            Description.SetDefault("Don't try pet it.");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("CorroderMinion")] > 0)
            {
                modPlayer.corroder = true;
            }
            if (!modPlayer.corroder)
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