using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Snake
{
 	public class GameControl : Single<GameControl>
	{
        public UnityAction onWin = delegate { };
        public UnityAction onLose = delegate { };
        public UnityAction onLifeOver = delegate { };
        public UnityAction onGameStart = delegate { };

        public int turnCount;
        public int remainLife;
        public int maxRemainLife;
        public bool isPresentSnake = true;

        public bool isGameStart;
        public bool isGamePause;

        public bool isInitialOpen;
        public UnityAction onEnterGame = delegate { };

        public AudioData winAudioData;
        public AudioData loseAudioData;
        public AudioData lifeOverAudioData;

        AudioBuilder winSfxBuilder;
        AudioBuilder loseSfxBuilder;
        AudioBuilder lifeOverSfxBuilder;

        private void Awake()
        {
            onGameStart += () =>
            {
                isGameStart = true;
                
            };
            winSfxBuilder = new AudioBuilder();
            loseSfxBuilder = new AudioBuilder();
            lifeOverSfxBuilder = new AudioBuilder();

            //winSfxBuilder.Initialize(winAudioData.audioClip,)
        }
        public void EnterGame()
        {
            isInitialOpen = true;
            onEnterGame.Invoke();
            //AudioManager.Instance().ResumeMusic();
        }
        public void GameRestart()
        {
            Time.timeScale = 1;
            BuffManager.Instance().RemoveNewBuff();
            TransitManager.Instance().TransitScene("Snake", "Snake");
        }
        public void ClearAllBuff()
        {
            Time.timeScale = 1;
            BuffManager.Instance().ClearAllBuff();
            turnCount = 0;
            remainLife = maxRemainLife;
            isPresentSnake = true;
            TransitManager.Instance().TransitScene("Snake", "Snake");
        }
        public void Continue()
        {
            Time.timeScale = 1;
            turnCount++;
            remainLife = maxRemainLife;
            isPresentSnake = turnCount % 2 == 0;
            TransitManager.Instance().TransitScene("Snake", "Snake");
        }
        public void PlayerWin()
        {
            isGameStart = false;
            //AudioManager.Instance().PlayVFX("Win",0.4f);
            //AudioManager.Instance().PauseMusic();
            onWin.Invoke();
            
        }
        public void PlayerLose()
        {
            isGameStart = false;
            remainLife--;
            if (remainLife == 0)
            {
                //AudioManager.Instance().PlayVFX("LifeOver", 0.4f);
                //AudioManager.Instance().PauseMusic();
                onLifeOver.Invoke();
            }
            else
            {
                onLose.Invoke();
                //AudioManager.Instance().PlayVFX("Lose", 0.4f);
                //AudioManager.Instance().PauseMusic();
            }
        }
	}
}
