using ElementsAwoken.Projectiles.Minions;
using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class HearthMinionBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Hearth");
            Description.SetDefault("It radiates a soft warmth");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[ProjectileType<HearthMinion>()] > 0)  modPlayer.hearthMinion = true;
            if (!modPlayer.hearthMinion)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else player.buffTime[buffIndex] = 18000;
        }
    }
}