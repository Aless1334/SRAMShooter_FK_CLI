using FK_CLI;

namespace R3Prog_Final
{
	class EnemySphere : Character
	{
		private const double MoveSpeed = 0.2;

		private const int LifeMax = 1;

		public EnemySphere(fk_Vector position) : base(CharacterSide.Enemy, LifeMax, position)
		{
			Model = new fk_Model()
			{
				Shape = new fk_Sphere(4, 2.0),
				Material = fk_Material.Cyan,
				BMode = fk_BoundaryMode.CAPSULE,
				BDraw = false,
				BLineColor = fk_Material.Red.Diffuse
			};
			Model.SetCapsule(new fk_Vector(0, 0, 0), new fk_Vector(0, 0, 0.01), 3.5);

			GameInfo.GetInstance().AddEnemySide(this);
		}

		protected override void UniqueMove()
		{
			Position.z += MoveSpeed;
			if (Position.z > 20)
				Escape();

			if (AriveFrames != 0 && AriveFrames % 95 == 0)
			{
				var targetPosition = GameInfo.GetInstance().ControllableCharacter.Position;
				var chaseVector = targetPosition - Position;
				chaseVector.Normalize();

				var normalBullet =
					new NormalBullet(CharacterSide.Enemy, new fk_Vector(Position), chaseVector,
						fk_Material.Orange);
			}
		}
	}
}
