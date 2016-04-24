using ContentConsole.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentConsole.Application.Services
{
    public interface INegativeWordsService
    {
        void Add(string input);

        void Remove(string input);

        IEnumerable<string> GetAll();

        IEnumerable<NegativeWordScan> ScanText(string input);

        string ObscureText(string input);
    }
}
