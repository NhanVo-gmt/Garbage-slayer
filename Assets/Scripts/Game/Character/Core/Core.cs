using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Core : MonoBehaviour
{
    List<CoreComponent> coreComponentList = new List<CoreComponent>();

    public void AddCoreComponent(CoreComponent coreComponent)
    {
        if (coreComponentList.Contains(coreComponent)) 
        {
            return;
        }

        coreComponentList.Add(coreComponent);
    }

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        var component = coreComponentList.OfType<T>().FirstOrDefault();

        if (component == null)
        {
            Debug.LogError("There is no component");
        }

        return component;
    }
}
