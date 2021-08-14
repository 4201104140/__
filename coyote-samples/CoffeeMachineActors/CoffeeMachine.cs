// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;
using Microsoft.Coyote.Actors;
using Microsoft.Coyote.Samples.Common;
using static Microsoft.Coyote.Samples.CoffeeMachineActors.FailoverDriver;

namespace Microsoft.Coyote.Samples.CoffeeMachineActors
{
    [OnEventDoAction(typeof(TerminateEvent), nameof(OnTerminate))]
    internal class CoffeeMachine : StateMachine
    {
        private ActorId Client;
        private ActorId WaterTank;
        private ActorId CoffeeGrinder;
        private ActorId DoorSensor;
        private readonly LogWriter Log = LogWriter.Instance;

        internal class ConfigEvent : Event
        {
            public ActorId WaterTank;
            public ActorId CoffeeGrinder;
            public ActorId Client;
            public ActorId DoorSensor;

            public ConfigEvent(ActorId waterTank, ActorId coffeeGrinder, ActorId doorSensor, ActorId client)
            {
                this.WaterTank = waterTank;
                this.CoffeeGrinder = coffeeGrinder;
                this.Client = client;
                this.DoorSensor = doorSensor;
            }
        }

        internal class MakeCoffeeEvent : Event
        {
            public int Shots;

            public MakeCoffeeEvent(int shots)
            {
                this.Shots = shots;
            }
        }

        internal class CoffeeCompletedEvent : Event
        {
            public bool Error;
        }

        internal class TerminateEvent : Event { }

        internal class HaltedEvent : Event { }

        [Start]
        [OnEntry(nameof(OnInit))]
        [DeferEvents(typeof(MakeCoffeeEvent))]
        private class Init : State { }

        private void OnInit(Event e)
        {
            if (e is ConfigEvent configEvent)
            {
                this.Log.WriteLine("initializing...");
                this.Client = configEvent.Client;
                this.WaterTank = configEvent.WaterTank;
                this.CoffeeGrinder = configEvent.CoffeeGrinder;
                this.DoorSensor = configEvent.DoorSensor;

                this.RaiseGotoStateEvent<Check>
            }
        }

        [OnEntry(nameof(OnError))]
        private class Error : State { }

        private void OnError()
        {
            if (this.Client != null)
            {
                this.SendEvent(this.Client, new CoffeeCompletedEvent() { Error = true });
            }
        }

        private void OnTerminate(Event e)
        {
            if (e is TerminateEvent te)
            {
                this.Log.WriteLine("Coffee Machine Terminating...");
                // better turn everything off then!
            }
        }
    }
}
