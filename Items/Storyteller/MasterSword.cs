using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Storyteller
{
    public class MasterSword : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 60;
            item.knockBack = 2;

            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 1;

            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 6;

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("MasterSwordWave");
            item.shootSpeed = 16f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Master Sword");
            Tooltip.SetDefault("Hold right click to charge the blade");
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (modPlayer.masterSwordCharge > 0)
            {
                if (player.altFunctionUse != 2)
                {
                    for (int l = 0; l < 10; l++)
                    {
                        int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 135);
                        Main.dust[dust].noGravity = true;
                    }
                }
            }
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (player.altFunctionUse == 2)
            {
                if (modPlayer.masterSwordCharge >= 50)
                {
                    return false;

                }
                else
                {
                    return true;
                }
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (player.altFunctionUse == 2)
            {
                item.noMelee = true;
                item.channel = true;
                item.useTime = 4;
                item.useAnimation = 20;
                item.UseSound = SoundID.Item13;
                item.useStyle = 4;

                modPlayer.masterSwordCharge++;


                if (modPlayer.masterSwordCharge <= 5)
                {
                    modPlayer.masterSwordCountdown = 900;
                }

                Color color = Color.LightCyan;
                if (modPlayer.masterSwordCharge == 50)
                {
                    color = Color.Magenta;
                }
                if (modPlayer.masterSwordCharge % 10 == 0) // if it is divisible by 10
                {
                    CombatText.NewText(player.getRect(), color, modPlayer.masterSwordCharge, true, false);
                }

                for (int l = 0; l < 30; l++)
                {
                    int dust = Dust.NewDust(new Vector2(player.Center.X - 100, player.Center.Y), 200, 8, 135, 0f, 0f, 100, default(Color), 1.4f);
                    Main.dust[dust].noGravity = true;
                }
            }
            else
            {
                item.noMelee = false;
                item.channel = false;
                item.useTime = 16;
                item.useAnimation = 16;
                item.UseSound = SoundID.Item1;
                item.useStyle = 1;

                if(modPlayer.masterSwordCharge > 30)
                {
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
                }
            }
            return false;
        }
        public override void GetWeaponDamage(Player player, ref int damage)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            damage = 60 + (int)(modPlayer.masterSwordCharge * 1.5f);
        }
        public override void HoldItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.direction == 1) // 1 is right 
                {
                    player.itemRotation -= 0.785f;
                }
                if (player.direction == -1) // -1 is left
                {
                    player.itemRotation += 0.785f;
                }
            }
            else
            {

            }
        }
        /*public override Vector2? HoldoutOffset()
        {
            Player player = Main.player[Main.myPlayer];
            if (player.altFunctionUse == 2)
            {
                return new Vector2(0, -15);
            }
            else
            {
                return new Vector2(0, 0);
            }
        }*/
    }
}
