// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace MidisTouch
{
    using System;
    using System.Linq;
    using Midis;

    internal class Program
    {
        private static void Main()
        {
            var e = new MidiEnumerator();
            Console.WriteLine("Ports:");

            var inPorts = e.GetInputDevices();
            foreach (var port in inPorts)
            {
                Console.WriteLine("  In {0} - {1}", port.PortId, port.Name);
            }

            var outPorts = e.GetOutputDevices();
            foreach (var port in outPorts)
            {
                Console.WriteLine("  Out {0} - {1}: [{2}] {3}", port.PortId, port.Name, port.PortType,
                                  String.Concat(port.WChannelMask.Cast<bool>().Select(b => b ? 'Y' : 'N')));
            }
        }
    }
}