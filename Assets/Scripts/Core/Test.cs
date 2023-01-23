using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public FelineFrenzy.Core.Collection collection;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        FelineFrenzy.Core.Collection temp = Instantiate(collection.gameObject).GetComponent<FelineFrenzy.Core.Collection>();
        temp.Left = collection.Right;
        collection = temp;
    }
}
