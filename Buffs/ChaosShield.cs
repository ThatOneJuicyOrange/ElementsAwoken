using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class ChaosShield : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Chaos Shield");
            Description.SetDefault("");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("ChaosRingShield")] == 0)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("ChaosRingShield"), 100, 0f, player.whoAmI);
            }
        }
    }
}