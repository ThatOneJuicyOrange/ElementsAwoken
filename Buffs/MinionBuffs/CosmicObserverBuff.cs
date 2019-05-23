using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class CosmicObserverBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Cosmic Observer");
            Description.SetDefault("Its edges are insanely sharp");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("CosmicObserver")] > 0)
            {
                modPlayer.cosmicObserver = true;
            }
            if (!modPlayer.cosmicObserver)
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