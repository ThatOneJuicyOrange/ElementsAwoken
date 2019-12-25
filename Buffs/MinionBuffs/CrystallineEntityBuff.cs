using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class CrystallineEntityBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Crystalline Entity");
            Description.SetDefault("A faint hum radiates off the crystal");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("DisarrayEntity")] > 0)
            {
                modPlayer.crystalEntity = true;
            }
            if (!modPlayer.crystalEntity)
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