using FK_CLI;

namespace R3Prog_Final
{
	public abstract class SceneBase
	{
		public fk_Scene MainScene { get; protected set; }
		public fk_Scene StateScene { get; protected set; }
		protected fk_Model mainCamera, stateCamera;
		protected fk_Light mainLight, stateLight;
		protected fk_Model mainLightModel, stateLightModel;


		private bool _isFirstCalled;
		protected int startTime;

		protected SceneBase()
		{
			_isFirstCalled = true;
		}

		protected void SetMainBlankScene(fk_Color backGround)
		{
			MainScene = new fk_Scene()
			{
				BGColor = backGround,
			};
		}

		protected void SetMainCamera(fk_Vector position, fk_Vector direction = null)
		{
			mainCamera = new fk_Model();
			mainCamera.GlMoveTo(position);
			if (direction != null) mainCamera.GlVec(direction);
			MainScene.Camera = mainCamera;
		}

		protected void SetMainLight(fk_Material lightMaterial, fk_Vector direction = null)
		{
			if (MainScene == null) return;
			mainLight = new fk_Light();
			mainLightModel = new fk_Model()
			{
				Shape = mainLight,
				Material = lightMaterial
			};
			if (direction != null) mainLightModel.GlVec(direction);

			MainScene.EntryModel(mainLightModel);
		}

		protected void SetStateCamera(fk_Vector position, fk_Vector direction = null)
		{
			stateCamera = new fk_Model();
			stateCamera.GlMoveTo(position);
			if (direction != null) stateCamera.GlVec(direction);
			StateScene.Camera = stateCamera;
		}

		protected void SetStateLight(fk_Material lightMaterial, fk_Vector direction = null)
		{
			if (StateScene == null) return;
			stateLight = new fk_Light();
			stateLightModel = new fk_Model()
			{
				Shape = mainLight,
				Material = lightMaterial
			};
			if (direction != null) stateLightModel.GlVec(direction);

			StateScene.EntryModel(stateLightModel);
		}

		protected void SetStateBlankScene(fk_Color backGround)
		{
			StateScene = new fk_Scene()
			{
				BGColor = backGround,
			};
		}

		public virtual bool LoadFiles()
		{
			return false;
		}

		public virtual void Initialize()
		{
		}

		public abstract void Process();

		public void Run()
		{
			if (_isFirstCalled)
			{
				Initialize();
				_isFirstCalled = false;
			}

			Process();
		}
	}
}
