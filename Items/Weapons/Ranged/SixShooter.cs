using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Ranged
{
    public class SixShooter : ModItem
    {
        public int shotNum = 0;
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 28;

            item.damage = 55;
            item.knockBack = 5f;

            item.useAnimation = 18;
            item.useTime = 18;
            item.useStyle = 5;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.UseSound = SoundID.Item41;
            item.shootSpeed = 12f;
            item.shoot = 10;
            item.useAmmo = 97;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Six Shooter");
            Tooltip.SetDefault("Every 6th shot does 4x damage");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            shotNum++;
            if (shotNum == 5)
            {
                item.reuseDelay = 40;
            }
            else
            {
                item.reuseDelay = 0;
            }
            if (shotNum == 6)
            {
                Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/RevolverReload"));
            }
            if (shotNum > 6)
            {
                shotNum = 1;
            }

            int projDamage = shotNum == 6 ? damage * 4 : damage;
            CombatText.NewText(player.getRect(), shotNum == 6 ? Color.Purple : Color.LightPink, shotNum, false, false);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, projDamage, knockBack, player.whoAmI);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Revolver, 1);
            recipe.AddIngredient(ItemID.SoulofMight, 8);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
