using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Wasteland
{
    public class Stinger : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50; 
            
            item.damage = 14;
            item.knockBack = 2;
            item.mana = 16;

            item.useTime = 4;
            item.useAnimation = 12;
            item.reuseDelay = 24;
            item.useStyle = 5;

            Item.staff[item.type] = true;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 2;

            item.shoot = mod.ProjectileType("WastelandStingerFriendly");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scepter of the Tortured");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(SoundID.Item17, (int)player.position.X, (int)player.position.Y);
            return true;
        }
    }
}
