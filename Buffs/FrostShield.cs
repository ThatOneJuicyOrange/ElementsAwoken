using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs
{
    public class FrostShield : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Frost Shield");
            Description.SetDefault("");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.meleeDamage *= 1.5f;
            player.rangedDamage *= 1.5f;
            player.magicDamage *= 1.5f;
            player.minionDamage *= 1.5f;
            player.statDefense += 20;
            if (player.ownedProjectileCounts[mod.ProjectileType("FrostShield")] == 0)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("FrostShield"), 100, 0f, player.whoAmI);
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("FrostShield2")] == 0)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("FrostShield2"), 100, 0f, player.whoAmI);
            }
        }
    }
}