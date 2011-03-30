// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.UnitTests.Context
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ChannelMessageObservableContext
    {
        private readonly ISubject<ChannelMessage> subject = new FastReplaySubject<ChannelMessage>();
        public ChannelMessageObservableContext()
        {
            Values = subject.AsObservable();
        }

        public IObservable<ChannelMessage> Values { get; set; }
        public IObserver<ChannelMessage> Source { get { return this.subject.AsObserver(); } }
    }
}