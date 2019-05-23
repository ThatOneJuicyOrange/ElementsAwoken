using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Weapons.Summon
{
    public class SoulburnerStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 52;

            item.damage = 60;
            item.mana = 10;
            item.knockBack = 3;

            item.noMelee = true;
            item.summon = true;

            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;
            item.UseSound = SoundID.Item44;

            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 6;

            item.shoot = mod.ProjectileType("SoulSkull");
            item.shootSpeed = 7f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soulburner Staff");
            Tooltip.SetDefault("Summons a corrupt skull to breathe soul flames");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.Ectoplasm, 8);
            recipe.AddIngredient(ItemID.Bone, 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
