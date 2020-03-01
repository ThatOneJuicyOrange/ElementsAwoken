using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Infernace
{
    public class InfernoVortex : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;
            
            item.damage = 32;
            item.mana = 5;

            item.useTime = 32;
            item.useAnimation = 32;

            Item.staff[item.type] = true;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.useStyle = 5;
            item.knockBack = 2;

            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 3;

            item.UseSound = SoundID.Item8;
            item.shoot = mod.ProjectileType("SpinningFlame");
            item.shootSpeed = 18f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inferno Vortex");
        }
        public override bool CanUseItem(Player player)
        {
            int max = 6;
            if (player.ownedProjectileCounts[mod.ProjectileType("SpinningFlame")] >= max)
            {
                return false;
            }
            else return true;
        }
    }
}
