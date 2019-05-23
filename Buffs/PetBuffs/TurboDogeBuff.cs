using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PetBuffs
{
    public class TurboDogeBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Turbo Doge");
            Description.SetDefault("Nyoooom");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<MyPlayer>(mod).turboDoge = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("TurboDoge")] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("TurboDoge"), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}