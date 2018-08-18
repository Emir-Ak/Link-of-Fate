using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class Test : MonoBehaviour
{
    [SerializeField]
    public UnionType[] test= new UnionType[3];

    [Serializable]
    public class UnionType 
    {
        public string stringField;
        public UnionType[] arrayField;
        public Type type;

        public UnionType(string s)
        {
            stringField = s;
            type = typeof(string);
        }

        public UnionType(UnionType[] array)
        {
            arrayField = array;
            type = typeof(UnionType[]);
        }
    }

    private void Start()
    {
        UnionType[] array = new UnionType[] {
            new UnionType("string1"),
            new UnionType(new UnionType[] {
                new UnionType("string2"),
                new UnionType("string3")
            })
        };

        PrintArray(array);
    }
    
    public static void PrintArray(UnionType[] array)
    {
        foreach (UnionType elem in array)
        {
            if (elem.type == typeof(string))
            {
                Console.WriteLine(elem.stringField);
            }
            else
            {
                PrintArray(elem.arrayField);
            }
        }
    }
}