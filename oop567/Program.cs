using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Diagnostics;

namespace nasledovanie
{

    public interface IPortable
    {
        void Portable();
        bool Port { get; set; }
        string SomeMethod();
    }
    public abstract class Product
    {
        private int price;
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value == 0) throw new PriceException("Товар не может быть бесплатным\n");
                else price = value;
            }

        } //цена товара
        public bool InStock { get; set; } //наличие товара
        public virtual void Info() //виртуальный метод
        {
            Console.WriteLine("У нас стоооолько продуктов в наличии!!");
        }

    }
    public enum Condition : int
    {
        worse = 1, bad, quite, good, best
    }
    public struct Feature
    {
        public int width { get; set; }
        public int height { get; set; }
        public Feature(int w, int h)
        {
            width = w;
            height = h;
        }
    }
    public class Table : Product
    {
        public string Wood { get; set; }
        public Table(int price, bool inStock, string Wood) //конструктор от базового класса + скорость печати, порт
        {
            this.Price = price;
            this.InStock = inStock;
            this.Wood = Wood;

        }
        public override void Info() //переопределение виртуального метода
        {
            Console.WriteLine("Стол из " + Wood + " стоит {0}\n ", Price);
        }
    }
    public abstract class Technique : Product 
    {
        public static int counter = 0;
        public Technique(int price, bool inStock)  
        {
            this.Price = price;
            this.InStock = inStock;
        }
        public override void Info() //переопределение виртуального метода
        {
            Console.WriteLine("Из техники у нас есть: ПК, Монитор, Наушники, Проектор, Экран.");
        }

        public abstract string SomeMethod(); 
    }

    public class Printer : Technique, IPortable
    {
        private int? speed;
        public int? Speed
        {
            get { return speed; }
            set
            {
                if (value == null) throw new NullException("Скорость не может быть равна null \n");
            }
        }
        public bool Port { get; set; } 
        public Feature spec;
        public Printer(int price, bool inStock, int? speed, bool Port) : base(price, inStock) 
        {
            this.Speed = speed;
            this.Port = Port;
        }
        public override void Info() 
        {
            Console.Write("Данный принтер печатает со скоростью: {0} \n", Speed);
            if (InStock) 
            {
                Console.WriteLine("Ты можешь его купить, еееееее)\n");
            }
            else
            {
                Console.WriteLine("Жди неделю, скоро будет в наличии с:\n");
            }
        }
        public void Portable() //проверяем на наличие проводов
        {
            if (!Port) { Console.WriteLine("Не портативный вообще, фу"); }
            else { Console.WriteLine("Портативный"); }
        }

        public override string SomeMethod() 
        {
            return "Переопределили одноименный метод SomeMethod, который находится в классе Техника, который абстрактный.\n";
        }
        string IPortable.SomeMethod() //реализуем как метод интерфейса
        {
            return "Одноименный метод из интерфейса\n";
        }
    }

    public class Computer : Technique, IPortable
    {
        public string RAM { get; set; }
        public string CPU { get; set; } 
        public bool Port { get; set; } 
        public Computer(int price, bool inStock, string cpu, string ram, bool Port) : base(price, inStock) 
        {
            this.RAM = ram;
            this.CPU = cpu;
            this.Port = Port;
        }
        public override void Info()
        {
            Console.Write("ПК имеет процессор {0} и RAM {1} \n", CPU, RAM);
            if (InStock) 
            {
                Console.WriteLine("Покупай!!\n");
            }
            else
            {
                Console.WriteLine("Раскупили всё :( \n");
            }
        }
        public void Portable() 
        {
            if (!Port) { Console.WriteLine("не portable(("); }
            else { Console.WriteLine("Портативный ПК, коеф!"); }
        }
        public override string SomeMethod()
        {
            return "переопределили как абстрактный метод\n";
        }
    }

    public class Monitor : Technique, IPortable
    {
        public int screenResolution { get; set; } //разрешение экрана
        public bool Port { get; set; } 
        public Feature spec;
        private int secondCondition;
        public int SecondCondition
        {
            get
            {
                return secondCondition;
            }
            set
            {
                if (value > 6 && value < 0) throw new ConditionException("Диапазон состояния находится в промежутке [1;5] ");
                else secondCondition = value;
            }
        }

        public Monitor(int price, bool inStock, int screenResolution, bool Port) : base(price, inStock) 
        {
            this.screenResolution = screenResolution;
            this.Port = Port;
        }
        public override void Info()
        {
            Console.Write("Монитор имеет разрешение экрана: {0}", screenResolution, "\n");
            if (InStock) 
            {
                Console.WriteLine("\nКупи со скидкой!");
            }
            else
            {
                Console.WriteLine("\nОн слишком крутой, его раскупают.");
            }
        }
        public void Portable()
        {
            if (!Port) { Console.WriteLine("Вряд ли ты с собой возьмёшь моник)"); }
            else { Console.WriteLine("Портативный??"); }
        }
        public override string SomeMethod() 
        {
            return "abstract class\n";
        }
    }
    public class HeadPhones : Technique, IPortable
    {
        public string Company { get; set; }
        public int ProductCondition { get; set; }
        public bool Port { get; set; }
        public bool Microphone { get; set; }
        public HeadPhones(int price, bool inStock, bool Microphone, string Company, bool Port) : base(price, inStock) //base + разрешение+порт
        {
            this.Company = Company;
            this.Port = Port;
        }
        public override void Info()
        {
            Console.Write("Наушники фирмы {0}", Company, "\n");
            if (InStock) 
            {
                Console.WriteLine("\nЕсть в наличии");
            }
            else
            {
                Console.WriteLine("\nНет в наличии");
            }
        }
        public void Portable()
        {
            if (!Port) { Console.WriteLine("Не запутайся в проводах)) \n"); }
            else { Console.WriteLine("Да ты крутой челик!!\n"); }
            if (!Microphone) { Console.WriteLine("Жаль, что в этих наушниках нет микрофона\n"); }
            else { Console.WriteLine("ЕЕЕ, общайся!\n"); }
        }
        public override string SomeMethod() 
        {
            return "abstract class\n";
        }
    }

    sealed partial class Projector : Product //запечатанный класс, от него нельзя наследовать (бесплодный)
    {
        public string MadeIn { get; set; } 
        public Projector(int price, bool inStock, string MadeIn) 
        {
            this.Price = price;
            this.InStock = inStock;
            this.MadeIn = MadeIn;
        }
    }
    public class TaskPrinter //класс с полиморфным методом
    {
        public static string IAmPrinting(Product someobj)
        {
            return someobj.ToString();
        }
    }
    public class Cabinet : ICloneable //класс-контейнер
    {
        public Product[] productArr; //реализуем лабораторию в виде одномерного массива
        public int size = 0;
        public Cabinet()
        {
            productArr = new Product[10];
        }
        public Product this[int index] //индексатор
        {
            get
            {
                return productArr[index];
            }
            set
            {
                productArr[index] = value;
            }
        }
        public void Add(Product abstr) //добавление объекта в лабораторию
        {
            productArr[size++] = abstr;
            Technique.counter++;
        }
        public void Delete(Product abstr) //удаление объекта в лаборатории
        {
            for (int i = 0; i < size; i++)
            {
                try
                {
                    if (productArr[i].Equals(abstr))
                    {
                        productArr[i] = null;
                        //size--;
                        break;
                    }

                    else
                    {
                        Console.WriteLine("Техника не найдена");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public void WriteArray() 
        {
            if (size == 0)
            {
                Console.WriteLine("В кабинете нет техники");
            }
            if (size != 0)
            {
                Console.WriteLine("\nТехника в кабинете:");
            }
            for (int i = 0; i < size; i++)
            {
                if (productArr[i] != null) productArr[i].Info();
            }
        }
        public object Clone()
        {
            var Cabinet = (Cabinet)this.MemberwiseClone();
            return Cabinet;
        }
    }

    public class CabinetMethods
    {
        int allPrice = 0;

        public CabinetMethods() { }
        public void AllCount(Cabinet Cabinet)
        {
            for (int i = 0; i < Technique.counter; i++)
            {
                allPrice += Cabinet.productArr[i].Price;
            }
            Console.WriteLine("Прайс на все объекты: " + allPrice);
        }

        public void OldProduct(Cabinet Cabinet)
        {
            for (int i = 0; i < Technique.counter; i++)
            {
                if (Cabinet.productArr[i].InStock == true)
                {
                    Console.WriteLine("Оборудование номер " + (i + 1) + " пора продать!");
                }
            }

        }


        public void WritePriceDes(Cabinet Cabinet)
        {
            for (int i = 0; i < Technique.counter; i++)
                for (int j = Technique.counter - 1; j > i; j--)
                {
                    if (Cabinet.productArr[j - 1].Price < Cabinet.productArr[j].Price)
                    {
                        var temp = Cabinet.productArr[j - 1];
                        Cabinet.productArr[j - 1] = Cabinet.productArr[j];
                        Cabinet.productArr[j] = temp;
                    }
                }
            try
            {
                foreach (Product x in Cabinet.productArr)
                {
                    //x.Info();
                    Console.WriteLine("{0}", x.Price);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine();
        }
    }

    sealed partial class Projector : Product
    {
        //переопределяем методы, определенный в Objects
        public override bool Equals(object obj)
        {
            Projector obj2 = (Projector)obj;
            return (obj2.Price == Price);
        }
        public override int GetHashCode()
        {
            return Math.Abs(base.GetHashCode() + MadeIn.GetHashCode());
        }

        public override string ToString()
        {
            return "Экран был произведен в " + MadeIn;
        }
    }

//////////////////////////////////////////////////////////////////  ИСКЛЮЧЕНИЯ
        public class ConditionException : ApplicationException
    {
        public ConditionException() { }
        public ConditionException(string message) : base(message) { }
        public ConditionException(string message, ApplicationException ex) : base(message)
        {
        }
    }
    public class PriceException : ApplicationException
    {
        public PriceException() { }
        public PriceException(string message) : base(message) { }
        public PriceException(string message, ApplicationException ex) : base(message)
        {
        }
    }

    public class NullException : ApplicationException
    {
        public NullException() { }
        public NullException(string message) : base(message) { }
        public NullException(string message, ApplicationException ex) : base(message)
        {
        }
    }
    public class OutOfIndexException : ApplicationException
    {
        public OutOfIndexException() { }
        public OutOfIndexException(string message) : base(message) { }
        public OutOfIndexException(string message, ApplicationException ex) : base(message)
        {
        }
    }
   
    /////////////////////////////////////////////////////////////////
    class Program
    {
        static void Red()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
        }
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("---------------------Создаем объекты, выводим инфу---------------------\n");
                Table stol = new Table(300, true, "сосны");
                stol.Info();

                Printer testException = new Printer(500, true, 19, false);
                Printer firstPrinter = new Printer(400, true, 70, false);
                firstPrinter.Info();
                Printer secondPrinter = new Printer(369, false, 55, false);
                secondPrinter.Info();

                HeadPhones firstHeadPhones = new HeadPhones(30, true, false, "Panasonic", false);
                firstHeadPhones.Info();
                Console.WriteLine("\n-----------Использовали перечисление-----------");
                firstHeadPhones.ProductCondition = (int)Condition.best;
                Console.WriteLine("Состояние наушников - {0} из 5 ", firstHeadPhones.ProductCondition);
                HeadPhones secondHeadPhones = new HeadPhones(200, false, true, "Apple", true);
                Console.WriteLine("\n-----------Дальше инфа об объектах-----------");
                secondHeadPhones.Info();

                Computer firstComputer = new Computer(1900, true, "Intel Core i5", "DDR4", false);
                firstComputer.Info();
                Computer secondComputer = new Computer(1000, false, "AMD", "DDR2", true);
                secondComputer.Info();
                Console.WriteLine("------------Проверяем ПК на портативность(метод интерфейса)------------\n");
                Console.WriteLine("1ый: ");
                firstComputer.Portable();
                Console.WriteLine();
                Console.WriteLine("2ой: ");
                secondComputer.Portable();
                Console.WriteLine();
                Console.WriteLine("\n-----------Дальше инфа об объектах-----------");
                Monitor firstMonitor = new Monitor(250, true, 600, false);
                firstMonitor.Info();
                Monitor secondMonitor = new Monitor(1000, false, 2800, true);
                secondMonitor.Info();

                Product firstProjector = new Projector(1231, false, "Belarus");
                Product secondProjector = new Projector(609, true, "Russia");
                Console.WriteLine("Стоимость проектора  - {0} рубль(ей)", firstProjector.Price); //доступна только стоимость,
                                                                                                 //т.к. ссылке на объект базового класса присвоен объект производного класс
                Console.WriteLine("Есть ли 1ый в наличии? -" + firstProjector.InStock);
                Console.WriteLine("\n----------Реализуем одноименный метод из интерфейса и класса----------");
                Console.Write(firstPrinter.SomeMethod()); //одноименный метод, вызывается реализованный с помощью абстрактного класса
                Console.WriteLine(((IPortable)firstPrinter).SomeMethod()); //.., реализованный с помощью интерфейса

                Console.WriteLine("\n-------Вызываем переопределенные методы-------");
                Console.WriteLine("ToString() - " + firstProjector.ToString());
                Console.WriteLine("GetHashCode() - " + firstProjector.GetHashCode());
                Console.WriteLine("Equals() - " + firstProjector.Equals(secondProjector) + "\n");

                Console.WriteLine("\n----------Операторы IS, AS ----------");
                //is
                Boolean check = firstPrinter is IPortable;
                Boolean check2 = firstProjector is IPortable;
                Console.WriteLine(check);
                Console.WriteLine(check2 + "\n");

                //as
                Printer p = new Printer(200, true, 20, true);
                Technique b = p as Technique;
                if (b != null) //проверка на успешное преобразование
                {
                    b.Info();
                    Console.WriteLine();
                }

                Console.WriteLine("----------Просто работка с массивом объектов----------");
                Product[] ArrayOfObjects = { firstPrinter, firstMonitor, firstComputer }; //массив ссылок на разнотипные объекты
                foreach (Product x in ArrayOfObjects)
                {
                    Console.WriteLine(TaskPrinter.IAmPrinting(x)); //вызываем метод IAmPrinting со всеми ссылками в качестве аргументов
                }
                Console.WriteLine("\n----------Использовали структуру----------");
                Printer printer = new Printer(400, true, 100, false);
                var a = printer.spec;
                a.height = 450;
                a.width = 300;
                Console.WriteLine("Принтер имеет размеры: " + a.width + "х" + a.height + "\n");
                Monitor monik = new Monitor(200, false, 600, true);
                monik.spec.height = 1080;
                monik.spec.width = 1920;
                Console.WriteLine("Монитор имеет разрешение: " + monik.spec.width + "х" + monik.spec.height + "\n");
                //Реализуем класс-контейнер: добавляем объекты в массив
                Cabinet cabinet = new Cabinet();
                cabinet.Add(stol); //1
                cabinet.Add(firstPrinter); //2
                cabinet.Add(firstMonitor); //3
                cabinet.Add(firstHeadPhones);//4
                cabinet.Add(firstComputer); //5
                cabinet.Add(firstProjector); //6
                cabinet.Add(secondMonitor); //7
                cabinet.Add(secondComputer); //8
                cabinet.Add(secondPrinter); //9

                string mySurname = "Ника";
                Debug.Assert(mySurname == "Ника", "Я не алмдаьыаьэм, а Ника.");

                CabinetMethods cabinetMethods = new CabinetMethods();
                Console.WriteLine("----------Выводим объекты в порядке убывания их стоимости----------");
                cabinetMethods.WritePriceDes(cabinet);
                Console.WriteLine("\n--------------------");
                cabinetMethods.AllCount(cabinet);
                Console.WriteLine("\n----------Какое оборудование устарело?----------");
                cabinetMethods.OldProduct(cabinet);

                int proverka1 = 5;
                Console.WriteLine("Введите число: ");
                int proverka2 = int.Parse(Console.ReadLine());
                int proverka = proverka1 / proverka2;

                int[] array = { 1, 2, 3 };
                int i = int.Parse(Console.ReadLine());
                if (i > array.Length) throw new OutOfIndexException("Выход индекса за границы");
                Console.WriteLine(array[i]);
                Console.WriteLine("Выполнено без ошибок");

            }
            catch (PriceException ex)
            {
                Console.WriteLine("ОШИБКА: " + ex.Message);
            }
            catch (NullException ex)
            {
                Console.WriteLine("ОШИБКА: " + ex.Message);
            }
            catch (OutOfIndexException ex)
            {
                Console.WriteLine("ОШИБКА: " + ex.Message);
            }
          
            catch (Exception ex)
            {
                Red();
                Console.WriteLine("\t\t ОШИБКА");
                Console.WriteLine(ex.Message + "\n\n");
                Console.WriteLine(ex.TargetSite + "\n\n"); //имя метода, в котором произошла ошибки
                Console.WriteLine(ex.StackTrace + "\n\n");
            }
            finally
            {
                Console.WriteLine("\nВызвался finally");
            }

        }
    }
}
