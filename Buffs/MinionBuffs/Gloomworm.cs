using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class Gloomworm : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Gloomworm");
            Description.SetDefault("The Gloomworm protects you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("GloomwormHead")] > 0)
            {
                modPlayer.gWorm = true;
            }
            if (!modPlayer.gWorm)
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