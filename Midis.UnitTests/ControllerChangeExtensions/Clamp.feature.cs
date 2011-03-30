// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.5.0.0
//      Runtime Version:4.0.30319.225
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
namespace Midis.UnitTests.ControllerChangeExtensions
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.5.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class ClampFeature : Xunit.IUseFixture<ClampFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Clamp.feature"
#line hidden
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Clamp", "In order to prevent out-of-range values\nAs a composer\nI want to be clamp ontrolle" +
                    "r change values to a specific range", GenerationTargetLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public virtual void SetFixture(ClampFeature.FixtureData fixtureData)
        {
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "Clamp")]
        [Xunit.TraitAttribute("Description", "Values should be clamped")]
        public virtual void ValuesShouldBeClamped()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Values should be clamped", new string[] {
                        "extensions"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
  testRunner.Given("I have clamped the values between 5 and 103");
#line 9
  testRunner.When("I provide the values \"5, 63, 64, 65, 90, 126\"");
#line 10
  testRunner.Then("the result should be \"5, 63, 64, 65, 90, 103\"");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [Xunit.FactAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "Clamp")]
        [Xunit.TraitAttribute("Description", "Values should not repeat")]
        public virtual void ValuesShouldNotRepeat()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Values should not repeat", new string[] {
                        "extensions"});
#line 13
this.ScenarioSetup(scenarioInfo);
#line 14
  testRunner.Given("I have clamped the values between 10 and 20");
#line 15
  testRunner.When("I provide the values \"5, 63, 3, 19, 90\"");
#line 16
  testRunner.Then("the result should be \"10, 20, 10, 19, 20\"");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [Xunit.FactAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "Clamp")]
        [Xunit.TraitAttribute("Description", "Values should be clamped normally if floor and ceiling are reversed")]
        public virtual void ValuesShouldBeClampedNormallyIfFloorAndCeilingAreReversed()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Values should be clamped normally if floor and ceiling are reversed", new string[] {
                        "extensions"});
#line 19
this.ScenarioSetup(scenarioInfo);
#line 20
  testRunner.Given("I have clamped the values between 20 and 10");
#line 21
  testRunner.When("I provide the values \"5, 63, 3, 19, 90\"");
#line 22
  testRunner.Then("the result should be \"10, 20, 10, 19, 20\"");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.5.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                ClampFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                ClampFeature.FeatureTearDown();
            }
        }
    }
}
#endregion
