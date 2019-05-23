using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class FireElementalBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Fire Elemental");
            Description.SetDefault("It serves you willingly");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("FireElemental")] > 0)
            {
                modPlayer.fireElemental = true;
            }
            if (!modPlayer.fireElemental)
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