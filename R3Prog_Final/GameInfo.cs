using System;
using System.Collections.Generic;
using FK_CLI;

namespace R3Prog_Final
{
	enum CharacterSide
	{
		Player,
		Enemy,
		None
	}

	class GameInfo
	{
		public const double MoveRange = 25.0;

		private static GameInfo _instance;

		public static bool Create()
		{
			if (_instance != null) return false;
			_instance = new GameInfo();
			RandomSeed = new Random();
			return true;
		}

		public static GameInfo GetInstance()
		{
			return _instance;
		}

		#region Characters

		private List<Character> _characterList;
		private List<Character> _playerSideList;
		private List<Character> _enemySideList;

		private List<Character> _predictionalCharacterList;
		private List<Character> _deleteOrderCharacterList;

		public List<Character> CharacterList => _characterList;
		public List<Character> PlayerSideList => _playerSideList;
		public List<Character> EnemySideList => _enemySideList;

		public void AddPlayerSide(Character character)
		{
			_predictionalCharacterList.Add(character);
		}

		public void AddEnemySide(Character character)
		{
			_predictionalCharacterList.Add(character);
		}

		#endregion

		public int NowFrames { get; private set; }
		public Character ControllableCharacter = null;

		public static Random RandomSeed;

		private bool _isGameEnd;

		private GameInfo()
		{
			_characterList = new List<Character>();
			_playerSideList = new List<Character>();
			_enemySideList = new List<Character>();
			_predictionalCharacterList = new List<Character>();
			_deleteOrderCharacterList = new List<Character>();
			NowFrames = 0;
			_isGameEnd = false;
		}

		public static bool Refresh()
		{
			if (_instance == null) return false;
			_instance._characterList = new List<Character>();
			_instance._playerSideList = new List<Character>();
			_instance._enemySideList = new List<Character>();
			_instance._predictionalCharacterList = new List<Character>();
			_instance._deleteOrderCharacterList = new List<Character>();
			_instance.ControllableCharacter = null;
			_instance.NowFrames = 0;
			_instance._isGameEnd = false;
			return true;
		}

		public static void Update()
		{
			_instance?.InnerUpdate();
			if (FKSettings.GetInstance().MainWindow.GetSpecialKeyStatus(fk_SpecialKey.ENTER, fk_SwitchStatus.DOWN))
				MainFrame.Instance.PopScene(1);
		}

		private void InnerUpdate()
		{
			foreach (var character in _predictionalCharacterList)
			{
				_characterList.Add(character);
				switch (character.Side)
				{
					case Character.CharacterSide.Player:
						_playerSideList.Add(character);
						break;
					case Character.CharacterSide.Enemy:
						_enemySideList.Add(character);
						break;
					case Character.CharacterSide.Other:
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				MainFrame.Instance.NowScene.MainScene.EntryModel(character.Model);
			}

			_predictionalCharacterList.Clear();

			foreach (var character in _characterList)
			{
				character.Move();
			}

			HitProcess();

			RemoveCharacter();

			NowFrames++;

			if (_isGameEnd) return;

			if (NowFrames < 30) return;
			if (NowFrames % 60 == 0)
			{
				new EnemySphere(new fk_Vector(RandomSeed.NextDouble() * MoveRange * 2 - MoveRange,
					RandomSeed.NextDouble() * MoveRange * 2 - MoveRange, -50));
			}
		}

		public void HitProcess()
		{
			foreach (var player in _playerSideList)
			{
				foreach (var enemy in _enemySideList)
				{
					if (!player.IsArive || !enemy.IsArive) continue;
					if (!player.Model.IsInter(enemy.Model)) continue;
					Console.WriteLine("Hit.");
					player.Damage(1);
					enemy.Damage(1);
				}
			}
		}

		public void DeadCharacter(Character character)
		{
			_deleteOrderCharacterList.Add(character);
		}

		public void RemoveCharacter()
		{
			foreach (var character in _deleteOrderCharacterList)
			{
				MainFrame.Instance.NowScene.MainScene.RemoveModel(character.Model);
				_characterList.Remove(character);
				switch (character.Side)
				{
					case Character.CharacterSide.Player:
						_playerSideList.Remove(character);
						break;
					case Character.CharacterSide.Enemy:
						_enemySideList.Remove(character);
						break;
					case Character.CharacterSide.Other:
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			_deleteOrderCharacterList.Clear();
		}

		public void GameClear()
		{
			foreach (var character in _enemySideList)
			{
				DeadCharacter(character);
			}

			_isGameEnd = true;
		}

		public void GameOver()
		{
			foreach (var character in _playerSideList)
			{
				DeadCharacter(character);
			}

			_isGameEnd = true;
		}
	}
}
