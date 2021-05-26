// ========================== 
// 描述：延迟执行
// 作者：郭佳龙
// 创建时间：2021/05/25 19:36:48 
// 版本：1.0
// 修改描述：
// 修改作者：
// 修改后版本：
// ========================== 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayExec : MonoBehaviour
{
    struct INFO
    {
        public float start;
        public float delay;
        public Action ev;
    }
    Action start = () => { };
    Dictionary<string, INFO> list = new Dictionary<string, INFO>();
    Action end = () => { };

    void Update()
    {
        start();
        start = () => { };
        foreach (string name in list.Keys)
        {
            if (Time.time - list[name].start > list[name].delay)
            {
                list[name].ev();
                Del(name);
            }
        }
        end();
        end = () => { };
    }

    public DelayExec Add(string name, Action ev, float time)
    {
        INFO temp;
        temp.start = Time.time;
        temp.delay = time;
        temp.ev = ev;
        start += () =>
        {
            if (list.ContainsKey(name))
            {
                list[name] = temp;
            }
            else
            {
                list.Add(name, temp);
            }
        };
        return this;
    }
    public DelayExec Del(string name)
    {
        if (list.ContainsKey(name))
        {
            end += () => { list.Remove(name); };
        }
        return this;
    }
}
