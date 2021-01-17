using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Items.Accessories;
using ElementsAwoken.Items.Elements.Desert;
using ElementsAwoken.Items.Elements.Fire;
using ElementsAwoken.Items.Elements.Frost;
using ElementsAwoken.Items.Elements.Sky;
using ElementsAwoken.Items.Elements.Water;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Elements.Void
{
    //[AutoloadEquip(EquipType.Wings)]
    public class VoidBoots : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 32;
            item.value = Item.sellPrice(0, 25, 0, 0);
            item.rare = 11;
            item.accessory = true;

            item.GetGlobalItem<EATooltip>().flyingBoots = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boots of the Void");
            Tooltip.SetDefault("Reach speeds of up to 65mph\nGreater mobility on ice\nWater and lava walking\nInfinite immunity to lava\nAllows the ability to climb walls and dash\nGives a chance to dodge attacks\n35% increased wingtime\n35% increased wing speed");
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
            modPlayer.eaDash = 1;

            player.accRunSpeed = 12.75f;

            player.iceSkate = true;
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            player.noFallDmg = true;
            player.blackBelt = true;

            modPlayer.wingTimeMult *= 1.35f;
            modPlayer.wingAccXMult *= 1.35f;
            modPlayer.wingSpdXMult *= 1.35f;
            modPlayer.wingAccYMult *= 1.35f;
            modPlayer.wingSpdYMult *= 1.35f;

            player.spikedBoots = 2;

            modPlayer.voidBoots = !hideVisual;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
           /* ascentWhenFalling = 0.65f;
            ascentWhenRising = 0.10f;
            //maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 2f;
            constantAscend = 0.135f;*/
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            /*speed = 12f;
            acceleration *= 3f;*/
        }

        public override bool WingUpdate(Player player, bool inUse)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (inUse && modPlayer.voidBoots)
            {
                int dust = Dust.NewDust(player.position, player.width, player.height, 127, 0, 0, 0, default);
                Main.dust[dust].scale *= 1.5f;
            }
            base.WingUpdate(player, inUse);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoidEssence", 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(null, "AqueousWaders");
            recipe.AddRecipeGroup("ElementsAwoken:LunarWings");
            recipe.AddIngredient(ItemID.MasterNinjaGear, 1);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
