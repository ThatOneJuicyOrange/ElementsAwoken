using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using ElementsAwoken.Items.Materials;

namespace ElementsAwoken.Tiles.Crafting
{
    public class CrystalCracker : ModTile
    {
        public override void SetDefaults()
        {
			Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Width = 2;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Crystal Cracker");
            AddMapEntry(new Color(133, 133, 133), name);

            disableSmartCursor = true;

            TileObjectData.addTile(Type);
			animationFrameHeight = 54;
        }
        public override bool HasSmartInteract()
        {
            return true;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ItemType<Items.Placeable.CrystalCracker>());
        }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.showItemIcon = true;
            player.showItemIcon2 = ItemType<Items.Placeable.CrystalCracker>();
            if (Main.mouseRight && Main.stackSplit <= 1)
            {
                if (Main.stackSplit == 0)
                {
                    Main.stackCounter = 0;
                    Main.stackDelay = 30;
                    Main.superFastStack = 0;
                }
                else
                {
                    Main.stackCounter++;
                    int num = Main.stackDelay * 5 - 5;
                    if (num < 5) num = 5;
                    if (Main.stackCounter >= num)
                    {
                        Main.stackDelay--;
                        if (Main.stackDelay < 2)
                        {
                            Main.stackDelay = 2;
                            Main.superFastStack++;
                        }
                        Main.stackCounter = 0;
                    }
                }
                if (Main.stackDelay < 10) Main.stackDelay = 10;
                if (Main.stackSplit == 0)
                {
                    Main.stackSplit = 15;
                }
                else
                {
                    Main.stackSplit = Main.stackDelay;
                }

                CreateCrystal(i, j);

            }
        }
        private void CreateCrystal(int i, int j)
        {
            Player player = Main.LocalPlayer;
            if (player.HasItem(ItemType<InfinityCrys>()) && player.HasItem(ItemType<NeutronFragment>()))
            {
                Item crystal = player.inventory[player.FindItem(ItemType<InfinityCrys>())];
                Item fragment = player.inventory[player.FindItem(ItemType<NeutronFragment>())];

                crystal.stack--;
                if (crystal.stack <= 0) crystal.TurnToAir();
                fragment.stack--;
                if (fragment.stack <= 0) fragment.TurnToAir();
                player.QuickSpawnItem(ItemType<CInfinityCrys>());
                Main.PlaySound(SoundID.Item37, new Vector2(i, j) * 16);
                Main.PlaySound(SoundID.Item66, new Vector2(i, j) * 16);
                Tile t = Main.tile[i, j];
                Vector2 middle = new Vector2(i, j) * 16 - new Vector2(t.frameX, t.frameY) + new Vector2(12, 30);
                for (int p = 1; p <= 2; p++)
                {
                    float strength = 3 + p * 1.2f;
                    int numDusts = p * 20;
                    OutwardsCircleDust(middle, p == 1 ? DustID.PinkFlame : 135, numDusts, strength, randomiseVel: true);
                }
                for (int p = 0; p < 20; p++)
                {
                    Dust dust = Main.dust[Dust.NewDust(middle, 0, 0, p >= 10 ? DustID.PinkFlame : 135, 0, 0, 0, default(Color), 1f)];
                    dust.velocity.Y = Main.rand.NextBool() ? -4 : 4;
                    dust.velocity.Y *= Main.rand.NextFloat(0.3f, 1);
                    dust.velocity.X *= 0.1f;
                    dust.noGravity = true;
                }
            }
            else
            {
                Tile t = Main.tile[i, j];
                if (ProjectileUtils.CountProjectiles(ProjectileType<Projectiles.Other.InfinityCrystalGuide>()) == 0)
                    Projectile.NewProjectile(new Vector2(i, j) * 16 - new Vector2(t.frameX, t.frameY) + new Vector2(16, -20), Vector2.Zero, ProjectileType<Projectiles.Other.InfinityCrystalGuide>(), 0, 0, Main.myPlayer);
            }
        }
        public static void OutwardsCircleDust(Vector2 pos, int dustID, int numDusts, float vel, bool fromCenter = false, int dustAlpha = 0, bool randomiseVel = false, float dustScale = 1.5f, float dustFadeIn = 0)
        {
            for (int i = 0; i < numDusts; i++)
            {
                Vector2 position = Vector2.One.RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + pos;
                Vector2 velocity = position - pos;
                Vector2 spawnPos = position + (fromCenter ? Vector2.Zero : velocity);
                Dust dust = Main.dust[Dust.NewDust(spawnPos, 0, 0, dustID, velocity.X * 2f, velocity.Y * 2f, dustAlpha, default(Color), dustScale)];
                dust.noGravity = true;
                dust.velocity = Vector2.Normalize(velocity) * vel * (randomiseVel ? Main.rand.NextFloat(0.8f, 1.2f) : 1);
                dust.fadeIn = dustFadeIn;
            }
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 4)
			{
				frameCounter = 0;
				frame++;
				if (frame > 11)
				{
					frame = 0;
				}
			}
		}
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.8f;
            g = 0.3f;
            b = 0.6f;
        }
    }
}