// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.UnitTests.ControllerChangeExtensions.Bindings
{
    using System;
    using System.Linq;
    using Midis.UnitTests.Context;
    using Midis.UnitTests.Utility;
    using TechTalk.SpecFlow;

    [Binding]
    public class When
    {
        private readonly ChannelMessageObservableContext context;

        public When(ChannelMessageObservableContext context)
        {
            this.context = context;
        }

        [When(@"I provide the values ""(.+)""")]
        public void WhenIProvideTheValues(string values)
        {
            values.ConvertTo(Int32.Parse)
                  .Select(val => new ChannelMessage { Value2 = val})
                  .ToObservable()
                  .Subscribe(context.Source);
        }
    }
}