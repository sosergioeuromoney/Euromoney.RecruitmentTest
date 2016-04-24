using ContentConsole.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentConsole.Application.Data
{
    public interface INegativeWordsRepository
    {
        IList<NegativeWord> GetAll();

        NegativeWord Add(NegativeWord word);

        void Remove(NegativeWord word);

        bool Exists(NegativeWord word);
    }
}
