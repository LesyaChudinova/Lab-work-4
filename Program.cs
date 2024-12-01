using System;
using System.Collections;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1;

class lab4
{
    static void PrintArray(int[] array) //функция печати массива
    {
        if (array.Length > 0) //если массив не пустой
        {
            Console.WriteLine("Ваш массив:");
            foreach (int item in array)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine(); //проходим по всему массиву и выводим каждый элемент в строчку
        }
        else
        {
            Console.WriteLine("Массив пуст"); //если в массиве нет элементов, то выполнить дальнейшие действия невозможно      
        }
    }

    static void FastSortArray(int[] array) //функция быстрой сортировки
    {
        double step = array.Length; //в начале интервал между элементами сравнения равен длине массива
        bool swapped = true; //переменная, которая будет отвечать за то, поменялись ли элементы местами
        while (step > 1 || swapped) //пока шаг не стал равным единице или пока элементы продолжают меняться местами
        {
            step /= 1.3; //делим шаг на фактор уменьшения
            if (step < 1) step = 1; //если в результате деления получилось число меньше 1, то понимаем значение шага ровно до единицы
            swapped = false; //присваиваем переменной ложное значение: если мы не поменяем никаких элементов за итерацию, то она останется ложной
            int i = 0;
            while (i + step < array.Length) //проходим циклом по элементам, находящимся друг от друга на расстоянии шага, пока не дойдем до конца массива
            {
                int j = i + (int)step; //находим индекс второго элемента путем округления шага до целого числа
                if (array[i] > array[j]) //если первый элемент меньше второго
                {
                    (array[i], array[j]) = (array[j], array[i]); //меняем элементы местами
                    swapped = true; //сигнализируем, что смена мест произошла
                }
                i++;
            }
        }
    }

    static void SortArray(int[] array) //сортировка массива методом простого выбора
    {
        for (int i = 0; i < array.Length; i++) //запускаем цикл, который пройдет по всему массиву
        {
            int min = array[i]; //возьмем за минимальное число 0-й элемент массива
            int indexMin = i; //индекс минимального элемента равен 0
            for (int j = i + 1; j < array.Length; j++) //вложенным циклом проходим по оставшемуся массиву
            {
                if (array[j] < min) //находим минимальный элемент в оставшемся массиве
                {
                    min = array[j];
                    indexMin = j;
                }
            }
            array[indexMin] = array[i];
            array[i] = min; //ставим найденный минимальный элемент на 0-е место и заходим в цикл снова: теперь вместо 0-го элемента будет рассматриваться 1-й
        }
    }

