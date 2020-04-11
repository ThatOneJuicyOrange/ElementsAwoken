using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Items.Elements.Desert;
using ElementsAwoken.Items.Elements.Fire;
using ElementsAwoken.Items.Elements.Frost;
using ElementsAwoken.Items.Elements.Void;
using ElementsAwoken.Items.Elements.Sky;
using ElementsAwoken.Items.Elements.Water;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
    public class NyanBoots : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 32;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boots of Nyan");
            Tooltip.SetDefault("Fly through space and time in style!\nGreater mobility on ice\nWater and lava walking\nInfinite immunity to lava\nAllows flight and slow fall\nAllows the ability to climb walls and dash\nGives a chance to dodge attacks");
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
                        player.armor[i].type == ItemType<SkylineWhirlwind>() ||
                        player.armor[i].type == ItemType<FrostWalkers>() ||
                        player.armor[i].type == ItemType<AqueousWaders>() ||
                        player.armor[i].type == ItemType<VoidBoots>()))
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
            modPlayer.eaDash = 2;

            player.accRunSpeed = 13.75f;

            player.rocketBoots = 3;

            player.spikedBoots = 2;

            player.wingTimeMax = 300;

            player.iceSkate = true;
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            player.noFallDmg = true;
            player.blackBelt = true;
        }
        public override void UpdateVanity(Player player, EquipType type)
        {
             player.GetModPlayer<MyPlayer>().nyanBoots = true;
            if (player.ownedProjectileCounts[mod.ProjectileType("NyanBootsTrail")] < 1 && player.GetModPlayer<MyPlayer>().nyanBoots)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("NyanBootsTrail"), 0, 0, player.whoAmI);
            }
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 13f;
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
            base.WingUpdate(player, inUse);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoiditeBar", 8);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            recipe.AddIngredient(null, "ElementalEssence", 5);
            recipe.AddIngredient(ItemID.RainbowDye, 1);
            recipe.AddIngredient(ItemID.RainbowBrick, 10);
            recipe.AddIngredient(null, "VoidBoots");
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
