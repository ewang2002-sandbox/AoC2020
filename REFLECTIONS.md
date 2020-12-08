# Reflections
Here, I'll reflect on the problem that I did. I'll provide my thought process, along with some basic stats. 

I should mention that I have not taken any programming courses aside from:
- AP Computer Science A.
- An introductory computer science college course (basically APCSA).

In other words, programming is something that, for the most part, I learned on my own. 

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

### [Day 05](https://adventofcode.com/2020/day/5)
The question wasn't too bad for me... after I understood what it was asking. I spent a good deal of time trying to understand what both parts were asking for. 

Bear in mind that I'm not too sure how binary works because I haven't really learned much about it and haven't found the need to use it. I'm sure I'll be learning more about binary later on in my college class; if not, then I'll learn more about it on my own. :) 

For part (A), I tried to find a formula that replicated how AoC did the examples. Almost immediately, I noticed that the new values were divided by taking the mean of the range (that is, (Max - Min) / 2) and then was either added to the minimum value or subtracted from the maximum value. 

What threw me off the most was the fact that the upper value was 127, not 128. When I tried to replicate their example, I would always be one off because they either rounded up or down. I didn't really understand how they got that. However, after thinking about it, I realized that the Math.Ceiling and Math.Floor methods exist (I admit, I never really had a reason to use them before). After learning about these two methods worked, I was able to make a function that worked with the example input.

Here's my work for the first example, **FBFBBFFRLR** (which I'll call a sequence).

Consider **FBFBBFF**, which represents the row sequence. I started by using the first 6 characters: **FBFBBF**. Note that the variable names I used didn't exactly reflect the nature of the problem, which also confused me and wasted some time. 
| Char  | Work | Min | Max |
| ---------  | ---- | ---- | ----- |
| - | - | 0 | 127 | 
| F | Max = 127 - (127 - 0) / 2 = 63.5 => 63 (FLOOR) | 0 | 63 |
| B | Min = 0 + (63 - 0) / 2 = 31.5 => 32 (CEILING) | 32 | 63 |
| F | Max = 63 - (63 - 32) / 2 = 47.5 => 47 (FLOOR) | 32 | 47 |
| B | Min = 32 + (47 - 32) / 2 = 39.5 => 40 (CEILING) | 40 | 47 |
| B | Min = 40 + (47 - 40) / 2 = 43.5 => 44 (CEILING) | 44 | 47 |
| F | Max = 47 - (47 - 44) / 2 = 45.5 => 45 (FLOOR) | 44 | 45 |

Then I determined whether I should use Min or Max based on the last character: **F**. 
Row = 44.

The formula here can be generalized like so (for the first six characters in the row sequence).
```
if Char == "F":
	Max = Floor(Max - (Max - Min) / 2)
else:
	Min = Floor(Min + (Max - Min) / 2)
```

And for the last character in the row sequence.
```
if Char == "F":
	// "The final F keeps the lower of the two" 
	Row = Min
else:
	Row = Max 
```

Consider **RLR**, the column sequence. The same idea generalization shown above can be applied to this part.  
| Char  | Work | Min | Max |
| ---------  | ---- | ---- | ----- |
| - | - | 0 | 7 | 
| R | Min = 0 + (7 - 0) / 2 = 3.5 => 4 (FLOOR) | 4 | 7 |
| L | Max = 7 - (7 - 4) / 2 = 5.5 => 5 (CEILING) | 4 | 5 |

The last character in the column sequence is **R**. Thus, we're using the Max value because "The final R keeps the upper of the two."

From there, the implementation wasn't hard at all.

Part (B) was definitely easier than part (A). However, the wording caught me off-guard. In particular, this caught me off-guard:
> Your seat wasn't at the very front or back, though; the seats with IDs +1 and -1 from yours will be in your list.

What does "+1" and "-1" mean? All I knew was that the ID couldn't be the minimum or the maximum ID found from part (A). I ended up looking on the AoC subreddit to see if anyone else had trouble -- and, indeed, [someone else did](https://www.reddit.com/r/adventofcode/comments/k727v4/2020_day_5_part_2_im_not_sure_what_this_one_is/).

