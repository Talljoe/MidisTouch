// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.UnitTests.ControllerChangeExtensions.Bindings
{
    using Midis.UnitTests.Context;
    using TechTalk.SpecFlow;

    [Binding]
    public class Given
    {
        private readonly ChannelMessageObservableContext context;

        public Given(ChannelMessageObservableContext context)
        {
            this.context = context;
        }

        [Given(@"I have scaled the values to between (\d+) and (\d+)")]
        public void GivenIHaveScaledTheValuesToBetween(int low, int high)
        {
            context.Values = context.Values.ToRange(low, high);
        }

        [Given(@"I have clamped the values between (\d+) and (\d+)")]
        public void GivenIHaveClampedTheValuesBetween(int low, int high)
        {
            context.Values = context.Values.Clamp(low, high);
        }
    }
}