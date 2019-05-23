using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.CosmicObserver
{
    public class CosmicShard : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = 50;
            item.rare = 4;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Shard");
            Tooltip.SetDefault("It resonates slightly");
        }
    }
}
