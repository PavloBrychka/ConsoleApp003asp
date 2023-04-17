
//Circle. Triangle. Square

using Autofac;
using System.ComponentModel;
using System.IO;

public class Context
{
  

    public void SaveFile(string path, List<int> list, string name)
    {
        if(path == "")
        {
            path = "temp.txt";
        }
        string str = "";
        using(StreamReader sr = new StreamReader(path))
        {
            str = sr.ReadToEnd();
        }
        using(StreamWriter writer = new StreamWriter(path))
        {
            string str2 = "";
            if(name == "Circle")
            {
                foreach(var i in list)
                {
                    str2 += i.ToString() + " - ";
                }
                str2 += "Circle";
            }
            else if(name == "Triangle")
            {
                foreach (var i in list)
                {
                    str2 += i.ToString() + " - ";
                }
                str2 += "Triangle";
            }
            else if(name == "Square")
            {
                foreach (var i in list)
                {
                    str2 += i.ToString() + " - ";
                }
                str2 += "Square";
            }
            else
            {
                return;
            }
            writer.WriteLine(str);
            writer.WriteLine(str2);
        }

    }
    public void Print(string str)
    {
        Console.WriteLine(str);
    }
}
public interface ICircle
{
    public Context context { get;}
    public void Print();
    public void SaveFile(string path);

}
public interface ITriangle
{
    public Context context { get; }
    public void Print();
    public void SaveFile(string path);
}
public interface ISquare
{
    public Context context { get; }
    public void Print();
    public void SaveFile(string path);
}

public class Circle : ICircle
{
   

    public int radius { get; set; } = 10;
    public int plosh { get; set; } = 30;

    private Context _context = new Context();
    public Context context
    {
        get { return _context; }

        //set { _context = new Context(); }
    }

    public void Print()
    {

        context.Print("Radius = " + radius.ToString() + " plosh = " + plosh.ToString());
    }

    public void SaveFile(string path)
    {
        List<int> val = new List<int>();
        val.Add(plosh);
        val.Add(radius);

        context.SaveFile(path, val, "Circle");
    }
}
public class Triangle : ITriangle
{
    private Context _context = new Context();
    public Context context { get { return _context; }  } 
    
    public int x { get; set; } = 5;
    public int y { get; set; } = 10;
    public int z { get; set; } = 10;

    public void Print()
    {
        context.Print("X = " + x.ToString() + " Y = " + y.ToString() + " Z = " + z.ToString());
    }

    public void SaveFile(string path)
    {
        List<int> val = new List<int>();
        val.Add(x);
        val.Add(y);
        val.Add(z);

        context.SaveFile(path, val, "Triangle");
    }
}
public class Square : ISquare
{
    private Context _context = new Context();
    public Context context { get { return _context; }}
  
    public int x { get; set; } = 10;
    public void Print()
    {
        context.Print("Square area = " + x*4);
    }

    public void SaveFile(string path)
    {
        List<int> val = new List<int>();
        val.Add(x);
       

        context.SaveFile(path, val, "Square");
    }
}
public class Program
{
    private static Autofac.IContainer Container { get; set; }
    public static void Main(string[] args)
    {
        var builder = new ContainerBuilder();
        builder.RegisterType< Circle> ().As<ICircle>();
        builder.RegisterType< Triangle > ().As<ITriangle>();
        builder.RegisterType< Square> ().As<ISquare>();
        Container = builder.Build();

        ICircle circle = Container.Resolve<ICircle>();
        ITriangle triangle = Container.Resolve<ITriangle>();
        ISquare square = Container.Resolve<ISquare>();

      

        while (true)
        {
            Console.WriteLine("0 - Exit");
            Console.WriteLine("1 - Create");
            Console.WriteLine("2 - Print");
            Console.WriteLine("3 - Save to File");
            Console.Write("Enter menu__ ");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            if(x == 0)
            {
                Console.WriteLine("Goodbaye!");
                break;
            }
            else if(x == 1)
            {
                Console.WriteLine("1 - Circle");
                Console.WriteLine("2 - Triangle");
                Console.WriteLine("3 - Square");
                Console.WriteLine("0 - back to menu");
                Console.Write("Enter __ ");
                x = Convert.ToInt32(Console.ReadLine());
                if (x == 1)
                {
                   
                    circle = Container.Resolve<ICircle>();
                  

                }
                else if (x == 2)
                {
                    triangle = Container.Resolve<ITriangle>();
                 
                }
                else if(x == 3)
                {
                    square = Container.Resolve<ISquare>();
                   
                }
                else
                {
                    continue;
                }
               
            }
            else if(x == 2)
            {
                circle.Print();
                triangle.Print();
                square.Print();
            }
            else if(x == 3)
            {
                Console.WriteLine("Enter Path file if not path click 'Enter'");
                string pathfile = Console.ReadLine();
                circle.SaveFile(pathfile);
                triangle.SaveFile(pathfile);
                square.SaveFile(pathfile);
                Console.WriteLine("File Save");
            }
            else
            {
                Console.WriteLine("Error!");
                
            }

        }
    }
}