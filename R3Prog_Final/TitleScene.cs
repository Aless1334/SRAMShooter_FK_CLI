using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FK_CLI;

namespace R3Prog_Final
{
	class TitleScene : SceneBase
	{
		private fk_SpriteModel _guideSprite;
		private fk_RectTexture _titleTex;
		private fk_Model _titleTexModel;

		public TitleScene()
		{
			SetMainBlankScene(fk_Material.MatBlack.Diffuse);
			SetStateBlankScene(fk_Material.DarkBlue.Diffuse);
		}

		public override void Initialize()
		{
			SetMainLight(fk_Material.TrueWhite, new fk_Vector(0, 0, -1));
			SetStateLight(fk_Material.TrueWhite, new fk_Vector(0, 0, -1));
			SetMainCamera(new fk_Vector(0, 0, 100));
			SetStateCamera(new fk_Vector(0, 0, 100));

			_titleTex = new fk_RectTexture();
			if(!_titleTex.ReadJPG("sram.jpg"))
				Console.WriteLine("Tex Load Error");
			_titleTex.TextureSize = new fk_TexCoord(40, 30);

			_titleTexModel = new fk_Model()
			{
				Shape = _titleTex,
				Material = fk_Material.White
			};
			_titleTexModel.GlMoveTo(new fk_Vector(0, 10, 0));
			MainScene.EntryModel(_titleTexModel);

			_guideSprite = new fk_SpriteModel();
			if (!_guideSprite.InitFont("PixelMplus12-Regular.ttf"))
				Console.WriteLine("Font Load Error.");

			_guideSprite.PrdScale(0.2);
			_guideSprite.DrawText("Enter : Start");
			_guideSprite.GlMoveTo(new fk_Vector(0, -20, 0));

			MainScene.EntryModel(_guideSprite);
			base.Initialize();
		}

		public override void Process()
		{
			if (FKSettings.GetInstance().MainWindow.GetSpecialKeyStatus(fk_SpecialKey.ENTER, fk_SwitchStatus.DOWN))
			{
				MainFrame.Instance.PushNewScene(new GameMain());
			}
		}
	}
}
