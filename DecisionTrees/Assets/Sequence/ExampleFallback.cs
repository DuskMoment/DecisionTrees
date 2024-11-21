using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleFallback : Sequence
{
    public ExampleFallback(GameObject obj) : base(obj)
    {

    }
    public override bool decision()
    {
        var ret = evaluateChildren();
        Debug.Log("did the sequence pass" + ret.didPass);
        return ret.didPass;
    }
    public override ReturnData evaluateChildren()
    {
        ReturnData returnData = new ReturnData(0);


        int ammountFail = 0;
        foreach (var child in children)
        {
            //see how many cases fail
            if (child.decision() == false)
            {
                ammountFail++;
            }

        }
        //check to see if pass or fail
        if (ammountFail >= children.Count)
        {
            returnData.didPass = false;
        }
        else
        {
            returnData.didPass = true;  
        }

        return returnData;
    }
}
