using System;
using System.Collections.Generic;
using ElementsAwoken.Buffs.Debuffs;
using ElementsAwoken.Items.Tech.Materials;
using ElementsAwoken.Items.Tech.Weapons.Tier2;
using ElementsAwoken.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.Tech.Weapons.Tier6
{
    public class Railgun : ModItem
    {
        public float heat = 0;
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 26; 
            
            item.damage = 270;
            item.knockBack = 3.5f;

            item.useAnimation = 40;
            item.useTime = 40;
            item.useStyle = 5;

            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 8;

            item.shootSpeed = 5f;
            item.shoot = 10;
            item.useAmmo = 97;
            item.GetGlobalItem<ItemEnergy>().energy = 18;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Railgun");
            Tooltip.SetDefault("Loves worms\nUsing the item charges heat\nGaining too much heat will damage the player and decrease the items effectiveness");
        }
        public override void UpdateInventory(Player player)
        {
            if (heat > 1800) heat = 1800;
            if (heat > 0)
            {
                heat--;
                if (player.wet) heat--;
                if (player.ZoneSnow) heat--;
            }
            if (heat < 0) heat = 0;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(SoundID.Item113, (int)player.position.X, (int)player.position.Y); 
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 70f;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            float damageScale = MathHelper.Lerp(1, 2, MathHelper.Clamp((float)modPlayer.aeroflakHits / 10,0,1));
            Projectile.NewProjectile(position.X, position.Y , speedX, speedY, ProjectileType<RailgunBeam>(), (int)(damage * damageScale), knockBack, player.whoAmI, 0.0f, 0.0f);
            if (heat > 1300)
            {
                player.AddBuff(BuffType<BurningHands>(), 180, false);
            }
            if (heat > 1700)
            {
                int amount = (int)(player.statLifeMax2 * 0.2f);
                player.statLife -= amount;
                if (player.statLife < 0) player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " burnt their face off"), 1, 1);
            }
            heat += 160;
            return false;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            if (heat > 1300)
            {
                mult = heat / 1800f;
            }
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-26, -2);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<Coilgun>(), 1);
            recipe.AddIngredient(ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ItemType<SiliconBoard>(), 1);
            recipe.AddIngredient(ItemType<Microcontroller>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
