using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Energy {

    //public Status status;

    public float fossilSource;
    public float cleanSource;
    public float nuclearSource;

    public Energy()
    {
        fossilSource  = 94.0f;
        cleanSource   = 5.0f;
        nuclearSource = 1.0f;

        //status = Status.Bad;
    }
}
