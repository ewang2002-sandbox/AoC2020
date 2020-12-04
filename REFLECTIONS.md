# Reflections
Here, I'll reflect on the problem that I did. I'll provide my thought process, along with some basic stats. 

### [Day 1](https://adventofcode.com/2020/day/1)
Part A and B were basically making use of two and three for-loops, respectively. However, I could definitely improve on its efficiency (Part A's time complexity is O(n^2) and Part B's time complexity is O(n^3)). 

| Part  | Time Taken |
| ------------- | ------------- | 
| A | ~2 Minutes. | 
| B | ~2 Minutes. | 

### [Day 2](https://adventofcode.com/2020/day/2)
This question was quite interesting. As soon as I saw the inputs, I began devising a way to parse the inputs. A lot of people decided to use RegExp; however, I don't really know how to use RegExp so I chose not to.

Consider the line shown below. All lines in the input will follow this pattern (min-max letter: password)
```
6-7 w: wwhmzwtwwk
```

First, I began by splitting this line by a space (" "). This would leave me with an array:
```
{ "6-7", "w:", "wwhmzwtwwk" }
```

Then, I split the first element by a hyphen so I could get the minimum and maximum amount of times the letter could be in the password. 
```
{ "6", "7" }
```

Alternatively, I could have used C#'s range functionality to get the appropriate character from the first element.
```cs
var data = line.Split(" ");
var min = int.Parse(data[0][0]);
var max = int.Parse(data[0][^1]); 
```

I got the appropriate letter from the second element by replacing the colon with an empty string. Again, I could have used C#'s range functionality. From there, the problem was basically making sure the password was valid by checking whether min <= amount of specified letter in the password <= max. 

The second part was mostly similar. However, the one thing that got me was the indices starting at 1 instead of 0. After spending a few minutes trying to figure out what I did wrong (reading is hard), I managed to solve the problem by checking whether the letter existed in the (first specified index - 1) position of the password OR the (last specified index - 1) position (but NOT both positions). In other words, password[min - 1] ^ password[max - 1]. 

| Part  | Time Taken |
| ------------- | ------------- | 
| A | ~6 Minutes. | 
| B | ~6 Minutes. | 

### [Day 3](https://adventofcode.com/2020/day/3)
Part (A) threw me off a bit. When I saw the input, I thought I had to transpose the given input. This took some valuable time and, sadly, did not work out in my favor.

Once I reread the question, I realized that I was supposed to essentially "copy" the input multiple times until my toboggan could reach the bottom of the given input. At first, I thought about expanding out my input a few hundred times; however, I chose to edit the element only if I had to. This worked out well in my favor. 

I did hit a few exceptions (mainly ArrayIndexOfBoundsException), but that was due to my confusing "right" with "down."

Part (B) was Part (A) but repeated several times. I decided to make an array of all directions I would have to evaluate, and then evaluate the number of trees I would encounter based on the said direction. From there, I could multiply the outputs of each evaluation. 

In my revised code, I accounted for the fact that there was a lot of repeating code. Thus, I decided to create a method that would calculate the number of trees based on the given directions. I also decided to go a more functional route, which was far more concise.  


| Part  | Time Taken |
| ------------- | ------------- | 
| A | ~15 Minutes. | 
| B | ~4 Minutes. | 

### [Day 4](https://adventofcode.com/2020/day/4)
This question got me pretty hard. Well, not really. But let's just say that it was a series of mistakes I made. 
- I might have copied and pasted the input incorrectly, which led to a significant amount of time spent debugging.
- I did this problem when I was tired.
- I did this problem when I had other homework assignments to do. 

But I still did it. 

Part (A) was straightforward. Given the string input, I decided to split the string input by two newlines. That way, each element in the split string (array) would just be the passport information. An example of what this array could look like is shown below.
```
{
	"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd \nbyr:1937 iyr:2017 cid:147 hgt:183cm",
	"iyr:2013 ecl:amb cid:350 eyr:2023 \npid:028048884 hcl:#cfa07d byr:1929"
	// rest of elements
}
```

I iterated through each passport in the list of passports. Right after a new iteration, I defined a temporary array that would hold all the properties found (excluding "cid").

From there, I split each passport by one newline, then proceeded to iterate through each line. 
- For each key/value pair I parsed, I put the key in the temporary array. 

After I parsed through each line in the passport, I checked to see if the length of the temporary array was 7. Remember: since the question stated that we didn't need to check for "cid," and the question never stated that there would be duplicate entries.

Part (B) was also straightforward; however, it was tedious. I started by defining a bunch of inline functions. Each function is designed to validate the input of a specific key/value pair. For example, I had a function called "checkByr" that checked whether the value corresponding to the "byr" key was valid or not. 


I copied my code from part (A) but made use of a dictionary instead of a temporary array. The dictionary would hold the keys and values (as it should). Then, I checked to make sure that the length of the dictionary was 7. If it's not 7, then it's automatically an invalid dictionary. Next, had a boolean expression that called each function to make sure the passport was good. If every function returns true, then it's valid.

So, why did I go from <20 minutes (in previous questions) to over 2 hours? 
- Well, as I said, I spent a significant amount of time doing useless debugging before realizing that my input was incorrect. I also rushed through the implementation of this solution, which ended up costing me even more time. 
- For the second part, the code that I used was my second attempt. My first attempt was a series of if-statements to check each property. At first, this may not seem like a bad idea. Inefficient? Probably. But it would have worked if I wasn't rushing through my implementation (I was trying to make up for lost time from part (A)). I kept making little mistakes when trying to work on the validation stuff. That, along with me being tired, meant that I was constantly making little errors. These little errors became an absolute pain to debug, and I ended up taking a break to clear my head. After I came back, I ended up rewriting my implementation for part (B), which is what you're seeing now. The good news is that it worked the first try. 

Despite the troubles that I faced, it was a fun puzzle. Do I regret it? Nope.

But I did learn several things from today's question.
- Take your time.
- Ensure you aren't tired and stressed. 

| Part  | Time Taken |
| ------------- | ------------- | 
| A | ~72 Minutes. | 
| B | ~54 Minutes. | 