using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class KanaUI : MonoBehaviour
{
    //�������QuestionSO��List
    [SerializeField] private List<QuestionSO> questions;

    //InputField
    [SerializeField] private TMP_InputField inputField;

    //Title Text
    [SerializeField] private TextMeshProUGUI titleText;

    //��ȷ��
    [SerializeField] private string answer;

    [SerializeField] private int lastQuestionIndex = -1;

    //��Ч������
    [SerializeField] private AudioSource audioSource;

    //�ش���ȷ����Ч
    [SerializeField] private AudioClip correctClip;

    //�ش�������Ч
    [SerializeField] private AudioClip errorClip;

    private void OnEnable()
    {
        //����InputField��OnEndEdit�¼�
        inputField.onEndEdit.AddListener(OnInputFieldEndEdit);
    }

    private void OnInputFieldEndEdit(string input)
    {
        if (input == answer)
        {
            //�ش���ȷ��������Ч
            audioSource.PlayOneShot(correctClip);
            //������Ŀ
            SetQuestion();
        }
        else
        {
            //�ش���󣬲�����Ч
            audioSource.PlayOneShot(errorClip);
            //���InputField
            inputField.text = "";
            //ѡ��InputField
            inputField.Select();
            inputField.ActivateInputField();
        }
    }

    private void Start()
    {
        //������Ŀ
        SetQuestion();
    }

    //������Ŀ
    private void SetQuestion()
    {
        //���ѡ��һ��QuestionSO
        QuestionSO question = questions[Random.Range(0, questions.Count)];
        //�����һ�����һ��һ��������ѡ��
        while (questions.Count > 1 && questions.IndexOf(question) == lastQuestionIndex)
        {
            question = questions[Random.Range(0, questions.Count)];
        }
        lastQuestionIndex = questions.IndexOf(question);

        //������Ŀ
        titleText.text = question.title;

        //������ȷ��
        answer = question.answer;

        //���InputField
        inputField.text = "";

        //ѡ��InputField
        inputField.Select();
        inputField.ActivateInputField();
    }
}