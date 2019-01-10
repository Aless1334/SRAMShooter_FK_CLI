using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FK_CLI;

namespace R3Prog_Final
{
	class StateDrawer
	{
		private fk_SpriteModel _titleLabelSprite;
		private fk_SpriteModel _scoreLabelSprite;
		private fk_SpriteModel _scoreSprite;
		private fk_SpriteModel _lifeLabelSprite;
		private fk_SpriteModel _lifeSprite;

		public void InitSprites()
		{
			_titleLabelSprite = new fk_SpriteModel();
			_scoreLabelSprite = new fk_SpriteModel();
			_scoreSprite = new fk_SpriteModel();
			_lifeLabelSprite = new fk_SpriteModel();
			_lifeSprite = new fk_SpriteModel();

			if (!_titleLabelSprite.InitFont("PixelMplus12-Regular.ttf"))
				Console.WriteLine("Font Load Error.");
			if (!_scoreLabelSprite.InitFont("PixelMplus12-Regular.ttf"))
				Console.WriteLine("Font Load Error.");
			if (!_scoreSprite.InitFont("PixelMplus12-Regular.ttf"))
				Console.WriteLine("Font Load Error.");
			if (!_lifeLabelSprite.InitFont("PixelMplus12-Regular.ttf"))
				Console.WriteLine("Font Load Error.");
			if (!_lifeSprite.InitFont("PixelMplus12-Regular.ttf"))
				Console.WriteLine("Font Load Error.");

			_titleLabelSprite.PrdScale(0.2);
			_scoreLabelSprite.PrdScale(0.2);
			_lifeLabelSprite.PrdScale(0.2);
			_scoreSprite.PrdScale(0.2);
			_lifeSprite.PrdScale(0.2);

			_titleLabelSprite.DrawText("Status");
			_scoreLabelSprite.DrawText("Score");
			_lifeLabelSprite.DrawText("Life");
		}

		public void Entry()
		{
			_titleLabelSprite.GlMoveTo(-20, 33, 0);
			_scoreLabelSprite.GlMoveTo(-15, 16, 0);
			_lifeLabelSprite.GlMoveTo(-15, 0, 0);
			_scoreSprite.GlMoveTo(0, 16, 0);
			_lifeSprite.GlMoveTo(0, 0, 0);

			MainFrame.Instance.NowScene.StateScene.EntryModel(_titleLabelSprite);
			//MainFrame.Instance.NowScene.StateScene.EntryModel(_scoreLabelSprite);
			MainFrame.Instance.NowScene.StateScene.EntryModel(_lifeLabelSprite);
			//MainFrame.Instance.NowScene.StateScene.EntryModel(_scoreSprite);
			MainFrame.Instance.NowScene.StateScene.EntryModel(_lifeSprite);
		}

		public void DrawUpdate()
		{
			_lifeSprite.DrawText(GameInfo.GetInstance().ControllableCharacter.Life.ToString(), true);
		}
	}
}
