using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContentLong : PlayerContent {

    public override bool HasWon(){
        return fetchProgress >= 100.0f ? true : false;
    }
}
