using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public int HP;
    public Image[] HPImages; 

    public State state;
    public float Timer;
    public float pauseTime;
    public GameObject[] fruitPrefabs;
    public SpawnManager[] levels;
    public int currentLevel;
    public Slicer slicer;
    public GameObject slice;
    public Animator curtains;
    public GameObject panel;
    public Animator anim;
    public Animator panelAnim;
    bool panelActive;
    public GameObject gameOverPanel;

    public TMP_Text levelText;

    public bool Reversed;
    // Start is called before the first frame update
    void Start()
    {
        HP = 3;
        state = State.LevelEnd;
        Timer = Time.time;
        anim.Play("save_hp");
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {

            case State.Start:
                if(Time.time > Timer + pauseTime)
                {
                    state = State.Playing;

                }
                break;
            case State.Playing:
                if(Reversed && slicer.fruits.Count == 0)
                {
                    state = State.LevelEnd;
                    Timer = Time.time;
                    curtains.Play("curtains_close");
                    anim.Play("save_hp");
                    break;
                }
                else if(Reversed && slicer.slicesLeft == 0 && Time.time > slicer.timer + levels[currentLevel].sliceTime +  1f)
                {
                    GameOver();
                    break;
                }
                if(!Reversed && slicer.slicesLeft == 0 && Time.time > slicer.timer + levels[currentLevel].sliceTime + 1f)
                {
                    state = State.LevelEnd;
                    Timer = Time.time;
                    curtains.Play("curtains_close");
                    anim.Play("save_hp");
                }
                break;
            case State.LevelEnd:
                if (Time.time > Timer + 1f)
                {
                    if (!panelActive)
                    {
                        HPImages[2].color = new Color(1f, 1f, 1f, 1);
                        HPImages[1].color = new Color(1f, 1f, 1f, 1);
                        HPImages[0].color = new Color(1f, 1f, 1f, 1);
                        levelText.text = "Level - " + (currentLevel + 1).ToString();
                        panel.SetActive(true);
                        if (levels[currentLevel + 1].Reversed)
                        {

                            panelAnim.Play("destroy");
                        }
                        else
                        {
                            anim.Play("destroy_hp");
                            panelAnim.Play("save");
                        }
                        panelActive = true;
                    }

                    if (Time.time > Timer + 4f)
                    {
                        if(slicer != null)
                        {
                            slicer.EndSlicer();
                        }
                        slicer = null;
                        currentLevel = currentLevel + 1;
                        SpawnManager sp = levels[currentLevel];
                        Reversed = sp.Reversed;
                        slicer = gameObject.AddComponent<Slicer>();
                        slicer.fruitPrefabs = fruitPrefabs;
                        slicer.level = currentLevel;
                        slicer.slice = slice;
                        slicer.Reversed = sp.Reversed;
                        slicer.sliceInterval = sp.sliceInterval;
                        slicer.sliceTime = sp.sliceTime;
                        slicer.slicesLeft = sp.slices;
                        slicer.player = this;
                        slicer.maxSlices = sp.maxSlices;

                        slicer.SpawnFruits(sp.fruitAmounts);
                        state = State.Start;
                        Timer = Time.time;
                        curtains.Play("curtains_open");
                        HP = 3;


                        panelActive = false;
                        panel.SetActive(false);
                    }
                }
                break;

        }
    }

    public void LoseHP()
    {
        HP -= 1;
        switch (HP)
        {
            case 2:
                HPImages[2].color = new Color(0.1f, 0.1f, 0.1f, 1);
                break;
            case 1:
                HPImages[1].color = new Color(0.1f, 0.1f, 0.1f, 1);
                break;
            case 0:
                HPImages[0].color = new Color(0.1f, 0.1f, 0.1f, 1);
                GameOver();
                break;
        }
    }


    private void GameOver()
    {
        slicer.EndSlicer();
        gameOverPanel.SetActive(true);
        state = State.GameOver;
    }

    public void Retry()
    {
        currentLevel -= 1;
        Timer = Time.time;
        curtains.Play("curtains_close");
        gameOverPanel.SetActive(false);
        HP = 3;
        HPImages[2].color = new Color(1f, 1f, 1f, 1);
        HPImages[1].color = new Color(1f, 1f, 1f, 1);
        HPImages[0].color = new Color(1f, 1f, 1f, 1);
        anim.Play("save_hp");
        state = State.LevelEnd;
    }

    public void PlayHeartAudio()
    {

    }

    public enum State
    {
        Start,
        Playing,
        LevelEnd,
        GameOver
    }
}
