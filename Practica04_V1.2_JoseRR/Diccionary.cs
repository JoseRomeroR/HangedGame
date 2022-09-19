using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Practica04_V1._2_JoseRR
{
   public struct Dictionary
    {
        private List<string> Words;
        public string words
        {
            get 
            { 
                if(Words==null) throw new Exception("Para jugar hace falta mínimo 1 palabra!");
                return Words.ElementAt(Random());
            } // Devuelve  la palabra con la que se jugará aleatoriamente
            set
            {                       
                if (Words==null) Words = new List<string>(); // Inicializamos la lista para almacenar palabras  
                if(String.IsNullOrEmpty(value)) throw new Exception("Introduce algo loco!"); // si esta vacio peta
                for (int i = 0; i < value.Length; i++) if (value[i] < 65 || value[i] > 90) throw new Exception("No has introducido caracteres válidos"); // Excepcion si no mete caracteres validos
                if (Words.Contains(value)) throw new Exception("Palabra repetida");         //si es repetida la palabra peta
                if(value.Length==1) throw new Exception("No existen palabras de 1 caracter"); // no hay palabras de 1 letra , peta
                Words.Add(value);
            }
        }
        private int Random()
        {
            Random random= new Random();
            return random.Next(0,Words.Count());
        } 
        
        // Devuelve un numero aleatorio entre 0 y las posiciones que tenga la lista
    }
}
