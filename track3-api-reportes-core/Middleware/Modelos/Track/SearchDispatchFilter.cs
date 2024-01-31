using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace track3_api_reportes_core.Middleware.Modelos.Track
{
    public class SearchDispatchFilter
    {
        public SearchDispatchFilter() { }
        public string UserName { get; set; }
        public long? DispatchId { get; set; }
        public long? TransportCarrierId { get; set; }
        public long? SourceSystemId { get; set; }
        public string WayBillId { get; set; }
        public long? SourceId { get; set; }
        public long? Destination { get; set; }
        public string Movil { get; set; }
        public bool? Open { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int Page { get; set; }
        public string Driver { get; set; }
        public int Order { get; set; }
        public int pagina { get; set; }
        public int registros_por_pagina { get; set; }



        private string _Text;

        public string Text
        {
            get { return string.IsNullOrEmpty(_Text) ? string.Empty : _Text.ToUpper(); }
            set { _Text = value; }
        }

    }
}
