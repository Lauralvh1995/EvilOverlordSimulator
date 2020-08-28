using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinionNameGenerator
{
    private readonly string[] nameParts;

    public MinionNameGenerator()
    {
        nameParts = new string[20];
    }

    public string GenerateName()
    {
        string[] newNameBuilder = new string[UnityEngine.Random.Range(0, 5)];
        for(int i=0; i<newNameBuilder.Length; i++)
        {
            newNameBuilder[i] = nameParts[UnityEngine.Random.Range(0, nameParts.Length)];
        }

        string newName = "";

        foreach(string p in newNameBuilder)
        {
            newName += p;
        }

        return FirstCharToUpper(newName);
    }

    public string FirstCharToUpper(string input)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentException("ARGH!");
        return input.First().ToString().ToUpper() + input.Substring(1);
    }
}
