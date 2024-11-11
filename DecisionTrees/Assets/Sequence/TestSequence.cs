using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSequence : Sequence
{
    public TestSequence(GameObject obj) : base(obj) { }
    public override bool decision()
    {
        var data = evaluateChildren();

        if (data.didPass == true)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public override ReturnData evaluateChildren()
    {
        ReturnData returnData = new ReturnData(0);

        foreach (var child in children)
        {
            Debug.Log("iteration");
            if(child.decision())
            {
                returnData.lastAccsedIndex++;
            }
            else
            {
                //failed and set struct to false
                returnData.didPass = false;
                return returnData;
            }
        }

        return returnData;
    }
}
