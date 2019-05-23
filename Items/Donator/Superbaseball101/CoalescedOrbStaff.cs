using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.Superbaseball101
{
    public class CoalescedOrbStaff : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 50;
            item.mana = 10;
            item.knockBack = 1.25f;

            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = 1;
            item.UseSound = SoundID.Item44;

            item.noMelee = true;
            item.autoReuse = true;
            item.summon = true;

            item.value = Item.buyPrice(0, 20, 0, 0);
            item.rare = 7;
            item.useAnimation = 25;
            item.shoot = mod.ProjectileType("CoalescedOrb");
            item.shootSpeed = 10f;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Staff of the Coalesced Orb");
            Tooltip.SetDefault("Summons a Coalesced Orb to protect you\nOrbs take 3 minion slots\nSuperbaseball101's donator item");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MysticLeaf", 2);
            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.AddIngredient(ItemID.ShroomiteBar, 10);
            recipe.AddTile(TileID.BewitchingTable);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
