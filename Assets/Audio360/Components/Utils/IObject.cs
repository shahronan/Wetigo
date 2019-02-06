/*
 *  Copyright 2013-Present Two Big Ears Limited
 *  All rights reserved.
 *  http://twobigears.com
 */

using UnityEngine;
using System.Collections;

namespace TBE
{
    public interface IObject 
    {
        void onInit();
        void onTerminate();

        bool mustNotDestroyOnLoad();
    }
}
