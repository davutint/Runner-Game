using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour,ICollectable
{
    public void Collect()
    {
        TextManager.instance.AddCoin();
    }

    
}
