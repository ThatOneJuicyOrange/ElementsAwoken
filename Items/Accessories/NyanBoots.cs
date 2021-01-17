using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Items.Elements.Desert;
using ElementsAwoken.Items.Elements.Fire;
using ElementsAwoken.Items.Elements.Frost;
using ElementsAwoken.Items.Elements.Void;
using ElementsAwoken.Items.Elements.Sky;
using ElementsAwoken.Items.Elements.Water;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ElementsAwoken.Items.Accessories
{
    public class NyanBoots : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 32;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.accessory = true;

            item.GetGlobalItem<EATooltip>().flyingBoots = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boots of Nyan");
            Tooltip.SetDefault("Reach speeds of up to 70mph\nGreater mobility on ice\nWater and lava walking\nInfinite immunity to lava\nAllows the ability to climb walls and dash\nGives a chance to dodge attacks\n50% increased wingtime\n50% increased wing speed\nDisable visuals to have normal wings");
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
                        player.armor[i].type == ItemType<VoidBoots>()))
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
            modPlayer.eaDash = 2;

            player.accRunSpeed = 13.75f;

            player.rocketBoots = 3;

            player.spikedBoots = 2;

            player.iceSkate = true;
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            player.noFallDmg = true;
            player.blackBelt = true;

            modPlayer.wingTimeMult *= 1.5f;
            modPlayer.wingAccXMult *= 1.5f;
            modPlayer.wingSpdXMult *= 1.5f;
            modPlayer.wingAccYMult *= 1.5f;
            modPlayer.wingSpdYMult *= 1.5f;

            if (!player.GetModPlayer<PlayerUtils>().hasVanityWings && !hideVisual) player.GetModPlayer<MyPlayer>().nyanBoots = true;
            if (player.ownedProjectileCounts[ProjectileType<Projectiles.NyanBootsTrail>()] < 1 && player.GetModPlayer<MyPlayer>().nyanBoots) Projectile.NewProjectile(player.Center, Vector2.Zero, ProjectileType<Projectiles.NyanBootsTrail>(), 0, 0, player.whoAmI);
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 13f;
            acceleration *= 4f;
        }

        public override bool WingUpdate(Player player, bool inUse)
        {
            /*if (inUse)
            {
                for (int num447 = 0; num447 < 2; num447++)
                {
                    int dust = Dust.NewDust(player.position, player.width, player.height, 63, 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
                    Main.dust[dust].scale *= 2f;
                    int dust2 = Dust.NewDust(player.position, player.width, player.height, 63, 0, 0, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
                    Main.dust[dust2].scale *= 1.7f;
                }
            }*/
            base.WingUpdate(player, inUse);
            return false;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            spriteBatch.End();
            spriteBatch.Begin(default, BlendState.Additive);

            Texture2D texture = GetTexture("ElementsAwoken/Extra/LightBeam");
            float texScale = 0.6f;
            float y = 8f * scale;
            float x = 2f ;
            float num = 6;
            float num1 = MyWorld.generalTimer / 500f;

            for (int l = 0; l < num; l++)
            {
                float distance = 6.28f / num;
                float rot = (float)MyWorld.generalTimer / 30;
                Vector2 value28 = (Vector2.UnitY.RotatedBy(rot + (float)l * distance, default(Vector2)) * new Vector2(x, y));
                Vector2 pos = position;
                pos += value28;

                Color color = Color.Red;
                if (l == 1) color = Color.Orange;
                else if (l == 2) color = Color.Yellow;
                else if (l == 3) color = Color.LightGreen;
                else if (l == 4) color = Color.Blue;
                else if (l == 5) color = Color.Purple;

                spriteBatch.Draw(texture, pos + new Vector2(20, frame.Height / 2), null, new Color(color.R, color.G, color.B), 1.57f, texture.Size() / 2, new Vector2(1.75f,0.6f) * texScale, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
            spriteBatch.Begin();

            spriteBatch.Draw(Main.itemTexture[item.type], position - new Vector2(6, 0), frame, drawColor, 0, origin, scale, SpriteEffects.None, 0f);

            return false;
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            spriteBatch.End();
            spriteBatch.Begin(default, BlendState.Additive);
            Texture2D itemTex = Main.itemTexture[item.type];
            Texture2D texture = GetTexture("ElementsAwoken/Extra/LightBeam");
            float texScale = 0.6f;
            float y = 8f;
            float x = 2f;
            float num = 6;
            float num1 = MyWorld.generalTimer / 500f;

            for (int l = 0; l < num; l++)
            {
                float distance = 6.28f / num;
                float rot = (float)MyWorld.generalTimer / 30;
                Vector2 value28 = (Vector2.UnitY.RotatedBy(rot + (float)l * distance, default(Vector2)) * new Vector2(x, y));
                Vector2 pos = item.position;
                pos += value28;

                Color color = Color.Red;
                if (l == 1) color = Color.Orange;
                else if (l == 2) color = Color.Yellow;
                else if (l == 3) color = Color.LightGreen;
                else if (l == 4) color = Color.Blue;
                else if (l == 5) color = Color.Purple;

                spriteBatch.Draw(texture, pos + new Vector2(35, itemTex.Height / 2) - Main.screenPosition, null, new Color(color.R, color.G, color.B), 1.57f, texture.Size() / 2, new Vector2(1.75f, 1.6f) * texScale, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
            spriteBatch.Begin();

            spriteBatch.Draw(itemTex, item.position - new Vector2(6, 0) - Main.screenPosition, null, lightColor, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);

            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VoiditeBar", 8);
            recipe.AddIngredient(null, "Pyroplasm", 50);
            //recipe.AddIngredient(null, "ElementalEssence", 5);
            recipe.AddIngredient(ItemID.RainbowDye, 1);
            recipe.AddIngredient(ItemID.RainbowBrick, 10);
            recipe.AddIngredient(null, "VoidBoots");
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
