using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Azana
{
    public class InfectionHeart : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;

            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.accessory = true;
            item.consumable = true;

            item.maxStack = 999;

            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 1;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("InfectionHeart");
            item.shootSpeed = 7f;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infection Heart");
            Tooltip.SetDefault("A dismal fusion of technology and magic...\nReactivate by using the item\nThrow ores nearby to convert them into Chaotron Ore");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.talkToAzana = true;
        }
    }
}