       static void Main()
    {
        //Объявление переменных, которые понадобятся в нескольких кейсах
        Random randomElement = new Random(); //создание класса для использования датчика рандомных чисел
        bool isNumberCorrect; //логическая переменная для проверки правильности ввода пунктов основного консольного меню
        bool isElementCorrect; //логическая переменная для проверки правильности ввода элементов массива

        int menuPoint = 0; //переменная, хранящая значения выбранных пользователем пунктов меню
        int length = 0; //переменная, хранящая длину массива
        int element = 0;  //переменная, хранящая элементы массива, если пользователь вводит их с клавиатуры
        int comparisons = 0; //переменная, которая будет подсчитывать количество сранений при поиске

        int[] arr = new int[length]; //задание массива, с которым мы будем работать; изначально - нулевой
        int[] temporaryArray = new int[length]; //задание вспомогательного массива

        int i = 0; //счетчик, который будет использоваться в циклах с параметром и сновных массивах
        int j = 0; //счетчик, который будет использоваться во вложенных циклах с параметром и временных массивах

        //организация основного тела программы и вывод пунктов меню
        do {
        Console.WriteLine();
        Console.WriteLine("Выберите пункт меню: введите нужное число");
        Console.WriteLine("1. Создать массив");
        Console.WriteLine("2. Распечатать массив");
        Console.WriteLine("3. Удалить все элементы с нечетными индексами из массива");
        Console.WriteLine("4. Добавить элемент с номером K в массив");
        Console.WriteLine("5. Перевернуть массив");
        Console.WriteLine("6. Найти в массиве элемент, который равен среднему арифметическому всех элементов массива и узнать количество сравнений, понадобившихся для поиска");
        Console.WriteLine("7. Отсортировать массив (методом простого выбора)");
        Console.WriteLine("8. Отсортировать массив (методом расчески)");
        Console.WriteLine("9. Выполнить бинарный поиск элемента, который пользователь вводит с клавиатуры");
        Console.WriteLine("0. Выход");
        Console.WriteLine();

        do
        {
            Console.Write("Выбранный вами пункт меню: ");
            isNumberCorrect = int.TryParse(Console.ReadLine(), out menuPoint);
            if (menuPoint < 0 || menuPoint > 10) isNumberCorrect = false;
            if (!isNumberCorrect) Console.WriteLine("Пожалуйста, введите число. Выберите одно из тех, что указаны в меню.");
        } while (!isNumberCorrect); //введенный пользователем пункт меню проходит проверку на корректность введенных данных

            switch (menuPoint) //основное консольное меню
            {
                case 1://ввод массива
                    bool isMethodCorrect; //логическая переменная для проверки правильности ввода метода заполнения массива - правильности пунктов вложенного консольного меню
                    int fillingMethod = 0;  //переменная, хранящая значения выбранных пользователем пунктов меню - способы заполнения массива

                    int counter = 0; //счетчик для ввода массива с клавиатуры

                    bool isLengthCorrect;//логическая переменная для проверки правильности ввода длины массива
                    do
                    {
                        Console.WriteLine("Введите длину массива:");
                        isLengthCorrect = int.TryParse(Console.ReadLine(), out length);
                        if (length < 0) isLengthCorrect = false;
                        if (!isLengthCorrect) Console.WriteLine($"Возникла ошибка ввода длины массива. Пожалуйста, попробуйте еще раз. Введенное вами значение должно быть целым числом от 0 до {Int32.MaxValue} включительно");
                    } while (!isLengthCorrect); //проверка введенной длины массива на правильность данных

                    arr = new int[length]; //задание заранее созданному нами массиву нужной длины

                    if (length == 0) Console.WriteLine("Массив пуст"); //если в массиве нет элементов, с ним невозможно выполнить дальшнейшие действия
                    else
                    {
                        //вывод пунктов вспомогательного консольного меню для выбора способа заполнения массива
                        Console.WriteLine("Выберите cпособ заполнения массива");
                        Console.WriteLine("1. Заполнить массив с помощью датчика случайных чисел");
                        Console.WriteLine("2. Заполнить массив с клавиатуры");
                        do
                        {
                            Console.WriteLine("Выбранный вами пункт меню: ");
                            isMethodCorrect = int.TryParse(Console.ReadLine(), out fillingMethod);
                            if (fillingMethod < 1 || fillingMethod > 2) isMethodCorrect = false;
                            if (!isMethodCorrect) Console.WriteLine("Пожалуйста, введите число. Выберите одно из тех, что указаны в меню.");
                        } while (!isMethodCorrect); //выбранный пункт меню проходит проверку на правильность введенных данных

                        switch (fillingMethod) //организация вспомогательного консольного меню для выбора способа заполнения массива
                        {
                            case 1: //заполнение массива датчиком случайных чисел
                                for (i = 0; i < length; i++)
                                {
                                    arr[i] = randomElement.Next(-100, 100);
                                }
                                break; //цикл проходится по всей длине массива и каждый элемент становится рандомно заданным числом от -100 до 100

                            case 2: //заполнение массива пользователем с клавиатуры
                                for (i = 0; i < length; i++)
                                {
                                    counter++;
                                    do
                                    {
                                        Console.WriteLine($"Введите {counter} элемент массива: ");
                                        isElementCorrect = int.TryParse(Console.ReadLine(), out element);
                                        if (!isElementCorrect) Console.WriteLine("Ошибка ввода. Пожалуйста, введите целое число.");
                                    } while (!isElementCorrect);
                                    arr[i] = element;
                                } //цикл проходится по всей длине массива и каждый элемент вводится пользователем вручную, после чего проходит проверку
                                counter = 0; //счетчик обнуляется для повторного задания массива
                                break;
                        }
                        Console.WriteLine("Массив создан");
                    }
                    break;

                case 2: //распечатать массив
                    if (length == 0) Console.WriteLine("Массив пуст"); //если в массиве нет элементов, с ним невозможно выполнить дальшнейшие действия
                    else
                    {
                        PrintArray(arr); //вызов ранее прописанной функции печати массива
                    }
                    break;

                case 3: //удалить из массива элементы с нечетным индексом
                    int elementsCount; //переменная, которая будет хранить количество элементов с нечетными индексами
                    int temporaryCounter = 0; //счетчик для временного массива

                    if (length == 0) Console.WriteLine("Массив пуст"); //если в массиве нет элементов, с ним невозможно выполнить дальшнейшие действия
                    else
                    {
                        if (arr.Length % 2 == 1) elementsCount = arr.Length / 2 + 1; //если длина элементов нечетная, то длина временного массива должна быть равна целой части от деления длины на 2, увеличенную на единицу
                        else
                        {
                            elementsCount = arr.Length / 2; //если длина элементов четная, то длина нового массива просто будет в 2 раза меньше старой
                        }
                        temporaryArray = new int[elementsCount]; //создадим временный новый массив, в который мы будем перемещать неудаленные значения
                        j = 0; //обнуляем счетчик номера элемента временного массива
                        for (temporaryCounter = 0; temporaryCounter < arr.Length; temporaryCounter += 2)
                        {
                            temporaryArray[j] = arr[temporaryCounter];
                            j++;
                        } //цикл проходится по всем элементам с четными индексами и добавляет их во временный массив. Тогда в этом временном массиве остаются только элементы с четными индексами, а элементов с нечетными не осталось
                        arr = temporaryArray; // переприсваиваем
                        Console.WriteLine("Элементы удалены");
                    }
                    break;

                case 4: //добавить элемент с номером к
                    
                    int kFillingMethod = 0;  //переменная, хранящая значения выбранных пользователем пунктов меню - способы ввода K
                    int elementFillingMethod = 0;  //переменная, хранящая значения выбранных пользователем пунктов меню - способы ввода элемента при добавлении

                    if (length == 0) Console.WriteLine("Массив пуст"); //если в массиве нет элементов, с ним невозможно выполнить дальшнейшие действия
                    else
                    {
                        //вывод вложенного консольного меню для выбора способа ввода K
                        Console.WriteLine("Выберите cпособ задания К");
                        Console.WriteLine("1. Задать К с помощью датчика случайных чисел");
                        Console.WriteLine("2. Задать К с клавиатуры");
                        do
                        {
                            Console.WriteLine("Выбранный вами пункт меню: ");
                            isMethodCorrect = int.TryParse(Console.ReadLine(), out kFillingMethod);
                            if (kFillingMethod < 1 || kFillingMethod > 2) isMethodCorrect = false;
                            if (!isMethodCorrect) Console.WriteLine("Пожалуйста, введите число. Выберите одно из тех, что указаны в меню.");
                        } while (!isMethodCorrect); //введенное значение проходит проверку на правильность ввода

                        switch (kFillingMethod) //консольное меню для выбранного способа задания номера элемента К
                        {
                            case 1: //К задается датчиком случайных чисел
                                int k = randomElement.Next(1, arr.Length ); //К примет какое-то значение от 1 до длины массива включительно
                                
                                //вывод пунктом менб для выбора способа задания нового элемента
                                Console.WriteLine("Выберите cпособ задания элемента");
                                Console.WriteLine("1. Задать с помощью датчика случайных чисел");
                                Console.WriteLine("2. Задать с клавиатуры");
                                
                                switch (elementFillingMethod) //организация меню для выбранного способа задания элемента
                                {
                                    case 1: //элемент задается при помощи датчика случайных чисел
                                        temporaryArray = new int[arr.Length + 1]; //создаем новый массив, у которого длина больше исходного на 1
                                        int currentElement = randomElement.Next(-100, 100); //текущий добавляемый элемент будет принимать рандомное значение от -100 до 100

                                        int temporaryElementNumber = 0; //обнуляем счетчик элементов временного массива
                                        for (i = 0; i < k - 1; i++)
                                        {
                                            temporaryArray[temporaryElementNumber] = arr[i]; //идем циклом с параметром по основному массиву и присваиваем 
                                            temporaryElementNumber++; //сдвигаемся на 1 элемент по временному массиву
                                        }
                                        temporaryArray[temporaryElementNumber] = currentElement; //присваиваем элементу с номером К новый элемент
                                        temporaryElementNumber++; //переходим к следующему элемнту
                                        for (i = k - 1; i < arr.Length; i++)
                                        {
                                            temporaryArray[temporaryElementNumber] = arr[i];
                                            temporaryElementNumber++;
                                        } //проходим циклом по оставшейся длине основного массива и переносим оставшиеся элементы во временный массив
                                        arr = temporaryArray; //присваиваем основному массиву значение временного
                                        break;

                                    case 2: //элемент задается с клавиатуры
                                        temporaryArray = new int[arr.Length + 1]; //создаем новый временный массив, у которого длина больше исходного на 1
                                        
                                        do
                                        {
                                            Console.WriteLine("Введите элемент массива: ");
                                            isElementCorrect = int.TryParse(Console.ReadLine(), out currentElement);
                                            if (!isElementCorrect) Console.WriteLine("Ошибка ввода. Пожалуйста, введите целое число.");
                                        } while (!isElementCorrect); //введенное значение проходит проверку

                                        temporaryElementNumber = 0; //обнуляем счетчик элементов временного массива
                                        for (i = 0; i < k - 1; i++)
                                        {
                                            temporaryArray[temporaryElementNumber] = arr[i]; //идем циклом с параметром по основному массиву и присваиваем 
                                            temporaryElementNumber++; //сдвигаемся на 1 элемент по временному массиву
                                        }
                                        temporaryArray[temporaryElementNumber] = currentElement; //присваиваем элементу с номером К новый элемент
                                        temporaryElementNumber++; //переходим к следующему элемнту
                                        for (i = k - 1; i < arr.Length; i++)
                                        {
                                            temporaryArray[temporaryElementNumber] = arr[i];
                                            temporaryElementNumber++;
                                        } //проходим циклом по оставшейся длине основного массива и переносим оставшиеся элементы во временный массив
                                        arr = temporaryArray; //присваиваем основному массиву значение временного
                                        break;
                                }
                                break;

                            case 2: //К задается пользователем с клавиатуры
                                do
                                {
                                    Console.WriteLine("Введите К: ");
                                    isElementCorrect = int.TryParse(Console.ReadLine(), out k);
                                    if (k < 1 || k > arr.Length) isElementCorrect = false;
                                    if (!isElementCorrect) Console.WriteLine("Пожалуйста, введите натуральное число. Оно должно быть меньше или равно длине массива");
                                } while (!isElementCorrect); //введенное значение проходит проверку

                                //вывод пунктов консольного меню для выбора способа задания элемента
                                Console.WriteLine("Выберите cпособ задания элемента");
                                Console.WriteLine("1. Задать с помощью датчика случайных чисел");
                                Console.WriteLine("2. Задать с клавиатуры");
                                do
                                {
                                    Console.WriteLine("Выбранный вами пункт меню: ");
                                    isMethodCorrect = int.TryParse(Console.ReadLine(), out elementFillingMethod);
                                    if (elementFillingMethod < 1 || elementFillingMethod > 2) isMethodCorrect = false;
                                    if (!isMethodCorrect) Console.WriteLine("Пожалуйста, введите число. Выберите одно из тех, что указаны в меню.");
                                } while (!isMethodCorrect); //введенное значение проходит проверку на корректность введенных данных

                                switch (elementFillingMethod) //организация меню для выбранного способа задания элемента
                                {
                                    case 1: //элемент задается датчиком случайных чисел
                                        temporaryArray = new int[arr.Length + 1]; //создаем новый массив, у которого длина больше исходной на 1
                                        int currentElement = randomElement.Next(-100, 100); //текущий добавляемый элемент - целое число от -100 до 100

                                        int temporaryElementNumber = 0; //обнуляем счетчик элементов временного массива
                                        for (i = 0; i < k - 1; i++)
                                        {
                                            temporaryArray[temporaryElementNumber] = arr[i]; //идем циклом с параметром по основному массиву и присваиваем 
                                            temporaryElementNumber++; //сдвигаемся на 1 элемент по временному массиву
                                        }
                                        temporaryArray[temporaryElementNumber] = currentElement; //присваиваем элементу с номером К новый элемент
                                        temporaryElementNumber++; //переходим к следующему элемнту
                                        for (i = k - 1; i < arr.Length; i++)
                                        {
                                            temporaryArray[temporaryElementNumber] = arr[i];
                                            temporaryElementNumber++;
                                        } //проходим циклом по оставшейся длине основного массива и переносим оставшиеся элементы во временный массив
                                        arr = temporaryArray; //присваиваем основному массиву значение временного
                                        break;

                                    case 2: //элемент задается с клавиатуры
                                        temporaryArray = new int[arr.Length + 1]; //создаем новый массив, у которого длина больше исходной на 1
                                        do
                                        {
                                            Console.Write("Введите элемент массива: ");
                                            Console.WriteLine();
                                            isElementCorrect = int.TryParse(Console.ReadLine(), out currentElement);
                                            if (!isElementCorrect) Console.WriteLine("Ошибка ввода. Пожалуйста, введите целое число.");
                                        } while (!isElementCorrect); //введенное значение проходит проверку на корректность

                                        temporaryElementNumber = 0; //обнуляем счетчик элементов временного массива
                                        for (i = 0; i < k - 1; i++)
                                        {
                                            temporaryArray[temporaryElementNumber] = arr[i]; //идем циклом с параметром по основному массиву и присваиваем 
                                            temporaryElementNumber++; //сдвигаемся на 1 элемент по временному массиву
                                        }
                                        temporaryArray[temporaryElementNumber] = currentElement; //присваиваем элементу с номером К новый элемент
                                        temporaryElementNumber++; //переходим к следующему элемнту
                                        for (i = k - 1; i < arr.Length; i++)
                                        {
                                            temporaryArray[temporaryElementNumber] = arr[i];
                                            temporaryElementNumber++;
                                        } //проходим циклом по оставшейся длине основного массива и переносим оставшиеся элементы во временный массив
                                        arr = temporaryArray; //присваиваем основному массиву значение временного
                                        break;
                                }
                                break;
                        }
                        Console.WriteLine("Элемент добавлен");
                    }
                    break;

                case 5: //переворот массива
                    int leftElement = 0;
                    int rightElement = arr.Length - 1; //задаем индексы крайних элементов неотсортированной части массива
                    if (length == 0) Console.WriteLine("Массив пуст"); //если в массиве нет элементов, с ним невозможно выполнить дальшнейшие действия
                    else
                    {
                        while (leftElement < rightElement) //пока индекс левого крайнего элемента меньше индекса правого крайнего элемнта
                        {
                            (arr[leftElement], arr[rightElement]) = (arr[rightElement], arr[leftElement]); //меняем местами левый крайний и правый крайний элементы
                            leftElement++;
                            rightElement--; //сужаем границы неотсортированной части массива
                        }
                        Console.WriteLine("Массив перевернут");
                    }
                    break;

                case 6: //поиск элемента в массиве
                    if (length == 0) Console.WriteLine("Массив пуст"); //если в массиве нет элементов, с ним невозможно выполнить дальшнейшие действия
                    else
                    {
                        if (length == 1) Console.WriteLine("В массиве всего 1 элемент");
                        else
                        {
                            int sum = 0;
                            for (i = 0; i < arr.Length; i++)
                            {
                                sum += arr[i];
                            } //с помощью этого цикла находим сумму всех элементов массива

                            double averageValue = sum / arr.Length; //среднее арифметическое массива - это сумма всех элементов, разделенная на его длину
                            bool flag = false; //логическая переменная, которая будет отвечать за нахождение искомого элемента
                            comparisons = 0; //в начале количество сравнений равняется 0
                            for (i = 0; i < arr.Length; i++)
                            {
                                comparisons += 1; //количество сравнений увеличивается на 1 при каждой итерации цикла
                                if (arr[i] == averageValue)
                                {
                                    flag = true; //если элемент равен значению среднего арифметического, то мы меняем значение логической переменной: мы нашли искомый элемент
                                    Console.WriteLine($"Элемент {arr[i]} является средним значением всех элементов и находится на {i + 1} месте. Для нахождения элемента понадобилось {comparisons} сравнений.");
                                    break;
                                }
                            }
                            if (flag == false) Console.WriteLine("Такого элемента нет в массиве"); //если мы так и не поменял значение переменной, значит, искомого элемента не нашлось
                        }
                    }
                    break;

                case 7: //сортировка массива методом простого выбора
                    if (length == 0) Console.WriteLine("Массив пуст"); //если в массиве нет элементов, с ним невозможно выполнить дальшнейшие действия
                    else
                    {
                        SortArray(arr); //вызов функции, производящей сортировку методом простого выбора
                        Console.WriteLine("Массив отсортрован");
                    }
                    break;

                case 8: //сортировка массива методом расчески
                    if (length == 0) Console.WriteLine("Массив пуст"); //если в массиве нет элементов, с ним невозможно выполнить дальшнейшие действия
                    else
                    {
                        FastSortArray(arr); //вызов функции, производящей быструю сортировку массива методом расчески
                        Console.WriteLine("Массив отсортрован");
                    }
                    break;

                case 9: //бинарный поиск
                    if (length == 0) Console.WriteLine("Массив пуст"); //если в массиве нет элементов, с ним невозможно выполнить дальшнейшие действия
                    else
                    {
                        FastSortArray(arr); //предварительно сортируем массив; на всякий случай используем быструю сортировку

                        int findItem = 0; //переменная, хранящая элемент, который надо найти
                        do {
                            Console.WriteLine("Введите элемент массива: ");
                            isElementCorrect = int.TryParse(Console.ReadLine(), out findItem);
                            if (!isElementCorrect) Console.WriteLine("Ошибка ввода. Пожалуйста, введите целое число.");
                        } while (!isElementCorrect) ;  //введенное значение проходит проверку
                        
                        int left = 0;
                        int right = arr.Length - 1; //задаем индексы границ, в пределах которых идет поиск
                        int middle = 0; //индекс центрального элемента
                        comparisons = 0; //в начале количество сравнений равно 0
                        do
                        {
                            middle = (left + right) / 2; //находим индекс серединного элемента
                            if (arr[middle] < findItem) left = middle + 1; //если центральный элемент меньше искомого, то сдвигаем левую границу на следующий после центральнго элемент: теперь поиск будет вестись в правой половине
                            else
                                right = middle; //если наоборот больше, то сдвигаем правую границу: поиск будет в левой части массива
                            comparisons += 1; //каждое вхождение в цикл считаем за сравнение
                        } while (left != right); //пока границы не сойдутся на последнем оставшемся элементе

                        if (arr[left] == findItem) Console.WriteLine($"Номер искомого элемента {findItem} в отсортированном массиве равен {left + 1}. Для нахождения понадобилось {comparisons} сравнений.");
                        else Console.WriteLine("Такого элемента в массиве нет"); //если левая граница (любая граница) - это искомый элемент, значит он был найден; если границы сошлись на последнем элементе, так как не нашли искомый, то выводим соответствующее сообщение
                    }
                    break;

            }
        } while (menuPoint != 0); //когда пользователь введет 0 - программа выйдет из цикла и завершит работу

    }
}
