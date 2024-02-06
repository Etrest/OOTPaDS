//Дана строка, состоящая из групп нулей и единиц, разделенных пробелами.
//Найдите и выведите на экран самую короткую группу. 
string phrase = "0101011000101 01010010100 1010 001000 01 010101010 ";
string[] words = phrase.Trim().Split(' ');
var min = words[0].Length;
var minWord = words[0];

foreach (var word in words)
{
	if (min>word.Length)
	{
		min = word.Length;
		minWord = word;
	}
}
Console.WriteLine(minWord);

Console.WriteLine($"var2 {words.FirstOrDefault(word => word.Length == words.Min(x => x.Length))}");