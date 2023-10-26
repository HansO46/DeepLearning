using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        const string archivoBaseDeDatos = "iris.txt";
        string[] filas = File.ReadAllLines(archivoBaseDeDatos);

        // Aleatoriamente reorganizar las filas
        /* Random rand = new Random();
        filas = filas.OrderBy(x => rand.Next()).ToArray();
       */

        double GuardadoAuxiliar= 0;
        // Dividir los datos en una proporción del 80/20
        int totalFilas = filas.Length;

        /*
        int entrenamientoSize = (int) (totalFilas * 0.8);


        */
                double[,] distancias= new double[totalFilas,2];

        string[] dataset = filas.Take(totalFilas).ToArray();
        string[] datatest = filas.Take(1).ToArray();

        int clase = 0;
        int K = 3;



        // Para acceder al valor de la clase de una fila del dataset:
        string ejemploFilaDataset = dataset[0];
       // string[]  datosEnFila = ejemploFilaDataset.Split(',');
        //string clase = datosEnFila[datosEnFila.Length-1];
        for (int datatest_points = 0; datatest_points < datatest.Length; datatest_points++)
        {
            string datosTest = datatest[datatest_points];
            string[] datosEnFilaTest = datosTest.Split(',');

             int i=0;
             foreach (string data in dataset)
             {
                string[] datosEnFila = data.Split(',');
                double distancia=0;
                double datosDataSet=0;
                double datosDataTest=0;
                for (int columna = 0; columna < datosEnFila.Length-1; columna++)
                {
                    //string valorEnColumna = datosEnFila[columna];
                    double.TryParse(datosEnFilaTest[columna], out datosDataTest);
                    double.TryParse(datosEnFila[columna], out datosDataSet);
                    distancia+=Math.Pow(datosDataSet-datosDataTest, 2);
                }
                distancia= Math.Sqrt(distancia);
                //distancias[i, 0]= distancia;
                int.TryParse(datosEnFila[datosEnFila.Length-1],out clase);
                //distancias[i, 1]= clase;
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
            Console.WriteLine("Su planta para el punto "+ datatest_points+ " ["+ datosEnFilaTest[0]+","+datosEnFilaTest[1]+","+datosEnFilaTest[2]+ ","+datosEnFilaTest[3]+"] pertenece a la especie Iris Setosa");

        }else if (Versicolor>Setosa&&  Versicolor>Virginical)
        {
            Console.WriteLine("Su planta para el punto "+ datatest_points+ " ["+ datosEnFilaTest[0]+","+datosEnFilaTest[1]+","+datosEnFilaTest[2]+ ","+datosEnFilaTest[3]+"] pertenece a la especie Iris Versicolor");

        }else if (Virginical>Versicolor&&  Virginical>Setosa)
        {
            Console.WriteLine("Su planta para el punto "+ datatest_points+ " ["+ datosEnFilaTest[0]+","+datosEnFilaTest[1]+","+datosEnFilaTest[2]+ ","+datosEnFilaTest[3]+"] pertenece a la especie Iris Virginical");

        }else{
            Console.WriteLine(Setosa+ "|" + Versicolor+ "|" + Virginical);
        }
        }
        // Puedes repetir el proceso para acceder a los datos en las filas de datatest según lo necesites.
        Console.WriteLine(dataset.Length+ " "+ datatest.Length);
    }
/*
    void imprimirValores(string[] a){
        for (int i = 0; i < a.Length-1; i++)
        {
            string[] b = a.Split(',');
            for (int j = 0; b.Length; j++)
            {
                
            }
        }



    }*/
}
