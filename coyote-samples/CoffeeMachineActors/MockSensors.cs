// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Coyote.Actors;
using Microsoft.Coyote.Actors.Timers;
using Microsoft.Coyote.Samples.Common;
using Microsoft.Coyote.Specifications;

namespace Microsoft.Coyote.Samples.CoffeeMachineActors
{
    public class BusyEvent : Event { }

    /// <summary>
    /// This safety monitor ensure nothing bad happens while a door is open on the
    /// coffee machine.
    /// </summary>
    internal class DoorSafetyMonitor : Monitor
    {
        [Start]
        [OnEventGotoState(typeof(DoorOpenEvent), typeof(Error))]
        [IgnoreEvents(typeof(BusyEvent))]
        private class Init : State { }

        [OnEventDoAction(typeof(BusyEvent), nameof(OnBusy))]
        private class Error : State { }

        private void OnBusy()
        {
            this.Assert(false, "Should not be doing anything while door is open");
        }
    }

    /// <summary>
    /// This Actor models is a sensor that detects whether any doors on the coffee machine are open.
    /// For safe operation, all doors must be closed before machine will do anything.
    /// </summary>
    [OnEventDoAction(typeof(ReadDoorOpenEvent), nameof(OnReadDoorOpen))]
    [OnEventDoAction(typeof(RegisterClientEvent), nameof(OnRegisterClient))]
    internal class MockDoorSensor : Actor
    {
        private bool DoorOpen;
        private ActorId Client;

        protected override Task OnInitializeAsync(Event initialEvent)
        {
            // Since this is a mock, we randomly it to false with one chance out of 5 just
            // to test this error condition, if the door is open, the machine should not
            // agree to do anything for you.
            this.DoorOpen = this.RandomBoolean(5);
            if (this.DoorOpen)
            {
                this.Monitor<DoorSafetyMonitor>(new DoorOpenEvent(this.DoorOpen));
            }

            return base.OnInitializeAsync(initialEvent);
        }

        private void OnRegisterClient(Event e)
        {
            this.Client = ((RegisterClientEvent)e).Caller;
        }

        private void OnReadDoorOpen()
        {
            if (this.Client != null)
            {
                this.SendEvent(this.Client, new DoorOpenEvent(this.DoorOpen));
            }
        }
    }
}
