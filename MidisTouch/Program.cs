// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace MidisTouch
{
    using System;
    using System.Linq;
    using System.Threading;
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
                Console.WriteLine("  Out {0} - {1}: [{2}] {3}", port.Id, port.Name, port.PortType,
                                  String.Concat(port.WChannelMask.Cast<bool>().Select(b => b ? 'Y' : 'N')));
                using(var p = e.OpenMidiOut(port.Id))
                {
                    var c = p.OpenChannels(1, 3, 5);
                    var c2 = p.OpenChannels(5);
                    c.NoteOn(64);
                    Thread.Sleep(2000);
                    c2.NoteOff(64);
                    c2.NoteOn(72);
                    Thread.Sleep(2000);
                    c.NoteOff(64);
                    c.NoteOff(72);
                }
            }
        }
    }
}