using System;
using System.IO;
using System.Media;
using WMPLib;
using System.Threading;

namespace Practica04_V1._2_JoseRR
{
    //CONSTANTES
    //VARIABLES
    //ENTRADAS
    //PROCESOS
    //SALIDAS
    internal class Program
    {
        static void Main(string[] args)
        {
            //CONSTANTES
            //VARIABLES                    
            //ENTRADAS
            Menu(); // menu del juego 
            //PROCESOS
            //SALIDAS
        }
        public static Dictionary StoreWords(Dictionary dictionary)
        {
            //CONSTANTES
            //VARIABLES
            bool exit = false; // bool para salir 
            string aux; // aux para el readline
            //ENTRADAS
            do
            {
                try
                {
                    WelcomeHanged();
                    Console.WriteLine($"Introduce una palabra.( \"E\" para salir)");
                    aux = Console.ReadLine();
                    aux=aux.ToUpper();
                    if (aux == "E") exit = true;
                    else dictionary.words = aux;
                }
                catch (Exception e)
                {
                    ErrorCatch(e.Message);                
                }
                Console.Clear();
            } while (!exit);
            //PROCESOS
            //SALIDAS
            return dictionary;
        }
        public static void PrintWord(char[] copy)// imprimo la palabra con espacios o lo q sea  
        {
            Console.WriteLine("\t\t\tAdivina la palabra!");
            Console.Write("\t\t\t");
            for (int i = 0; i < copy.Length; i++) Console.Write($"{copy[i]} ");  
        }
        public static void PrintLettersUser(Hanged hanged)// print las letras introducidas
        {
            Console.WriteLine("Letras introducidas");
            for (int i = 0; i < hanged.letters.Length; i++) Console.Write(hanged.letters[i] + " ");
            Console.WriteLine();
        }
        public static string GetWordUser() // metodo pide palabra o letra   
        {
            string msg = "";
            Console.WriteLine("\t\tResuelve o introduce una letra!");
            msg = Console.ReadLine();
            msg = msg.ToUpper();
            return msg;
        }
        public static void PlayGame(Dictionary dictionary)
        {
            //CONSTANTES
            //VARIABLES
            string directory= Directory.GetCurrentDirectory();//   directorio actual del net 5.0   restamos -17 para llegar a la ruta y concatenerla con la musica
            directory = directory.Substring(0, directory.Length-17); // sacamos solamante la ruta hasta Practica04
            directory = directory + "/music/chickenFried.mp3"; // añadimos la ruta mas donde se encuentra la musica           
            WindowsMediaPlayer music = new WindowsMediaPlayer(); // instancio musica de juego
            music.URL = directory; // añado el directorio donde se encuentra la musica 
            music.controls.play(); // musica on
            Hanged hanged = new Hanged(); //instancio ahorcado
            hanged.wordChoosen = dictionary.words; // Añado palabra para jugar del diccionario.         
            bool exit = false; // bool para salir del bucle                 
            //ENTRADAS
            for (int i = 0; i < hanged.copy.Length; i++) hanged.copy[i] = '_'; // Relleno de espacios la palabra para jugar         
            do
            {
                try
                {
                    WelcomeHanged(); // cabezera
                    PrintWord(hanged.copy);// imprimo la palabra con espacios o letras
                    PrintError(hanged); // print los errores ahorcado
                    PrintLettersUser(hanged);// print las letras introducidas
                    hanged.userDecision=GetWordUser();  // metodo pide palabra o letra                
                    if (hanged.userDecision.Length == 1) // si el tamaño es de 1 ha introducido letra 
                    {
                        if (hanged.letters.Contains(hanged.userDecision)) throw new Exception("Palabra repetida!");
                        if (!hanged.wordChoosen.Contains(hanged.userDecision[0]))  // si no contiene la letra 
                        {
                            hanged.letters += hanged.userDecision; // almacenar la letras introducidas
                            hanged.ErrorIncrease(); // si no contiene la letra sumamos error
                            Console.Beep(); // sonido error si falla
                            if (hanged.error == 6)
                            {
                                exit = true; // si se acaban los errores acaba game
                                hanged.win = false; // win a false porque hemos perdido
                            }                         
                        } 
                        else
                        {                         
                            for (int i = 0; i < hanged.wordChoosen.Length; i++) // poner la letra correspondiente en su sitio 
                            {
                                if (hanged.userDecision[0] == hanged.wordChoosen[i]) hanged.copy[i] = hanged.userDecision[0];// si la letra del usuario es igual a la letra en la posicion de la palabra la asignamos en la misma posicion en una variable copia que contiene las barrabaja 
                            }                                            
                        }                   
                    }
                    else // si no, es palabra a adivinar 
                    {
                        if (String.IsNullOrWhiteSpace(hanged.userDecision)) throw new Exception("Introduce algo loco!");
                        if (hanged.userDecision == hanged.wordChoosen)
                        {
                            exit = true; // si es igual fin del game salimos
                            hanged.win = true;// win a true porque hemos ganado
                        }
                        else
                        {
                            hanged.ErrorIncrease();
                            Console.Beep(); // sonido error si falla
                            if (hanged.error == 6)
                            {
                                hanged.win = false;  // win a false porque hemos perdido   
                                exit = true;
                            }              
                        }
                    }
                }
                catch (Exception e)
                {
                    ErrorCatch(e.Message);
                }
                Console.Clear();               
            } while (!exit);
            //PROCESOS
            //SALIDAS
            if (hanged.win) PrintWin();
            else PrintLose(hanged.wordChoosen);
            music.close();
        } // comienza el juego
        public static void PrintWin()
        {
          
            //SALIDAS
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("**************************************************************");
            Console.WriteLine("                        GANASTE!                          ");
            Console.WriteLine("**************************************************************");
            GoodBye();
            Console.WriteLine("PRESS BATMAN para continuar.");
            Console.ReadLine();
            Console.ResetColor();

        } // si ganas print
        public static void GoodBye()
        {
            string msg = "Gracias por jugar a este juego.\nProgramado por José Romero Roldan.\nIES Luis Carrillo De SotoMayor.";
            string msg2 = @"          .  .
          |\_ |\
          | a_a\
          | | l]
      ____ | '-\___
     /.----.___.- '\

