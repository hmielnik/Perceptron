﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron
{
    class BinaryOperations
    {
        public string name;
        public int index;
        public override string ToString() { return this.name; }
    }

    public class BinaryOperationsLearningSet
    {
        public int[,] input;
        public int[] outputs;
    }

}
