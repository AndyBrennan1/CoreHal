using System;
using System.Collections.Generic;
using System.Text;

namespace CoreHal.Tests.Fixtures
{
    public class ComplexModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ExampleModel ComplexField { get; set; }

        public List<int> Numbers { get; set; }

        public DateTime Date { get; set; }

        public List<ExampleModel> ListOfComplexFields { get; set; }
    }
}
