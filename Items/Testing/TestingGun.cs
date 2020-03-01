using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Testing
{
    public class TestingGun : ModItem
    {
        public int mode = 0;

        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26;

            item.damage = 65;
            item.knockBack = 3.5f;

            item.useAnimation = 13;
            item.useTime = 13;
            item.useStyle = 5;

            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.value = Item.buyPrice(0, 0, 0, 0);
            item.rare = 0;

            item.UseSound = SoundID.Item12;
            item.shootSpeed = 15f;
            item.shoot = mod.ProjectileType("SuperGrenade");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Testing Gun");
            Tooltip.SetDefault("hit or miss, guess they never miss");
        }
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int npcType = 519;
            if (mode == 0)
            {
                npcType = 519;
            }
            else if (mode == 1)
            {
                npcType = mod.NPCType("SolarFragment");
            }
            if (mode < 2)
            {
                int npc = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 60, npcType, player.whoAmI);
                Vector2 newVelocity = new Vector2(speedX, speedY);
                Main.npc[npc].velocity = newVelocity;
                Main.npc[npc].damage = damage;
            }
            else
            {
                Projectile.NewProjectile(position.X, position.Y - 60, speedX, speedY, mod.ProjectileType("SolarFragmentProj"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("ShockwaveCreator"), damage, knockBack, player.whoAmI, 10, 9);
            return false;
        }*/
       /* public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                mode++;
                if (mode >= 3)
                {
                    mode = 0;
                }
                string text = "";
                switch (mode)
                {
                    case 0:
                        text = "Vanilla";
                        break;
                    case 1:
                        text = "Modded";
                        break;
                    case 2:
                        text = "Projectile";
                        break;
                    default:
                        return base.CanUseItem(player);
                }
                Main.NewText(text, Color.White.R, Color.White.G, Color.White.B);
            }
            else
            {
                return true;
            }
            return base.CanUseItem(player);
        }*/
    }
}
