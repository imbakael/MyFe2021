using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyContainer {

    public delegate object Creator(MyContainer container);

    private Dictionary<Type, Creator> typeToCreator = new Dictionary<Type, Creator>();

    public void Register<T>(Creator creator) {
        typeToCreator.Add(typeof(T), creator);
    }

    public T Create<T>() {
        return (T)typeToCreator[typeof(T)](this);
    }

}