After getting some clarification, the implementation for this part was extremely easy. First, sort the IDs from smallest to greatest. Then, iterate through each ID and check if the difference between this ID and the previous ID is two (because |+1 - (-1)| = 2). 

Overall though, I feel like the thing that got me in today's question was my lack of knowledge of Ceiling and Floor and the fact that I was overthinking the problem a lot (overthinking is something that I'm quite good at). 

| Part  | Time Taken |
| ------------- | ------------- | 
| A | ~55 Minutes. | 
| B | ~5 Minutes. | 

### [Day 06](https://adventofcode.com/2020/day/6)
This problem was way easier than yesterday's problem. However, I did end up spending more time than what was needed. In the process, though, I learned a few new things.

Part (A) was simple. Given the example input below:
```
abc

a
b
c

ab
ac

a
a
a
a

b
```

You would split the content of the input file by two newlines so each element in the generated array represents a group's custom declaration form. 
```
{ "abc", "a\nb\nc", "ab\nac", ... }
```

Then, you would split each group's custom declaration form (a string) by a newline, join the split array to produce one line with the responses, and then split the string into an array of characters. I'll use the third element in the array above to showcase this.
```
"ab\nac" => { "ab", "ac" } => "abac" => { 'a', 'b', 'a', 'c' }
```

From there, I can just find all the distinct elements and add them up. 
```
{ 'a', 'b', 'c' }
```

So, what went wrong? 
- I forgot that, in Windows, you had to split each line by "\r\n." At first, I used "\n" and that caused a lot of errors. After 20 minutes of suffering, I realized that I should have used "\r\n" (C# has a NewLine property that I could use, so I used that).
	- I actually had a similar issue in an early AoC question; however, I never really thought much about the issue. 
- In C#, splitting a string by "" does not give you a character array; it gives you an array with one element -- the one element being the string. If you want a character array from a string, use Array#ToCharArray. It took me a while to realize this. 

Part (B) required the use of a dictionary which would exist in the outer loop's scope (where I'm iterating through each form). Basically, for each question, I did the same thing as part (A). The only difference was after I got all distinct elements of the character array, I put the amount of times the character existed in a dictionary. Then, I repeated this for each line in the form. 

For example, consider the form `ab\nac`. Splitting by a new line would yield:
```
ab\nac => { "ab", "ac" }
```

The dictionary would contain the following key/value pairs (key is the character, value is the amount of times that character occurred in each element):
```
{ 'a' => 2 } // 'a' exists in both elements
{ 'b' => 1 } // 'b' exists in the first element
{ 'c' => 1 } // 'c' exists in the second element
```

From there, I can check whether the amount of time each character existed was equal to the length of the array. If they're equal, then everyone answered "yes" to that question. In the example, the length of the array is 2. 'a' is the only character that has 2 occurrances, so 'a' is the only question that everyone in the group answered "yes" to. The rest is self-explanatory. 

So, what did I learn? 
- Unless you know what the string contains and you can literally see the new line sequence used, always consider the environment when splitting by a new line.
	- For example, if the string given is "abc\ndef," then you can split by "\n" since it's explicitly there. 
	- Otherwise, you must decide whether to use "\n" or "\r\n" (or just use Environment.NewLine).
- If you need to get a character array from a string, use Array#ToCharArray. Don't split by "" -- it doesn't work. 

Overall though, this was an easy question. I hope I see more of this.

| Part  | Time Taken |
| ------------- | ------------- | 
| A | ~22 Minutes. | 
| B | ~4 Minutes. | 

### [Day 07](https://adventofcode.com/2020/day/7)
This problem was, in my opinion, significantly harder than the previous problem. It took me multiple hours to figure out how to approach both problems; however, it was satisfying getting the right answers. 

Part (A) involved several steps. First, I had to parse each bag. In this case, I made use of a dictionary; the key would be the string before the word **contain**; the value would be the string after the word **contain**, represented as an array (split by ", "). If the value contains "no other bags," then this will be an empty array. For both the key and value, I changed "bags" to "bag," and then removed the period at the end. For example, consider these three inputs.

