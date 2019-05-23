using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.Other
{
    public class FlareShield : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Flare Shield");
            Description.SetDefault("");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("FlareShield")] == 0)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("FlareShield"), 0, 0f, player.whoAmI);
            }
        }
    }
}