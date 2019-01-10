using System;
using FK_CLI;

namespace R3Prog_Final
{
	abstract class Character
	{
		public enum CharacterSide
		{
			Player,
			Enemy,
			Other
		}

		public fk_Vector Position { get; protected set; }

		private fk_Model _model;

		public fk_Model Model
		{
			get => _model;
			protected set
			{
				_model = value;
				_havingMaterial = _model.Material;
			}
		}

		private const int SwitchDamageMaterialFrame = 3;

		private int _life;
		public int Life => _life;

		private bool _isArive;
		public bool IsArive => _isArive;

		private CharacterSide _side;
		public CharacterSide Side => _side;

		private int _ariveFrames;
		public int AriveFrames => _ariveFrames;

		private fk_Material _havingMaterial;
		private fk_Material _damageMaterial;

		private int _restNoHitTime;
		private int _noDamageTime;

		private bool _isDamageMaterial;

		protected Character(CharacterSide side, int life, fk_Vector position, int noDamageTime = 60,
			fk_Material damageMaterial = null)
		{
			_side = side;
			Position = position;
			_life = life;
			_isArive = true;
			_ariveFrames = 0;
			_noDamageTime = noDamageTime;
			_restNoHitTime = -1;
			_damageMaterial = damageMaterial ?? fk_Material.DarkRed;
		}

		public void Move()
		{
			UniqueMove();
			_model.GlMoveTo(Position);
			_ariveFrames++;
			DamageEffect();
		}

		protected abstract void UniqueMove();

		private void DamageEffect()
		{
			if (_restNoHitTime <= -1) return;
			_restNoHitTime--;
			if (_restNoHitTime == -1)
			{
				_model.Material = _havingMaterial;
				return;
			}

			if (_restNoHitTime % SwitchDamageMaterialFrame == 0)
			{
				_model.Material = _isDamageMaterial ? _havingMaterial : _damageMaterial;
				_isDamageMaterial = !_isDamageMaterial;
			}
		}

		public void Damage(int value)
		{
			if (_restNoHitTime >= 0 || !IsTakeDamage()) return;
			_life -= value;
			if (_life <= 0)
			{
				Destroy();
				return;
			}

			_restNoHitTime = _noDamageTime;
			_model.Material = _damageMaterial;
			_isDamageMaterial = true;
		}

		protected virtual bool IsTakeDamage()
		{
			return true;
		}

		protected void Destroy()
		{
			DeadProcess();
			Escape();
		}

		protected void Escape()
		{
			_isArive = false;
			GameInfo.GetInstance().DeadCharacter(this);
		}

		protected virtual void DeadProcess()
		{
		}
	}
}
