using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLua;
using System;
using UnityEngine.UI;

[CustomLuaClass]
public class LuaFileManager : MonoBehaviour {

    public LuaSvr luaSvr;

    public LuaTable self;

    public delegate void LifeFunctionDelegate();

    public LifeFunctionDelegate d_start;
    public LifeFunctionDelegate d_update;
    public LifeFunctionDelegate d_onEnable;
    public LifeFunctionDelegate d_onDisable;

    public void TestArea()
    {

        
    }



	// Use this for initialization
	void Start () {
        luaSvr = new LuaSvr();
        luaSvr.init(null, OnComplete);

        if (d_start != null)
            d_start();

    }
	private void OnComplete()
    {
        self = (LuaTable)luaSvr.start("luaFile/knapsack");
        LuaFunction luafunc_start = (LuaFunction)self["Start"];
        LuaFunction luafunc_update = (LuaFunction)self["Update"];
        LuaFunction luafunc_onEnable = (LuaFunction)self["OnEnable"];
        LuaFunction luafunc_onDisable = (LuaFunction)self["OnDisable"];

        d_start = luafunc_start.cast<LifeFunctionDelegate>();
        d_update = luafunc_update.cast<LifeFunctionDelegate>();
        d_onEnable = luafunc_onEnable.cast<LifeFunctionDelegate>();
        d_onDisable = luafunc_onDisable.cast<LifeFunctionDelegate>();
    }

    private void Update()
    {
        if (d_update != null)
            d_update();
    }

    private void OnEnable()
    {
        if (d_onEnable != null)
            d_onEnable();
    }

    private void OnDisable()
    {
        if (d_onDisable != null)
            d_onDisable();
    }

    public static Sprite LoadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }
}
