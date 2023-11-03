using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class TutorialHandler : MonoBehaviour
{
    [SerializeField] private TutorialData tutorialData;


    [SerializeField] private TextMeshProUGUI tutorialTitle;
    [SerializeField] private Image tutorialImage;
    [SerializeField] private TextMeshProUGUI tutorialDescription;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;

    int tutorialIndex;

    void Start()
    {
        tutorialIndex = 0;

        tutorialTitle.SetText(tutorialData.tutorialTitle);
        tutorialImage.sprite = tutorialData.tutorialImages[tutorialIndex];
        tutorialDescription.SetText(tutorialData.textDescription[tutorialIndex]);
    }


    void Update()
    {
        if (tutorialIndex < 0 || tutorialIndex >= tutorialData.tutorialImages.Length)
            return;
       
        nextButton.interactable = tutorialIndex < tutorialData.tutorialImages.Length - 1;
        previousButton.interactable = tutorialIndex > 0;

    }

    public void Next()
    {
        if (tutorialIndex >= tutorialData.tutorialImages.Length)
            return;

        tutorialIndex += 1;

        tutorialImage.sprite = tutorialData.tutorialImages[tutorialIndex];
        tutorialDescription.SetText(tutorialData.textDescription[tutorialIndex]);

        nextButton.interactable = tutorialIndex < tutorialData.tutorialImages.Length - 1;
        previousButton.interactable = tutorialIndex > 0;
    }

    public void Previous()
    {

        if (tutorialIndex <= 0)
        {
            tutorialIndex = 0;
        }

        tutorialIndex -= 1;

        tutorialImage.sprite = tutorialData.tutorialImages[tutorialIndex];
        tutorialDescription.SetText(tutorialData.textDescription[tutorialIndex]);

        nextButton.interactable = tutorialIndex < tutorialData.tutorialImages.Length - 1;
        previousButton.interactable = tutorialIndex > 0;
    }

}
