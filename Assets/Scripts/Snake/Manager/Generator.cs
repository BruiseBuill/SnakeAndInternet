using BF;
using Snake.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
 	public class Generator : Single<Generator>
	{
        [SerializeField] GameObject snakePrefab;
        [SerializeField] GameObject rabbitPrefab;
        [SerializeField] GenericEventChannel<object> onPlayerCreate;
        [SerializeField] GenericEventChannel<int> onRabbitCountChange;

        [SerializeField] int remainRabbitCount;

        public Vector3 playerSnakePos;
        public Vector3 playerRabbitPos;
        public Vector3[] AISnakePos;
        public Vector3[] AIRabbitPos;
        public int maxAICount;
        List<int> AICreatePosIndices = new List<int>();

        private void Start()
        {
            LoadGame();
            GameControl.Instance().onGameStart += StartGame;
        }
        private void OnDestroy()
        {
            GameControl.Instance().onGameStart -= StartGame;
        }
        public void LoadGame()
        {
            MapManager.Instance().CreateMap();
            CursorManager.Instance().canInput = false;
        }
        public void StartGame()
        {
            CreatePlayer();
            CreateAI();
            CursorManager.Instance().canInput = true;
        }
        void CreatePlayer()
        {
            if (GameControl.Instance().isPresentSnake)
            {
                var snake = PoolManager.Instance().Release(snakePrefab.name).GetComponent<SnakeControl>();
                snake.Initialize(GetSnakeInit(CharacterType.Player));
                snake.Open();
                onPlayerCreate.Invoke(snake.gameObject);
            }
            else
            {
                var rabbit = PoolManager.Instance().Release(rabbitPrefab.name).GetComponent<RabbitControl>();
                rabbit.Initialize(GetRabbitInit(CharacterType.Player));
                rabbit.Open();
                onPlayerCreate.Invoke(rabbit.gameObject);
            }
            
        }
        void CreateAI()
        {
            AICreatePosIndices.Clear();
            var count = Mathf.Min(GameControl.Instance().turnCount / 2 + 1, maxAICount);
            if (GameControl.Instance().isPresentSnake)
            {
                remainRabbitCount = count;
            }

            for(int i = 0; i < count; i++)
            {
                if (!GameControl.Instance().isPresentSnake)
                {
                    var snake = PoolManager.Instance().Release(snakePrefab.name).GetComponent<SnakeControl>();
                    snake.Initialize(GetSnakeInit(CharacterType.AI));
                    snake.Open();
                }
                else
                {
                    var rabbit = PoolManager.Instance().Release(rabbitPrefab.name).GetComponent<RabbitControl>();
                    rabbit.Initialize(GetRabbitInit(CharacterType.AI));
                    rabbit.Open();
                }
            }
        }
        SnakeInit GetSnakeInit(CharacterType type)
        {
            var init = new SnakeInit();
            if(type== CharacterType.Player)
            {
                init.pos = playerSnakePos;
            }
            else
            {
                int index = -1;
                while (true)
                {
                    index = Random.Range(0, AISnakePos.Length);
                    if (!AICreatePosIndices.Contains(index))
                    {
                        break;
                    }
                }
                AICreatePosIndices.Add(index);
                init.pos = AISnakePos[index];
            }
            init.orient = Vector3.right;
            init.type = type;
            init.buffList = BuffManager.Instance().snakeBuffList;
            init.rabbitPos = playerRabbitPos;
            return init;
        }
        RabbitInit GetRabbitInit(CharacterType type)
        {
            var init = new RabbitInit();
            if (type == CharacterType.Player)
            {
                init.pos = playerRabbitPos;
            }
            else
            {
                int index = -1;
                while (true)
                {
                    index = Random.Range(0, AIRabbitPos.Length);
                    if (!AICreatePosIndices.Contains(index))
                    {
                        break;
                    }
                }
                AICreatePosIndices.Add(index);
                init.pos = AIRabbitPos[index];
            }
            init.orient = Vector3.right;
            init.type = type;
            init.buffList = BuffManager.Instance().rabbitBuffList;
            init.trackLevel = BuffManager.Instance().TrackLevel;
            return init;
        }
        public void RabbitDie()
        {
            if (GameControl.Instance().isPresentSnake)
            {
                remainRabbitCount--;
                onRabbitCountChange.Invoke(remainRabbitCount);
                //AudioManager.Instance().PlayVFX("EatRabbit");
                if (remainRabbitCount == 0)
                {
                    GameControl.Instance().PlayerWin();
                }
            }
                
        }

	}
}
