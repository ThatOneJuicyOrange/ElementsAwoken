using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Tech.Accessories.Tier2
{
    public class RustedMechanism : ModItem
    {
        public bool hasShot = false;
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 2;    
            item.accessory = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rusted Mechanism");
            Tooltip.SetDefault("When you are hit, you shoot out a bunch of the first ammo you have\nConsumes 3 energy on use");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();

            int ammoType = 0;
            int whichSlot = 0;
            for (int i = 0; i < 4; i++)
            {
                if (ammoType == 0)
                {
                    int slot = 54 + i;
                    if (Main.LocalPlayer.inventory[slot].type != 0 && Main.LocalPlayer.inventory[slot].type != ItemID.JungleSpores)
                    {
                        ammoType = Main.LocalPlayer.inventory[slot].shoot;
                        whichSlot = slot;
                    }
                }
            }
            if (player.immune && ammoType != 0)
            {
                if (!hasShot)
                {
                    if (modPlayer.energy > 3)
                    {
                        modPlayer.energy -= 3;
                        float rotation = MathHelper.ToRadians(360);
                        float numberProjectiles = 8;
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(2, 2).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 2f;
                            int num1 = Projectile.NewProjectile(player.Center.X, player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ammoType, 10, 2f, 0);
                            Main.projectile[num1].noDropItem = true;
                        }
                        if (Main.LocalPlayer.inventory[whichSlot].consumable)
                        {
                            Main.LocalPlayer.inventory[whichSlot].stack--;
                        }
                    }
                    hasShot = true;
                }
            }
            else
            {
                hasShot = false;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(null, "CopperWire", 10);
            recipe.AddIngredient(null, "GoldWire", 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
