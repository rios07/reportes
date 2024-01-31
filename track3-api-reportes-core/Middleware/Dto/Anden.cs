using System.Data;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class Anden
    {

        public string status { get; set; }
        public string errorMessage { get; set; }
        public int Id { get; set; }
        public int IdHojaRuta { get; set; }
        public string fecha { get; set; }
        public string Stamp { get; set; }
        public string id_anden { get; set; }
        public string hora_desde { get; set; }
        public string hora_hasta { get; set; }
        public string locatario { get; set; }
        public string descripcion { get; set; }
        public string lado { get; set; }
        public int enabled { get; set; }

        public Anden()
        {

        }


       // [JsonConstructor]
        public Anden(string status, string errorMessage, Anden Data)
        {
            this.status = status;
            this.errorMessage = errorMessage;

            if (Data != null)
            {
                this.id_anden = Data.id_anden;
                this.Stamp = Data.fecha;
                this.hora_desde = Data.hora_desde;
                this.hora_hasta = Data.hora_hasta;
                this.enabled = Data.enabled;
            }
        }
        public Anden(DataRow row)
        {
            if (row != null)
            {
                if (row.Table.Columns.Contains("ID_ANDEN"))
                    this.Id = Convert.ToInt32(row["ID_ANDEN"].ToString());

                if (row.Table.Columns.Contains("ID_HOJARUTA"))
                    this.IdHojaRuta = Convert.ToInt32(row["ID_HOJARUTA"].ToString());
                if (row.Table.Columns.Contains("STAMP"))
                    this.Stamp = row.Field<DateTime>("STAMP").ToString("yyyy-MM-dd");

                if (row.Table.Columns.Contains("ANDEN"))
                    this.id_anden = row.Field<string>("ANDEN");

                if (row.Table.Columns.Contains("ANDEN"))
                    this.id_anden = row.Field<string>("ANDEN");

                if (row.Table.Columns.Contains("HORA_DESDE"))
                    this.hora_desde = row.Field<string>("HORA_DESDE");

                if (row.Table.Columns.Contains("HORA_HASTA"))
                    this.hora_hasta = row.Field<string>("HORA_HASTA");
            }
        }
        public class AndenResult
        {
            public string status { get; set; }
            public object Data { get; set; }
        }
        public class Anden2
        {
            public string id_anden { get; set; }
            public string descripcion { get; set; }
            public string hora_desde { get; set; }
            public string hora_hasta { get; set; }

        }

        public class AndenBody
        {
            public string id_anden { get; set; }
            public string fecha { get; set; }
        }


    }
}
