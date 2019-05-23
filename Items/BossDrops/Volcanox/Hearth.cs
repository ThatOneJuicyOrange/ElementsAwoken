using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Volcanox
{
    public class Hearth : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 170;
            item.magic = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 25;
            item.useAnimation = 25;
            Item.staff[item.type] = true;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(0, 80, 0, 0);
            item.rare = 11;
            item.mana = 5;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SpinningFlame");
            item.shootSpeed = 18f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hearth");
        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int i = Main.myPlayer;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            vector2.X = (float)Main.mouseX + Main.screenPosition.X;
            vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
            int numberProjectiles = 3;
            for (int num131 = 0; num131 < numberProjectiles; num131++)
            {
                Projectile.NewProjectile(vector2.X, vector2.Y, 0, 0, mod.ProjectileType("HearthP"), damage, 3f, i, 0f, 0f);
            }
            return false;
        }
    }
}
