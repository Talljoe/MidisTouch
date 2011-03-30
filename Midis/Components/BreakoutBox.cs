// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BreakoutBox : IDisposable
    {
        private readonly ISubject<ChannelMessage> channelPressure = new FastSubject<ChannelMessage>();
        private readonly ISubject<ChannelMessage> controllerChange = new FastSubject<ChannelMessage>();
        private readonly IDisposable disposable;

        private readonly ISubject<ChannelMessage> notes = new FastSubject<ChannelMessage>();
        private readonly ISubject<ChannelMessage> pitchBend = new FastSubject<ChannelMessage>();
        private readonly ISubject<ChannelMessage> polyphonicPressure = new FastSubject<ChannelMessage>();
        private readonly ISubject<ChannelMessage> programChange = new FastSubject<ChannelMessage>();
        private readonly IList<ISubject<ChannelMessage>> subjects;

        public BreakoutBox(IObservable<ChannelMessage> source)
        {
            this.disposable = source.Subscribe(this.OnMessage, this.OnError, this.OnCompleted);
            this.subjects = new List<ISubject<ChannelMessage>>
                                {
                                    this.notes,
                                    this.controllerChange,
                                    this.programChange,
                                    this.pitchBend,
                                    this.channelPressure,
                                    this.polyphonicPressure
                                };
        }

        ~BreakoutBox()
        {
            this.Dispose(false);
        }

        public IObservable<ChannelMessage> ChannelPressure
        {
            get { return this.channelPressure.AsObservable(); }
        }

        public IObservable<ChannelMessage> ControllerChange
        {
            get { return this.controllerChange.AsObservable(); }
        }

        public IObservable<ChannelMessage> Notes
        {
            get { return this.notes.AsObservable(); }
        }

        public IObservable<ChannelMessage> PitchBend
        {
            get { return this.pitchBend.AsObservable(); }
        }

        public IObservable<ChannelMessage> PolyphonicPressure
        {
            get { return this.polyphonicPressure.AsObservable(); }
        }

        public IObservable<ChannelMessage> ProgramChange
        {
            get { return this.programChange.AsObservable(); }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void OnMessage(ChannelMessage message)
        {
            this.GetSubject(message).OnNext(message);
        }

        private void OnError(Exception exception)
        {
            this.subjects.Run(s => s.OnError(exception));
        }

        private void OnCompleted()
        {
            this.subjects.Run(s => s.OnCompleted());
        }

        private ISubject<ChannelMessage> GetSubject(ChannelMessage message)
        {
            switch (message.MessageType)
            {
                case ChannelMessageType.NoteOff:
                case ChannelMessageType.NoteOn:
                    return this.notes;
                case ChannelMessageType.PolyphonicPressure:
                    return this.polyphonicPressure;
                case ChannelMessageType.ControllerChange:
                    return this.controllerChange;
                case ChannelMessageType.ProgramChange:
                    return this.programChange;
                case ChannelMessageType.ChannelPressure:
                    return this.channelPressure;
                case ChannelMessageType.PitchBend:
                    return this.pitchBend;
                default:
                    throw new InvalidOperationException("Unknown message type.");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.disposable.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}