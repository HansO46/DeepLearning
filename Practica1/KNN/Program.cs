using System;
using System.IO;
using System.Linq;

class Program
{
    static void Burbuja(double[,] distancias)
    {
        int n = distancias.GetLength(0);

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (distancias[j, 0] > distancias[j + 1, 0])
                {
                    // Intercambiar las distancias y las clases
                    double tempDistancia = distancias[j, 0];
                    int tempClase = (int)distancias[j, 1];

                    distancias[j, 0] = distancias[j + 1, 0];
                    distancias[j, 1] = distancias[j + 1, 1];

                    distancias[j + 1, 0] = tempDistancia;
                    distancias[j + 1, 1] = tempClase;
                }
            }
        }
       
    }


    static void Main()
    {
        const string archivoBaseDeDatos = "iris.txt";//leer archivo
        string[] filas = File.ReadAllLines(archivoBaseDeDatos);//guarda cada ilera del txt en un array
        double proporcion;
        proporcion= 0.8;
        foreach (var fila in filas)
        {
            Console.WriteLine(fila);
        }
        // Aleatoriamente reorganizar las filas
        Random rand = new Random();
        filas = filas.OrderBy(x => rand.Next()).ToArray();

        // Dividir los datos en una proporción del 80/20
        int totalFilas = filas.Length;
        int entrenamientoSize = (int) (totalFilas * proporcion);

        string[] dataset = filas.Take(entrenamientoSize).ToArray();
        string[] datatest = filas.Skip(entrenamientoSize).ToArray();
        double[,] distancias= new double[entrenamientoSize,2];

        int clase = 0;
        int K = 3;
        // Para acceder al valor de un rasgo en una fila del dataset:
        string ejemploFilaDataset = dataset[0];
       // string[] datosEnFila = ejemploFilaDataset.Split(',');
        //string clase = datosEnFila[datosEnFila.Length-1];

        //bucle que evalua los datos del datatest punto a punto
        for (int datatest_points = 0; datatest_points < datatest.Length; datatest_points++)
        {
            //guarda una ilera de la DB deacuerdo al indice del bucle
            string datosTest = datatest[datatest_points];
            string[] datosEnFilaTest = datosTest.Split(',');//divide la ilera guardada por las comas

             
             //bucle por cada valor en los datos de entrenamiento
             int ix = 0;
             foreach (string data in dataset) //guarda una ilera de la DB deacuerdo al indice del bucle pero ahora para el datatest
             {
                string[] datosEnFila = data.Split(','); //divide los datos por comas

                double distancia=0;
                double datosDataSet=0;
                double datosDataTest=0;
                for (int columna = 0; columna < datosEnFila.Length-1; columna++)//por cada dato en la fila menos la clase
                {
                    //string valorEnColumna = datosEnFila[columna];

                    // guardar los valores de cada columna ( cuando las columnas coincidan) en datosDataTEst y DatosDataSet Respectivamente
                    double.TryParse(datosEnFilaTest[columna], out datosDataTest);
                    double.TryParse(datosEnFila[columna], out datosDataSet);
                    
                    distancia+=Math.Pow(datosDataSet-datosDataTest, 2);
            

            
                }
                //Console.WriteLine("distancia " + ix+ ": "+ distancia+ "|" + datosEnFila[datosEnFila.Length-1]);

                //distancia= Math.Sqrt(distancia);
                //distancias[i, 0]= distancia;
                 int.TryParse(datosEnFila[datosEnFila.Length-1],out clase);
                if (distancia == 0)
                {
                    Console.WriteLine("CEROOO"+ datosEnFila[0]+ "|" + datosEnFila[1]+"|"+ datosEnFila[2]+ "|"+datosEnFila[3]);
                }
                distancias[ix, 0]= distancia; 
                distancias[ix, 1] = clase;
               
                //distancias[i, 1]= clase;
                
                 Burbuja(distancias);
               

                //metodo de ordenamiento
                ix ++;
             }
 if (datatest_points< 5)
                {
                    for (int i = 0; i < dataset.Length; i++)
                     {
                
                       // Console.WriteLine("distancias prebubble:"+ ix+ ":" + distancias[i,0] + " " + distancias[i,1]);
                     }
                }
           /* */
          /*  Console.WriteLine(distancias[0,0]+ " | "+ distancias[0,1]);*/
        
        
        
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

        }else if (Versicolor>Setosa&&  Versicolor>Virginical)
        {
            Console.WriteLine("Su planta para el punto "+ datatest_points+ " ["+ datosEnFilaTest[0]+","+datosEnFilaTest[1]+","+datosEnFilaTest[2]+ ","+datosEnFilaTest[3]+"] pertenece a la especie Iris Versicolor");

        }else if (Virginical>Versicolor&&  Virginical>Setosa)
        {
            Console.WriteLine("Su planta para el punto "+ datatest_points+ " ["+ datosEnFilaTest[0]+","+datosEnFilaTest[1]+","+datosEnFilaTest[2]+ ","+datosEnFilaTest[3]+"] pertenece a la especie Iris Virginical");

        }else{
            Console.WriteLine(Setosa+ "|" + Versicolor+ "|" + Virginical);
            for (int i = 0; i < dataset.Length; i++)
                     {
                
                        Console.WriteLine("distancias rror:"+ i+ ":" + distancias[i,0] + " " + distancias[i,1]);
                     }

        }
                Console.WriteLine("________________________________________________");

        }
        // Puedes repetir el proceso para acceder a los datos en las filas de datatest según lo necesites.
        Console.WriteLine(dataset.Length+ " "+ datatest.Length);
    }

   

}
