using Aspose.Cells;
using FrequentDataMining.AgrawalFaster;
using FrequentDataMining.Apriori;
using FrequentDataMining.Common;
using lib_apriori_net.Models;
using lib_apriori_net.Pattern;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;

namespace lib_apriori_net.Data
{
    public static class loadData
    {
        private static SingletonPattern _SingletonPattern;

        static loadData()
        {
            _SingletonPattern = new SingletonPattern();
        }

        #region Test
        public static void InitializeProcess()
        {
            Console.WriteLine("Inicio del proceso");
            Console.WriteLine("Cargando fichero Excel");
            var wb = loadExcel("C:\\Develop\\08. Net-Core\\IA-apriori\\IA-Apriori-app\\data\\Data-v1r001.xlsx");
            Console.WriteLine("Obtienendo transacciones");
            var datos = getDatafromWorkBook<DataExcel>(wb);
            var ubigeo = getDatafromWorkBook<Ubigeo>(wb).Distinct().ToList();

            TypeRegister.Register<Muestra>((a, b) => a.Name.CompareTo(b.Name));

            //obtenemos los valores unicos de los linajes de covid
            //List<string> linajeCovid = datos.Select(x => x.Resultado).Distinct().ToList<string>();
            // creamos una lista de lista string con todo el muestreo de datos
            List<List<Muestra>> muestraPorAnalizar = new List<List<Muestra>>();

            foreach (var item in datos)
            {
                List<Muestra> muestras = new List<Muestra>();

                if (!string.IsNullOrWhiteSpace(item.Edad)) muestras.Add(new Muestra(item.Edad));
                if (!string.IsNullOrWhiteSpace(item.Sexo)) muestras.Add(new Muestra(item.Sexo));
                if (!string.IsNullOrWhiteSpace(item.Ubigeo)) muestras.Add(new Muestra(item.Ubigeo));
                if (!string.IsNullOrWhiteSpace(item.Resultado)) muestras.Add(new Muestra(item.Resultado));
                muestraPorAnalizar.Add(muestras);
            }

            /// Preparamos muestra
            var itemsets = new List<Itemset<Muestra>>();
            /// asignamos transacciones cargadas desde el Excel
            var transactions = muestraPorAnalizar;

            var apriori = new Apriori<Muestra>
            {
                MinSupport = (double)1 / 900,
                SaveItemset = itemset => itemsets.Add(itemset),
                GetTransactions = () => transactions
            };

            apriori.ProcessTransactions();

            var rules = new List<Rule<Muestra>>();

            var agrawal = new AgrawalFaster<Muestra>
            {
                MinLift = 0.01,
                MinConfidence = 0.01,
                TransactionsCount = transactions.Count(),
                GetItemsets = () => itemsets,
                SaveRule = rule => rules.Add(rule)
            };

            agrawal.Run();

            foreach (var item in itemsets.OrderByDescending(v => v.Support))
            {
                Console.WriteLine(string.Join("; ", item.Value.Select(x => x.Name)) + " #SUP: " + item.Support);
                //.OrderBy(i => i)
            }

            Console.WriteLine("====");

            foreach (var item in rules.OrderByDescending(r => r.Confidence))
            {
                Console.WriteLine(string.Join("; ", item.Combination.Select(x => x.Name)) + " => " + string.Join("; ", item.Remaining.Select(x => x.Name)) + " ===> Confidence: " + item.Confidence + " Lift: " + item.Lift);
            }

            Console.ReadLine();
        }

        private static Workbook loadExcel(string path)
        {
            Workbook wb = new Workbook(path);

            return wb;
        }
        #endregion


