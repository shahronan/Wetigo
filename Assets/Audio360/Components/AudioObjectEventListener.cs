/*
 *  Copyright 2013-Present Two Big Ears Limited
 *  All rights reserved.
 *  http://twobigears.com
 */

using UnityEngine;
using System.Collections;

namespace TBE
{
    public class AudioObjectEventListener
    {
        public delegate void EventDelegate(TBE.Event e, AudioObject obj);
        public EventDelegate newEvent;

        public void onNewEvent(TBE.Event e, AudioObject obj)
        {
            if (newEvent != null)
            {
                newEvent(e, obj);
            }
        }
    }

}