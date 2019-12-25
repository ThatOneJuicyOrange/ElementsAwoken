using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PetBuffs
{
    public class ChamchamRatBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Chamcham Rat");
            Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<MyPlayer>().chamchamRat = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("ChamchamRat")] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("ChamchamRat"), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}