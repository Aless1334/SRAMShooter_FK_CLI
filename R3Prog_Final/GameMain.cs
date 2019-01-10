using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FK_CLI;

namespace R3Prog_Final
{
	class GameMain : SceneBase
	{
		private const double BackAnchor = 30;

		private StateDrawer _stateDrawer;
		private fk_RectTexture _backTex;
		private fk_Model[] _backModels;
		private double _graphicSize;

		public override bool LoadFiles()
		{
			return base.LoadFiles();
		}

		public GameMain()
		{
			SetMainBlankScene(fk_Material.MatBlack.Diffuse);
			SetStateBlankScene(fk_Material.DarkBlue.Diffuse);
		}

		public override void Initialize()
		{
			base.Initialize();

			MainScene.ClearModel();
			StateScene.ClearModel();

			SetMainLight(fk_Material.TrueWhite, new fk_Vector(0, 0, -1));
			SetStateLight(fk_Material.TrueWhite, new fk_Vector(0, 0, -1));
			SetMainCamera(new fk_Vector(0, 0, 100));
			SetStateCamera(new fk_Vector(0, 0, 100));

			InitializeBackground();

			GameInfo.Create();
			GameInfo.Refresh();

			var player = new Player(new fk_Vector(0, 0, 20));

			var boss = new Boss(new fk_Vector(0, 0, -40));

			_stateDrawer = new StateDrawer();
			_stateDrawer.InitSprites();
			_stateDrawer.Entry();
		}

		private void InitializeBackground()
		{
			_backTex = new fk_RectTexture();
			if (!_backTex.ReadJPG("cyback.jpg"))
			{
				Console.WriteLine("Tex Load Error.");
			}

			_graphicSize = (GameInfo.MoveRange + 7) * 2;

			_backTex.TextureSize = new fk_TexCoord(_graphicSize, _graphicSize);

			_backModels = new fk_Model[8];
			for (var i = 0; i < _backModels.Length; i++)
			{
				_backModels[i] = new fk_Model()
				{
					Shape = _backTex,
					Material = fk_Material.White,
				};
			}

			SetModelTransform(_backModels[0], new fk_Vector(0, GameInfo.MoveRange, BackAnchor - _graphicSize),
				new fk_Vector(0, 1, 0));
			SetModelTransform(_backModels[1], new fk_Vector(0, GameInfo.MoveRange, BackAnchor), new fk_Vector(0, 1, 0));
			SetModelTransform(_backModels[2], new fk_Vector(0, -GameInfo.MoveRange, BackAnchor - _graphicSize),
				new fk_Vector(0, -1, 0));
			SetModelTransform(_backModels[3], new fk_Vector(0, -GameInfo.MoveRange, BackAnchor),
				new fk_Vector(0, -1, 0));
			SetModelTransform(_backModels[4], new fk_Vector(GameInfo.MoveRange, 0, BackAnchor - _graphicSize),
				new fk_Vector(1, 0, 0),
				new fk_Vector(0, 0, 1));
			SetModelTransform(_backModels[5], new fk_Vector(GameInfo.MoveRange, 0, BackAnchor), new fk_Vector(1, 0, 0),
				new fk_Vector(0, 0, 1));
			SetModelTransform(_backModels[6], new fk_Vector(-GameInfo.MoveRange, 0, BackAnchor - _graphicSize),
				new fk_Vector(-1, 0, 0),
				new fk_Vector(0, 0, 1));
			SetModelTransform(_backModels[7], new fk_Vector(-GameInfo.MoveRange, 0, BackAnchor),
				new fk_Vector(-1, 0, 0),
				new fk_Vector(0, 0, 1));

			foreach (var backModel in _backModels)
			{
				MainScene.EntryModel(backModel);
			}
		}

		private void SetModelTransform(fk_Model model, fk_Vector position, fk_Vector direction, fk_Vector upVec = null)
		{
			model.GlVec(direction);
			if (upVec != null) model.GlUpvec(upVec);
			model.GlMoveTo(position);
		}

		public override void Process()
		{
			BackUpdate();
			GameInfo.Update();
			_stateDrawer.DrawUpdate();
		}

		private void BackUpdate()
		{
			foreach (var backModel in _backModels)
			{
				backModel.GlTranslate(new fk_Vector(0, 0, 0.3));
				if (backModel.Position.z > BackAnchor + _graphicSize)
					backModel.GlMoveTo(new fk_Vector(backModel.Position.x, backModel.Position.y,
						BackAnchor - _graphicSize));
				//Console.WriteLine(backModel.Position);
			}
		}
	}
}
