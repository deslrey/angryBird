using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Brid
{

    /// <summary>
    /// 重写炫技方法
    /// </summary>
    public override void ShowSkill()
    {
        base.ShowSkill();
        rg.velocity *= 2;
    }

}
