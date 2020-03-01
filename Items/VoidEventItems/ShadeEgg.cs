using System;
using ElementsAwoken.Buffs.PetBuffs;
using ElementsAwoken.Projectiles;
using ElementsAwoken.Projectiles.Pets.BabyShadeWyrm;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.VoidEventItems
{
    public class ShadeEgg : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 30;

            item.damage = 0;

            item.useStyle = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.UseSound = SoundID.Item2;

            item.rare = 11;
            item.value = Item.sellPrice(0, 5, 0, 0);

            item.noMelee = true;

            item.shoot = ModContent.ProjectileType<AnarchyWave>(); // shoot isnt run if its a pet projectile
            item.shootSpeed = 1f;
            item.buffType = ModContent.BuffType<BabyShadeWyrmBuff>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shade Egg");
            Tooltip.SetDefault("Summons a Baby Shade Wyrm");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<BabyShadeWyrmHead>()] <= 0)
            {
                int current = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<BabyShadeWyrmHead>(), 0, 0f, Main.myPlayer);

                    int previous = current;
                    for (int k = 0; k < 5; k++)
                    {
                        previous = current;
                        current = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<BabyShadeWyrmBody>(), 0, 0f, Main.myPlayer, previous);
                    }
                    previous = current;
                    current = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<BabyShadeWyrmTail>(), 0, 0f, Main.myPlayer, previous);
                    Main.projectile[previous].localAI[1] = (float)current;
                    Main.projectile[previous].netUpdate = true;
                
            }
            return false;
        }
        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}
