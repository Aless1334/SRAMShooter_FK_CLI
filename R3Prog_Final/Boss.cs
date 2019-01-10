using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FK_CLI;

namespace R3Prog_Final
{
	class Boss : Character
	{
		private const int LifeMax = 10;
		private const int ChildrenCount = 4;

		private BossChild[] _childList;

		private fk_Vector _velocity;
		private double _moveRange;

		private int _restReviveCount;

		public Boss(fk_Vector position) : base(CharacterSide.Enemy, LifeMax, position)
		{
			_moveRange = GameInfo.MoveRange - 5;

			_childList = new BossChild[ChildrenCount];

			Model = new fk_Model()
			{
				Shape = new fk_Block(4, 4, 4),
				Material = fk_Material.Cyan,
				BMode = fk_BoundaryMode.CAPSULE,
				BDraw = false,
				BLineColor = fk_Material.Red.Diffuse
			};
			Model.SetCapsule(new fk_Vector(0, 0, 0), new fk_Vector(0, 0, 0.01), 2);

			GameInfo.GetInstance().AddEnemySide(this);

			SetRandomVelocity();

			GenerateNewChild(position);

			_restReviveCount = -1;
		}

		private void GenerateNewChild(fk_Vector basePosition)
		{
			_childList[0] = new BossChild(basePosition, this, new fk_Vector(0, 5, 0));
			_childList[1] = new BossChild(basePosition, this, new fk_Vector(0, -5, 0));
			_childList[2] = new BossChild(basePosition, this, new fk_Vector(5, 0, 0));
			_childList[3] = new BossChild(basePosition, this, new fk_Vector(-5, 0, 0));
		}

		protected override void DeadProcess()
		{
			GameInfo.GetInstance().GameClear();
		}

		protected override void UniqueMove()
		{
			Position += _velocity;
			if (IsOuterMoveSpace())
			{
				Position.x = fk_Math.Clamp(Position.x, -_moveRange, _moveRange);
				Position.y = fk_Math.Clamp(Position.y, -_moveRange, _moveRange);
				SetRandomVelocity();
			}

			if (_restReviveCount >= 0)
			{
				_restReviveCount--;
				if (_restReviveCount == -1)
				{
					GenerateNewChild(Position);
				}
			}
		}

		private bool IsOuterMoveSpace()
		{
			return Position.x < -_moveRange || Position.x > _moveRange || Position.y < -_moveRange ||
			       Position.y > _moveRange;
		}

		private void SetRandomVelocity()
		{
			_velocity = new fk_Vector(GameInfo.RandomSeed.NextDouble() - 0.5, GameInfo.RandomSeed.NextDouble() - 0.5,
				0);
		}

		public void DeadChild(BossChild child)
		{
			for (var i = 0; i < ChildrenCount; i++)
			{
				if (_childList[i] != child) continue;
				_childList[i] = null;
				if (!IsAriveLeastChild())
				{
					_restReviveCount = 600;
				}

				return;
			}
		}

		protected override bool IsTakeDamage()
		{
			return !IsAriveLeastChild();
		}

		private bool IsAriveLeastChild()
		{
			foreach (var bossChild in _childList)
			{
				if (bossChild != null)
					return true;
			}

			return false;
		}
	}
}
