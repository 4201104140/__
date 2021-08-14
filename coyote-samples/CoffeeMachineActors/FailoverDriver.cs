// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;
using Microsoft.Coyote.Actors;
using Microsoft.Coyote.Actors.Timers;
using Microsoft.Coyote.Samples.Common;
using static Microsoft.Coyote.Samples.CoffeeMachineActors.FailoverDriver;

namespace Microsoft.Coyote.Samples.CoffeeMachineActors
{
    /// <summary>
    /// This class is designed to test how the CoffeeMachine handles "failover" or specifically,
    /// can it correctly "restart after failure" without getting into a bad state.  The CoffeeMachine
    /// will be randomly terminated.  The only thing the CoffeeMachine can depend on is
    /// the persistence of the state provided by the MockSensors.
    /// </summary>
    internal class FailoverDriver : StateMachine
    {
        private ActorId DoorSensorId;

        private bool RunForever;
        private int Iterations;
        private readonly LogWriter Log = LogWriter.Instance;

        internal class StartTestEvent : Event { }

        [Start]
        [OnEntry(nameof(OnInit))]
        [OnEventGotoState(typeof(StartTestEvent), typeof(Test))]
        internal class Init : State { }

        internal void OnInit(Event e)
        {
            if (e is ConfigEvent ce)
            {
                this.RunForever = ce.RunSlowly;
            }

            // Create the persistent sensor state
            this.DoorSensorId = this.CreateActor(typeof(MockDoorSensor), new ConfigEvent(this.RunForever));
        }

        [OnEntry(nameof(OnStartTest))]
        [OnEventDoAction(typeof(TimerElapsedEvent), nameof(HandleTimer))]
        [OnEventGotoState(typeof(CoffeeMachine.CoffeeCompletedEvent), typeof(Stop))]
        internal class Test : State { }

        internal void OnStartTest()
        {
            this.Log.WriteLine("#################################################################");
            this.Log.WriteLine("starting new CoffeeMachine.");
        }

        private void HandleTimer()
        {
            this.RaiseGotoStateEvent<Stop>();
        }

        private void OnStopTest()
        {
            if (this.RunForever || this.Iterations == 0)
            {
                this.Iterations += 1;
                // Run another CoffeeMachine instance!
                this.RaiseGotoStateEvent<Test>();
            }
        }

        [OnEntry(nameof(OnStopTest))]
        [OnEventDoAction(typeof(CoffeeMachine.HaltedEvent), nameof(OnCoffeeMachineHalted))]
        [IgnoreEvents(typeof(CoffeeMachine.CoffeeCompletedEvent))]
        internal class Stop : State { }

        internal void OnCoffeeMachineHalted()
        {
            // ok, the CoffeeMachine really is halted now, so we can go to the stopped state.
            this.RaiseGotoStateEvent<Stopped>();
        }

        [OnEntry(nameof(OnStopped))]
        internal class Stopped : State { }

        private void OnStopped()
        {
            if (this.RunForever || this.Iterations == 0)
            {
                this.Iterations += 1;
                // Run another CoffeeMachine instance!
                this.RaiseGotoStateEvent<Test>();
            }
        }
    }
}
