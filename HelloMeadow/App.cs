using System;
using System.Collections.Generic;
using System.Threading;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation.Leds;
using Meadow.Foundation.Sensors.Buttons;
using Meadow.Hardware;

namespace BasicHelloMeadow
{
    public class App : App<F7Micro, App>
    {
        IDigitalOutputPort redLed;
        IDigitalOutputPort blueLed;
        IDigitalOutputPort greenLed;

        List<PwmLed> _leds;

        public App()
        {
            ConfigurePorts();

            //var pushButton = new PushButton(Device, Device.Pins.D08);
            //pushButton.Clicked += PushButton_Clicked;
            BlinkLeds();
            //Thread.Sleep(Timeout.Infinite);
        }

        private void PushButton_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Click");
        }

        public void ConfigurePorts()
        {
            Console.WriteLine("Creating Outputs...");
            redLed = Device.CreateDigitalOutputPort(Device.Pins.OnboardLedRed);
            blueLed = Device.CreateDigitalOutputPort(Device.Pins.OnboardLedBlue);
            greenLed = Device.CreateDigitalOutputPort(Device.Pins.OnboardLedGreen);

            _leds = new List<PwmLed>()
            {
                new PwmLed(Device.CreatePwmPort(Device.Pins.D08), TypicalForwardVoltage.Red),
                new PwmLed(Device.CreatePwmPort(Device.Pins.D07), TypicalForwardVoltage.Green),
                new PwmLed(Device.CreatePwmPort(Device.Pins.D06), TypicalForwardVoltage.Blue),
                new PwmLed(Device.CreatePwmPort(Device.Pins.D05), TypicalForwardVoltage.Yellow)
            };
            
        }

        public void BlinkLeds()
        {
            var state = false;

            while (true)
            {
                state = !state;

                Console.WriteLine($"State: {state}");

                redLed.State = state;
                Thread.Sleep(500);
                blueLed.State = state;
                Thread.Sleep(500);
                greenLed.State = state;
                Thread.Sleep(500);

                foreach(var led in _leds)
                {
                    led.IsOn = state;
                    Thread.Sleep(500);
                }
            }
        }
    }
}
