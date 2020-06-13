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
        public IEnumerable<FotoXTelaio> FotoXTelaio { get; set; }
        public IEnumerable<TipiDocumento> TipiDocumento { get; set; }
        public IEnumerable<FotoXTelaio_vw> FotoXTelaio_vw { get; set; }
        public IEnumerable<Status> Status { get; set; }
        public IEnumerable<StoricoStatus> StoricoStatus { get; set; }
        public IEnumerable<StoricoStatus_vw> StoricoStatus_vw { get; set; }
        public IEnumerable<Telai_LastStatus_vw> Telai_LastStatus_vw { get; set; }
    }

}