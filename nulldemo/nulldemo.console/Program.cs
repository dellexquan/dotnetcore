int? i = null;
string? s = null;

// var p = new PersonModel();

// Console.WriteLine(p);

// int j = 6;
// int k = j;
// j = 10;
// Console.WriteLine(k);

var test1 = new PersonModel();
test1.Name = s;
test1.Name = "Tim";
var test2 = test1;
test2.Name = "Sue";
Console.WriteLine(test1);
Console.WriteLine(test2);
PersonModel? p = null;
//p = test2;
Console.WriteLine(i ?? 999);
Console.WriteLine(p?.Name?.Length > 0 ? p.Name.Length : "This was false!");

s = Console.ReadLine();
Console.WriteLine(s?.Length);


record PersonModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsValid { get; set; }
}


