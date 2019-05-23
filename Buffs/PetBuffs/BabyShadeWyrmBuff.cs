using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PetBuffs
{
    public class BabyShadeWyrmBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Baby Shade Wyrm");
            Description.SetDefault("Unlike normal Shade Wyrms, it is not trying to trap you in the vast, endless void.");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<MyPlayer>(mod).babyShadeWyrm = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("BabyShadeWyrmHead")] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                int current = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("BabyShadeWyrmHead"), 0, 0f, Main.myPlayer);

                int previous = current;
                for (int k = 0; k < 5; k++)
                {
                    previous = current;
                    current = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("BabyShadeWyrmBody"), 0, 0f, Main.myPlayer, previous);
                }
                previous = current;
                current = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("BabyShadeWyrmTail"), 0, 0f, Main.myPlayer, previous);
                Main.projectile[previous].localAI[1] = (float)current;
                Main.projectile[previous].netUpdate = true;
            }
        }
    }
}