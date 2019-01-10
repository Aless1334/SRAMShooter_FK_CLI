using System;
using FK_CLI;

namespace R3Prog_Final
{
	class Player : Character
	{
		private const double MoveSpeed = 1;
		private const int LifeMax = 10;
		private const double ShootLineLength = 60;

		private bool _isImmotal;

		private fk_Model _shootLine;

		public Player(fk_Vector position) : base(CharacterSide.Player, LifeMax, position, 100)
		{
			Model = new fk_Model()
			{
				Shape = new fk_Block(4, 4, 4),
				Material = new fk_Material()
				{
					Ambient = new fk_Color(0, 0, 0),
					Alpha = 0.7f,
					Diffuse = new fk_Color(0.8, 0.6, 0),
					Emission = new fk_Color(0, 0, 0),
					Shininess = 0,
					Specular = new fk_Color(0, 0, 0)
				},
				BMode = fk_BoundaryMode.CAPSULE,
				//BDraw = true,
				BLineColor = fk_Material.Blue.Diffuse
			};
			Model.GlUpvec(new fk_Vector(1, 1, 0));
			Model.SetCapsule(new fk_Vector(0, 0, 0), new fk_Vector(0, 0, 0.01), 2);

			GameInfo.GetInstance().AddPlayerSide(this);
			GameInfo.GetInstance().ControllableCharacter = this;

			_shootLine = new fk_Model()
			{
				Shape = new fk_Block(0.2, 0.2, ShootLineLength),
				Material = fk_Material.Pink,
			};
			_shootLine.Material.Alpha = 0.5f;

			_isImmotal = false;
		}

		protected override void UniqueMove()
		{
			MainFrame.Instance.NowScene.MainScene.EntryModel(_shootLine);
			MainFrame.Instance.NowScene.MainScene.EntryModel(Model);

			var window = FKSettings.GetInstance().MainWindow;

			if (window.GetSpecialKeyStatus(fk_SpecialKey.LEFT, fk_SwitchStatus.PRESS))
				Position.x -= MoveSpeed;
			if (window.GetSpecialKeyStatus(fk_SpecialKey.RIGHT, fk_SwitchStatus.PRESS))
				Position.x += MoveSpeed;
			if (window.GetSpecialKeyStatus(fk_SpecialKey.UP, fk_SwitchStatus.PRESS))
				Position.y += MoveSpeed;
			if (window.GetSpecialKeyStatus(fk_SpecialKey.DOWN, fk_SwitchStatus.PRESS))
				Position.y -= MoveSpeed;

			CorrectPosition();

			_shootLine.GlMoveTo(new fk_Vector(Position.x, Position.y, -ShootLineLength / 2));

			if (window.GetKeyStatus('Z', fk_SwitchStatus.DOWN))
			{
				var normalBullet = new NormalBullet(CharacterSide.Player, new fk_Vector(Position),
					new fk_Vector(0, 0, -2), fk_Material.GrassGreen);
			}

			if (window.GetKeyStatus('X', fk_SwitchStatus.DOWN))
			{
				var normalBullet =
					new PierceBullet(CharacterSide.Player, new fk_Vector(Position), new fk_Vector(0, 0, -2));
			}

			if (window.GetKeyStatus('C', fk_SwitchStatus.DOWN))
			{
				var normalBullet =
					new ClusterBullet(CharacterSide.Player, new fk_Vector(Position), new fk_Vector(0, 0, -2));
			}

			if (window.GetKeyStatus('V', fk_SwitchStatus.PRESS))
			{
				var normalBullet = new NormalBullet(CharacterSide.Player, new fk_Vector(Position),
					new fk_Vector(0, 0, -2), fk_Material.HolidaySkyBlue, 1.5);
			}

			if (window.GetKeyStatus('I', fk_SwitchStatus.DOWN))
			{
				_isImmotal = !_isImmotal;
			}
		}

		private void CorrectPosition()
		{
			Position.x = fk_Math.Clamp(Position.x, -GameInfo.MoveRange, GameInfo.MoveRange);
			Position.y = fk_Math.Clamp(Position.y, -GameInfo.MoveRange, GameInfo.MoveRange);
		}

		protected override void DeadProcess()
		{
			MainFrame.Instance.NowScene.MainScene.RemoveModel(_shootLine);
			GameInfo.GetInstance().GameOver();
		}

		protected override bool IsTakeDamage()
		{
			return !_isImmotal;
		}
	}
}