        #region Private
        private static List<T> getDatafromWorkBook<T>(Workbook wb)
        {

            List<T> listObjects = new List<T>();
            T obj = default(T);

            Worksheet worksheet = wb.Worksheets[0];

            // Get number of rows and columns
            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            // Loop through rows
            for (int i = 1; i < rows; i++)
            {
                obj = Activator.CreateInstance<T>();
                // Loop through each column in selected row
                for (int j = 0; j <= cols; j++)
                {

                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        string displayName = ((DisplayAttribute)prop.GetCustomAttributes(typeof(DisplayAttribute), true)[0]).Name;

                        if (displayName == worksheet.Cells[0, j].Value.ToString())
                        {
                            if (worksheet.Cells[i, j].Value != null)
                            {
                                prop.SetValue(obj, worksheet.Cells[i, j].Value.ToString(), null);
                            }
                        }
                    }
                    // Pring cell value
                    //Console.Write(worksheet.Cells[i, j].Value + " | ");
                }
                // Print line break
                //Console.WriteLine(" ");
                listObjects.Add(obj);
            }

            return listObjects;
        }
        #endregion

        #region MVC APP

        public static List<DataExcel> LoadExcelFromStream(Stream data)
        {
            Workbook wb = new Workbook(data);
            var datos = getDatafromWorkBook<DataExcel>(wb);
            return datos;
        }

        public static void LearnProcess(List<DataExcel> datos)
        {
            TypeRegister.Register<Muestra>((a, b) => a.Name.CompareTo(b.Name));

            List<List<Muestra>> muestraPorAnalizar = new List<List<Muestra>>();

            foreach (var item in datos)
            {
                List<Muestra> muestras = new List<Muestra>();

                if (!string.IsNullOrWhiteSpace(item.Edad)) muestras.Add(new Muestra(item.Edad));
                if (!string.IsNullOrWhiteSpace(item.Sexo)) muestras.Add(new Muestra(item.Sexo));
                if (!string.IsNullOrWhiteSpace(item.Ubigeo)) muestras.Add(new Muestra(item.Ubigeo));
                if (!string.IsNullOrWhiteSpace(item.Resultado)) muestras.Add(new Muestra(item.Resultado));
                muestraPorAnalizar.Add(muestras);
            }

            /// Preparamos muestra
            var itemsets = new List<Itemset<Muestra>>();
            /// asignamos transacciones cargadas desde el Excel
            var transactions = muestraPorAnalizar;

            var apriori = new Apriori<Muestra>
            {
                MinSupport = (double)1 / 900,
                SaveItemset = itemset => itemsets.Add(itemset),
                GetTransactions = () => transactions
            };

            apriori.ProcessTransactions();

            var rules = new List<Rule<Muestra>>();

            var agrawal = new AgrawalFaster<Muestra>
            {
                MinLift = 0.01,
                MinConfidence = 0.01,
                TransactionsCount = transactions.Count(),
                GetItemsets = () => itemsets,
                SaveRule = rule => rules.Add(rule)
            };

            agrawal.Run();

            foreach (var item in itemsets.OrderByDescending(v => v.Support))
            {
                Console.WriteLine(string.Join("; ", item.Value.Select(x => x.Name)) + " #SUP: " + item.Support);
                //.OrderBy(i => i)
            }

            Console.WriteLine("====");

            List<ResultProcess> resultProcesses = new List<ResultProcess>();
            foreach (var item in rules.OrderByDescending(r => r.Confidence))
            {
                string combination = string.Join("; ", item.Combination.Select(x => x.Name));
                string remaingin = string.Join("; ", item.Remaining.Select(x => x.Name));

                resultProcesses.Add(new ResultProcess(combination, remaingin, item.Confidence.ToString(), item.Lift.ToString()));

                ///Console.WriteLine(string.Join("; ", item.Combination.Select(x => x.Name)) + " => " + string.Join("; ", item.Remaining.Select(x => x.Name)) + " ===> Confidence: " + item.Confidence + " Lift: " + item.Lift);
            }
            _SingletonPattern._singletonSelfService.FinalResult = resultProcesses;
        }

        #endregion


    }
}
