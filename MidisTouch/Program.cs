// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace MidisTouch
{
    using System;
    using System.Disposables;
    using System.Linq;
    using Midis;
    using Midis.Abstraction;
    using Midis.Components;
    using Midis.Devices;
    using Midis.Windows;
    using Ninject;

    internal class Program
    {
        private static IKernel GetNinjectKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IMidiHAL>().To<Win32MidiHAL>();
            kernel.Bind<IMidiHAL>().To<Loopback>().WithConstructorArgument("count", 1);
            return kernel;
        }

        private static void Main()
        {
            var kernel = GetNinjectKernel();
            var e = kernel.Get<MidiEnumerator>();
            Console.WriteLine("Ports:");
            using (var disposable = new CompositeDisposable())
            {
                var inPorts = e.GetInputDevices()
                               .Do(portInfo => Console.WriteLine("  In {0} - {1}", portInfo.Id, portInfo.Name))
                               .Select(portInfo => e.OpenMidiIn(portInfo.Id))
                               .Do(disposable.Add)
                               .Do(port => port.ChannelMessages.Subscribe(
                                            m => Console.WriteLine("{0}@{4}:{1}: ({2}, {3})",
                                                m.MessageType, m.Channel, m.Value1, m.Value2, port.Id)))
                               .ToList();

                var outPorts = e.GetOutputDevices()
                                .Do(portInfo => Console.WriteLine("  Out {0} - {1}: [{2}] {3}", portInfo.Id, portInfo.Name,
                                                portInfo.PortType, String.Concat(portInfo.WChannelMask.Cast<bool>().Select(b => b ? 'Y' : 'N'))))
                                .Select(portInfo => e.OpenMidiOut(portInfo.Id))
                                .Do(disposable.Add)
                                .ToList();

                using (var channelMessages = inPorts.ChannelMessages().BreakOut())
                {
                    channelMessages.Notes.ConnectTo(outPorts);
                    channelMessages.ControllerChange
                                   .Where(message => message.Value1 == 64) // Sustain
                                   .ToChannels(1, 2, 3, 4)
                                   .ConnectTo(outPorts);
                    channelMessages.ProgramChange.ToChannel(1).ConnectTo(outPorts);
                    Console.ReadKey();
                }
            }
        }
    }
}