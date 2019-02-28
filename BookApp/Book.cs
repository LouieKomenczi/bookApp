using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp
{
    class Book
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }

        public override string ToString()
        {
            return $"{Title}:{Author} - {Genres}";
        }
    }
}
