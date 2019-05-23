using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Weapons.Tier4
{
    public class ArcBlade : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 70;
            item.height = 70;

            item.damage = 55;
            item.knockBack = 5;

            item.useTurn = true;
            item.autoReuse = true;
            item.melee = true;
           
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;

            item.value = Item.buyPrice(0, 15, 0, 0);
            item.rare = 5;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arc Blade");
            Tooltip.SetDefault("Electrifies enemies\nRequires 8 energy to electrify enemies");
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>(mod);
            if (modPlayer.energy > 8)
            {
                modPlayer.energy -= 8;
                target.AddBuff(mod.BuffType("ElectrifiedNPC"), 120);
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteBar, 8);
            recipe.AddIngredient(null, "Capacitor", 1);
            recipe.AddIngredient(null, "GoldWire", 10);
            recipe.AddIngredient(null, "SiliconBoard", 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
