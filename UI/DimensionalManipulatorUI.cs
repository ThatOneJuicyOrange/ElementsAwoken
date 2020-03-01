using ElementsAwoken.NPCs.Town;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.UI
{
	internal class DimensionalManipulatorUI : UIState
	{
        internal UIFloatRangedDataValue timeToSet;
        public override void OnInitialize() 
        {
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            timeToSet = new UIFloatRangedDataValue("timeToSet:", modPlayer.voidTimeChangeTime, 0, 1);
        }


        public override void Update(GameTime gameTime) {
			base.Update(gameTime);
            Mod mod = ModLoader.GetMod("ElementsAwoken");

            if (Main.LocalPlayer.HeldItem.type != mod.ItemType("DimensionalManipulator")) GetInstance<ElementsAwoken>().VoidTimerChangerUI.SetState(null);
		}
       
		protected override void DrawSelf(SpriteBatch spriteBatch) 
        {
            Main.NewText("f");
		}
	}

}
