// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Coyote.Actors;

namespace Microsoft.Coyote.Samples.CoffeeMachineActors
{
    // This file contains the events you can use to talk read/write sensor values
    // Think of this as the "async interface" to the sensors, where the client could
    // be talking to the real machine or just a mock implementation of the machine.

    /// <summary>
    /// Internal mock sensor flag, essentially tells the sensor if we are
    /// running in production mode (RunSlowly=true) or test mode (RunSlowly=false).
    /// </summary>
    internal class ConfigEvent : Event
    {
        public bool RunSlowly;

        public ConfigEvent(bool runSlowly)
        {
            this.RunSlowly = runSlowly;
        }
    }

    /// <summary>
    /// Pass this caller ActorId to each sensor so it knows how to call you back.
    /// </summary>
    internal class RegisterClientEvent : Event
    {
        public ActorId Caller;

        public RegisterClientEvent(ActorId caller) { this.Caller = caller; }
    }

    /// <summary>
    /// Returned from ReadDoorOpenEvent, cannot set this value.
    /// </summary>
    internal class DoorOpenEvent : Event
    {
        public bool Open; // true if open, a safety check to make sure machine is buttoned up properly before use.

        public DoorOpenEvent(bool value) { this.Open = value; }
    }

    internal class ReadDoorOpenEvent : Event { }
}
