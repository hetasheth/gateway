using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment8.Test.SequentialTestingHelpers
{
    class TestSequenceAttribute : Attribute
    {
        public int Sequence { get; set; }

        public TestSequenceAttribute(int sequence)
        {
            Sequence = sequence;
        }
    }
}
