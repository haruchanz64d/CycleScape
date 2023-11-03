using UnityEngine.UI;
using UnityEngine;
[CreateAssetMenu(fileName = "Tutorial Data", menuName = "Tutorial/New Tutorial Data")]
public class TutorialData : ScriptableObject
{
    public string tutorialTitle;
    public Sprite[] tutorialImages;
    public string[] textDescription;
}