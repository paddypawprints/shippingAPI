Usage is straightforward for the most common cases: strings and ints. The only thing you need to know is approximately how many items you'll be adding. 

Here's an example demonstrating its use with strings:

int capacity = 2000000; // the number of items you expect to add to the filter
Filter<string> filter = new Filter<string>(capacity);
// add your items, using:
filter.Add("SomeString");
// now you can check for them, using:
if (filter.Contains("SomeString"))
	Console.WriteLine("Match!");

Bloom filters can't be resized, so the capacity is important for memory sizing. The false-positive rate also plays in here. 

If you don't specify false-positive rate you will get 1 / capacity, unless that is too small in which case you will get 0.6185^(int.MaxValue / capacity), which is nearly optimal

If you are going to be working with some other type T you will have to provide your own hash algorithm that takes a T and returns an int. Do NOT use the BCL's GetHashCode method. If you end up creating one for a common type (e.g., CRC for files) please add it to the source so that others may make use of your work.

Overloads are provided for specifying your own error rate, hash function, array size, double hash function count, etc. 