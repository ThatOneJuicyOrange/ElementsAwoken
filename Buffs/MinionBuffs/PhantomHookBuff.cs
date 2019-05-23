using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class PhantomHookBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Phantom Hook");
            Description.SetDefault("Dont get caught in the crossfire");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("PhantomHook0")] > 0)
            {
                modPlayer.phantomHook = true;
            }
            if (!modPlayer.phantomHook)
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