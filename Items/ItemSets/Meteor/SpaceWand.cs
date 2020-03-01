using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ElementsAwoken.Items.ItemSets.Meteor
{
    public class SpaceWand : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;

            item.damage = 26;
            item.knockBack = 3.75f;
            item.mana = 20;

            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = 5;
            item.UseSound = SoundID.Item15;

            item.noMelee = true;
            item.autoReuse = false;
            item.magic = true;
            item.channel = true;
            item.noUseGraphic = true;

            item.value = Item.sellPrice(0, 0, 40, 0);
            item.rare = 1;

            item.shoot = mod.ProjectileType("SpaceWand");
            item.shootSpeed = 6f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Space Wand");
            Tooltip.SetDefault("Hold down to charge an exploding meteor burst");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteoriteBar, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
            if (player.armor[0].type == ItemID.MeteorHelmet && player.armor[1].type == ItemID.MeteorSuit && player.armor[2].type == ItemID.MeteorLeggings) mult = 0;
        }
    }
}
