using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.zVanilla
{
    class NinjaKatana : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 48;

            item.damage = 12;
            item.knockBack = 6;


            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;

            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 1;

            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 1;

            item.UseSound = SoundID.Item1;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ninja Katana");
            Tooltip.SetDefault("Right click to dash");
        }

        public override bool AltFunctionUse(Player player)
        {
            if (player.FindBuffIndex(mod.BuffType("DashCooldown")) == -1)
            {
                return true;
            }
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.direction == 1 )
                {
                    item.noMelee = true;
                    item.noUseGraphic = true;
                    player.velocity.X += 15f;
                    item.useStyle = 5;
                    player.AddBuff(mod.BuffType("DashCooldown"), 300);
                }
                if (player.direction == -1)
                {
                    item.noMelee = true;
                    item.noUseGraphic = true;
                    player.velocity.X -= 15f;
                    item.useStyle = 5;
                    player.AddBuff(mod.BuffType("DashCooldown"), 300);
                }
                player.GetModPlayer<MyPlayer>().ninjaDash = true;
                player.GetModPlayer<MyPlayer>().dashDustTimer = 60;

                for (int l = 0; l < 100; l++)
                {
                    int num = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 31, 0f, 0f, 100, default(Color), 2f);
                    Dust expr_A4_cp_0 = Main.dust[num];
                    expr_A4_cp_0.position.X = expr_A4_cp_0.position.X + (float)Main.rand.Next(-20, 21);
                    Dust expr_CB_cp_0 = Main.dust[num];
                    expr_CB_cp_0.position.Y = expr_CB_cp_0.position.Y + (float)Main.rand.Next(-20, 21);
                    Main.dust[num].velocity *= 0.4f;
                    Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                    Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(player.cWaist, player);
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                        Main.dust[num].noGravity = true;
                    }
                }
            }
            else
            {
                item.noMelee = false;
                item.noUseGraphic = false;
                item.useStyle = 1;
            }
            return base.CanUseItem(player);
        } 
    }
}
