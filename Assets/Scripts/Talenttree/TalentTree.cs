using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentTree : MonoBehaviour
{

    [SerializeField]
    private Talent[] talents;

    [SerializeField]
    private Talent[] unlockedByDefault;



    // Start is called before the first frame update
    void Start()
    {
        ResetTalents();
    }


    private void ResetTalents()
    {
        foreach (Talent talent in talents)
        {
            talent.Lock();
        }

        foreach (Talent talent in unlockedByDefault)
        {
            talent.Unlock();
        }
    }
}
