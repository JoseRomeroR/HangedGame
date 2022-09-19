using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica04_V1._2_JoseRR
{
    public struct Hanged
    {
        private byte Error; //contador de errores
        private string Letters; //letras del usuario
        private string WordChoosen; // palabra elegida para jugar
        private string UserDecision; // lo que introduce el usuario
        private bool Win;
        private char[] Copy;

        public char[] copy
        {
            get
            {
                if (Copy == null) Copy =new char[WordChoosen.Length]; // si esta vacio lo inicializas en nada
                return Copy;
            }
            set
            {
                if (Copy == null) Copy = new char[WordChoosen.Length];
                Copy = value;      
            }
        }
        public byte error
        {
            get
            {
                
                return Error;
            }
        }
        public string letters
        {         
            get 
            {
                if (Letters==null) Letters = ""; // si esta vacio lo inicializas en nada
                return Letters;
            }
            set
            {
                for (int i = 0; i < value.Length; i++) if (value[i] < 65 || value[i] > 90) throw new Exception("No has introducido caracteres válidos"); // Excepcion si no mete caracteres validos
                Letters = value;            
            }
        }
        // 
        public string wordChoosen
        {
            get
            {
                if (WordChoosen == null) throw new Exception("No hay palabra!");
                return WordChoosen;
            }
            set 
            {
                for (int i = 0; i < value.Length; i++) if (value[i] < 65 || value[i] > 90) throw new Exception("Caracteres inválidos"); // Excepcion si no mete caracteres validos
                WordChoosen = value; 
            }
        } 
        public bool win
        {
            get { return Win; }
            set { Win = value; }
        }
        public string userDecision
        {
            get { return UserDecision; }
            set { UserDecision = value; }
        }
        public void ErrorIncrease() // incrementador de error
        {
            Error++;
        }
        

         
    }
}
