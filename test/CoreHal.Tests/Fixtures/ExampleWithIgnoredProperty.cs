using CoreHal.Annotation;

namespace CoreHal.Tests.Fixtures
{
    public class ExampleWithIgnoredProperty
    {
        public int Integer1 { get; set; }

        [NoOutput]
        public int Integer2 { get; set; }

        public int Integer3 { get; set; }
    }
}