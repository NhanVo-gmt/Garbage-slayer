using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DescriptionView : Label
{
    public new class UxmlFactory : UxmlFactory<DescriptionView, Label.UxmlTraits> {}

    public DescriptionView() 
    {

    }

    public void UpdateText(string text)
    {
        this.text = text;
    }
}
