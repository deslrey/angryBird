using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird
{

    /// <summary>
    /// ��д�ż�����
    /// </summary>
    public override void ShowSkill()
    {
        base.ShowSkill();
        rg.velocity *= 2;
    }

}
