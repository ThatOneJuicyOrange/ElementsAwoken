using ElementsAwoken.Projectiles.Pets;
using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Buffs.PetBuffs
{
    public class CelestialWatcherBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Fallen Stargazer");
            Description.SetDefault("A fallen stargazer stares through you.");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<MyPlayer>().royalEye = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ProjectileType<CelestialWatcher>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ProjectileType<CelestialWatcher>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}