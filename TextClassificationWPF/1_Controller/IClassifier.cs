using System;
using System.Collections.Generic;
using System.Text;
using TextClassificationWPF.Domain;

namespace TextClassificationWPF.Controller
{
    public interface IClassifier
    {
        public string Classify(List<bool> vector);
    }
}