  //        _    \
  //   .-. (~v~) /|
  | '|  /\:  .--  / \

// |-/  \_/____/\/~|
|/  \ |  []_ | _ | _] \ |
    | \  | \ | ___   _\ ]_}
| |  '-' /   '.'  |
| |     /    /|:  | 
| |     |   / |:  /\
| |     /  /  |  /  \
| |    |  /  /  |    \
\ |    |/\/  |/|/\    \
 \|\ |\|  |  | / /\/\__\
  \ \| | /   | |__
       / |   |____)
       |_/";
            for (int i = 0; i < msg.Length; i++)
            {
                Console.Write(msg[i]);
                Thread.Sleep(50);
            }
            Console.WriteLine();
            for (int i = 0; i < msg2.Length; i++)
            {
                Console.Write(msg2[i]);
                Thread.Sleep(10);
            }
            Console.WriteLine();

        }
        public static void PrintLose(String wordChoosen)
        {
            //SALIDAS
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("**************************************************************");
            Console.WriteLine("                        PERDISTE!                          ");
            Console.WriteLine($"               La palabra era: {wordChoosen}");
            Console.WriteLine("**************************************************************");
            GoodBye();
            Console.WriteLine("PRESS BATMAN para continuar.");
            Console.ReadLine();
            Console.ResetColor();
          
          
        } // si pierdes print
        public static void PrintBye()
        {
            //SALIDAS
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**************************************************************");
            Console.WriteLine("                Thanks for play the game!                     ");
            Console.WriteLine("**************************************************************");
            GoodBye();
            Console.ResetColor();
            Console.WriteLine("PRESS BATMAN para continuar.");
            Console.ReadLine();
          
        } // print despedida
        public static void Menu() // comienza game 
        {
            //CONSTANTES
            //VARIABLES
            Dictionary dictionary = new Dictionary();  //instancio diccionario
            byte choose;
            bool exit=false;
            //ENTRADAS           
            do
            {
                try
                {
                    WelcomeHanged();  
                    Console.WriteLine("\t\t\t1.Añadir palabras");
                    Console.WriteLine("\t\t\t2.Empezar partida");
                    Console.WriteLine("\t\t\t3.Salir");
                    choose = Convert.ToByte(Console.ReadLine());
                    Console.Clear(); //Free style para mas bonito 
                    if (choose < 1 || choose > 3) throw new Exception("Valor introducido no válido");
                    switch (choose)
                    {
                        case 1:
                            dictionary = StoreWords(dictionary); //Almacenador de palabras 
                            break;
                        case 2:
                            PlayGame(dictionary);                     
                            break;
                        case 3:
                            exit = true;
                            PrintBye();                           
                            break;
                    }
                }
                catch (Exception e)
                {
                    ErrorCatch(e.Message);                  
                }
               
                Console.Clear();
            } while (!exit);          
            //PROCESOS
            //SALIDAS
        }
        public static void ErrorCatch(string e)
        {
            //SALIDAS
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR,{e}");
            Console.WriteLine("PRESS ENTER para continuar.");
            Console.ReadLine();
            Console.Clear();
            Console.ResetColor();
        } // error del try catch
        public static void WelcomeHanged()
        {
            //SALIDAS
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**************************************************************");
            Console.WriteLine("                        Hanged Game                           ");
            Console.WriteLine("**************************************************************");
            Console.ResetColor();
        } //cabezera 
        public static void PrintError(Hanged hanged) // print ahorcado segun errores
        {
            switch (hanged.error)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine($"_______");
                    Console.WriteLine($"|     |");
                    Console.WriteLine($"|");
                    Console.WriteLine($"|");
                    Console.WriteLine($"|");
                    Console.WriteLine($"|");
                    Console.WriteLine($"|");
                    Console.WriteLine("Te quedan 5 intentos");
                    Console.WriteLine();
                    Console.ResetColor();
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine($"_______");
                    Console.WriteLine($"|     |");
                    Console.WriteLine($"|     O");
                    Console.WriteLine($"|");
                    Console.WriteLine($"|");
                    Console.WriteLine($"|");
                    Console.WriteLine($"|");
                    Console.WriteLine("Te quedan 4 intentos");
                    Console.WriteLine();
                    Console.ResetColor();
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine($"_______");
                    Console.WriteLine($"|     |");
                    Console.WriteLine($"|     O");
                    Console.WriteLine($"|     | ");
                    Console.WriteLine($"|");
                    Console.WriteLine($"|");
                    Console.WriteLine($"|");
                    Console.WriteLine("Te quedan 3 intentos");
                    Console.WriteLine();
                    Console.ResetColor();

                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine($"_______");
                    Console.WriteLine($"|     |");
                    Console.WriteLine($"|     O");
                    Console.WriteLine($"|    (|) ");
                    Console.WriteLine($"|");
                    Console.WriteLine($"|");
                    Console.WriteLine($"|");
                    Console.WriteLine("Te quedan 2 intentos");
                    Console.WriteLine();
                    Console.ResetColor();
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine($"_______");
                    Console.WriteLine($"|     |");
                    Console.WriteLine($"|     O");
                    Console.WriteLine($"|    (|) ");
                    Console.WriteLine($"|     ┴ ");
                    Console.WriteLine($"|");
                    Console.WriteLine($"|");
                    Console.WriteLine("Te quedan 1 intentos");
                    Console.WriteLine();
                    Console.ResetColor();
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine($"_______");
                    Console.WriteLine($"|");
                    Console.WriteLine($"|");
                    Console.WriteLine($"|");
                    Console.WriteLine($"|");
                    Console.WriteLine($"|");
                    Console.WriteLine($"|");
                    Console.WriteLine();
                    break;
            }
        } // imprimir juego 
    }
}
