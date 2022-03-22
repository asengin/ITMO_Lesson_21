using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GardenQuest
{
    class Program
    {
        const int gardenSize = 10; //Размерность участка
        //static char[,] garden = new char[gardenSize, gardenSize]; //Двумерный массив "Сад"
        static int[,] garden = new int[gardenSize, gardenSize];
        const int sleepTime = 30; //Константа временной задержки

        static void Main(string[] args)
        {
            ThreadStart threadStart = new ThreadStart(StartSecondGardener);
            Thread thread = new Thread(threadStart);
            thread.Start();

            StartFirstGardener();

            Thread.Sleep(200);
            PrintGarden();
            Console.ReadKey();
        }

        static void StartFirstGardener() //Бежит змейкой слева направо, спускается вниз и бежит справо налево и т.д.
        {
            //int k = 1;
            //garden[2, 6] = 99; garden[3, 7] = 99; garden[0, 9] = 99; //garden[1, 6] = 99;
            int j = 0;
            for (int i = 0; i < gardenSize; i++)
            {
                if (i % 2 == 0) //Условие проверки четности строки, для реализации движения змейкой слева направо вниз налево
                {
                    for (; j < gardenSize; j++)
                    {
                        if (garden[i, j] == 0) //Если в ячейке еще нет значения, то присваиваем
                            garden[i, j] = 1; //k++
                        else
                        {
                            j--; i++; //Если в ячейке есть значение, то ищем куда перейти. Опускаемся на строчку вниз и обратным циклом ищем свободную ячейку.
                            for (; j >= 0 && j < gardenSize; j--)
                            {
                                if (garden[i, j] == 0) //Нашли свободную ячейку в обратном порядке на строчку ниже и вышли
                                {
                                    i--; j++; //сменили значения счетчиков, т.к. они изменятся до нужных во внешнем контуре
                                    break;
                                }
                            }
                            break;
                        }
                    } // j
                    j--;
                    Thread.Sleep(sleepTime);

                }
                else
                {
                    for (; j >= 0 && j < gardenSize; j--)
                    {
                        if (garden[i, j] == 0) //Если в ячейке еще нет значения, то присваиваем
                            garden[i, j] = 1; //k++
                        else
                        {
                            //j++;
                            break;
                        }
                    } //j++
                    j = 0;
                    Thread.Sleep(sleepTime);

                }
            }
        }


        static void StartSecondGardener() //Бежит змейкой справа снизу вверх, сдвигается влево далее сверху вниз и т.д.
        {
            int k = 1;
            //garden[1, 9] = 99; garden[2, 8] = 99; garden[3, 8] = 99;
            int j = gardenSize - 1;
            for (int i = gardenSize - 1; i >= 0; i--)
            {
                if (i % 2 == 0)
                {
                    for (; j >= 0 && j < gardenSize; j++)
                    {
                        if (garden[j, i] == 0)
                            garden[j, i] = 2; //k++
                        else
                            break;
                    }
                    j--;
                    Thread.Sleep(sleepTime);

                }
                else
                {
                    for (; j >= 0; j--)
                    {
                        if (garden[j, i] == 0)
                            garden[j, i] = 2; //k++
                        else
                        {
                            j++; i--;
                            for (; j < gardenSize; j++)
                            {
                                if (garden[j, i] == 0)
                                {
                                    j--; i++;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    j++;
                    Thread.Sleep(sleepTime);

                }
            }
        }


        static void PrintGarden()
        {
            for (int i = 0; i < gardenSize; i++)
            {
                for (int j = 0; j < gardenSize; j++)
                    Console.Write($"{garden[i, j],4}");
                Console.WriteLine();
            }
        }

    }
}



