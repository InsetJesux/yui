using Newtonsoft.Json.Linq;
using System;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace InteractionFramework.Utils
{
    public class Utils
    {

        /// <summary>
        /// Devuelve el valor correspondiente del json
        /// </summary>
        /// <param name="json">Json del que extraer el valor</param>
        /// <param name="dataName">Nombre del valor a extraer</param>
        /// <exception cref="JsonReaderException">Si el parametro json no corresponde a un json valido tremenda throweada</exception>
        /// <returns>El valor de json pedido</returns>
        public static string GetSingleDataFromJson(string json, string dataName)
        {
            string str;
            JObject jo = JObject.Parse(json);
            if (jo != null)
            {
                str = jo[dataName] != null ? (string)jo[dataName] : null;
            }
            else
            {
                //log.error(formato de json incorrecto, e);
                str = null;
            }

            //return (string)jo?[dataName]??null;
            return str;
        }
        
    }
}

