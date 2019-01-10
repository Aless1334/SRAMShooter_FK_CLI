using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FK_CLI;

namespace R3Prog_Final
{
	abstract class BulletBase : Character
	{
		protected fk_Vector Velocity;

		protected BulletBase(CharacterSide side, int lifeMax, fk_Vector position, fk_Vector velocity) : base(side, lifeMax, position)
		{
			Velocity = velocity;

			switch (side)
			{
				case CharacterSide.Player:
					GameInfo.GetInstance().AddPlayerSide(this);
					break;
				case CharacterSide.Enemy:
					GameInfo.GetInstance().AddEnemySide(this);
					break;
				case CharacterSide.Other:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(side), side, null);
			}
		}

		protected virtual void BulletMove()
		{

		}

		protected override void UniqueMove()
		{
			BulletMove();
			if (Position.x > 50 || Position.x < -50 || Position.y > 50 || Position.y < -50 || Position.z > 50 ||
			    Position.z < -50 || AriveFrames > 120)
				Escape();
		}
	}
}
