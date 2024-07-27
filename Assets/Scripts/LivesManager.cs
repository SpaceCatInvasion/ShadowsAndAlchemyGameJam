using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public List<GameObject> lifeSprites;
    public static LivesManager instance;
    private int index;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            index = lifeSprites.Count - 1;
        }
    }
    public void TakeLife()
    {
        lifeSprites[index].SetActive(false);
        if (index > 0) index--;
    }

    public void AddLives(int lives)
    {
        int i;
        for (i = index + 1; i < index + lives && i < lifeSprites.Count; i++)
        {
            lifeSprites[i].SetActive(true);
        }
        index = --i;
    }
}
