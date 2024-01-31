namespace track3_api_reportes_core.Aplicacion.Logs
{
    public class Logs
    {

        public string Proceso { get; set; }
        public List<string> lineas { get; set; }

        private readonly IWebHostEnvironment _environment;

        public Logs(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public void GrabarLogs()
        {
            string path = "";
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            path = baseDir + "Logs\\" + string.Format("{0:yyyyMMdd}", DateTime.Now) + "\\";

            try
            {

                if (_environment.IsDevelopment())
                {
                    path = Directory.GetCurrentDirectory() + "\\Logs\\" + string.Format("{0:yyyyMMdd}", DateTime.Now) + "\\";
                }
                else
                {
                    path = Directory.GetCurrentDirectory() + "//Logs//" + string.Format("{0:yyyyMMdd}", DateTime.Now) + "//";
                }


                lineas = AgregarFechayHora(lineas);
                //verifico si existe el directorio 
                if (Directory.Exists(path))
                {
                    //creo o escribo en el fichero txt 
                    CreateOrWriteFile(path);
                    lineas = new List<string>();
                }
                else
                {
                    //creo el directorio y creo o escribo en el fichero txt
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    CreateOrWriteFile(path);
                    lineas = new List<string>();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"error - {e.Message}");
            }
        }


        public List<string> AgregarFechayHora(List<string> lineas)
        {
            List<string> nuevasLineas = new List<string>();
            foreach (string item in lineas.ToList())
            {
                string value = string.Join(" ", string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now), item);
                nuevasLineas.Add(value);
            }

            return nuevasLineas;
        }

        public void CreateOrWriteFile(string path)
        {
            if (File.Exists(path + Proceso + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".txt"))
            {
                File.AppendAllLines(path + Proceso + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".txt", lineas);
            }
            else
            {

                File.WriteAllLines(path + Proceso + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".txt", lineas);
            }
        }

        public bool depurarLogs(int CantidadDías, string basedir)
        {
            bool result = false;
            //Limpio los logs a partir de una cantidad de  días parametrizados hacias atras
            for (int i = CantidadDías; i <= 10; i++)
            {
                string path = basedir + "Logs\\" + string.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(-i));

                if (Directory.Exists(path))
                {
                    result = true;

                    foreach (string item in Directory.GetFiles(path))
                    {
                        File.Delete(item);
                    }
                    Directory.Delete(path);

                }
            }

            return result;
        }
    }
}
