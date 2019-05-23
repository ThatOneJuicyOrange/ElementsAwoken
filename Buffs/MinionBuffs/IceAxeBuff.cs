using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class IceAxeBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Ice Axe");
            Description.SetDefault("It slices anything in its path");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("IceAxe")] > 0)
            {
                modPlayer.iceAxe = true;
            }
            if (!modPlayer.iceAxe)
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