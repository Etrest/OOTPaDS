using System.Numerics;


Console.WriteLine("КР1 Бричак Егор");

SegmentFigure sf = new SegmentFigure(7, 12, 20, 1, 7);


sf.ModifyFigure(sf);



public class Location
{
	public double x;
	public double y;
	
	public Location()
	{
		x = 0;
		y = 0;
	}

	public Location(double x, double y)
	{
		this.x = x;
		this.y = y;
	}
}

public class Clip
{
	public Location min;
	public Location max;
	public Clip(Location min, Location max)
	{
		this.min = min;
		this.max = max;
	}

	public Clip (double xn, double yn, double xk, double yk)
	{
		min = new Location();
		max = new Location();

		if (xn < xk)
		{
			min.x = xn;
			max.x = xk;
		}
		else
		{
			min.x = xk;
			max.x = xn;
		}
		if(yn<yk)
		{
			min.y = yn;
			max.y = yk;
		}
		else
		{
			min.y = yk;
			max.y = yn;
		}
	}
	public double SizeX()
	{
		return max.x- min.x;
	}
	public double SizeY()
	{
		return max.y- min.y;
	}
}

public class Geometry
{
	public Geometry()
	{

	}
	const double pi = Math.PI;
	const double pi2 = Math.PI*2;
	const double extent = double.MaxValue;
	public static double accurateExtent (double value)
	{
		return value < (-extent)? (-extent) : (value>extent) ? extent: value;
	}
}

public class Primitive : Geometry
{
	private bool visible;
	private byte color;

	public int getColor()
	{
		return color;
	}

	public void setColor (int color) 
	{
		this.color = (byte)((color < 0 ) ? 0 : (color > 255) ? 255 : color);
	}

	public bool isVisible()
	{
		return visible;
	}

	public void setShow()
	{
		this.visible = true;
	}
	public void setHide()
	{
		this.visible = false;
	}

	public Primitive()
	{
		this.visible = false;
		this.color = 0;
	}

	public Primitive(Primitive primitive)
	{
		this.color =(byte) primitive.color;
		this.visible = primitive.visible;
	}
}


public class Point : Location 
{
	//Скопировать все методы из primitive
	private bool visible;
	private byte color;


	public const double pi = Math.PI;
	public const double pi2 = Math.PI * 2;
	public const double extent = double.MaxValue;

	Primitive prim;
	Location loc;

	public Point()
	{
		this.visible = false;
		this.color = 0;

		prim = new Primitive();
		loc = new Location();
	}


	public static double accurateExtent(double value)
	{
		return value < (-extent) ? (-extent) : (value > extent) ? extent : value;
	}

	public Point (Point point) 
	{
		this.color = (byte)point.color;
		this.visible = point.visible;

		this.prim = point.prim; 
		this.loc = point.loc;
	}
	public int getColor()
	{
		return color;
	}

	public void setColor(int color)
	{
		this.color = (byte)((color < 0) ? 0 : (color > 255) ? 255 : color);
	}

	public bool isVisible()
	{
		return visible;
	}

	public void setShow()
	{
		this.visible = true;
	}
	public void setHide()
	{
		this.visible = false;
	}

	public Point(double xx, double yy, byte color)
	{
		this.prim = new Primitive();
		this.loc = new Location();

		setX(xx);
		setY(yy);
		this.y = yy;
		setShow();
		setColor(color);
	}

	public virtual double getY() => y;
	public virtual double getX() => x;

	public virtual void setX(double x) { this.x = x; }
	public virtual void setY(double y) { this.y= y; }

	Location getPosition(double x, double y) { return new Location(x, y); }

	public virtual Clip getClipBox(double x, double y)
	{
		return new Clip(x, y, x, y);
	}
}

public class SegmentFigure : Point
{
	private double radius;
	private double engle;
	
	private Point point;
	public SegmentFigure() 
	{
		engle = 0;
		point = new Point();
		radius = 0;
	}

	public SegmentFigure(SegmentFigure sf)
	{
		this.radius = sf.radius;
		this.point = sf.point;
	}

	public SegmentFigure(double xc, double yc, double rad, double engle, byte color)
	{
		this.point = new Point();
		

		setColor(color);
		setShow();
		setRadius(rad);
		setEngle(engle);
		setX(xc);
		setY(yc);
	}



	public void setEngle(double engle)
	{
		this.engle = engle;
	
	}

	public double getEngle()
	{
		return this.engle;
	}

	public void setRadius(double rad)
	{
		this.radius = ((rad < 0) ? -rad: rad );
	}

	public double getRadius()
	{
		return radius;
	}

