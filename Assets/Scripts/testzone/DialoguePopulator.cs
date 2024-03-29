using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialoguePopulator : MonoBehaviour
{
     public CSVtoSOTwo csvToSOTwo;
    private int currentIndex = 0;
    //private int destinationID = 0;
    private VisualElement dlogElements;
    private VisualElement dlogBG;
    private VisualElement propImage;
    private VisualElement characterImageLeft;
    private VisualElement characterImageRight;
    private VisualElement rewindUI;
    private Button rewind;
    private Button rewindYes;
    private Button rewindNo;
    private Button journal;
    ///

    ///
    private VisualElement DBox;
    private TextElement nameText;
    private TextElement dialogueText;
    private Button nextButton;
    ///
    private VisualElement twoOptionAnswers;
    private TextElement questionTwoAnswers;
    private TextElement textATwo;
    private Button aTwo;
    private TextElement textBTwo;
    private Button bTwo;
    ///
    private VisualElement threeOptionAnswers;
    private TextElement questionThreeAnswers;
    private TextElement textAThree;
    private Button aThree;
    private TextElement textBThree;
    private Button bThree;
    private TextElement textCThree;
    private Button cThree;

    private string testText;

 private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        dlogElements = root.Q<VisualElement>("dlog-elements");
        dlogBG = root.Q<VisualElement>("dlog-bg");
        characterImageLeft = root.Q<VisualElement>("CharacterLeft");
        characterImageRight = root.Q<VisualElement>("CharacterRight");
        propImage = root.Q<VisualElement>("Props");
        rewindUI = root.Q<VisualElement>("Rewind");

        DBox = root.Q<VisualElement>("DBox");
        nameText = root.Q<TextElement>("DialogueName");
        dialogueText = root.Q<TextElement>("DialogueText");

        rewind = root.Q<Button>("rewind-button");
        rewindYes = root.Q<Button>("rewind-yes");
        rewindNo = root.Q<Button>("rewind-no");
        nextButton = root.Q<Button>("dlog-button");

        twoOptionAnswers = root.Q<VisualElement>("answer-two-options-bg");
        questionTwoAnswers = root.Q<TextElement>("question-text-two");
        textATwo = root.Q<TextElement>("text-a-two");
        aTwo = root.Q<Button>("button-a-two");
        textBTwo = root.Q<TextElement>("text-b-two");
        bTwo = root.Q<Button>("button-b-two");

        threeOptionAnswers = root.Q<VisualElement>("answer-three-options-bg");
        questionThreeAnswers = root.Q<TextElement>("question-text-three");
        textAThree = root.Q<TextElement>("text-a-three");
        aThree = root.Q<Button>("button-a-three");
        textBThree = root.Q<TextElement>("text-b-three");
        bThree = root.Q<Button>("button-b-three");
        textCThree = root.Q<TextElement>("text-c-three");
        cThree = root.Q<Button>("button-c-three");

        nextButton.RegisterCallback<ClickEvent>(NextDialogue);
        aTwo.RegisterCallback<ClickEvent>(NextDialogueA);
        bTwo.RegisterCallback<ClickEvent>(NextDialogueB);
        aThree.RegisterCallback<ClickEvent>(NextDialogueA);
        bThree.RegisterCallback<ClickEvent>(NextDialogueB);
        cThree.RegisterCallback<ClickEvent>(NextDialogueC);

        rewind.RegisterCallback<ClickEvent>(ShowRewind);
        rewindYes.RegisterCallback<ClickEvent>(RewindYes);
        rewindNo.RegisterCallback<ClickEvent>(RewindNo);

        rewindUI.style.display = DisplayStyle.None;
        twoOptionAnswers.style.display = DisplayStyle.None;
        threeOptionAnswers.style.display = DisplayStyle.None;
        PopulateUI();
    }
    private void PopulateUI()
    {
        var dialogueSO = csvToSOTwo.dialogues[currentIndex];

        dlogBG.style.backgroundImage = new StyleBackground(dialogueSO.Background);
        characterImageLeft.style.backgroundImage = new StyleBackground(dialogueSO.LeftSideSpeaker);
        characterImageRight.style.backgroundImage = new StyleBackground(dialogueSO.RightSideSpeaker);
        propImage.style.backgroundImage = new StyleBackground(dialogueSO.Prop);

        if (dialogueSO.Type.Equals("a", StringComparison.CurrentCultureIgnoreCase))
        {
            dlogBG.style.display = DisplayStyle.Flex;
            DBox.style.display = DisplayStyle.Flex;
            twoOptionAnswers.style.display = DisplayStyle.None;
            threeOptionAnswers.style.display = DisplayStyle.None;

            nameText.text = dialogueSO.Speaker;
            //dialogueText.text = dialogueSO.Line;
            testText = dialogueSO.Line;
            ScrollText();
        }
        else if (dialogueSO.Type.Equals("b", StringComparison.CurrentCultureIgnoreCase))
        {
            dlogBG.style.display = DisplayStyle.Flex;
            DBox.style.display = DisplayStyle.None;
            twoOptionAnswers.style.display = DisplayStyle.Flex;
            threeOptionAnswers.style.display = DisplayStyle.None;

            questionTwoAnswers.text = dialogueSO.Line;
            textATwo.text = dialogueSO.A1Answer;
            textBTwo.text = dialogueSO.A2Answer;
        }
        else if (dialogueSO.Type.Equals("c", StringComparison.CurrentCultureIgnoreCase))
        {
            dlogBG.style.display = DisplayStyle.Flex;
            DBox.style.display = DisplayStyle.None;
            twoOptionAnswers.style.display = DisplayStyle.None;
            threeOptionAnswers.style.display = DisplayStyle.Flex;

            questionThreeAnswers.text = dialogueSO.Line;
            textAThree.text = dialogueSO.A1Answer;
            textBThree.text = dialogueSO.A2Answer;
            textCThree.text = dialogueSO.A3Answer;
        }
    }

    public void ScrollText()
    {
        StopAllCoroutines();
        StartCoroutine(TypeSentence(testText));

    }
    IEnumerator TypeSentence (string sentence) 
    {   //char space = " ";
        dialogueText.text = "";
        int counter = 0;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            // if (stopAudioSource)
            // {
            //     audioSource.Stop();
            // }
            // if ((letter.Equals(" ") == false) && (counter % charInterval == 0))
            // {
            //     audioSource.PlayOneShot(dialogueTypingSoundClip);
            // }
            yield return new WaitForSeconds(.02f);
            counter++;
        }
    }
    private void NextDialogue(ClickEvent evt)
    {
        var dialogueSO = csvToSOTwo.dialogues[currentIndex];
        currentIndex = dialogueSO.GoToID;
        PopulateUI();
    }
    private void NextDialogueA(ClickEvent evt)
    {
        var dialogueSO = csvToSOTwo.dialogues[currentIndex];
        currentIndex = dialogueSO.GoToIDA1;
        PopulateUI();
    }
    private void NextDialogueB(ClickEvent evt)
    {
        var dialogueSO = csvToSOTwo.dialogues[currentIndex];
        currentIndex = dialogueSO.GoToIDA2;
        PopulateUI();
    }
    private void NextDialogueC(ClickEvent evt)
    {
        var dialogueSO = csvToSOTwo.dialogues[currentIndex];
        currentIndex = dialogueSO.GoToIDA3;
        PopulateUI();
    }
    private void ShowRewind(ClickEvent evt)
    {
        rewindUI.style.display = DisplayStyle.Flex;
    }
    private void RewindYes(ClickEvent evt)
    {
        var dialogueSO = csvToSOTwo.dialogues[currentIndex];
        currentIndex = dialogueSO.Checkpoint;
        PopulateUI();
        rewindUI.style.display = DisplayStyle.None;
    }
    private void RewindNo(ClickEvent evt)
    {
        rewindUI.style.display = DisplayStyle.None;
    }
}


