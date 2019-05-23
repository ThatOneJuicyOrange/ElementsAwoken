using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PetBuffs
{
    public class RoyalEyeBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Royal Eye");
            Description.SetDefault("It believes it is the next Celestial\nIts crown looks like it was made by a 3 year old");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<MyPlayer>(mod).royalEye = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("RoyalEye")] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("RoyalEye"), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}