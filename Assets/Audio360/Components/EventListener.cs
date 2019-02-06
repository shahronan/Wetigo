/*
 *  Copyright 2013-Present Two Big Ears Limited
 *  All rights reserved.
 *  http://twobigears.com
 */

using UnityEngine;
using System.Collections;

namespace TBE
{   
    public class EventListener 
    {
        public delegate void EventDelegate(TBE.Event e);
        public EventDelegate newEvent;

        public void onNewEvent(TBE.Event e)
        {
            if (newEvent != null)
            {
                newEvent(e);
            }
        }
    }

}