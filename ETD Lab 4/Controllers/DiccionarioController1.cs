using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using ETD_Lab_4.Models;

namespace ETD_Lab_4.Controllers
{
    public class DiccionarioController1 : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        public static Dictionary<string, Dictionary<string, Dictionary<string, bool>>> Stickers = new Dictionary<string, Dictionary<string, Dictionary<string, bool>>>();
        public static Dictionary<string, Dictionary<string, Dictionary<string, bool>>> Tipo = new Dictionary<string, Dictionary<string, Dictionary<string, bool>>>();
        public static Dictionary<string, Dictionary<string, bool>> EspTipo = new Dictionary<string, Dictionary<string, bool>>();
        public static Dictionary<string, bool> Equipo = new Dictionary<string, bool>();
        public static Dictionary<string, Dictionary<string, bool>> Equipos_Normales = new Dictionary<string, Dictionary<string, bool>>();
        public static Dictionary<string, bool> Jugadores = new Dictionary<string, bool>();


        //cambiar los datos de medicamento a sticker
        public void leerArchivo()//metodo para leer archivo
        {
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/MEDICAMENTOS.csv";   // ubicación de tu archivo
                                                                                                                // guarda en el string "Path" TODO el archivo
            Sticker aux; //auxiliar de tu clase medicamento
            using (StreamReader sr = System.IO.File.OpenText(Path)) //toma el string "Path" para leer línea por línea
            {
                string s;
                while ((s = sr.ReadLine()) != null) // guarda en el string "s" cada línea
                {
                    if (s != "") //si la línea no está vacía
                    {
                        aux = new Sticker(); // nuevo medicamento
                        aux.Tipo = s.Split('|')[0]; // va guardando los datos haciendo un split por el caracter que separa cada elemento. En este caso es "|" en la posición [0]
                        aux.Equipo = s.Split('|')[1];
                        aux.Nombre = s.Split('|')[2];
                        aux.Obtenida = Convert.ToBoolean(s.Split('|')[3]);
                        if (aux.Tipo == "Especial")
                        {
                            aux.Tipo_Especial = s.Split('|')[4];
                        }

                        if (aux.Tipo == "Especial")
                        {
                            Equipo.Add(aux.Equipo, aux.Obtenida);
                            EspTipo.Add(aux.Tipo, Equipo);
                            Tipo.Add(aux.Tipo, EspTipo);

                        }
                        else if (aux.Tipo == "Normales")
                        {
                            Jugadores.Add(aux.Nombre, aux.Obtenida);
                            Equipos_Normales.Add(aux.Equipo, Jugadores);
                            Tipo.Add(aux.Tipo, Equipos_Normales);
                        }
                    }
                }


            }
        }
    }
}