using System;
using System.IO;
using System.Linq;

class Program
{
    static void Burbuja(double[,] distancias)
    {
        int n = distancias.GetLength(0);
        //Console.WriteLine("bubble N:" + n);

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (distancias[j, 0] > distancias[j + 1, 0])
                {
                    // Intercambiar las distancias
                    double tempDistancia = distancias[j, 0];
                    double tempClase = distancias[j, 1];

                    distancias[j, 0] = distancias[j + 1, 0];
                    distancias[j, 1] = distancias[j + 1, 1];

                    distancias[j + 1, 0] = tempDistancia;
                    distancias[j + 1, 1] = tempClase;
                }
            }
        }
       /* for (int i = 0; i < n; i++)
        {
           Console.WriteLine(i+ " | "+ distancias[i, 0] + " | " + distancias[i, 1]);

        }*/
       
           
    }

    static void DeterminarDistancias(string[] datatrain,string[] datosEnFilaTest, double[,] distancias ){
                     int ix = 0;
            foreach (string data in datatrain) //guarda una ilera de la DB deacuerdo al indice del bucle pero ahora para el datatest
             {
                //Console.WriteLine(ix +"|"+data+ "|"+ datosTest);
                string[] datosEnFilaTrain = data.Split(','); //divide los datos por comas
                int clase = 0;
                double distancia=0;
                double datosDataTrain=0;
                double datosDataTest=0;
                for (int columna = 0; columna < datosEnFilaTrain.Length-1; columna++)//por cada dato en la fila menos la clase
                {
                    //string valorEnColumna = datosEnFila[columna];

                    // guardar los valores de cada columna ( cuando las columnas coincidan) en datosDataTEst y DatosDataSet Respectivamente
                    
                    try
                    {
                        datosDataTest= Convert.ToDouble(datosEnFilaTest[columna]);
                        datosDataTrain = Convert.ToDouble(datosEnFilaTrain[columna]);
                       // Console.WriteLine(datosDataTest+"|"+ datosDataTrain);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error");
                    }
                    
                    
                    
                    distancia+=Math.Pow(datosDataTrain-datosDataTest, 2);

            
                }
                //Console.WriteLine("distancia " + ix+ ": "+ distancia+ "|" + datosEnFila[datosEnFila.Length-1]);

                //distancia= Math.Sqrt(distancia);
                //distancias[i, 0]= distancia; 
                    try
                    {
                         clase = Convert.ToInt32(datosEnFilaTrain[datosEnFilaTrain.Length-1]);
                        // Console.WriteLine("clase: " + clase);

                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error");
                    }
                distancias[ix, 0]= distancia; 
                distancias[ix, 1] = clase;

                ix ++;
             }
    }




    static void Main()
    {
        int acierto=0;
        int error=0;
        int total = 0;
        double puntuacion = 0;
        const string archivoBaseDeDatos = "iris.txt";//leer archivo
        string[] filas = File.ReadAllLines(archivoBaseDeDatos);//guarda cada ilera del txt en un array
        double proporcion;
        proporcion= 0.7;
        
        // Aleatoriamente reorganizar las filas
        Random rand = new Random();
        filas = filas.OrderBy(x => rand.Next()).ToArray();
        //foreach (var fila in filas)
        //{
        //    Console.WriteLine(fila);
        //}
        // Dividir los datos en una proporción del 80/20
        int totalFilas = filas.Length;
        int entrenamientoSize = (int) (totalFilas * proporcion);

        string[] datatrain = filas.Take(entrenamientoSize).ToArray();
        string[] datatest = filas.Skip(entrenamientoSize).ToArray();
        double[,] distancias= new double[datatrain.Length,2];

        int clase = 0;
        int K = 3;
        // Para acceder al valor de un rasgo en una fila del dataset:
        string ejemploFilaDataset = datatrain[0];
        int numCentroides = 3;
       // string[] datosEnFila = ejemploFilaDataset.Split(',');
        //string clase = datosEnFila[datosEnFila.Length-1];
        
        //bucle que evalua los datos del datatest punto a punto
        for (int datatest_points = 0; datatest_points < datatest.Length; datatest_points++)
        {
            //guarda una ilera de la DB deacuerdo al indice del bucle
            string datosTest = datatest[datatest_points];
            string[] datosEnFilaTest = datosTest.Split(',');//divide la ilera guardada por las comas
             
            DeterminarDistancias(datatrain,datosEnFilaTest,distancias);
                //metodo de ordenamiento
            Burbuja(distancias); //bucle por cada valor en los datos de entrenamiento

               // Console.ReadLine();

             if (datatest_points< 5)
                {
                    for (int i = 0; i < datatrain.Length; i++)
                     {
                
                       // Console.WriteLine("distancias prebubble:"+ ix+ ":" + distancias[i,0] + " " + distancias[i,1]);
                     }
                }
           /* */
          /*  Console.WriteLine(distancias[0,0]+ " | "+ distancias[0,1]);*/
        
        
        //Obtener K neighbors
        int Setosa=0, Versicolor=0, Virginical=0;
        for (int counter = 0; counter < K; counter++)
        {
            if(distancias[counter,1]==1){
                Setosa++;
            }else if (distancias[counter,1]==2){
                Versicolor++;
            }else if (distancias[counter,1]==3){
                Virginical++;
            }
        }
        


            Console.WriteLine(Setosa+ "|" + Versicolor+ "|" + Virginical );
            
        if (Setosa>Versicolor&&  Setosa>Virginical)
        {
            Console.WriteLine("Su planta para el punto "+ datatest_points+ " ["+ datosEnFilaTest[0]+","+datosEnFilaTest[1]+","+datosEnFilaTest[2]+ ","+datosEnFilaTest[3]+"] pertenece a la especie Iris Setosa");
            clase = 1;

        }else if (Versicolor>Setosa&&  Versicolor>Virginical)
        {
            Console.WriteLine("Su planta para el punto "+ datatest_points+ " ["+ datosEnFilaTest[0]+","+datosEnFilaTest[1]+","+datosEnFilaTest[2]+ ","+datosEnFilaTest[3]+"] pertenece a la especie Iris Versicolor");
            clase = 2;
        }else if (Virginical>Versicolor&&  Virginical>Setosa)
        {
            Console.WriteLine("Su planta para el punto "+ datatest_points+ " ["+ datosEnFilaTest[0]+","+datosEnFilaTest[1]+","+datosEnFilaTest[2]+ ","+datosEnFilaTest[3]+"] pertenece a la especie Iris Virginical");
            clase = 3;
        }else{
            Console.WriteLine(Setosa+ "|" + Versicolor+ "|" + Virginical);
            clase = -1;
        }
    //verificar clase
    int clase_real = Convert.ToInt32(datosEnFilaTest[4]);
    Console.WriteLine(clase_real);
    
        if (clase == clase_real){
            acierto++;
        }else{
            error++;
        }
        total = acierto+error;
        puntuacion = (double) acierto/total* 100;

       Console.WriteLine(acierto+"+"+ error+":"+total+" -> % accuracy: "+ puntuacion+ "%");
                Console.WriteLine("________________________________________________");

                //Console.ReadLine();

        }

        Console.WriteLine(datatrain.Length+ " "+ datatest.Length);
        for (int i = 0; i < distancias.Length/2; i++)
        {
            distancias[i,1]=-1;
            distancias[i,0] =-1;
        }
    }

   

}
