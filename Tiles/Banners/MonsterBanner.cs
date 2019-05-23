using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Tiles.Banners
{
    public class MonsterBanner : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 111;
            TileObjectData.addTile(Type);
            dustType = -1;
            disableSmartCursor = true;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Banner");
            AddMapEntry(new Color(13, 88, 130), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int style = frameX / 18;
            string item;
            switch (style)
            {
                case 0:
                    item = "AnomalyBanner";
                    break;
                case 1:
                    item = "AuraAssailantBanner";
                    break;
                case 2:
                    item = "AuraSlimeBanner";
                    break;
                case 3:
                    item = "DragonBatBanner";
                    break;
                case 4:
                    item = "DragonSlimeBanner";
                    break;
                case 5:
                    item = "DragonWarriorBanner";
                    break;
                case 6:
                    item = "DrakoniteElementalBanner";
                    break;
                case 7:
                    item = "ChargetteBanner";
                    break;
                case 8:
                    item = "ElectrodeBanner";
                    break;
                case 9:
                    item = "FlyingJawBanner";
                    break;
                case 10:
                    item = "PetalClasperBanner";
                    break;
                case 11:
                    item = "StrangeBulbBanner";
                    break;
                case 12:
                    item = "DesertElementalBanner";
                    break;
                case 13:
                    item = "FireElementalBanner";
                    break;
                case 14:
                    item = "SkyElementalBanner";
                    break;
                case 15:
                    item = "FrostElementalBanner";
                    break;
                case 16:
                    item = "WaterElementalBanner";
                    break;
                case 17:
                    item = "VoidElementalBanner";
                    break;
                case 18:
                    item = "VampireBatBanner";
                    break;
                case 19:
                    item = "GiantVampireBatBanner";
                    break;
                case 20:
                    item = "InfernoSpiritBanner";
                    break;
                case 21:
                    item = "GiantTickBanner";
                    break;
                case 22:
                    item = "SkyCrawlerBanner";
                    break;
                case 23:
                    item = "PuffBanner";
                    break;
                case 24:
                    item = "SpikedPuffBanner";
                    break;
                case 25:
                    item = "StellarBatBanner";
                    break;
                case 26:
                    item = "StellarEntityBanner";
                    break;
                default:
                    return;
            }
            Item.NewItem(i * 16, j * 16, 16, 48, mod.ItemType(item));
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Player player = Main.LocalPlayer;
                int style = Main.tile[i, j].frameX / 18;
                string type;
                switch (style)
                {
                    case 0:
                        type = "Anomaly";
                        break;
                    case 1:
                        type = "AuraAssailant";
                        break;
                    case 2:
                        type = "AuraSlime";
                        break;
                    case 3:
                        type = "DragonBat";
                        break;
                    case 4:
                        type = "DragonSlime";
                        break;
                    case 5:
                        type = "DragonWarrior";
                        break;
                    case 6:
                        type = "DrakoniteElemental";
                        break;
                    case 7:
                        type = "Chargette";
                        break;
                    case 8:
                        type = "Electrode";
                        break;
                    case 9:
                        type = "FlyingJaw";
                        break;
                    case 10:
                        type = "PetalClasper";
                        break;
                    case 11:
                        type = "StrangeBulb";
                        break;
                    case 12:
                        type = "DesertElemental";
                        break;
                    case 13:
                        type = "FireElemental";
                        break;
                    case 14:
                        type = "SkyElemental";
                        break;
                    case 15:
                        type = "FrostElemental";
                        break;
                    case 16:
                        type = "WaterElemental";
                        break;
                    case 17:
                        type = "VoidElemental";
                        break;
                    case 18:
                        type = "VampireBat";
                        break;
                    case 19:
                        type = "GiantVampireBat";
                        break;
                    case 20:
                        type = "InfernoSpirit";
                        break;
                    case 21:
                        type = "GiantTick";
                        break;
                    case 22:
                        type = "SkyCrawler";
                        break;
                    case 23:
                        type = "Puff";
                        break;
                    case 24:
                        type = "SpikedPuff";
                        break;
                    case 25:
                        type = "StellarBat";
                        break;
                    case 26:
                        type = "StellarEntity";
                        break;
                    default:
                        return;
                }
                player.NPCBannerBuff[mod.NPCType(type)] = true;
                player.hasBanner = true;
            }
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }
    }
}