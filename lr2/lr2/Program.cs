//Создать объект класса Звездная система, используя классы Планета, Звезда, Луна.
//Методы:вывести на консоль количество планет в звездной системе, название звезды, добавление планеты в систему. 

using System.Xml.Linq;

var ss = new SolarSystem(10);
ss.AddPlanet(new Planet(2, "ArturchickPlan"));
ss.AddPlanet(new Planet(2, "Nikitka"));
ss.AddPlanet(new Planet(2, "Lenusik"));
ss.AddPlanet(new Planet(2, "Nastuha"));

Console.WriteLine($"Количество планет: {ss.PlanetsCount()}");

var star = new Star(5, "Arturchik");
Console.WriteLine($"Масса звезды: {star.getStarMass()}");
Console.WriteLine($"Название звезды: {star.Name}");


public class SolarSystem
{
	private double _mass;

	public List<Planet> planets;
	public SolarSystem(double mass)
	{
		Mass = mass;
		planets= new List<Planet>();
	}

	public double Mass
	{
		get => _mass;
		set => _mass = value < 0 ? 0 : value;
	}

	public void AddPlanet(Planet planet) =>
		planets.Add(planet);

	public int PlanetsCount() => planets.Count;
}
public class Star
{
	private string _name;
	public Star(double mass, string name)
	{
		Name = name;
		Mass= mass;
	}

	public string Name { get => _name; set => _name = value; }
	public double Mass { get; set; }

	public double getStarMass() =>	
		Mass;
	
}

public class Planet : Star
{
	private Sputnik _sputnik;
	public List<Star> Star { get; set; }
	public double Mass{ get; set; }
	public Planet(double mass, string name) : base(mass, name)
	{
		Console.WriteLine($"{name}: Я родился!");
	}
}

public class Sputnik : Planet
{
	private string PlanetName;
	public Sputnik(double mass, string name) : base (mass, name) 
	{
		Console.WriteLine($"Спутник приближен к планете {name} массой {mass}");
		PlanetName = name;
	
	}
	
}
