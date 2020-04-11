using ElementsAwoken.Projectiles.Pets;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Buffs.PetBuffs
{
    public class AncientStellateBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Ancient Stellate");
            Description.SetDefault("It shimmers brightly");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<MyPlayer>().stellate = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ProjectileType<AncientStellate>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.Center,Vector2.Zero, ProjectileType<AncientStellate>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}