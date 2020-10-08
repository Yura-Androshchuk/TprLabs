using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
namespace TPRLab2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int[,]> arraysForNM = new List<int[,]>();
            int[,] arr1 = ReaFromFile("1.txt"); arraysForNM.Add(arr1);
            int[,] arr2 = ReaFromFile("2.txt"); arraysForNM.Add(arr2);
            int[,] arr3 = ReaFromFile("3.txt"); arraysForNM.Add(arr3);
            int[,] arr4 = ReaFromFile("4.txt"); arraysForNM.Add(arr4);
            int[,] arr5 = ReaFromFile("5.txt"); arraysForNM.Add(arr5);
            int[,] arr6 = ReaFromFile("6.txt"); arraysForNM.Add(arr6);
            int[,] arr7 = ReaFromFile("7.txt"); arraysForNM.Add(arr7);
            int[,] arr8 = ReaFromFile("8.txt"); arraysForNM.Add(arr8);
            int[,] arr9 = ReaFromFile("9.txt"); arraysForNM.Add(arr9);
            int[,] arr10 = ReaFromFile("10.txt"); arraysForNM.Add(arr10);

            foreach (var item in arraysForNM)
            {
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                if (Graph.TakeMatrixAndCheck(item) == true)
                {
                    FullCheckNeimanMorgrnstern(item);
                }
                else
                {
                    string[,] lMatrix = FormIPNmatrix(item);
                    KFullCheck(lMatrix);
                }
            }
            Console.ReadLine();
        }

        public static void FullCheckNeimanMorgrnstern(int[,] arr1)
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    Console.Write(arr1[i, j] + " ");
                }
                Console.WriteLine();
            }
            List<int> served = new List<int>();
            List<List<int>> lIte = new List<List<int>>(); //s ite values
        List<ColoumnMatrix> coloumnsMatrix = new List<ColoumnMatrix>(); // represents array coloumns as lists
        List<ColoumnMatrix> rowsMatrix = new List<ColoumnMatrix>();

        List<List<int>> qList = new List<List<int>>();

            for (int i = 0; i< 15; i++)
            {
                coloumnsMatrix.Add(new ColoumnMatrix());
                lIte.Add(new List<int>());
                qList.Add(new List<int>());
            }
                int p = 0;
            while(served.Count!=15)
            {
                NeimanMorgenstern(arr1, lIte, coloumnsMatrix, served, p);
                p++;
            }

            for (int i = 0; i < 14; i++)
            {
                    Console.WriteLine("lIte");
                foreach (var item in lIte[i])
                    {
                        Console.WriteLine(item);
                    }
            }
        NeimanMorgensternQPart(arr1, lIte, qList, p);
            InnerStikist(arr1, qList);
            OuterStikist(arr1, qList);
        }
        public static void InnerStikist(int[,] arr, List<List<int>> qlist)
        {
            bool b = true;
            //for (int k = 0; k <qlist.Count()-1; k++)
            //{
                for (int i = 0; i < 15; i++)
                {
                if (b == false)
                {
                    Console.WriteLine("There Is a problem");
                    break; }
                for (int j = 0; j < 15; j++)
                   {
                        if (b == false)
                        { break; }
                        else if (i < qlist[0].Count()-1)
                        {
                            if (arr[qlist[0].ElementAt(i), qlist[0].ElementAt(i + 1)] != 0 || arr[qlist[0].ElementAt(i + 1), qlist[0].ElementAt(i)] != 0)
                            {
                               b = false;
                               break;
                          }
                        }            
                   }
                }
            //}
            if (b == true)
            {
                Console.WriteLine("Proidena perevirka na vnutrishniu stikist");
            }
            else
            {
                Console.WriteLine("Ne proidena perevirka na vnutrishniu stikist");
            }
        }
        public static void OuterStikist(int[,] arr, List<List<int>> qlist)
        {
            List<ColoumnMatrix> coloumnsMatrix1 = new List<ColoumnMatrix>(); // represents array coloumns as lists
            List<int> outerValues = new List<int>();
            List<int> resultToCompare = new List<int>();
            for (int i = 0; i <15; i++)
            {
                outerValues.Add(i);
            }
            for (int i = 0; i < 15; i++)
            {
                coloumnsMatrix1.Add(new ColoumnMatrix());
            }
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {

                    if (arr[j, i] != 0)
                    { coloumnsMatrix1[i].Values.Add(j); coloumnsMatrix1[i].Number = i; }

                }
            }

            outerValues.RemoveAll(item => qlist[0].Contains(item));
            //foreach (var item in outerValues)
            //{
            //    Console.WriteLine(item);
            //}
            for (int i = 0; i < outerValues.Count(); i++)
            {
                if (coloumnsMatrix1[outerValues[i]].Values.Any(item => qlist[0].Contains(item)))
                {
                    resultToCompare.Add(outerValues[i]);
                }
            }
            if (outerValues.Count() == resultToCompare.Count())
            {
                Console.WriteLine("Proidena perevirka na zovnishniu stikist"); ;
            }
            else { Console.WriteLine("Ne proidena perevirka na vnutrishniu stikist"); }
        }
        public static void NeimanMorgenstern(int[,] arr, List<List<int>> lIte, List<ColoumnMatrix> listS, List<int> served, int p)
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (served.Contains(i) == false && served.Contains(j) == false)
                    {
                        if (arr[j, i] != 0)
                        { listS[i].Values.Add(j); listS[i].Number = i; }
                    }
                    else
                    {
                        
                    }
                }
            }
            for (int i = 0; i < 15; i++)
            {
                if (served.Contains(i) == false)
                {
                    if (listS[i].Values.Count == 0)
                    {
                        lIte[p].Add(i);
                        served.Add(i);
                    }
                }
            }
            //Console.WriteLine("Values");
            //foreach (var item in served)
            //{
            //    Console.WriteLine(item);
            //}
            foreach (var item in listS)
            {
                item.Values.Clear();
            }
          //  Console.WriteLine();
        }
        public static void NeimanMorgensternQPart(int[,] arr,List<List<int>> lIte,List<List<int>> qList, int p)
        {
            List<ColoumnMatrix> coloumnsMatrix1 = new List<ColoumnMatrix>(); // represents array coloumns as lists
            for (int i = 0; i < 15; i++)
            {
                coloumnsMatrix1.Add(new ColoumnMatrix());
            }

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {

                        if (arr[j, i] != 0)
                        { coloumnsMatrix1[i].Values.Add(j); coloumnsMatrix1[i].Number = i; }

                }
            }

            for (int i = 0; i < 15; i++)
            {
                qList[i] = lIte[0].DeepClone();
            }
            for (int i = 1; i < 15; i++)
            {
                for (int j = 0; j < lIte[i].Count; j++)
                {
                    if (coloumnsMatrix1[lIte[i].ElementAt(j)].Values.Any(item => qList[i - 1].Contains(item))==false)
                    {
                        for (int m = 0; m < 15; m++)
                        {
                            qList[m].Add(lIte[i].ElementAt(j));
                        }
                    }
                }
            }
            Console.WriteLine("Q part");
            foreach (var item in qList[0])
            {
                Console.WriteLine(item);
            }
        }
        public static int[,] ReaFromFile(String filepath)
        {
            var text = File.ReadAllLines(@filepath);

            // Split on "  ", convert to int32, add to array, add to outer array
            var result = text.Select(x => x.Split("  ").Select(Int32.Parse).ToArray()).ToArray();
            int[][] a = result;
            int[,] a1 = new int[15, 15];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    a1[i, j] = a[i][j];
                }
            }
           
            return a1;
        }
        public static string[,] FormIPNmatrix(int[,] arr)
        {
            string[,] lMatrix = new string[15,15];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (arr[i,j] == 1 && arr[j,i] == 1)
                    {
                        lMatrix[i,j] = "I"; lMatrix[j,i] = "I";
                    }
                    else if(arr[i,j] == 0 && arr[j,i] == 0)
                    {
                        lMatrix[i,j] = "N"; lMatrix[j,i] = "N";
                    }
                    else if (arr[i,j] == 1 && arr[j,i] == 0)
                    {
                        lMatrix[i,j] = "P"; lMatrix[j,i] = " ";
                    }
                    else if (arr[i, j] == 0 && arr[j, i] == 1)
                    {
                        lMatrix[i,j] = " "; lMatrix[j,i] = "P";
                    }
                }
            }
            return lMatrix;
        }

        public static void KFullCheck(string[,] arr)
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("K1");
            K1(arr);
            Console.WriteLine("K2");
            K2(arr);
            Console.WriteLine("K3");
            K3(arr);
            Console.WriteLine("K4");
            K4(arr);
        }
        public static void K2(string[,] arr)
        {
            List<int> l0 = new List<int>();
            List<int> l1 = new List<int>();
            List<int> l2 = new List<int>();
            List<int> l3 = new List<int>();
            List<int> l4 = new List<int>();
            List<int> l5 = new List<int>();
            List<int> l6 = new List<int>();
            List<int> l7 = new List<int>();
            List<int> l8 = new List<int>();
            List<int> l9 = new List<int>();
            List<int> l10 = new List<int>();
            List<int> l11 = new List<int>();
            List<int> l12 = new List<int>();
            List<int> l13 = new List<int>();
            List<int> l14 = new List<int>();

            List<List<int>> listListov = new List<List<int>>();

            listListov.Add(l0); listListov.Add(l1); listListov.Add(l2); listListov.Add(l3); listListov.Add(l4);
            listListov.Add(l5); listListov.Add(l6); listListov.Add(l7); listListov.Add(l8); listListov.Add(l9);
            listListov.Add(l10); listListov.Add(l11); listListov.Add(l12); listListov.Add(l13); listListov.Add(l14);

            
                for (int j = 0; j < 15; j++)
                {
                    if (!arr[0, j].SequenceEqual(" ")&& !arr[0, j].SequenceEqual("I"))
                    { l0.Add(j); }
                    if (!arr[1, j].SequenceEqual(" ") && !arr[1, j].SequenceEqual("I"))
                    { l1.Add(j); }
                    if (!arr[2, j].SequenceEqual(" ") && !arr[2, j].SequenceEqual("I"))
                    { l2.Add(j); }
                    if (!arr[3, j].SequenceEqual(" ") && !arr[3, j].SequenceEqual("I"))
                    { l3.Add(j); }
                    if (!arr[4, j].SequenceEqual(" ") && !arr[4, j].SequenceEqual("I"))
                    { l4.Add(j); }
                    if (!arr[5, j].SequenceEqual(" ") && !arr[5, j].SequenceEqual("I"))
                    { l5.Add(j); }
                    if (!arr[6, j].SequenceEqual(" ") && !arr[6, j].SequenceEqual("I"))
                    { l6.Add(j); }
                    if (!arr[7, j].SequenceEqual(" ") && !arr[7, j].SequenceEqual("I"))
                    { l7.Add(j); }
                    if (!arr[8, j].SequenceEqual(" ") && !arr[8, j].SequenceEqual("I"))
                    { l8.Add(j); }
                    if (!arr[9, j].SequenceEqual(" ") && !arr[9, j].SequenceEqual("I"))
                    { l9.Add(j); }
                    if (!arr[10, j].SequenceEqual(" ") && !arr[10, j].SequenceEqual("I"))
                    { l10.Add(j); }
                    if (!arr[11, j].SequenceEqual(" ") && !arr[11, j].SequenceEqual("I"))
                    { l11.Add(j); }
                    if (!arr[12, j].SequenceEqual(" ") && !arr[12, j].SequenceEqual("I"))
                    { l12.Add(j); }
                    if (!arr[13, j].SequenceEqual(" ") && !arr[13, j].SequenceEqual("I"))
                    { l13.Add(j); }
                    if (!arr[14, j].SequenceEqual(" ") && !arr[14, j].SequenceEqual("I"))
                    { l14.Add(j); }
            }
            List<int> c = listListov[0];
            //foreach (var item in c)
            //{
            //    Console.WriteLine(item);
            //}

            List<int> otvet = new List<int>();
            for(int i = 0; i < 15; i++)
            {
                bool b = !listListov[0].Except(listListov[i]).Any() && !listListov[1].Except(listListov[i]).Any() && !listListov[2].Except(listListov[i]).Any()
                    && !listListov[3].Except(listListov[i]).Any() && !listListov[4].Except(listListov[i]).Any() && !listListov[5].Except(listListov[i]).Any()
                    && !listListov[6].Except(listListov[i]).Any() && !listListov[7].Except(listListov[i]).Any() && !listListov[8].Except(listListov[i]).Any()
                    && !listListov[9].Except(listListov[i]).Any() && !listListov[10].Except(listListov[i]).Any() && !listListov[11].Except(listListov[i]).Any()
                    && !listListov[12].Except(listListov[i]).Any() && !listListov[13].Except(listListov[i]).Any() && !listListov[14].Except(listListov[i]).Any();
                if (b == true) { otvet.Add(i); }
            }
            Console.WriteLine("Answers for K2");
            foreach (var item in otvet)
            {
                Console.WriteLine($"THE ANSWER INCLUDES max {item}");
            }
            for (int i = 0; i < listListov.Count(); i++)
            {
                if (listListov[i].Count == 15)
                {
                    Console.WriteLine($"THE ANSWER INCLUDES  opt {i}");
                }
            }
        }
        public static void K1(string[,] arr)
        {
            List<int> l0 = new List<int>();
            List<int> l1 = new List<int>();
            List<int> l2 = new List<int>();
            List<int> l3 = new List<int>();
            List<int> l4 = new List<int>();
            List<int> l5 = new List<int>();
            List<int> l6 = new List<int>();
            List<int> l7 = new List<int>();
            List<int> l8 = new List<int>();
            List<int> l9 = new List<int>();
            List<int> l10 = new List<int>();
            List<int> l11 = new List<int>();
            List<int> l12 = new List<int>();
            List<int> l13 = new List<int>();
            List<int> l14 = new List<int>();

            List<List<int>> listListov = new List<List<int>>();

            listListov.Add(l0); listListov.Add(l1); listListov.Add(l2); listListov.Add(l3); listListov.Add(l4);
            listListov.Add(l5); listListov.Add(l6); listListov.Add(l7); listListov.Add(l8); listListov.Add(l9);
            listListov.Add(l10); listListov.Add(l11); listListov.Add(l12); listListov.Add(l13); listListov.Add(l14);


            for (int j = 0; j < 15; j++)
            {
                if (!arr[0, j].SequenceEqual(" "))
                { l0.Add(j); }
                if (!arr[1, j].SequenceEqual(" "))
                { l1.Add(j); }
                if (!arr[2, j].SequenceEqual(" "))
                { l2.Add(j); }
                if (!arr[3, j].SequenceEqual(" "))
                { l3.Add(j); }
                if (!arr[4, j].SequenceEqual(" "))
                { l4.Add(j); }
                if (!arr[5, j].SequenceEqual(" "))
                { l5.Add(j); }
                if (!arr[6, j].SequenceEqual(" "))
                { l6.Add(j); }
                if (!arr[7, j].SequenceEqual(" "))
                { l7.Add(j); }
                if (!arr[8, j].SequenceEqual(" "))
                { l8.Add(j); }
                if (!arr[9, j].SequenceEqual(" "))
                { l9.Add(j); }
                if (!arr[10, j].SequenceEqual(" "))
                { l10.Add(j); }
                if (!arr[11, j].SequenceEqual(" "))
                { l11.Add(j); }
                if (!arr[12, j].SequenceEqual(" "))
                { l12.Add(j); }
                if (!arr[13, j].SequenceEqual(" "))
                { l13.Add(j); }
                if (!arr[14, j].SequenceEqual(" "))
                { l14.Add(j); }
            }
            List<int> c = listListov[0];
            //foreach (var item in c)
            //{
            //    Console.WriteLine(item);
            //}

            List<int> otvet = new List<int>();
            for (int i = 0; i < 15; i++)
            {
                bool b = !listListov[0].Except(listListov[i]).Any() && !listListov[1].Except(listListov[i]).Any() && !listListov[2].Except(listListov[i]).Any()
                    && !listListov[3].Except(listListov[i]).Any() && !listListov[4].Except(listListov[i]).Any() && !listListov[5].Except(listListov[i]).Any()
                    && !listListov[6].Except(listListov[i]).Any() && !listListov[7].Except(listListov[i]).Any() && !listListov[8].Except(listListov[i]).Any()
                    && !listListov[9].Except(listListov[i]).Any() && !listListov[10].Except(listListov[i]).Any() && !listListov[11].Except(listListov[i]).Any()
                    && !listListov[12].Except(listListov[i]).Any() && !listListov[13].Except(listListov[i]).Any() && !listListov[14].Except(listListov[i]).Any();
                if (b == true) { otvet.Add(i); }
            }
            Console.WriteLine("Answers for K1");
            foreach (var item in otvet)
            {
                Console.WriteLine($"THE ANSWER INCLUDES  max {item}");
            }
            for (int i = 0; i < listListov.Count(); i++)
            {
                if (listListov[i].Count == 15)
                {
                    Console.WriteLine($"THE ANSWER INCLUDES  opt {i}");
                }
            }
        }
        public static void K3(string[,] arr)
        {
            List<int> l0 = new List<int>();
            List<int> l1 = new List<int>();
            List<int> l2 = new List<int>();
            List<int> l3 = new List<int>();
            List<int> l4 = new List<int>();
            List<int> l5 = new List<int>();
            List<int> l6 = new List<int>();
            List<int> l7 = new List<int>();
            List<int> l8 = new List<int>();
            List<int> l9 = new List<int>();
            List<int> l10 = new List<int>();
            List<int> l11 = new List<int>();
            List<int> l12 = new List<int>();
            List<int> l13 = new List<int>();
            List<int> l14 = new List<int>();

            List<List<int>> listListov = new List<List<int>>();

            listListov.Add(l0); listListov.Add(l1); listListov.Add(l2); listListov.Add(l3); listListov.Add(l4);
            listListov.Add(l5); listListov.Add(l6); listListov.Add(l7); listListov.Add(l8); listListov.Add(l9);
            listListov.Add(l10); listListov.Add(l11); listListov.Add(l12); listListov.Add(l13); listListov.Add(l14);


            for (int j = 0; j < 15; j++)
            {
                if (!arr[0, j].SequenceEqual(" ") && !arr[0, j].SequenceEqual("N"))
                { l0.Add(j); }
                if (!arr[1, j].SequenceEqual(" ") && !arr[1, j].SequenceEqual("N"))
                { l1.Add(j); }
                if (!arr[2, j].SequenceEqual(" ") && !arr[2, j].SequenceEqual("N"))
                { l2.Add(j); }
                if (!arr[3, j].SequenceEqual(" ") && !arr[3, j].SequenceEqual("N"))
                { l3.Add(j); }
                if (!arr[4, j].SequenceEqual(" ") && !arr[4, j].SequenceEqual("N"))
                { l4.Add(j); }
                if (!arr[5, j].SequenceEqual(" ") && !arr[5, j].SequenceEqual("N"))
                { l5.Add(j); }
                if (!arr[6, j].SequenceEqual(" ") && !arr[6, j].SequenceEqual("N"))
                { l6.Add(j); }
                if (!arr[7, j].SequenceEqual(" ") && !arr[7, j].SequenceEqual("N"))
                { l7.Add(j); }
                if (!arr[8, j].SequenceEqual(" ") && !arr[8, j].SequenceEqual("N"))
                { l8.Add(j); }
                if (!arr[9, j].SequenceEqual(" ") && !arr[9, j].SequenceEqual("N"))
                { l9.Add(j); }
                if (!arr[10, j].SequenceEqual(" ") && !arr[10, j].SequenceEqual("N"))
                { l10.Add(j); }
                if (!arr[11, j].SequenceEqual(" ") && !arr[11, j].SequenceEqual("N"))
                { l11.Add(j); }
                if (!arr[12, j].SequenceEqual(" ") && !arr[12, j].SequenceEqual("N"))
                { l12.Add(j); }
                if (!arr[13, j].SequenceEqual(" ") && !arr[13, j].SequenceEqual("N"))
                { l13.Add(j); }
                if (!arr[14, j].SequenceEqual(" ") && !arr[14, j].SequenceEqual("N"))
                { l14.Add(j); }
            }
            List<int> c = listListov[0];
            //foreach (var item in c)
            //{
            //    Console.WriteLine(item);
            //}

            List<int> otvet = new List<int>();
            for (int i = 0; i < 15; i++)
            {
                bool b = !listListov[0].Except(listListov[i]).Any() && !listListov[1].Except(listListov[i]).Any() && !listListov[2].Except(listListov[i]).Any()
                    && !listListov[3].Except(listListov[i]).Any() && !listListov[4].Except(listListov[i]).Any() && !listListov[5].Except(listListov[i]).Any()
                    && !listListov[6].Except(listListov[i]).Any() && !listListov[7].Except(listListov[i]).Any() && !listListov[8].Except(listListov[i]).Any()
                    && !listListov[9].Except(listListov[i]).Any() && !listListov[10].Except(listListov[i]).Any() && !listListov[11].Except(listListov[i]).Any()
                    && !listListov[12].Except(listListov[i]).Any() && !listListov[13].Except(listListov[i]).Any() && !listListov[14].Except(listListov[i]).Any();
                if (b == true) { otvet.Add(i); }
            }
            Console.WriteLine("Answers for K3");
            foreach (var item in otvet)
            {
                Console.WriteLine($"THE ANSWER INCLUDES max {item}");
            }
            for (int i = 0; i < listListov.Count(); i++)
            {
                if (listListov[i].Count == 15)
                {
                    Console.WriteLine($"THE ANSWER INCLUDES  opt {i}");
                }
            }
        }
        public static void K4(string[,] arr)
            {
                List<int> l0 = new List<int>();
                List<int> l1 = new List<int>();
                List<int> l2 = new List<int>();
                List<int> l3 = new List<int>();
                List<int> l4 = new List<int>();
                List<int> l5 = new List<int>();
                List<int> l6 = new List<int>();
                List<int> l7 = new List<int>();
                List<int> l8 = new List<int>();
                List<int> l9 = new List<int>();
                List<int> l10 = new List<int>();
                List<int> l11 = new List<int>();
                List<int> l12 = new List<int>();
                List<int> l13 = new List<int>();
                List<int> l14 = new List<int>();

                List<List<int>> listListov = new List<List<int>>();

                listListov.Add(l0); listListov.Add(l1); listListov.Add(l2); listListov.Add(l3); listListov.Add(l4);
                listListov.Add(l5); listListov.Add(l6); listListov.Add(l7); listListov.Add(l8); listListov.Add(l9);
                listListov.Add(l10); listListov.Add(l11); listListov.Add(l12); listListov.Add(l13); listListov.Add(l14);


                for (int j = 0; j < 15; j++)
                {
                    if (!arr[0, j].SequenceEqual(" ") && !arr[0, j].SequenceEqual("N") && !arr[0, j].SequenceEqual("I"))
                    { l0.Add(j); }
                    if (!arr[1, j].SequenceEqual(" ") && !arr[1, j].SequenceEqual("N") && !arr[0, j].SequenceEqual("I"))
                    { l1.Add(j); }
                    if (!arr[2, j].SequenceEqual(" ") && !arr[2, j].SequenceEqual("N") && !arr[0, j].SequenceEqual("I"))
                    { l2.Add(j); }
                    if (!arr[3, j].SequenceEqual(" ") && !arr[3, j].SequenceEqual("N") && !arr[0, j].SequenceEqual("I"))
                    { l3.Add(j); }
                    if (!arr[4, j].SequenceEqual(" ") && !arr[4, j].SequenceEqual("N") && !arr[0, j].SequenceEqual("I"))
                    { l4.Add(j); }
                    if (!arr[5, j].SequenceEqual(" ") && !arr[5, j].SequenceEqual("N") && !arr[0, j].SequenceEqual("I"))
                    { l5.Add(j); }
                    if (!arr[6, j].SequenceEqual(" ") && !arr[6, j].SequenceEqual("N") && !arr[0, j].SequenceEqual("I"))
                    { l6.Add(j); }
                    if (!arr[7, j].SequenceEqual(" ") && !arr[7, j].SequenceEqual("N") && !arr[0, j].SequenceEqual("I"))
                    { l7.Add(j); }
                    if (!arr[8, j].SequenceEqual(" ") && !arr[8, j].SequenceEqual("N") && !arr[0, j].SequenceEqual("I"))
                    { l8.Add(j); }
                    if (!arr[9, j].SequenceEqual(" ") && !arr[9, j].SequenceEqual("N") && !arr[0, j].SequenceEqual("I"))
                    { l9.Add(j); }
                    if (!arr[10, j].SequenceEqual(" ") && !arr[10, j].SequenceEqual("N") && !arr[0, j].SequenceEqual("I"))
                    { l10.Add(j); }
                    if (!arr[11, j].SequenceEqual(" ") && !arr[11, j].SequenceEqual("N") && !arr[0, j].SequenceEqual("I"))
                    { l11.Add(j); }
                    if (!arr[12, j].SequenceEqual(" ") && !arr[12, j].SequenceEqual("N") && !arr[0, j].SequenceEqual("I"))
                    { l12.Add(j); }
                    if (!arr[13, j].SequenceEqual(" ") && !arr[13, j].SequenceEqual("N") && !arr[0, j].SequenceEqual("I"))
                    { l13.Add(j); }
                    if (!arr[14, j].SequenceEqual(" ") && !arr[14, j].SequenceEqual("N") && !arr[0, j].SequenceEqual("I"))
                    { l14.Add(j); }
                }
                List<int> c = listListov[0];
                //foreach (var item in c)
                //{
                //    Console.WriteLine(item);
                //}

                List<int> otvet = new List<int>();
                for (int i = 0; i < 15; i++)
                {
                    bool b = !listListov[0].Except(listListov[i]).Any() && !listListov[1].Except(listListov[i]).Any() && !listListov[2].Except(listListov[i]).Any()
                        && !listListov[3].Except(listListov[i]).Any() && !listListov[4].Except(listListov[i]).Any() && !listListov[5].Except(listListov[i]).Any()
                        && !listListov[6].Except(listListov[i]).Any() && !listListov[7].Except(listListov[i]).Any() && !listListov[8].Except(listListov[i]).Any()
                        && !listListov[9].Except(listListov[i]).Any() && !listListov[10].Except(listListov[i]).Any() && !listListov[11].Except(listListov[i]).Any()
                        && !listListov[12].Except(listListov[i]).Any() && !listListov[13].Except(listListov[i]).Any() && !listListov[14].Except(listListov[i]).Any();
                    if (b == true) { otvet.Add(i); }
                }
                Console.WriteLine("Answers for K4");
                foreach (var item in otvet)
                {
                    Console.WriteLine($"THE ANSWER INCLUDES max {item}");
                }
            for (int i = 0; i < listListov.Count(); i++)
            {
                if (listListov[i].Count == 15)
                {
                    Console.WriteLine($"THE ANSWER INCLUDES  opt {i}");
                }
            }
        }
    }
    public class ColoumnMatrix {
        public int Number { get; set; }
        public List<int> Values { get; set; }
        public ColoumnMatrix()
        {
            
            Values = new List<int>();  
        }
        public ColoumnMatrix(int num, List<int> l)
        {

        }
    }
    public static class ExtensionMethods
    {
        // Deep clone
        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
