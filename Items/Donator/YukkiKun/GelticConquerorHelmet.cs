using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Donator.YukkiKun
{
    [AutoloadEquip(EquipType.Head)]
    public class GelticConquerorHelmet : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 1;

            item.defense = 2;

            item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Geltic Conqueror Helmet");
            Tooltip.SetDefault("Yukki-Kun's donator armor");
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("GelticConquerorBreastplate") && legs.type == mod.ItemType("GelticConquerorLeggings");
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowEOCShield = true;
            player.armorEffectDrawOutlinesForbidden = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases jump height\nNegates 75% of fall damage\nYou bounce when you land\nPress down to stop bouncing- this will also not negate fall damage";
            Point playerTopLeft = (player.TopLeft / 16).ToPoint();
            Point playerBottomRight = (player.BottomRight / 16).ToPoint();
            bool touchingCobweb = false;
            for (int k = playerTopLeft.X; k <= playerBottomRight.X; k++)
            {
                for (int l = playerTopLeft.Y; l <= playerBottomRight.Y; l++)
                {
                    Tile t = Framing.GetTileSafely(k, l);
                    if (t.type == TileID.Cobweb)
                    {
                        touchingCobweb = true;
                    }
                }
            }
            if (!touchingCobweb && !player.controlDown && !player.mount.Active)
            {
                if (player.velocity.Y <= player.gravity && player.oldVelocity.Y != 0 && player.oldVelocity.Y > 5) // > 5 so he doesnt bounce when jumping, only when falling
                {
                    player.velocity.Y = player.oldVelocity.Y * -0.98f;
                    player.velocity.X *= 1.2f;
                }
            }
            player.oldVelocity.Y = player.velocity.Y; // setting what the old velocity is after so it is the OLD velocity and not current

            player.jumpSpeedBoost += 2.0f;
            player.GetModPlayer<MyPlayer>().gelticConqueror = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Gel, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
