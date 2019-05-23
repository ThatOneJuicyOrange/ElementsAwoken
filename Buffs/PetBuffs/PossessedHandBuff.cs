using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Buffs.PetBuffs
{
    public class PossessedHandBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Possessed Hand");
            Description.SetDefault("It has noBody");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<MyPlayer>(mod).possessedHand = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("PossessedHand")] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("PossessedHand"), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}