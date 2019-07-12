using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
    public class NyanBoots : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 32;
            item.value = Item.buyPrice(1, 20, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boots of Nyan");
            Tooltip.SetDefault("Fly through space and time in style!\nGreater mobility on ice\nWater and lava walking\nInfinite immunity to lava\nAllows flight and slow fall\nAllows the ability to climb walls and dash\nGives a chance to dodge attacks");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            player.accRunSpeed = 25f;
            player.rocketBoots = 3;
            player.moveSpeed += 20f;
            player.iceSkate = true;
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            player.noFallDmg = true;
            player.dash = 1;
            player.blackBelt = true;
            player.spikedBoots = 1;
            player.spikedBoots = 2;
            player.wingTimeMax = 300;
            /*if (!hideVisual)
            {
                if (player.ownedProjectileCounts[mod.ProjectileType("NyanBootsTrail")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("NyanBootsTrail"), 0, 0, player.whoAmI);
                }
            }
            else
            {
                for (int k = 0; k < Main.projectile.Length; k++)
                {
                    Projectile other = Main.projectile[k];
                    if (other.type == mod.ProjectileType("NyanBootsTrail") && other.owner == player.whoAmI)
                    {
                        other.active = false;
                    }
                }
            }*/
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3.5f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 20f;
            acceleration *= 4f;
        }

        public override bool WingUpdate(Player player, bool inUse)
        {
            /*if (inUse)
            {
                for (int num447 = 0; num447 < 2; num447++)
                {
                    int dust = Dust.NewDust(player.position, player.width, player.height, 63, 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
                    Main.dust[dust].scale *= 2f;
                    int dust2 = Dust.NewDust(player.position, player.width, player.height, 63, 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
                    Main.dust[dust2].scale *= 1.7f;
                }
            }*/
            if (player.ownedProjectileCounts[mod.ProjectileType("NyanBootsTrail")] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("NyanBootsTrail"), 0, 0, player.whoAmI);
            }
            player.GetModPlayer<MyPlayer>(mod).nyanBoots = true;

            base.WingUpdate(player, inUse);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoiditeBar", 8);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddIngredient(null, "ElementalEssence", 5);
            recipe.AddIngredient(ItemID.IntenseRainbowDye, 1);
            recipe.AddIngredient(ItemID.RainbowBrick, 10);
            recipe.AddIngredient(null, "VoidBoots");
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