	public double getLength()
	{
		double dugL = radius * ((engle * pi) / 180);
		double triL = Math.Sqrt((Math.Pow(radius,2) + Math.Pow(radius, 2) - (2* radius * radius * Math.Cos((engle * 180)/pi))));
		double len = dugL + triL;
		return len;
	}

	public double getSquare()
	{
		double triS = ((radius * radius * Math.Sin((engle * 180) / pi)) / 2);
		double dugS = ((radius * ((radius * engle) / 2)));
		double sq = dugS - triS;
		return sq;
	}

	public new virtual void setX( double xx)
	{
		x = (((xx - radius) < -extent)? (-extent + radius):
			((xx+radius)> extent)?(extent - radius): xx);
	}

	public override void setY(double yy)
	{
		x = (((yy - radius) < -extent) ? (-extent + radius) :
			((yy + radius) > extent) ? (extent - radius) : yy);
	}

	public override Clip getClipBox(double x, double y)
	{
		return new Clip(x - radius, y - radius, x + radius, y + radius);
	}

	void ErrorValue()
	{
		Console.Clear();
		Console.In.ReadToEnd();
	}

	public void ModifyFigure(SegmentFigure sf)
	{
		string on = "On";
		string off = "Off";
		string ch1 = "P";
		char ch = 'P';

		do 
		{
			switch (ch)
			{
				case 'P' when ch == 'P' || ch == 'p':
				case 'p' when ch == 'p' || ch == 'P':

					Console.WriteLine("Свойства фрагмента:");
					Console.WriteLine($"Центр - {sf.getX()}; {sf.getY()}");
					Console.WriteLine($"Радиус - {sf.getRadius()}");
					Console.WriteLine($"Площадь - {sf.getSquare()}");
					Console.WriteLine($"Периметр - {sf.getLength()}");
					Console.WriteLine($"Видимость - {sf.isVisible()}");
					Console.WriteLine($"Номер цвета - {sf.getColor()} \n");

					Clip area = sf.getClipBox(sf.x, sf.y);
					Console.WriteLine($"Область - {area.min.x}; {area.min.y}||" +
						$"{area.max.x}:{area.max.y}");
					Console.WriteLine($"Размер - {area.SizeX()} x {area.SizeY()}");

					break;

				case 'X' when ch == 'X' || ch == 'x':
				case 'x' when ch == 'x' || ch == 'X':


					Console.Write("\n Введите координату Х: ");
					try 
					{ 
						double value = Convert.ToDouble(Console.ReadLine());
						sf.setX(value);
					}
					catch { ErrorValue(); }
					
					break;

				case 'Y' when ch == 'Y' || ch == 'y':
				case 'y' when ch == 'y' || ch == 'Y':

					Console.Write("\n Введите координату Y: ");
					try
					{
						double value = Convert.ToDouble(Console.ReadLine());
						sf.setY(value);
					}
					catch { ErrorValue(); }

					break;

				case 'R' when ch == 'R' || ch == 'r':
				case 'r' when ch == 'r' || ch == 'R':

					Console.Write("\n Введите радиус [R > 0]: ");
					try
					{
						double value = Convert.ToDouble(Console.ReadLine());
						sf.setRadius(value);
					}
					catch { ErrorValue(); }

					break;

				case 'T' when ch == 'T' || ch == 't':
				case 't' when ch == 't' || ch == 'T':

					Console.Write("\n Введите цвет [0...255]: ");
					try
					{
						byte value = Convert.ToByte(Console.ReadLine());
						sf.setColor(value);
					}
					catch { ErrorValue(); }

					break;

				case 'V' when ch == 'V' || ch == 'v':
				case 'v' when ch == 'v' || ch == 'V':

					Console.Write("\n Видимость [N-on|F-off]: ");
					try
					{
						ch1 = Console.ReadLine();
						ch = Convert.ToChar(ch1);
						if (ch == 'N' || ch == 'n')
							sf.setShow();
						else if (ch == 'F' || ch == 'f')
							sf.setHide();
					}
					catch { ErrorValue(); }
					
					break;

				default: Console.WriteLine("Ошибка: некоректная операция");
					break;
			}
			try
			{
				Console.WriteLine("(T) Изменить цвет");
				Console.WriteLine("(R) Изменить радиус");
				Console.WriteLine("(V) Изменить видимость");
				Console.WriteLine("(X) Изменить координату Х");
				Console.WriteLine("(Y) Изменить координату Y");
				Console.WriteLine("(P) Вывести свойства");
				Console.WriteLine("(Q) Выход");
				Console.Write("Выберите пункт: ");
				ch1 = Console.ReadLine();
				ch = Convert.ToChar(ch1);
			}
			catch { ErrorValue(); };
			
			
		}
		while (ch != 'Q' && ch != 'q');
		
			 
	}
}