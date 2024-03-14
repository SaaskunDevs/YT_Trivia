using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "Questions", menuName = "QuestionsSO", order = 0)]
public class Questions : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string question = "Enter your question here";
    
    [TextArea(2, 6)]
    [SerializeField] string[] answers = new string[4];
    [SerializeField] public int correctAnswer;
    [SerializeField] int lifes = 2;

    public string GetQuestion()
    {
        return question;
    } 

    public string GetAnswer(int index)
    {
        // Verifica si el índice está dentro del rango de respuestas
        if (index >= answers.Length)
            return "";
        else // Si está dentro del rango, devuelve la respuesta
            return answers[index];
    }

    public int GetAnswerLength()
    {
        return answers.Length;
    }

    public void Lifes()
    {
        lifes--;
    }

    public int GetLifes()
    {
        return lifes;
    }
}