```
dark maroon bags contain 2 striped silver bags, 4 mirrored maroon bags, 5 shiny gold bags, 1 dotted gold bag.
{ "dark maroon bag" => { "striped silver bag", "mirrored maroon bag", "shiny gold bag", "dotted gold bag" }}
```

```
dark coral bags contain 4 pale blue bags, 3 wavy yellow bags, 4 vibrant tan bags, 3 striped purple bags.
{ "dark coral bag" => {"pale blue bag", "wavy yellow bag", "vibrant tan bag", "striped purple bag" }}
```

```
wavy yellow bags contain no other bags.
{ "wavy yellow bags" => {}}
```

Then, I created a new HashSet that contains all the bags I checked to see which bag contained the "shiny gold bag" and declared a variable that held the old length of the HashSet. 

Now, this is where the hard part is. The question states that we have to find "how many colors can, eventually, contain at least one shiny gold bag?" Basically, we need to find a bag that will contain another bag that will eventually contain the gold bag. 

Consider this example:
```
B1 -> B2 -> Gold Bag
B3 -> B2
B4 -> B1
```

Which will lead to: 
```
B1 -> B2 -> Gold Bag
B4	  B3
```

Which means the following bags will eventually link to the Gold Bag:
```
B1, B2, B3, B4 => Gold Bag
```

Although it took a while, the way I approached the problem is this:
- Start an infinite loop.
- Go through each bag and its elements (i.e. go through each key/value pair in the dictionary).
- Go through each defined element.
- If the defined element is in the HashSet.
	- Add the defined element to the HashSet.
		- This means the defined element, the bag, will contain a bag that will contain the gold bag.
- Otherwise, continue to the next bag. 
- After each bag is checked, check if the old length is equal to the length of the HashSet.
	- If it is, then don't break out of the loop.
		- This means that we need to check each element again in case there are more bags that will eventually have the gold bag.

So, that's that. It was hard for me to grasp, but at least I got the answer. 

Part (B) took about the same time because I tried to approach the problem recursively. Although that is supposedly the ideal way to do the question, I rarely use recursion in my programs so I had a hard time grasping the problem. 

First, I had to parse the file again. This is because I had to use the numbers (in the previous part, I removed the numbers). After I parsed the input, I tried to figure out a pattern that I could use to evaluate the problem. 

I forgot exactly what I did, but it involved quite a bit of guesswork. When I have time, I'll explain more. 

| Part  | Time Taken |
| ------------- | ------------- | 
| A | ~60 Minutes. | 
| B | ~60 Minutes. | 

### [Day 08](https://adventofcode.com/2020/day/8)
This question was certainly refreshing, considering yesterday's disaster. I was, at first, a bit apprehensive when I saw the question since this question looked very familiar to last year's IntCode questions (which is also why I basically stopped). However, it wasn't too bad.

For part (A), I made use of a HashSet to determine the indices that I visited. Then, I worked through the problem like usual -- not much to it. If the index that I visited is already in the HashSet, exit the loop and return the accumulator value.

For part (B), I tried to find the best way to approach the problem but ended up bruteforcing it. The idea is simple: I would have two nested while loops. The first while loop, which contains a for-loop, would clone the array (not clone, but literally parse the input array again) and change one command to the other command ("jmp" to "nop" or vice versa). After I changed one command, I would enter the second while loop, which was basically me copying and pasting the code from part (A). If I was able to make it through all the instructions without encountering an index that I already visited, then return the accumulator value; otherwise, restart the loop. 

So, what took me so long? Essentially, the index variable that I used to keep track of which directions I performed never changed -- it was always 0. The reason was because the original instructions array's values were being changed due to the way references worked. I, however, thought a simple clone would work when, in reality, that would not work. So much for that.

| Part  | Time Taken |
| ------------- | ------------- | 
| A | ~10 Minutes. | 
| B | ~23 Minutes. | 