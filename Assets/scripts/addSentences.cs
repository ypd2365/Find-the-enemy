using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/addSentences", order = 1)]

public class addSentences : ScriptableObject
{
    [Serializable]
    public class Sentences//class which stores sentences as per level
    {
        public List<string> name = new List<string>();

    }
    public Sentences[] sentence;//levels of sentences
}
