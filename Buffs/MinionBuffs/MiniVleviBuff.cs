using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.MinionBuffs
{
    public class MiniVleviBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Miniature Void Leviathan");
            Description.SetDefault("This Void Leviathan will protect you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("VleviHead")] > 0)
            {
                modPlayer.miniVlevi = true;
            }
            if (!modPlayer.miniVlevi)
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