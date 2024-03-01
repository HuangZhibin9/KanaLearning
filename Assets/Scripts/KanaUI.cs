using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class KanaUI : MonoBehaviour
{
    //存放所有QuestionSO的List
    [SerializeField] private List<QuestionSO> questions;

    //InputField
    [SerializeField] private TMP_InputField inputField;

    //Title Text
    [SerializeField] private TextMeshProUGUI titleText;

    //正确答案
    [SerializeField] private string answer;

    [SerializeField] private int lastQuestionIndex = -1;

    //音效播放器
    [SerializeField] private AudioSource audioSource;

    //回答正确的音效
    [SerializeField] private AudioClip correctClip;

    //回答错误的音效
    [SerializeField] private AudioClip errorClip;

    private void OnEnable()
    {
        //订阅InputField的OnEndEdit事件
        inputField.onEndEdit.AddListener(OnInputFieldEndEdit);
    }

    private void OnInputFieldEndEdit(string input)
    {
        if (input == answer)
        {
            //回答正确，播放音效
            audioSource.PlayOneShot(correctClip);
            //设置题目
            SetQuestion();
        }
        else
        {
            //回答错误，播放音效
            audioSource.PlayOneShot(errorClip);
            //清空InputField
            inputField.text = "";
            //选中InputField
            inputField.Select();
            inputField.ActivateInputField();
        }
    }

    private void Start()
    {
        //设置题目
        SetQuestion();
    }

    //设置题目
    private void SetQuestion()
    {
        //随机选择一个QuestionSO
        QuestionSO question = questions[Random.Range(0, questions.Count)];
        //如果上一题和这一题一样，重新选择
        while (questions.Count > 1 && questions.IndexOf(question) == lastQuestionIndex)
        {
            question = questions[Random.Range(0, questions.Count)];
        }
        lastQuestionIndex = questions.IndexOf(question);

        //设置题目
        titleText.text = question.title;

        //设置正确答案
        answer = question.answer;

        //清空InputField
        inputField.text = "";

        //选中InputField
        inputField.Select();
        inputField.ActivateInputField();
    }
}