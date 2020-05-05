using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grandine.Models
{
    public class HomeModel
    {
        public IEnumerable<Utenti> Utenti { get; set; }
        public IEnumerable<Clienti> Clienti { get; set; }
        public IEnumerable<Commesse> Commesse { get; set; }
        public IEnumerable<Tecnici> Tecnici { get; set; }
    }

}