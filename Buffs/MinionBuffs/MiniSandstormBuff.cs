using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class MiniSandstormBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Miniature Sandstorm");
            Description.SetDefault("This strange entity will protect you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("MiniatureSandstorm")] > 0)
            {
                modPlayer.miniatureSandStorm = true;
            }
            if (!modPlayer.miniatureSandStorm)
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