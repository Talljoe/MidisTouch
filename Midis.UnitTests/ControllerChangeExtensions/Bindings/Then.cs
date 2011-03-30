// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.UnitTests.ControllerChangeExtensions.Bindings
{
    using System;
    using Midis.UnitTests.Context;
    using TechTalk.SpecFlow;
    using System.Linq;
    using FluentAssertions;
    using Midis.UnitTests.Utility;

    [Binding]
    public class Then
    {
        private readonly ChannelMessageObservableContext context;

        public Then(ChannelMessageObservableContext context)
        {
            this.context = context;
        }

        [Then(@"the result should be ""(.+)""")]
        public void ThenTheResultShouldBe(string values)
        {
            context.Values.ToEnumerable()
                   .Select(cm => cm.Value2)
                   .Should().Equal(values.ConvertTo(Int32.Parse));
        }

        [Then(@"the result should be (\d+)")]
        public void ThenTheResultShouldBe(int value)
        {
            var expected = new [] {value};
            context.Values.ToEnumerable()
                   .Select(cm => cm.Value2)
                   .Should().Equal(expected);
        }
    }
}