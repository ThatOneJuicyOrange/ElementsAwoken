using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Developer
{
    public class Neovirtuo : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 11;
            item.accessory = true;

            item.GetGlobalItem<EATooltip>().developer = true;
            item.GetGlobalItem<EARarity>().rare = 12;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Neovirtuo");
            Tooltip.SetDefault("Press Z to summon a portal at the cursor\nOccasionally fires homing projectiles from the player\nThe lower your health the less damage you take\nMana increased by 100\nMagic damage increased by 15%\n16% increased movement speed\nImmunity to most debuffs\nCreates a swirling galaxy\n12 defence\nAsterox effects\nRanipla's developer item");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.neovirtuoBonus = true;
            //asterox effects
            player.noKnockback = true;
            player.statDefense += 12;
            player.armorPenetration += 5;
            player.lifeRegen += 3;
            player.moveSpeed *= 1.2f;
            player.panic = true;
            if (!hideVisual)
            {
                if (player.ownedProjectileCounts[mod.ProjectileType("NeovirtuoShieldBase")] < 1)
                {
                    Projectile.NewProjectile(player.position.X, player.position.Y, 0f, 0f, mod.ProjectileType("NeovirtuoShieldBase"), 50, 10f, player.whoAmI, 0.0f, 0.0f);
                }
            }

            //unity effects
            player.statDefense += 5;
            if (player.statLife <= (player.statLifeMax2 * 0.9f) && player.statLife >= (player.statLifeMax2 * 0.9f))
            {
                player.endurance += 0.04f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife >= (player.statLifeMax2 * 0.7f))
            {
                player.endurance += 0.08f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.7f) && player.statLife >= (player.statLifeMax2 * 0.6f))
            {
                player.endurance += 0.16f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife >= (player.statLifeMax2 * 0.5f))
            {
                player.endurance += 0.20f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.5f) && player.statLife >= (player.statLifeMax2 * 0.4f))
            {
                player.endurance += 0.24f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife >= (player.statLifeMax2 * 0.3f))
            {
                player.endurance += 0.28f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.3f) && player.statLife >= (player.statLifeMax2 * 0.2f))
            {
                player.endurance += 0.32f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.2f) && player.statLife >= (player.statLifeMax2 * 0.1f))
            {
                player.endurance += 0.36f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.1f))
            {
                player.endurance += 0.40f;
            }

            player.statManaMax2 += 100;
            player.magicDamage *= 1.15f;
            player.moveSpeed *= 1.16f;
            player.noKnockback = true;
            player.fireWalk = true;
            player.buffImmune[46] = true;
            player.buffImmune[44] = true;
            player.buffImmune[33] = true;
            player.buffImmune[36] = true;
            player.buffImmune[30] = true;
            player.buffImmune[20] = true;
            player.buffImmune[32] = true;
            player.buffImmune[31] = true;
            player.buffImmune[35] = true;
            player.buffImmune[23] = true;
            player.buffImmune[22] = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddIngredient(null, "NeutronFragment", 8);
            recipe.AddIngredient(null, "VoiditeBar", 8);
            recipe.AddIngredient(null, "Unity", 1);
            recipe.AddIngredient(null, "Asterox", 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
