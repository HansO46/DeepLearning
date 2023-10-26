using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        const string archivoBaseDeDatos = "iris.txt";//leer archivo
        string[] filas = File.ReadAllLines(archivoBaseDeDatos);//guarda cada ilera del txt en un array
        double proporcion;
        proporcion= 0.8;
        int claseReal= 0;

        // Aleatoriamente reorganizar las filas
        Random rand = new Random();
        filas = filas.OrderBy(x => rand.Next()).ToArray();
        double GuardadoAuxiliar= 0;

        // Dividir los datos en una proporción del 80/20
        int totalFilas = filas.Length;
        int entrenamientoSize = (int) (totalFilas * proporcion);
        double[,] distancias= new double[totalFilas,2];

        string[] dataset = filas.Take(entrenamientoSize).ToArray();
        string[] datatest = filas.Skip(entrenamientoSize).ToArray();

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

             int i=0;
             //bucle por cada valor en los datos de entrenamiento
             foreach (string data in dataset) //guarda una ilera de la DB deacuerdo al indice del bucle pero ahora para el datatest
             {
                string[] datosEnFila = data.Split(','); //divide los datos por comas

                double distancia=0;
                double datosDataSet=0;
                double datosDataTest=0;
                for (int columna = 0; columna < datosEnFila.Length-1; columna++)//por cada dato en la fila menos la clase
                {
                    //string valorEnColumna = datosEnFila[columna];

                    // guardar los valores de cada columna (cuando las columnas coincidan) en datosDataTEst y DatosDataSet Respectivamente
                    double.TryParse(datosEnFilaTest[columna], out datosDataTest);
                    double.TryParse(datosEnFila[columna], out datosDataSet);
                    
                    distancia+=Math.Pow(datosDataSet-datosDataTest, 2);
                }
                //distancia= Math.Sqrt(distancia);
                //distancias[i, 0]= distancia;




                int.TryParse(datosEnFila[datosEnFila.Length-1],out clase);




                //distancias[i, 1]= clase;

                //metodo de ordenamiento
                if(i < K){
                   distancias[i, 0]= distancia; 
                   distancias[i, 1]= clase;
                }else{
                    for (int j = 0; j < K; j++)
                    {
                        if (distancia<distancias[j,0])
                        {
                            GuardadoAuxiliar= distancias[j,0];
                            distancias[j,0]= distancia;
                            distancias[j, 1]= clase;
                        }
                    }
                }
                i++;
             }
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

        if (Setosa>Versicolor&&  Setosa>Virginical)
        {
            clase=1;
            claseReal=
            
        }else if (Versicolor>Setosa&&  Versicolor>Virginical)
        {
            clase=2;
        }else if (Virginical>Versicolor&&  Virginical>Setosa)
        {
            clase=3;
        }else{
            Console.WriteLine(Setosa+ "|" + Versicolor+ "|" + Virginical);
        }


        }
        // Puedes repetir el proceso para acceder a los datos en las filas de datatest según lo necesites.
        Console.WriteLine(dataset.Length+ " "+ datatest.Length);
    }
}
