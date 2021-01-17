using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Items.Accessories;
using ElementsAwoken.Items.Elements.Desert;
using ElementsAwoken.Items.Elements.Fire;
using ElementsAwoken.Items.Elements.Frost;
using ElementsAwoken.Items.Elements.Void;
using ElementsAwoken.Items.Elements.Water;
using static Terraria.ModLoader.ModContent;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ElementsAwoken.Items.Elements.Sky
{
    public class SkylineWhirlwind : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 32;
            item.value = Item.sellPrice(0, 12, 0, 0);
            item.rare = 6;
            item.accessory = true;

            item.GetGlobalItem<EATooltip>().flyingBoots = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skyline Whirlwind");
            Tooltip.SetDefault("Reach speeds of up to 50mph\nGrants immunity to fire blocks\nTemporary immunity to lava\n10% increased wingtime\n10% increased wing speed\nDisable visuals to have normal wings");
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i &&
                        (player.armor[i].type == ItemID.HermesBoots ||
                        player.armor[i].type == ItemID.SpectreBoots ||
                        player.armor[i].type == ItemID.LightningBoots ||
                        player.armor[i].type == ItemID.FrostsparkBoots ||
                        player.armor[i].type == ItemType<DesertTrailers>() ||
                        player.armor[i].type == ItemType<FireTreads>() ||
                        player.armor[i].type == ItemType<FrostWalkers>() ||
                        player.armor[i].type == ItemType<AqueousWaders>() ||
                        player.armor[i].type == ItemType<VoidBoots>() ||
                        player.armor[i].type == ItemType<NyanBoots>()))
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
            modPlayer.flyingBoots = true;
            player.accRunSpeed = 9.8f;

            player.lavaMax += 420;
            player.noFallDmg = true;
            player.fireWalk = true;

            modPlayer.wingTimeMult *= 1.1f;
            modPlayer.wingAccXMult *= 1.1f;
            modPlayer.wingSpdXMult *= 1.1f;
            modPlayer.wingAccYMult *= 1.1f;
            modPlayer.wingSpdYMult *= 1.1f;

            bool hasWings = false;
            if (UI.BootWingsUI.itemSlot.Item.type != 0) hasWings = true;
            if (hasWings) player.wingTimeMax = (int)(player.wingTimeMax * 1.1f);
            if (player.velocity.Y != 0 && !hideVisual && !player.GetModPlayer<PlayerUtils>().hasVanityWings && hasWings) player.GetModPlayer<MyPlayer>().skylineFlying = true;
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            Main.NewText(speed);
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            base.VerticalWingSpeeds(player, ref ascentWhenFalling, ref ascentWhenRising, ref maxCanAscendMultiplier, ref maxAscentMultiplier, ref constantAscend);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod); 
            recipe.AddIngredient(ItemType<Materials.WingsClip>());
            recipe.AddIngredient(ItemID.LuckyHorseshoe);
            recipe.AddIngredient(null, "FireTreads", 1);
            recipe.AddIngredient(null, "SkyEssence", 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<Materials.WingsClip>());
            recipe.AddIngredient(ItemID.ObsidianHorseshoe);
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
