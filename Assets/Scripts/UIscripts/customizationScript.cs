using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomizationScript : MonoBehaviour
{
    public List<GameObject> headParts;
    public List<GameObject> bodyParts;

    private int currentHeadIndex = 0;
    private int currentBodyIndex = 0;

    private void Start()
    {
        SetAllPartsInactive();
        SetActiveHeadPart(currentHeadIndex);
        SetActiveBodyPart(currentBodyIndex);
    }
    public void nextHeadPart()
    {
        CycleHeadParts(true);
    }
    public void prevHeadPart()
    {
        CycleHeadParts(false);
    }
    public void nextBodyPart()
    {
        CycleBodyParts(true);
    }
    public void prevBodyPart()
    {
        CycleBodyParts(false);
    }

    #region Cycle Parts
    void SetAllPartsInactive()
    {
        foreach (GameObject headPart in headParts)
        {
            headPart.SetActive(false);
        }
        foreach (GameObject bodyPart in bodyParts)
        {
            bodyPart.SetActive(false);
        }
    }

    void CycleHeadParts(bool forward)
    {
        SetActiveHeadPart(forward ? GetNextIndex(currentHeadIndex, headParts.Count) : GetPreviousIndex(currentHeadIndex, headParts.Count));
    }

    void CycleBodyParts(bool forward)
    {
        SetActiveBodyPart(forward ? GetNextIndex(currentBodyIndex, bodyParts.Count) : GetPreviousIndex(currentBodyIndex, bodyParts.Count));
    }

    int GetNextIndex(int currentIndex, int count)
    {
        return (currentIndex + 1) % count;
    }

    int GetPreviousIndex(int currentIndex, int count)
    {
        return (currentIndex - 1 + count) % count;
    }

    void SetActiveHeadPart(int index)
    {
        headParts[currentHeadIndex].SetActive(false);
        currentHeadIndex = index;
        headParts[currentHeadIndex].SetActive(true);
    }

    void SetActiveBodyPart(int index)
    {
        bodyParts[currentBodyIndex].SetActive(false);
        currentBodyIndex = index;
        bodyParts[currentBodyIndex].SetActive(true);
    }
    #endregion
}
