using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.DataStructures;

namespace ElementsAwoken.Items.Artifacts
{
    public class ElementalArcanum : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 11;
            item.value = Item.sellPrice(0, 25, 0, 0);
            item.accessory = true;

            item.GetGlobalItem<EATooltip>().artifact = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elemental Arcanum");
            Tooltip.SetDefault("Damage increased by 35%\n" +
                "Critical strike chance increased by 10%\n" +
                "Maximum mana increased by 150\n" +
                "Life regen increased\n" +
                "30% increased mining speed\n" +
                "Reduces damage taken by 5%\n" +
                "Has all the effects from the Elemental Artifacts");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(7, 4));
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10) // This allows the accessory to equip in Vanity slots with no reservations.
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    // We need "slot != i" because we don't care what is currently in the slot we will be replacing.
                    if (slot != i && player.armor[i].type == mod.ItemType("ChaosFlameFlask"))
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType("DiscordantSkull"))
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType("EtherealShell"))
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType("FrozenGauntlet"))
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType("GreatThunderTotem"))
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType("Nanocore"))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            //Discordant Skull
            player.pickSpeed -= 0.3f;
            /*player.rangedDamage *= 1.04f;
            player.magicDamage *= 1.04f;
            player.minionDamage *= 1.04f;*/
            player.endurance += 0.05f;
            //Chaos Flame Flask
            player.jumpSpeedBoost += 2.0f;
            /*player.meleeCrit += 5;
            player.magicCrit += 5;
            player.rangedCrit += 5;
            player.thrownCrit += 5;*/
            modPlayer.eaMagmaStone = true;
            player.fireWalk = true;
            //Great Thunder Totem
            modPlayer.lightningCloud = true;
            if (hideVisual)
            {
                modPlayer.lightningCloudHidden = true;
            }
            player.armorPenetration += 5;
            player.moveSpeed += 0.50f;
            //Frozen Gauntlet
            modPlayer.frozenGauntlet = true;
            player.longInvince = true;
            player.starCloak = true;
            /*player.meleeDamage *= 1.15f;
            player.rangedDamage *= 1.15f;
            player.magicDamage *= 1.15f;
            player.minionDamage *= 1.15f;*/
            //Ethereal Shell
            player.noKnockback = true;
            player.magicCuffs = true;
            player.manaMagnet = true;
            player.manaFlower = true;
            player.arcticDivingGear = true;
            /*player.statManaMax2 += 100;
            player.magicDamage *= 1.2f;*/
            //jellyfish necklace
            if (player.wet)
            {
                Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.9f, 0.2f, 0.6f);
            }
            //Nanocore
            /*player.meleeDamage *= 1.20f;
            player.rangedDamage *= 1.20f;
            player.magicDamage *= 1.20f;
            player.minionDamage *= 1.20f;*/
            player.pStone = true;
            player.accMerman = true;
            player.wolfAcc = true;
            if (hideVisual)
            {
                player.hideMerman = true;
                player.hideWolf = true;
            }
            //player.lifeRegen += 1;
            if (player.statLife <= (player.statLifeMax2 * 0.5f))
            {
                player.endurance += 0.15f;
            }
            //Combined
            player.meleeDamage *= 1.35f;
            player.rangedDamage *= 1.35f;
            player.magicDamage *= 1.40f;
            player.minionDamage *= 1.35f;
            player.meleeCrit += 10;
            player.magicCrit += 10;
            player.rangedCrit += 10;
            player.thrownCrit += 10;
            player.statManaMax2 += 150;
            player.lifeRegen += 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DiscordantSkull", 1);
            recipe.AddIngredient(null, "ChaosFlameFlask", 1);
            recipe.AddIngredient(null, "GreatThunderTotem", 1);
            recipe.AddIngredient(null, "FrozenGauntlet", 1);
            recipe.AddIngredient(null, "EtherealShell", 1);
            recipe.AddIngredient(null, "Nanocore", 1);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
