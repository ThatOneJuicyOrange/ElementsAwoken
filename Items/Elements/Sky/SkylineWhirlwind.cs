using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Elements.Sky
{
    [AutoloadEquip(EquipType.Wings)]
    public class SkylineWhirlwind : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 32;
            item.value = Item.buyPrice(0, 25, 0, 0);
            item.rare = 6;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skyline Whirlwind");
            Tooltip.SetDefault("Crazy speed!\nGrants immunity to fire blocks\nTemporary immunity to lava\nAllows flight and slow fall");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.accRunSpeed = 12f;
            player.rocketBoots = 3;
            player.moveSpeed += 5f;
            player.fireWalk = true;
            player.lavaMax += 420;
            player.wingTimeMax = 160;
            player.noFallDmg = true;
        }
        public override bool WingUpdate(Player player, bool inUse)
        {
            if (inUse)
            {
                float num98 = 16f;
                int num99 = 0;
                while ((float)num99 < num98)
                {
                    Vector2 vector11 = Vector2.UnitX * 0f;
                    vector11 += -Vector2.UnitY.RotatedBy((double)((float)num99 * (6.28318548f / num98)), default(Vector2)) * new Vector2(1f, 4f);
                    vector11 = vector11.RotatedBy((double)player.velocity.ToRotation(), default(Vector2));
                    int num100 = Dust.NewDust(player.Center, 0, 0, 31, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num100].scale = 1f;
                    Main.dust[num100].noGravity = true;
                    Main.dust[num100].position = new Vector2(player.position.X + 10, player.position.Y + 3f + 30);
                    Main.dust[num100].velocity = player.velocity * 0f + vector11.SafeNormalize(Vector2.UnitY) * 1f;
                    num99++;
                }
            }
            base.WingUpdate(player, inUse);
            return false;
        }
    public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("WingGroup");
            recipe.AddIngredient(ItemID.LuckyHorseshoe, 1);
            recipe.AddIngredient(null, "FireTreads", 1);
            recipe.AddIngredient(null, "SkyEssence", 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("WingGroup");
            recipe.AddIngredient(ItemID.ObsidianHorseshoe, 1);
            recipe.AddIngredient(null, "FireTreads", 1);
            recipe.AddIngredient(null, "SkyEssence", 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
