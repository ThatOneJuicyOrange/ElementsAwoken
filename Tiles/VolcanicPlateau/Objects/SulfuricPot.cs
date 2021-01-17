using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Tiles.VolcanicPlateau.Objects
{
    public class SulfuricPot : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileLighted[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Origin = new Point16(0,1);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Pot");
            dustType = 54;
            AddMapEntry(new Color(100, 100, 100), name);
        }
        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
        {
            offsetY = 2;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Main.PlaySound(13, new Vector2(i, j) * 16);
            for (int k = 0; k < 8; k++)
            {
                Dust dust1 = Main.dust[Dust.NewDust(new Vector2(i, j) * 16, 32, 32, 49, 0, -1, 0, new Color(195, 222, 155), 0.5f)];
                Dust dust2 = Main.dust[Dust.NewDust(new Vector2(i, j) * 16, 32, 32, 74, 0, 0, 0, default, 0.35f)];
                dust2.fadeIn = 1f;
                dust2.velocity *= 0.5f;
            }
            for (int g = 0; g < 3; g++)
            {
                Gore.NewGore(new Vector2((int)i * 16 + Main.rand.Next(-10, 10), (int)j * 16 + Main.rand.Next(-10, 10)), new Vector2(-1, 1), mod.GetGoreSlot("Gores/SulfuricPot" + g));
            }

            if (Main.rand.NextBool(365))
            {
                if (Main.netMode != 1) Projectile.NewProjectile((float)(i * 16 + 16), (float)(j * 16 + 16), 0f, -12f, 518, 0, 0f, Main.myPlayer, 0f, 0f);
            }
            else if (Main.rand.NextBool(45) || (Main.rand.NextBool(45) && Main.expertMode))
            {
                int potionitem = Main.rand.Next(new int[] { ItemID.SpelunkerPotion, ItemID.FeatherfallPotion, ItemID.ManaRegenerationPotion, ItemID.ObsidianSkinPotion, ItemID.MagicPowerPotion, ItemID.InvisibilityPotion,
                ItemID.HunterPotion, ItemID.GravitationPotion, ItemID.ThornsPotion, ItemID.WaterWalkingPotion, ItemID.BattlePotion, ItemID.HeartreachPotion, ItemID.TitanPotion });
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem(i * 16, j * 16, 32, 32, potionitem, Main.rand.Next(1, 3));
                }
            }
            else if (Main.netMode == NetmodeID.Server && Main.rand.NextBool(30)) Item.NewItem(i * 16, j * 16, 32, 32, ItemID.WormholePotion, 1);
            else
            {
                int num9 = Main.rand.Next(8);
                if (Main.expertMode) num9--;
                if (num9 == 0 && Main.player[(int)Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].statLife < Main.player[(int)Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].statLifeMax2)
                {
                    // hearts
                    Item.NewItem(i * 16, j * 16, 16, 16, ItemID.Heart, 1, false, 0, false, false);
                    if (Main.rand.NextBool(2)) Item.NewItem(i * 16, j * 16, 16, 16, ItemID.Heart, 1, false, 0, false, false);
                    if (Main.expertMode)
                    {
                        if (Main.rand.NextBool(2)) Item.NewItem(i * 16, j * 16, 16, 16, ItemID.Heart, 1, false, 0, false, false);
                        if (Main.rand.NextBool(2)) Item.NewItem(i * 16, j * 16, 16, 16, ItemID.Heart, 1, false, 0, false, false);
                    }
                }
                else if (num9 == 1 && Main.player[(int)Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].statMana < Main.player[(int)Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].statManaMax2)
                {
                    // star
                    Item.NewItem(i * 16, j * 16, 16, 16, ItemID.Star, 1, false, 0, false, false);
                }
                else if (num9 == 2)
                {
                    // torch
                    int torchItem = Main.rand.Next(new int[] { ItemID.Torch, ModContent.ItemType<Items.Placeable.IgneousTorchItem>() });
                    Item.NewItem(i * 16, j * 16, 32, 32, torchItem, Main.rand.Next(4, 13));
                }
                else if (num9 == 3)
                {
                    //ammo
                    int ammoItem = Main.rand.Next(new int[] { ItemID.MeteorShot, ItemID.HellfireArrow });
                    Item.NewItem(i * 16, j * 16, 16, 16, ammoItem, Main.rand.Next(10, 21));
                }
                else if (num9 == 4)
                {
                    // healing potions
                    int potion = ItemID.HealingPotion;
                    if (Main.hardMode) potion = ItemID.GreaterHealingPotion;
                    int stack = 1;
                    if (Main.expertMode && Main.rand.Next(3) != 0) stack++;
                    Item.NewItem(i * 16, j * 16, 16, 16, potion, stack);
                }
                else if (num9 == 5 && (double)j > Main.rockLayer)
                {
                    int ammoItem = Main.rand.Next(new int[] { ItemID.Bomb, ModContent.ItemType<Items.Tools.AcidFlask>() });
                    int stack = Main.rand.Next(1,5);
                    if (Main.expertMode) stack += Main.rand.Next(4);
                    Item.NewItem(i * 16, j * 16, 16, 16, ammoItem, stack);
                }
                else
                {
                    // money
                    int[] coins = EAUtils.CalculatePotCash(i, j, 2.5f); // hell pots 2.1
                    if (coins[0] > 0) Item.NewItem(i * 16, j * 16, 16, 16, ItemID.CopperCoin, coins[0]);
                    if (coins[1] > 0) Item.NewItem(i * 16, j * 16, 16, 16, ItemID.SilverCoin, coins[1]);
                    if (coins[2] > 0) Item.NewItem(i * 16, j * 16, 16, 16, ItemID.GoldCoin, coins[2]);
                    if (coins[3] > 0) Item.NewItem(i * 16, j * 16, 16, 16, ItemID.PlatinumCoin, coins[3]);
                }
            }
        }

    }
}